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

    public CanvasGroup exitButton;

    public Text titleText;
    public Text touchToStartText;

    // 최고 점수
    public Text highScore;

    public Text overFinalScore;
    public Text overHighScore;

    // 인게임에서의 현재 점수
    public Text currentScore;

    // 게임 오버시의 마지막 점수
    public Text finalScore;

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
        titleText.fontSize = 400;
        cam = Camera.main;
        audioSource = gameObject.GetComponent<AudioSource>();
        //titleText.DOText(text, 2f);
        StartCoroutine(ShowReady());

        //게임오버를 구독한다
        GameManager.instance.gameover += GameOver;
    }

    void Update()
    {
        // 점수 연산
        currentScore.text = GameManager.instance.score.ToString();
        highScore.text = GameManager.instance.highScore.ToString();
    }


    // inGame패널의 Stop 버튼을 누를 시 실행되는 함수, Stop 패널로 전환한다. Clear
    public void StopGame()
    {
        // 플레이어 고정
        GameManager.instance.pause(true);

        stopPanel.alpha = 1;
        stopPanel.interactable = true;
        stopPanel.blocksRaycasts = true;
        exitButton.alpha = 0;
        
    }
    public void ResumeGame()
    {
        GameManager.instance.pause(false);

        stopPanel.alpha = 0;
        stopPanel.interactable = false;
        stopPanel.blocksRaycasts = false;
        exitButton.alpha = 1;
    }

    // Stop 패널의 title 버튼을 누르면 타이틀 패널로 전환되는 함수
    public void GoTitle()
    {
        // stop 패널 비활성화
        stopPanel.alpha = 0;
        stopPanel.interactable = false;
        stopPanel.blocksRaycasts = false;

        //inGame 패널 비활성화
        //inGame.interactable = false;
        //inGame.blocksRaycasts = false;
        //StartCoroutine(CheckTime(inGame));

        // title 패널 활성화
        title.alpha = 1;
        title.blocksRaycasts = true;
        title.interactable = true;
        currentScore.gameObject.SetActive(false);

        GameManager.instance.reset();
    }

    // 클릭시 사운드 아이콘과 사운드 재생을 해주는 함수 Clear
    public void SoundChange()
    {
        if (!change)
        {
            audioSource.Stop();
            soundMute.sprite = muteIcon;
            change = true;
            audioSource.Stop();
        }
        else if (change)
        {
            audioSource.Play();
            soundMute.sprite = onIcon;
            change = false;
        }
        else
            return;
    }

    // title 패널, 클릭 시 inGame 패널로 전환된다.
    public void TitlePanel()
    {
        title.alpha = 0;
        title.interactable = false;
        title.blocksRaycasts = false;
        StartCoroutine(CheckTime(title));


        // 게임시작
        GameManager.instance.startGame();

        audioSource.Play();
        currentScore.gameObject.SetActive(true); // 점수 구현
        // 먹이 구현

    }

    // 플레이어가 패배한다면, 사용되는 함수, GameOver패널로 전환된다.
    public void GameOver()
    {
        //inGame.GetComponent<GameObject>().SetActive(false);
        inGame.alpha = 0;
        inGame.interactable = false;
        inGame.blocksRaycasts = false;
        StartCoroutine(CheckTime(inGame));

        //gameOver.GetComponent<GameObject>().SetActive(true);
        gameOver.alpha = 1;
        gameOver.interactable = true;
        gameOver.blocksRaycasts = true;

        overFinalScore.text = GameManager.instance.score.ToString();
        overHighScore.text = GameManager.instance.highScore.ToString();
        //currentScore
        

        // 플레이어 비활
        // 점수 비활
        // 먹이 비활
    }
    
    // GameOver패널의 Restart버튼을 누른다면 사용되는 함수다.
    public void Restart()
    {

        stopPanel.alpha = 0;
        stopPanel.interactable = false;
        stopPanel.blocksRaycasts = false;

        exitButton.alpha = 1;
        exitButton.interactable = true;
        exitButton.blocksRaycasts = true;

        inGame.alpha = 1;
        inGame.interactable = true;
        inGame.blocksRaycasts = true;

        gameOver.alpha = 0;
        gameOver.interactable = false;
        gameOver.blocksRaycasts = false;

        GameManager.instance.reset();
        GameManager.instance.startGame();

       

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

    IEnumerator ShowReady()
    {
        yield return new WaitForSeconds(1f);                              
        int count = 0;
        while (count < 100)
        {
            touchToStartText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            touchToStartText.gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);
            count++;
        }
        touchToStartText.gameObject.SetActive(true);
    }
}
