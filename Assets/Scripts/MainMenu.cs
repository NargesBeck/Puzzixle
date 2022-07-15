﻿using DG.Tweening;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Transform PlayButton;

    public void PlayButtonClick()
    {
        return;
        Debug.Log("main menu >> click");
        switch (name)
        {
            case "PlayButton":
                Play();
                break;
        }
    }

    public void SetUpMainMenu()
    {
        PlayButton.DOScale(0.55f, 0.25f).OnComplete(() => PlayButton.DOScale(0.5f, 1));
    }

    private void Play()
    {
        Debug.Log("main menu >> click >> play");
        
    }
}
