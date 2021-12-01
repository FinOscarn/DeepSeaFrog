using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    //������ ��������Ʈ��
    public Sprite[] sprites;
    //������ ��������Ʈ ������
    private SpriteRenderer spriteRenderer;

    //�÷��̾�����
    [SerializeField]
    protected Player player;
    //�÷��̾� �±׸� ����� ����
    protected const string PLAYER_TAG = "PLAYER";

    //���̰� �÷��̾�� �پ��ִ��� ����
    public bool isCling = false;

    //���� �������� �ӵ�
    public float originSpeed = 1.5f;
    //�پ��� �� �������� �ӵ�
    public float clingSpeed;
    //���� �������� �ӵ�
    public float moveSpeed;

    //�Ͻ����� �Ǿ����� ����
    public bool isPaused = false;

    private void Awake()
    {
        //��������Ʈ �������� �����´�
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        //�÷��̾ �̸� ���Ӹ޴������� �����´�
        player = GameManager.instance.player;

        //�Ͻ����� �Ǿ��� �� ���� ���� �����ش�
        GameManager.instance.pause += pause =>
        {
            //�Ͻ����� ���¸� �ݿ����ش�
            isPaused = pause;
        };

        //�����̵��ӵ��� ���� �̵��ӵ��� �����ش�
        moveSpeed = originSpeed;
        //�پ������� �̼��� �����̼��� 75%�� �Ѵ�
        clingSpeed = originSpeed * 0.75f;
    }

    protected virtual void Update()
    {
        //�Ͻ��������¶�� ����
        if (isPaused) return;

        //���� �÷��̾�� �پ��ִٸ�
        if(isCling)
        {
            //���� �̵��ӵ��� �پ��� �� �̵��ӵ�
            moveSpeed = clingSpeed;
        }
        //�ƴ϶��
        else
        {
            //���� �̵��ӵ��� ���� �̵��ӵ�
            moveSpeed = originSpeed;
        }

        //��ġ���� �Ʒ������� ��� �̵������ش�
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);

        //�÷��̾�� �����Ÿ��̻� ������������ ��Ȱ��ȭ��Ų��
        if (player.transform.position.y - transform.position.y >= 10)
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// ���̸� �ʱ�ȭ ���ִ� �Լ��Դϴ�
    /// </summary>
    /// <param name="pos">�ʱ� ��ġ��</param>
    public void Init(Vector3 pos)
    {
        isPaused = false;
        isCling = false;
        moveSpeed = originSpeed;

        //������ ������ ��������Ʈ�� �����´�
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        //��ġ �ʱ�ȭ
        transform.position = pos;
    }

    /// <summary>
    /// �� ���̰� �÷��̾ �پ��ִ� ���������� ��ȯ�ϴ� �Լ��Դϴ�
    /// </summary>
    /// <returns>�� ���̿� �÷��̾ �پ��ִ���</returns>
    public bool IsPlayerFood()
    {
        return this == player.food;
    }

    /// <summary>
    /// ���̸� ��Ȱ��ȭ ��Ű�� �Լ��Դϴ�
    /// </summary>
    public void Disable()
    {
        //���� �÷��̾�� �پ��ִ� ���̰� �̰Ŷ��
        if(player.food == this)
        {
            //�÷��̾��� �پ��ִ»��¸� ���ش�
            player.isCling = false;
            //������ �پ��ִ� ���¸� ���ش�
            isCling = false;

            //�÷��̾�� �پ��ִ� ���̸� �����ش�
            player.food = null;
        }

        //���ӿ�����Ʈ�� ��Ȱ��ȭ
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //���� �÷��̾�� �浹�ߴٸ�
        if(collision.CompareTag(PLAYER_TAG))
        {
            //�÷��̾�� ���� �� �ִ��� �˻��Ѵ�
            if (player.CanUnite(this))
            {
                //�����ϴٸ� �÷��̾�� �ٴ´�
                player.UniteFood(this);
            }
        }
    }
}
