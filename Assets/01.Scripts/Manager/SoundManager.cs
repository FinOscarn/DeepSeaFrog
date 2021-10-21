using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    bool change = false;

    public Sprite onIcon;
    public Sprite muteIcon;

    public AudioSource audioSource;

    UIManager uiManager;
    // Start is called before the first frame update

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        uiManager = GetComponent<UIManager>();
    }

    public void SoundChange()
    {
        if (!change)
        {
            audioSource.Stop();
            uiManager.soundMute.sprite = muteIcon;
            change = true;
            audioSource.Stop();
        }
        else if (change)
        {
            audioSource.Play();
            uiManager.soundMute.sprite = onIcon;
            change = false;
        }
        else
            return;
    }

}
