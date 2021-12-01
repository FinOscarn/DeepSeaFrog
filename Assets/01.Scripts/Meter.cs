using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meter : MonoBehaviour
{
    //�÷��̾ ��Ƶδ� ����
    private Player player;

    //�޽÷�����
    private MeshRenderer meshRenderer;
    //�������� 0���� �ʱ�ȭ
    private Vector2 offset = Vector2.zero;
    //�������� �ӵ�
    public float downSpeed = 0.1f;
    //�ö󰡴� �ӵ�
    public float upSpeed = 0.1f;
    //���� �������� ��������
    public bool isDown = true;

    //�Ͻ����� ��������
    public bool isPaused = false;

    private void Awake()
    {
        //�޽÷������� �����´�
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        //�÷��̾ ���ӸŴ������� ������ ��Ƶд�
        player = GameManager.instance.player;

        //������ �Ͻ������Ǿ��� �� ������ �Լ��� �߰����ش�
        GameManager.instance.pause += pause =>
        {
            //�Ͻ��������� �ݿ����ش�
            isPaused = pause;
        };
    }

    private void Update()
    {
        //���� �÷��̾ �پ��ִ»���(�������»���)���
        if(player.isCling)
        {
            //�������� ���� �÷��ش�
            offset.y += downSpeed * Time.deltaTime;
        }
        //�ƴ϶��
        else
        {
            //�������� ���� �����ش�
            offset.y -= upSpeed * Time.deltaTime;
        }

        //���׸����� offset���� offset���� �ٲ��ش�
        meshRenderer.material.SetTextureOffset("_MainTex", offset);
    }
}
