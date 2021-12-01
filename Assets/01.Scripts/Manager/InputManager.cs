using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�̵����� enum
public enum MoveDir
{
    Left,
    Right
}

public class InputManager : MonoBehaviour
{
    //�̱��� ������ ���� ���� ����
    public static InputManager instance;

    //�����Է¹޴� ��ư
    public Button left;
    //�������Է¹޴� ��ư
    public Button right;

    private const int center = 535; //��� X ����
    private Vector2 inputPos; //�Է¹��� ��ġ��

    private MoveDir moveDir; //����

    //ȭ���� ������ �� �߰��� �������� �Լ���
    public Action onClick = ()=> { };

    private void Awake()
    {
        //�̱��� ����
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        //����ȭ���� ������ ��
        left.onClick.AddListener(() => 
        {
            //���ʹ������� �����ϰ�
            moveDir = MoveDir.Left;
            //������ �� �Լ��� ȣ�����ش�
            onClick();
        });

        //������ ȭ���� ������ ��
        right.onClick.AddListener(() =>
        {
            //�����ʹ������� �����ϰ�
            moveDir = MoveDir.Right;
            //������ �� �Լ��� ȣ�����ش�
            onClick();
        });
    }

    /// <summary>
    /// ���� � �������� �˷��ִ� �Լ��Դϴ�
    /// </summary>
    /// <returns>���� ����</returns>
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
