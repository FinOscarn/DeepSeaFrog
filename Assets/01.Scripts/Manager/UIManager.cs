using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public CanvasGroup title;
    public CanvasGroup inGame;
    public CanvasGroup gameOver;
    public CanvasGroup stopPanel;

    public Text titleText;
    public Text scoreText;

    public AudioSource audioSource;
    private Camera cam;

    public Image soundMute;
  
    public Sprite muteIcon;
    public Sprite onIcon;

    bool change = false;

    [TextArea]
    public string text;

    void Start()
    {
        cam = Camera.main;
        audioSource = gameObject.GetComponent<AudioSource>();

        titleText.DOText(text, 2f);
    }

    void Update()
    {
        // 점수 연산
        //float a = 0;
        //scoreText.text = 
        //a += Time.deltaTime;
    }


    // inGame패널의 Stop 버튼을 누를 시 실행되는 함수, Stop 패널로 전환한다. Clear
    public void StopGame()
    {
        // 플레이어 고정
        stopPanel.alpha = 1;
        stopPanel.interactable = true;
        stopPanel.blocksRaycasts = true;
    }

    // Stop 패널의 title 버튼을 누르면 타이틀 패널로 전환되는 함수
    public void GoTitle()
    {

    }

    // 클릭시 사운드 아이콘과 사운드 재생을 해주는 함수 Clear
    public void SoundChange()
    {
        if (!change)
        {
            //audioSource.Stop();
            soundMute.sprite = muteIcon;
            change = true;
        }
        else if (change)
        {
            //audioSource.Play();
            soundMute.sprite = onIcon;
            change = false;
        }
        else
            return;
    }

    // title 패널, 클릭 시 inGame 패널로 전환된다.
    public void TitlePanel()
    {
        title.gameObject.SetActive(false);
        title.interactable = false;
        title.blocksRaycasts = false;
        StartCoroutine(CheckTime(title));


        // 플레이어 생성
        //audioSource.Play();
        scoreText.gameObject.SetActive(true); // 점수 구현
        
        // 먹이 구현
    }

    // 플레이어가 패배한다면, 사용되는 함수, GameOver패널로 전환된다.
    public void GameOver()
    {
        inGame.GetComponent<GameObject>().SetActive(false);
        inGame.interactable = false;
        inGame.blocksRaycasts = false;
        StartCoroutine(CheckTime(inGame));

        

        // 플레이어 비활
        // 점수 비활
        // 먹이 비활
    }
    
    // GameOver패널의 Restart버튼을 누른다면 사용되는 함수다.
    public void Restart()
    {
        gameOver.interactable = false;
        gameOver.blocksRaycasts = false;
        StartCoroutine(CheckTime(gameOver));

        // 먹이 이동 시작
        // 플레이어 생성
        // 점수 초기화 후 점수 연산활성화
    }


    // 캔버스 그룹의 알파값을 서서히 바꿔주는 함수 Clear
    IEnumerator CheckTime(CanvasGroup cGroup)
    {
        while(cGroup.alpha > 0)
        {
            cGroup.alpha -= 0.07f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
