using System;
using System.Collections.Generic;
using UnityEngine;

public enum Pages
{
    MainMenu, Puzzle, BoardSelection, LevelWon, LevelFailed
}

public class PageTurner : MonoBehaviour
{
    private Action OnFinishedGoingToNewPage;
    private Pages CurrentPage;

    //private void Awake()
    //{
    //    ManagersSingleton.Managers.GameManager.OnAppOpen += () => GoToPage(Pages.MainMenu);
    //}

    public void GoToPage(Pages page)
    {
        CurrentPage = page;
        TurnAllPagesOff();

        switch (page)
        {
            case Pages.Puzzle:
                ManagersSingleton.Managers.PuzzlePageManager.PreparePage();
                ManagersSingleton.Managers.PuzzlePageManager.gameObject.SetActive(true);
                ManagersSingleton.Managers.PuzzlePageManager.DisplayPage();
                break;
            case Pages.BoardSelection:
                ManagersSingleton.Managers.BoardSelectionPageManager.PreparePage();
                ManagersSingleton.Managers.BoardSelectionPageManager.gameObject.SetActive(true);
                ManagersSingleton.Managers.BoardSelectionPageManager.DisplayPage();
                break;
            case Pages.LevelWon:
                ManagersSingleton.Managers.LevelWonPageManager.PreparePage();
                ManagersSingleton.Managers.LevelWonPageManager.gameObject.SetActive(true);
                ManagersSingleton.Managers.LevelWonPageManager.DisplayPage();
                break;
            case Pages.LevelFailed:
                ManagersSingleton.Managers.LevelLostPageManager.PreparePage();
                ManagersSingleton.Managers.LevelLostPageManager.gameObject.SetActive(true);
                ManagersSingleton.Managers.LevelLostPageManager.DisplayPage();
                break;
            case Pages.MainMenu:
                ManagersSingleton.Managers.MainMenu.PreparePage();
                ManagersSingleton.Managers.MainMenu.gameObject.SetActive(true);
                ManagersSingleton.Managers.MainMenu.DisplayPage();
                break;
        }

    }

    private void TurnAllPagesOff()
    {
        ManagersSingleton.Managers.MainMenu.gameObject.SetActive(false);
        ManagersSingleton.Managers.LevelLostPageManager.gameObject.SetActive(false);
        ManagersSingleton.Managers.LevelWonPageManager.gameObject.SetActive(false);
        ManagersSingleton.Managers.BoardSelectionPageManager.gameObject.SetActive(false);
        ManagersSingleton.Managers.PuzzlePageManager.gameObject.SetActive(false);
    }
}
