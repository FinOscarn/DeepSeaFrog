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
    public Text scoreText;
    public Text touchToStartText;

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
        StartCoroutine(ShowReady());
    }


    void Update()
    {
        // ���� ����
        //float a = 0;
        //scoreText.text = 
        //a += Time.deltaTime;
    }


    // inGame�г��� Stop ��ư�� ���� �� ����Ǵ� �Լ�, Stop �гη� ��ȯ�Ѵ�. Clear
    public void StopGame()
    {
        // �÷��̾� ����
        stopPanel.alpha = 1;
        stopPanel.interactable = true;
        stopPanel.blocksRaycasts = true;
        exitButton.alpha = 0;
        
    }
    public void ResumeGame()
    {
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

        // inGame �г� ��Ȱ��ȭ
        //inGame.interactable = false;
        //inGame.blocksRaycasts = false;
        //StartCoroutine(CheckTime(inGame));

        // title �г� Ȱ��ȭ
        title.alpha = 1;
        title.blocksRaycasts = true;
        title.interactable = true;
        scoreText.gameObject.SetActive(false);
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
        title.gameObject.SetActive(false);
        title.interactable = false;
        title.blocksRaycasts = false;
        StartCoroutine(CheckTime(title));



        // �÷��̾� ����
        audioSource.Play();
        scoreText.gameObject.SetActive(true); // ���� ����
        
        // ���� ����
    }

    // �÷��̾ �й��Ѵٸ�, ���Ǵ� �Լ�, GameOver�гη� ��ȯ�ȴ�.
    public void GameOver()
    {
        inGame.GetComponent<GameObject>().SetActive(false);
        inGame.interactable = false;
        inGame.blocksRaycasts = false;
        StartCoroutine(CheckTime(inGame));

        

        // �÷��̾� ��Ȱ
        // ���� ��Ȱ
        // ���� ��Ȱ
    }
    
    // GameOver�г��� Restart��ư�� �����ٸ� ���Ǵ� �Լ���.
    public void Restart()
    {
        gameOver.interactable = false;
        gameOver.blocksRaycasts = false;
        StartCoroutine(CheckTime(gameOver));

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
        yield return new WaitForSeconds(2.5f);                              
        int count = 0;
        while (count < 3)
        {
            touchToStartText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            touchToStartText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            count++;
        }
        touchToStartText.gameObject.SetActive(true);
    }
}
