using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFish : Fish
{
    //��ǥ ���̿� ǥ�õ� ǥ��
    private Mark mark;

    //ǥ���� �߰� �����̱� �����ϴ� ������
    private float moveDelay = 0.5f;
    //������ �� �ִ��� ����
    private bool canMove = false;

    //��ǥ ����
    private Food target;

    protected override void Start()
    {
        //Fish Ŭ������ Start�� �״�� ���
        base.Start();
    }

    protected override void Update()
    {
        //�����ϼ����ٸ� ����
        if (!canMove) return;

        //��ǥ���̿� �������� �ӵ��� �����ش�
        downSpeed = target.moveSpeed;
        //�θ��� ������Ʈ�� �״�� ���
        base.Update();
    }

    protected override void OnEnable()
    {
        //�θ��� OnEnable�� �״�� ���
        base.OnEnable();
    }

    private void OnDisable()
    {
        //������ �� ���� ���·� ������ش�
        canMove = false;
    }

    /// <summary>
    /// �i�ư� ���̿� �ʱ���ġ�� �������ִ� �Լ��Դϴ�
    /// </summary>
    /// <param name="target">�i�ư� ����</param>
    /// <param name="pos">������ ��ġ</param>
    public void Init(Food target, bool isLeftMove)
    {
        StartCoroutine(Setting(target, isLeftMove));
    }

    /// <summary>
    /// �i�ư� ���̿� �ʱ���ġ�� �������ִ� ��ƾ�Դϴ�
    /// </summary>
    /// <param name="target">�i�ư� ����</param>
    /// <param name="pos">������ ��ġ</param>
    private IEnumerator Setting(Food target, bool isLeftMove)
    {
        //��ǥ���̸� �������ش�
        this.target = target;
        //FishManager���� ǥ�� �ϳ��� �����´�
        mark = FishManager.instance.GetMark(target);

        //�����̸�ŭ ��ٷ��ش�
        yield return new WaitForSeconds(moveDelay);

        Vector3 pos;

        //�������� ������ �����̶��
        if (isLeftMove)
        {
            //������ ���� �������ش�
            pos = new Vector3(FishManager.instance.rightSpawnX, target.transform.position.y, 1);
        }
        //�ݴ���
        else
        {
            //���� ���� �������ش�
            pos = new Vector3(FishManager.instance.leftSpawnX, target.transform.position.y, 1);
        }

        //��ġ�� ������ ��ġ�� �������ش�
        transform.position = pos;
        //������ �� �ִ� ���·� �ٲ��ش�
        canMove = true;
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        //�θ��� OnTriggerEnter2D�� �״�� ���ش�
        base.OnTriggerEnter2D(col);
    }

    protected override void OnFoodTrigger(Food food)
    {
        //���� �÷��̾ �پ��ִ� ���̶��
        if (food.IsPlayerFood())
        {
            //���ӿ����� ȣ�����ش�
            GameManager.instance.gameover();
        }

        //���� ��ǥ�� �ϰ� �ִ� �����϶��� �浹üũ�� ���ش�
        if(food == this.target)
        {
            //���̸� ��Ȱ��ȭ��Ų��
            food.Disable();
            //ǥ���� ��Ȱ��ȭ��Ų��
            mark.Disable();

            //�ٽ��ѹ� üũ���ش�
            base.OnFoodTrigger(food);
        }
    }
}
