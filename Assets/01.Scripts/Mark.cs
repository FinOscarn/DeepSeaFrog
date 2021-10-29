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
    /// 표식을 비활성화 시키는 함수입니다
    /// </summary>
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
