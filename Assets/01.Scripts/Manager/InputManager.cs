using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MoveDir
{
    Left,
    Right
}

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public Button left;
    public Button right;

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

    private void Start()
    {
        left.onClick.AddListener(() => 
        {
            moveDir = MoveDir.Left;
            onClick();
        });

        right.onClick.AddListener(() =>
        {
            moveDir = MoveDir.Right;
            onClick();
        });
    }

    public static MoveDir GetDir()
    {
        return instance.moveDir;
    }

    //��ư �ΰ��� ����ؼ� �Ͻ����� ��ư�� ������ �������� �ʰ� �ٲ���
    //private void Update()
    //{
    //    if(Input.GetMouseButtonDown(0))
    //    {
    //        inputPos = Input.mousePosition;
            
    //        if(inputPos.x >= center)
    //        {
    //            moveDir = MoveDir.Right;
    //        }
    //        else
    //        {
    //            moveDir = MoveDir.Left;
    //        }

    //        onClick();
    //    }
    //}
}
