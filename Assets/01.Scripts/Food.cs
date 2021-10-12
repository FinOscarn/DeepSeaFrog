using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public bool isCling;
    public float downSpeed;

    private void Update()
    {
        if(!isCling)
        {
            transform.Translate(Vector2.up * downSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.up * (downSpeed * 0.75f) * Time.deltaTime);
        }
    }
}
