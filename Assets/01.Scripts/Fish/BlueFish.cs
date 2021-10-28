using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFish : Fish
{
    private Mark mark;

    private float moveDelay = 0.5f;
    private bool canMove = false;

    private Food target;

    private void Awake()
    {
        mark = GetComponentInChildren<Mark>();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (!canMove) return;
        downSpeed = target.moveSpeed;
        base.Update();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        mark.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        canMove = false;
    }

    /// <summary>
    /// �i�ư� ���̿� �ʱ���ġ�� �������ִ� �Լ��Դϴ�
    /// </summary>
    /// <param name="target">�i�ư� ����</param>
    /// <param name="pos">������ ��ġ</param>
    public void Init(Food target)
    {
        StartCoroutine(Setting(target));
    }

    /// <summary>
    /// �i�ư� ���̿� �ʱ���ġ�� �������ִ� ��ƾ�Դϴ�
    /// </summary>
    /// <param name="target">�i�ư� ����</param>
    /// <param name="pos">������ ��ġ</param>
    private IEnumerator Setting(Food target)
    {
        this.target = target;
        mark.targetTrm = target.gameObject.transform;

        yield return new WaitForSeconds(moveDelay);

        Vector3 pos = new Vector3(FishManager.instance.spawnX, target.transform.position.y, 1);

        transform.position = pos;
        canMove = true;
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);
    }

    protected override void OnFoodTrigger(Food food)
    {
        if (food.IsPlayerFood())
        {
            GameManager.instance.pause(true);
            //�ӽ÷� ��� �ɾ������ ���߿� ���ӿ����� �ٲ���
        }

        food.Disable();
        mark.Disable();

        base.OnFoodTrigger(food);
    }
}
