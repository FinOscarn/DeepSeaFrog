using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public Sprite sprite;

    public float moveSpeed;
    public float downSpeed;

    public float maxX = 3.6f;

    public bool isPaused = false;

    public Food targetFood = null;

    private void Start()
    {
        GameManager.instance.pause += pause =>
        {
            isPaused = pause;
        };
    }

    private void Update()
    {
        if (isPaused) return;

        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        transform.Translate(Vector2.down * downSpeed * Time.deltaTime);

        if(transform.position.x >= maxX)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetTarget(Food targetFood)
    {
        this.targetFood = targetFood;
        this.downSpeed = targetFood.clingSpeed;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
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
            food.Disable();
        }
    }
}
