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

    private const int center = 535; //가운데 X 지점
    private Vector2 inputPos; //입력받은 위치값

    private MoveDir moveDir; //방향

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

    //버튼 두개를 사용해서 일시정지 버튼이 눌려도 떨어지지 않게 바꾸자
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
