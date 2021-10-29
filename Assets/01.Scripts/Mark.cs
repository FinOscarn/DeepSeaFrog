using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mark : MonoBehaviour
{
    public Transform targetTrm;

    private void Update()
    {
        if (targetTrm == null) return;

        transform.position = targetTrm.position;
    }

    /// <summary>
    /// ǥ���� ��Ȱ��ȭ ��Ű�� �Լ��Դϴ�
    /// </summary>
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
