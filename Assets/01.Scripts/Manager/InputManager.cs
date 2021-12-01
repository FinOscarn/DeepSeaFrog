using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//이동방향 enum
public enum MoveDir
{
    Left,
    Right
}

public class InputManager : MonoBehaviour
{
    //싱글톤 패턴을 위한 정적 변수
    public static InputManager instance;

    //왼쪽입력받는 버튼
    public Button left;
    //오른쪽입력받는 버튼
    public Button right;

    private const int center = 535; //가운데 X 지점
    private Vector2 inputPos; //입력받은 위치값

    private MoveDir moveDir; //방향

    //화면이 눌렸을 때 추가로 실행해줄 함수들
    public Action onClick = ()=> { };

    private void Awake()
    {
        //싱글톤 패턴
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        //왼쪽화면이 눌렸을 때
        left.onClick.AddListener(() => 
        {
            //왼쪽방향으로 설정하고
            moveDir = MoveDir.Left;
            //눌렸을 떄 함수를 호출해준다
            onClick();
        });

        //오른쪽 화면이 눌렸을 떄
        right.onClick.AddListener(() =>
        {
            //오른쪽방향으로 설정하고
            moveDir = MoveDir.Right;
            //눌렸을 떄 함수를 호출해준다
            onClick();
        });
    }

    /// <summary>
    /// 현재 어떤 방향인지 알려주는 함수입니다
    /// </summary>
    /// <returns>현재 방향</returns>
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
