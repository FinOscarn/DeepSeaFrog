using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meter : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private Vector2 offset = Vector2.zero;
    public float moveSpeed = 0.1f;
    public bool isDown = true;

    public bool isPaused = false;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        GameManager.instance.pause += pause =>
        {
            isPaused = pause;
        };
    }

    private void Update()
    {
        offset.y += moveSpeed * Time.deltaTime;
        meshRenderer.material.SetTextureOffset("_MainTex", offset);
    }
}
