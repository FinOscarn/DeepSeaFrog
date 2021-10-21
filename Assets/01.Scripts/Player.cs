using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isCling = false; //먹이에 붙어있는지
    public float upSpeed = 1f; //상하로 이동하는 속도 
    public float moveSpeed = 1f; //좌우로 이동하는 속도

    private MoveDir moveDir;

    private void Start()
    {
        InputManager.instance.onClick += () =>
        {
            moveDir = InputManager.GetDir();

            if(isCling)
            {
                //isCling = false;
            }
        };
    }

    private void Update()
    {
        if(moveDir == MoveDir.Left)
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }

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
