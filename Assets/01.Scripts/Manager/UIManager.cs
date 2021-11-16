using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Image[] tutorialImages;

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
    public Image soundMuteTitle;
  
    public Sprite muteIcon;
    public Sprite onIcon;

    bool change = true;
    bool change1 = false;

    bool canPause = false;

    public Sprite[] tutorialSprites;
    public int tutorialIdx = 0;
    public Image tutorialImage;
    public CanvasGroup cvsTutorial;

    [TextArea]
    public string text;

    void Start()
    {
        titleText.fontSize = 400;
        cam = Camera.main;
        audioSource = gameObject.GetComponent<AudioSource>();
        //titleText.DOText(text, 2f);
        StartCoroutine(ShowReady());

        audioSource.Play();

        //게임오버를 구독한다
        GameManager.instance.gameover += GameOver;

        GameManager.instance.playerD2ve += () =>
        {
            canPause = true;
        };

        GameManager.instance.reset += () =>
        {
            canPause = false;
        };
    }

    void Update()
    {
        // 점수 연산
        currentScore.text = $"Score\n{GameManager.instance.score}";
        highScore.text = $"HighScore\n{GameManager.instance.highScore}";
    }

    public void StartTutorial()
    {
        tutorialIdx = 0;
        cvsTutorial.alpha = 1;
        tutorialImage.sprite = tutorialSprites[tutorialIdx];
        cvsTutorial.interactable = true;
        cvsTutorial.blocksRaycasts = true;
    }

    public void NextTutorial()
    {
        if(tutorialIdx >= 4)
        {
            cvsTutorial.alpha = 0;

            cvsTutorial.interactable = false;
            cvsTutorial.blocksRaycasts = false;
        }
        else
        {
            tutorialIdx += 1;
            tutorialImage.sprite = tutorialSprites[tutorialIdx];
        }
    }

    // inGame패널의 Stop 버튼을 누를 시 실행되는 함수, Stop 패널로 전환한다. Clear
    public void StopGame()
    {
        if (!canPause) return;

        // 플레이어 고정
        GameManager.instance.pause(true);

        stopPanel.alpha = 1;
        stopPanel.interactable = true;
        stopPanel.blocksRaycasts = true;
        exitButton.alpha = 1;
        
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

        gameOver.alpha = 0;
        gameOver.interactable = false;
        gameOver.blocksRaycasts = false;

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
            audioSource.volume = 0;
            soundMute.sprite = muteIcon;
            change = true;
        }
        else if (change)
        {
            audioSource.volume = 1;
            soundMute.sprite = onIcon;
            change = false;
        }
        else
            return;
    }

    // title 패널, 클릭 시 inGame 패널로 전환된다.
    public void TitlePanel()
    {
        //SoundChange();

        title.alpha = 0;
        title.interactable = false;
        title.blocksRaycasts = false;
        StartCoroutine(CheckTime(title));

        gameOver.alpha = 0;
        gameOver.interactable = false;
        gameOver.blocksRaycasts = false;

        inGame.alpha = 1;
        inGame.interactable = true;
        inGame.blocksRaycasts = true;

        // 게임시작
        GameManager.instance.startGame();

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

        overFinalScore.text = $"Score\n{GameManager.instance.score}";
        overHighScore.text = $"{GameManager.instance.highScore}";
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
