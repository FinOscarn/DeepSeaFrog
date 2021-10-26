using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGObject : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private Player player;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        player = GameManager.instance.player;
    }

    private void Update()
    {
        //�÷��̾�� �����Ÿ��̻� ������������ ��Ȱ��ȭ��Ų��
        if (Mathf.Abs(player.transform.position.y - transform.position.y) > 15)
        {
            gameObject.SetActive(false);
        }
    }

    public void Init(Vector3 pos)
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        transform.position = pos;
    }
}
