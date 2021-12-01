using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCamConfiner : MonoBehaviour
{
    //�÷��̾��� ��ġ��
    private Transform playerTrm;

    void Start()
    {
        //�÷��̾��� ��ġ���� GameManager���� �����´�
        playerTrm = GameManager.instance.player.transform;
    }

    void Update()
    {
        //y���� �÷��̾��� y������ �������ش�
        transform.position = new Vector2(0, playerTrm.position.y);
    }
}
