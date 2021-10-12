using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isCling;
    public float speed;

    void Update()
    {
        if(!isCling)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
    }
}
