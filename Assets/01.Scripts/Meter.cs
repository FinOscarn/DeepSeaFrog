using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meter : MonoBehaviour
{
    private Player player;

    private MeshRenderer meshRenderer;
    private Vector2 offset = Vector2.zero;
    public float downSpeed = 0.1f;
    public float upSpeed = 0.1f;
    public bool isDown = true;

    public bool isPaused = false;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        player = GameManager.instance.player;

        GameManager.instance.pause += pause =>
        {
            isPaused = pause;
        };
    }

    private void Update()
    {
        if(player.isCling)
        {
            offset.y += downSpeed * Time.deltaTime;
        }
        else
        {
            offset.y -= upSpeed * Time.deltaTime;
        }

        meshRenderer.material.SetTextureOffset("_MainTex", offset);
    }
}
