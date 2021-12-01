using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mark : MonoBehaviour
{
    //��ǥ������ ��ġ
    public Transform targetTrm;

    private void Update()
    {
        //��ǥ���̰� ���ٸ� ����
        if (targetTrm == null) return;

        //��ǥ���̰� ��Ȱ��ȭ�Ǿ��ִٸ� ��Ȱ��ȭ���ش�
        if (!targetTrm.gameObject.activeSelf) Disable();

        //��ġ���� ��ǥ������ ��ġ������ ����
        transform.position = targetTrm.position;
    }

    /// <summary>
    /// ǥ���� ��Ȱ��ȭ ��Ű�� �Լ��Դϴ�
    /// </summary>
    public void Disable()
    {
        //��Ȱ��ȭ
        gameObject.SetActive(false);
    }
}
