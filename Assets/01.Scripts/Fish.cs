using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public Sprite sprite;

    public float moveSpeed;
    public float downSpeed;

    public Food targetFood = null;

    private void Update()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        transform.Translate(Vector2.down * downSpeed * Time.deltaTime);
    }

    public void Init(Sprite sprite, float moveSpeed, Food targetFood = null)
    {
        this.sprite = sprite;
        this.moveSpeed = moveSpeed;
        this.targetFood = targetFood;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Food food = col.gameObject.GetComponent<Food>();

        if (food == null) return;

        if(food.isCling)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            food.gameObject.SetActive(false);
        }
    }
}
