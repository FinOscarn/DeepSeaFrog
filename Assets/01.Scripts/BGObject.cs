using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGObject : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(Vector3 pos)
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        transform.position = pos;

        Invoke("SetActiveFalse", 10f);
    }

    public void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }
}
