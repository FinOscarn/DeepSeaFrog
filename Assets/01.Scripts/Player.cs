using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public bool isPaused = true; //�Ͻ������Ǿ�����
    
    public bool isCling = false; //���̿� �پ��ִ���
    public bool canMove = true; //�¿�� �̵��� �� �ִ���

    public const float MAX_X = 2.4f;
    public const float MIN_X = -2.74f;

    public readonly Vector3 ORGIN_POS = new Vector3(0, 7, 0);

    public float upSpeed = 1f; //���� �̵��ϴ� �ӵ�, �ݴ�����̴ϱ� -�� �ٿ�����
    public float downSpeed = 1f; //�Ʒ��� �������� �ӵ�

    public float virSpeed = 1f; //���Ϸ� �̵��ϴ� �ӵ�
    public float horSpeed = 1f; //�¿�� �̵��ϴ� �ӵ�

    public float clingTimer; //�� �ʰ� ���̿� �پ��־�����
    public float deathTimer; //�� �ʰ� �������־�����
    public const float MAX_TIME = 3f; //�ִ�� ���̿� �پ����� �� �ִ� �ð� && �ִ�� ���̿��� ������ ���� �� �ִ� �ð�

    public Food food = null; //���� �پ��ִ� ����
    public Action<Food> onCling = food => { }; //���̿� �پ��� ��

    public Animator anim;
    public ParticleSystem particle;

    private MoveDir moveDir; //�÷��̾��� �̵�����

    private void Awake()
    {
        //���ϸ����� ������Ʈ�� �����´�
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        //������ ���۵Ǿ��� �� ������ �Լ��� ������ش�
        GameManager.instance.startGame += () =>
        {
            //��Ʈ������ �÷��̾ ���̺��ϴ°� ó�� ���̰�
            Sequence seq = DOTween.Sequence();

            seq.Append(transform.DOMoveY(9.6f, 1).SetEase(Ease.InExpo));
            seq.Append(transform.DOMoveY(0, 1).SetEase(Ease.InExpo));
            seq.AppendCallback(() =>
            {
                //�÷��̾� ��ġ�� ���� ����
                Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                GameManager.instance.foodManager.GetFood(pos);

                //�÷��̾ ���̺��ߴٰ� �˷��ش�
                GameManager.instance.playerD2ve();
                isPaused = false;

                anim.SetTrigger("isD2veEnd");

                particle.Play();
            });
        };

        //������ �Ͻ������Ǿ��� �� ������ �Լ��� ������ش�
        GameManager.instance.pause += pause =>
        {
            //�Ͻ����� ���¿� �����ϰ� ����
            isPaused = pause;
        };

        GameManager.instance.gameover += () =>
        {
            gameObject.SetActive(false);
        };

        GameManager.instance.reset += () =>
        {
            deathTimer = 0f;
            clingTimer = 0f;

            particle.Stop();

            gameObject.SetActive(true);
            anim.ResetTrigger("isD2veEnd");
            transform.position = ORGIN_POS;
            isPaused = true;
        };

        //ȭ���� ������ �� ����� �Լ��� ������ش�
        InputManager.instance.onClick += () =>
        {
            //�̵������� InputManager���� ������ �̵�����
            moveDir = InputManager.GetDir();

            //���� ���̿� �پ��ִ� ���¶��
            if(isCling)
            {
                //���̿��� �������� ���ش�
                DisuniteFood(food);
            }
        };
    }

    private void Update()
    {
        //���� �Ͻ��������¶�� return
        if (isPaused) return;

        //�¿�� ������ �� �ִ� �ִ� ���� ����
        if (transform.position.x <= MIN_X)
        {
            transform.position = new Vector2(MIN_X, transform.position.y);
        }
        if (transform.position.x >= MAX_X)
        {
            transform.position = new Vector2(MAX_X, transform.position.y);
        }

        //�¿�� ������ �� �ִ� ���¶��
        if (canMove)
        {
            //�̵������� �����̶��
            if (moveDir == MoveDir.Left)
            {
                //horSpeed��ŭ ��� �������� �����δ�
                transform.Translate(Vector2.left * horSpeed * Time.deltaTime);
            }
            else
            {   //horSpeed��ŭ ��� ���������� �����δ�
                transform.Translate(Vector2.right * horSpeed * Time.deltaTime);
            }
        }

        //���� ���̿� �پ��������� ���¶��
        if (!isCling)
        {
            //�����̵��ӵ��� upSpeed�� �����ش�
            virSpeed = upSpeed;
            //���̿� �������ִ� �ð��� ��� �����ش�
            deathTimer += Time.deltaTime;

            //���������ϱ� ClingTimer �ʱ�ȭ
            clingTimer = 0f;
        }
        else
        {
            //�����̵��ӵ��� downSpeed�� �����ش�
            virSpeed = downSpeed;
            //���̿� �پ��ִ� �ð��� ��� �����ش�
            clingTimer += Time.deltaTime;

            //�پ����ϱ� DeathTimer �ʱ�ȭ
            deathTimer = 0f;
        }
        // virSpeed ��ŭ ��� �Ʒ��� �����ش�
        transform.Translate(Vector2.down * virSpeed * Time.deltaTime);

        //���� �ִ�� �پ����� �� �ִ� �ð���ŭ �پ��־��ٸ�
        if (clingTimer >= MAX_TIME)
        {
            //FishManager���� �ĵ���⸦ �θ���
            FishManager.instance.CallBlueFish(food);
            //Ÿ�̸Ӵ� �ٽ� 0���� �ٲ��ش�
            clingTimer = 0f;
        }

        //���� �ִ�� ���������� �� �ִ� �ð���ŭ �������־��ٸ�
        if (deathTimer >= MAX_TIME)
        {
            //���δ�.
            GameManager.instance.gameover();
        }

        //���� ������Ʈ
        GameManager.instance.UpdateScore(Mathf.Clamp(-(int)(transform.position.y * 10), 0, int.MaxValue));
    }

    /// <summary>
    /// �÷��̾ ���̿� ���� �� �ִ� ���������� ��ȯ�մϴ�
    /// </summary>
    /// <param name="food">����</param>
    /// <returns>�÷��̾ ���̿� ���� �� �ִ� ����</returns>
    public bool CanUnite(Food food)
    {
        //�پ��ִ� ���̰� null �̰� food�� ���������� true�� ����
        return this.food == null && food.gameObject.activeSelf;
    }

    /// <summary>
    /// �÷��̾ ���̿� �ٴ� �Լ��Դϴ�
    /// </summary>
    /// <param name="food">���� ����</param>
    public void UniteFood(Food food)
    {
        //�ִϸ��̼��� �ٴ� �ִϸ��̼����� �ٲ۴�
        anim.SetBool("isHang", true);

        //�پ��ִ� ���¸� true�� �ٲ��ְ�
        isCling = true;
        //������ �پ��ִ� ���µ� true�� �ٲ��ش�
        food.isCling = true;

        //�¿�δ� ������ �� ���� ���ش�
        canMove = false;

        //player�� food�� ���� food�� �ٲ��ְ�
        this.food = food;
        //downSpeed�� ���̰� �������ִ� �پ������� �������� �ӵ��� �ٲ��ش�
        this.downSpeed = food.clingSpeed;
    }

    /// <summary>
    /// �÷��̾ ���̿��� ������ �������� �Լ��Դϴ�
    /// </summary>
    /// <param name="food">������ ����</param>
    public void DisuniteFood(Food food)
    {
        //�ִϸ��̼��� �����ϴ� �ִϸ��̼����� �ٲ۴�
        anim.SetBool("isHang", false);

        //�پ��ִ� ���¸� ���ְ�
        isCling = false;
        //���̵� �پ����� ���� ���·� ������ش�
        food.isCling = false;

        //�ٽ� �¿�� ������ �� �ְ� ���ش�
        canMove = true;

        //food�� �ٽ� null�� �ٲ��ش�
        this.food = null;

        //�پ��ִ� �ð��� �ʱ�ȭ���ش�
        clingTimer = 0f;
    }
}
