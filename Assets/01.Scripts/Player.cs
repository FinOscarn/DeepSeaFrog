using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isCling;
    public float upSpeed = 1f;
    public float moveSpeed = 1f;

    private void Update()
    {
        if(!isCling)
        {
            transform.Translate(Vector2.up * upSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.down * upSpeed * Time.deltaTime);
        }
    }
}
