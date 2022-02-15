using DG.Tweening;
using UnityEngine;
using System;
public enum Pages
{
    MainMenu, Puzzle
}

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform pinMain, pinPuzzle;


    public Pages CurrentPage;
    private new Transform transform;
    private bool ZoomedOut = false;

    public Action OnFinishedGoingToNewPage;

    private bool IsSwitchingPages;

    private void Awake()
    {
        transform = GetComponent<Transform>();

        OnFinishedGoingToNewPage = ManagersSingleton.Managers.PuzzlePageManager.DisplayPage;
        GoHere(Pages.MainMenu);
    }

    public void GoHere(Pages page)
    {
        if (IsSwitchingPages)
            return;

        if (page == Pages.Puzzle)
        {
            OnFinishedGoingToNewPage = ManagersSingleton.Managers.PuzzlePageManager.DisplayPage;
        }

        CurrentPage = page;
        IsSwitchingPages = true;
        ZoomInOut();
        transform.DOMove(GetPagePinPos(page), 1, false).OnComplete(ZoomInOut);
    }

    private Vector3 GetPagePosition(Pages page)
    {
        Vector3 output = default;
        switch(page)
        {
            case Pages.MainMenu:
                output = pinMain.position;
                break;

            case Pages.Puzzle:
                output = pinPuzzle.position;
                break;
        }
        return output;
    }

    public void ZoomInOut()
    {
        ManagersSingleton.Managers.Camera.DOOrthoSize((ZoomedOut) ? 5 : 5.5f, 0.5f).OnComplete(FinishedGoingToNewPage);
        ZoomedOut = !ZoomedOut;
    }

    private void FinishedGoingToNewPage()
    {
        IsSwitchingPages = false;
        if (OnFinishedGoingToNewPage != null)
            OnFinishedGoingToNewPage();
    }

    private Vector3 GetPagePinPos(Pages page)
    {
        switch(page)
        {
            case Pages.MainMenu:
                return pinMain.position;
            case Pages.Puzzle:
                return pinPuzzle.position;
        }
        return Vector3.zero;
    }
}
