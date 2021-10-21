using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCamConfiner : MonoBehaviour
{
    private Transform playerTrm;

    void Start()
    {
        playerTrm = GameManager.instance.player.transform;
    }

    void Update()
    {
        transform.position = new Vector2(0, playerTrm.position.y);
    }
}
