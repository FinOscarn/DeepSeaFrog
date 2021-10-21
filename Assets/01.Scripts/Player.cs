using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isCling = false; //���̿� �پ��ִ���
    public float upSpeed = 1f; //���Ϸ� �̵��ϴ� �ӵ� 
    public float moveSpeed = 1f; //�¿�� �̵��ϴ� �ӵ�

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
