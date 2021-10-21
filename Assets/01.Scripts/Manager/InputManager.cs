using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDir
{
    Left,
    Right
}

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    private const int center = 535; //��� X ����
    private Vector2 inputPos; //�Է¹��� ��ġ��

    private MoveDir moveDir; //����

    public Action onClick = ()=> { };

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public static MoveDir GetDir()
    {
        return instance.moveDir;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            inputPos = Input.mousePosition;
            
            if(inputPos.x >= center)
            {
                moveDir = MoveDir.Right;
            }
            else
            {
                moveDir = MoveDir.Left;
            }

            onClick();
        }
    }
}
