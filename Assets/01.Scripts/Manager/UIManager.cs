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

    // �ְ� ����
    public Text highScore;

    public Text overFinalScore;
    public Text overHighScore;

    // �ΰ��ӿ����� ���� ����
    public Text currentScore;

    // ���� �������� ������ ����
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

        //���ӿ����� �����Ѵ�
        GameManager.instance.gameover += GameOver;
    }

    void Update()
    {
        // ���� ����
        currentScore.text = GameManager.instance.score.ToString();
        highScore.text = GameManager.instance.highScore.ToString();
    }


    // inGame�г��� Stop ��ư�� ���� �� ����Ǵ� �Լ�, Stop �гη� ��ȯ�Ѵ�. Clear
    public void StopGame()
    {
        // �÷��̾� ����
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

    // Stop �г��� title ��ư�� ������ Ÿ��Ʋ �гη� ��ȯ�Ǵ� �Լ�
    public void GoTitle()
    {
        // stop �г� ��Ȱ��ȭ
        stopPanel.alpha = 0;
        stopPanel.interactable = false;
        stopPanel.blocksRaycasts = false;

        //inGame �г� ��Ȱ��ȭ
        //inGame.interactable = false;
        //inGame.blocksRaycasts = false;
        //StartCoroutine(CheckTime(inGame));

        // title �г� Ȱ��ȭ
        title.alpha = 1;
        title.blocksRaycasts = true;
        title.interactable = true;
        currentScore.gameObject.SetActive(false);

        GameManager.instance.reset();
    }

    // Ŭ���� ���� �����ܰ� ���� ����� ���ִ� �Լ� Clear
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

    // title �г�, Ŭ�� �� inGame �гη� ��ȯ�ȴ�.
    public void TitlePanel()
    {
        title.alpha = 0;
        title.interactable = false;
        title.blocksRaycasts = false;
        StartCoroutine(CheckTime(title));


        // ���ӽ���
        GameManager.instance.startGame();

        audioSource.Play();
        currentScore.gameObject.SetActive(true); // ���� ����
        // ���� ����

    }

    // �÷��̾ �й��Ѵٸ�, ���Ǵ� �Լ�, GameOver�гη� ��ȯ�ȴ�.
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
        

        // �÷��̾� ��Ȱ
        // ���� ��Ȱ
        // ���� ��Ȱ
    }
    
    // GameOver�г��� Restart��ư�� �����ٸ� ���Ǵ� �Լ���.
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

       

        // ���� �̵� ����
        // �÷��̾� ����
        // ���� �ʱ�ȭ �� ���� ����Ȱ��ȭ
    }


    // ĵ���� �׷��� ���İ��� ������ �ٲ��ִ� �Լ� Clear
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
