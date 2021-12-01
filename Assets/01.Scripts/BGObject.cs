using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGObject : MonoBehaviour
{
    //��潺������Ʈ��
    public Sprite[] sprites;
    //��������Ʈ������
    private SpriteRenderer spriteRenderer;
    //�÷��̾ ��Ƴ��� ����
    private Player player;

    private void Awake()
    {
        //��������Ʈ�������� �����´�
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        //�÷��̾ ���ӸŴ������� �̸� �����´�
        player = GameManager.instance.player;
    }

    private void Update()
    {
        //�÷��̾�� �����Ÿ��̻� ������������ ��Ȱ��ȭ��Ų��
        if (Mathf.Abs(player.transform.position.y - transform.position.y) > 15)
        {
            //��Ȱ��ȭ�����ش�
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// ��������Ʈ�� �ʱ�ȭ���ִ� �Լ��Դϴ�
    /// </summary>
    /// <param name="pos">�ʱ�ȭ�� ��ġ</param>
    public void Init(Vector3 pos)
    {
        //��������Ʈ�� �����ϰ� �������ְ�
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        //��ġ���� ���������� �ٲ��ش�
        transform.position = pos;
    }
}
