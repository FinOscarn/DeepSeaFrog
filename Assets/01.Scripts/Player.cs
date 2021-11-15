using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public bool isPaused = true; //일시정지되었는지
    
    public bool isCling = false; //먹이에 붙어있는지
    public bool canMove = true; //좌우로 이동할 수 있는지

    public const float MAX_X = 2.4f;
    public const float MIN_X = -2.74f;

    public readonly Vector3 ORGIN_POS = new Vector3(0, 7, 0);

    public float upSpeed = 1f; //위로 이동하는 속도, 반대방향이니까 -를 붙여주자
    public float downSpeed = 1f; //아래로 떨어지는 속도

    public float virSpeed = 1f; //상하로 이동하는 속도
    public float horSpeed = 1f; //좌우로 이동하는 속도

    public float clingTimer; //몇 초간 먹이에 붙어있었는지
    public float deathTimer; //몇 초간 떨어져있었는지
    public const float MAX_TIME = 3f; //최대로 먹이에 붙어있을 수 있는 시간 && 최대로 먹이에서 떨어져 있을 수 있는 시간

    public Food food = null; //현재 붙어있는 먹이
    public Action<Food> onCling = food => { }; //먹이에 붙었을 때

    public Animator anim;
    public ParticleSystem particle;

    private MoveDir moveDir; //플레이어의 이동방향

    private void Awake()
    {
        //에니메이터 컴포넌트를 가져온다
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        //게임이 시작되었을 때 실행할 함수를 등록해준다
        GameManager.instance.startGame += () =>
        {
            //닷트윈으로 플레이어가 다이빙하는것 처럼 보이게
            Sequence seq = DOTween.Sequence();

            seq.Append(transform.DOMoveY(9.6f, 1).SetEase(Ease.InExpo));
            seq.Append(transform.DOMoveY(0, 1).SetEase(Ease.InExpo));
            seq.AppendCallback(() =>
            {
                //플레이어 위치에 먹이 생성
                Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                GameManager.instance.foodManager.GetFood(pos);

                //플레이어가 다이빙했다고 알려준다
                GameManager.instance.playerD2ve();
                isPaused = false;

                anim.SetTrigger("isD2veEnd");

                particle.Play();
            });
        };

        //게임이 일시정지되었을 때 실행할 함수를 등록해준다
        GameManager.instance.pause += pause =>
        {
            //일시정지 상태와 동일하게 세팅
            isPaused = pause;
        };

        GameManager.instance.gameover += () =>
        {
            gameObject.SetActive(false);
        };

        GameManager.instance.reset += () =>
        {
            deathTimer = 0f;
            clingTimer = 0f;

            particle.Stop();

            gameObject.SetActive(true);
            anim.ResetTrigger("isD2veEnd");
            transform.position = ORGIN_POS;
            isPaused = true;
        };

        //화면이 눌렸을 때 실행될 함수를 등록해준다
        InputManager.instance.onClick += () =>
        {
            //이동방향은 InputManager에서 보내준 이동방향
            moveDir = InputManager.GetDir();

            //만약 먹이에 붙어있는 상태라면
            if(isCling)
            {
                //먹이에서 떨어지게 해준다
                DisuniteFood(food);
            }
        };
    }

    private void Update()
    {
        //만약 일시정지상태라면 return
        if (isPaused) return;

        //좌우로 움직일 수 있는 최대 영역 제한
        if (transform.position.x <= MIN_X)
        {
            transform.position = new Vector2(MIN_X, transform.position.y);
        }
        if (transform.position.x >= MAX_X)
        {
            transform.position = new Vector2(MAX_X, transform.position.y);
        }

        //좌우로 움직일 수 있는 상태라면
        if (canMove)
        {
            //이동방향이 왼쪽이라면
            if (moveDir == MoveDir.Left)
            {
                //horSpeed만큼 계속 왼쪽으로 움직인다
                transform.Translate(Vector2.left * horSpeed * Time.deltaTime);
            }
            else
            {   //horSpeed만큼 계속 오른쪽으로 움직인다
                transform.Translate(Vector2.right * horSpeed * Time.deltaTime);
            }
        }

        //만약 먹이에 붙어있지않은 상태라면
        if (!isCling)
        {
            //상하이동속도는 upSpeed로 맞춰준다
            virSpeed = upSpeed;
            //먹이에 떨어져있는 시간을 계속 더해준다
            deathTimer += Time.deltaTime;

            //떨어졌으니까 ClingTimer 초기화
            clingTimer = 0f;
        }
        else
        {
            //상하이동속도를 downSpeed로 맞춰준다
            virSpeed = downSpeed;
            //먹이에 붙어있는 시간을 계속 더해준다
            clingTimer += Time.deltaTime;

            //붙었으니까 DeathTimer 초기화
            deathTimer = 0f;
        }
        // virSpeed 만큼 계속 아래로 내려준다
        transform.Translate(Vector2.down * virSpeed * Time.deltaTime);

        //만약 최대로 붙어있을 수 있는 시간만큼 붙어있었다면
        if (clingTimer >= MAX_TIME)
        {
            //FishManager에서 파도고기를 부르고
            FishManager.instance.CallBlueFish(food);
            //타이머는 다시 0으로 바꿔준다
            clingTimer = 0f;
        }

        //만약 최대로 떨어져있을 수 있는 시간만큼 떨어져있었다면
        if (deathTimer >= MAX_TIME)
        {
            //죽인다.
            GameManager.instance.gameover();
        }

        //점수 업데이트
        GameManager.instance.UpdateScore(Mathf.Clamp(-(int)(transform.position.y * 10), 0, int.MaxValue));
    }

    /// <summary>
    /// 플레이어가 먹이에 붙을 수 있는 상태인지를 반환합니다
    /// </summary>
    /// <param name="food">먹이</param>
    /// <returns>플레이어가 먹이에 붙을 수 있는 상태</returns>
    public bool CanUnite(Food food)
    {
        //붙어있는 먹이가 null 이고 food가 켜져있으면 true를 리턴
        return this.food == null && food.gameObject.activeSelf;
    }

    /// <summary>
    /// 플레이어가 먹이에 붙는 함수입니다
    /// </summary>
    /// <param name="food">붙을 먹이</param>
    public void UniteFood(Food food)
    {
        //애니메이션을 붙는 애니메이션으로 바꾼다
        anim.SetBool("isHang", true);

        //붙어있는 상태를 true로 바꿔주고
        isCling = true;
        //먹이의 붙어있는 상태도 true로 바꿔준다
        food.isCling = true;

        //좌우로는 움직일 수 없게 해준다
        canMove = false;

        //player의 food를 붙은 food로 바꿔주고
        this.food = food;
        //downSpeed를 먹이가 가지고있는 붙어있을때 내려가는 속도로 바꿔준다
        this.downSpeed = food.clingSpeed;
    }

    /// <summary>
    /// 플레이어가 먹이에서 강제로 떨어지는 함수입니다
    /// </summary>
    /// <param name="food">떨어질 먹이</param>
    public void DisuniteFood(Food food)
    {
        //애니메이션을 수영하는 애니메이션으로 바꾼다
        anim.SetBool("isHang", false);

        //붙어있는 상태를 꺼주고
        isCling = false;
        //먹이도 붙어있지 않은 상태로 만들어준다
        food.isCling = false;

        //다시 좌우로 움직일 수 있게 해준다
        canMove = true;

        //food를 다시 null로 바꿔준다
        this.food = null;

        //붙어있던 시간도 초기화해준다
        clingTimer = 0f;
    }
}
