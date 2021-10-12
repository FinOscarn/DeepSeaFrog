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

    public Text titleText;
    public Text start;
    public Camera cam;

    [TextArea]
    public string text;

    void Start()
    {
        cam = Camera.main;
        titleText.DOText(text, 3f);
    }

    void Update()
    {

    }
    public void TitlePanel()
    {
        title.interactable = false;
        title.blocksRaycasts = false;
        StartCoroutine(CheckTime(title));
    }

    public void GameOver()
    {
        inGame.interactable = false;
        inGame.blocksRaycasts = false;
        StartCoroutine(CheckTime(inGame));
    }

    public void Restart()
    {
        gameOver.interactable = false;
        gameOver.blocksRaycasts = false;
        StartCoroutine(CheckTime(gameOver));
    }

    IEnumerator CheckTime(CanvasGroup cGroup)
    {
        while(cGroup.alpha > 0)
        {
            cGroup.alpha -= 0.07f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
