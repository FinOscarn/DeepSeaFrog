using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Player player;

    public bool isCling = false;
    public float orginSpeed = 1.5f;
    public float clingSpeed;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        player = GameManager.instance.player;
        
        clingSpeed = orginSpeed * 0.75f;
    }

    private void Update()
    {
        if(isCling)
        {
            transform.Translate(Vector2.down * clingSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.down * orginSpeed * Time.deltaTime);
        }
    }

    public void Init(Vector3 pos)
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        player.ReachFood(this);
    }
}
