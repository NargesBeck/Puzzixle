using DG.Tweening;
using UnityEngine;
using System;
public enum Pages
{
    MainMenu, Puzzle, BoardSelection, LevelWon, LevelFailed
}

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform pinMain, pinPuzzle, pinBoard, pinLevelCompleted, pinLevelFailed;


    public Pages CurrentPage;
    private new Transform transform;
    private bool ZoomedOut = false;

    private Action OnFinishedGoingToNewPage;

    private bool IsSwitchingPages;

    private void Awake()
    {
        ManagersSingleton.Managers.GameManager.OnLifeLoss += Shake;
        transform = GetComponent<Transform>();
        ManagersSingleton.Managers.Profile.ResetPlayerProgress();

        //OnFinishedGoingToNewPage = ManagersSingleton.Managers.PuzzlePageManager.DisplayPage;
        GoHere(Pages.MainMenu);
    }

    public void GoHere(Pages page)
    {
        if (IsSwitchingPages)
            return;

        switch (page)
        {
            case Pages.Puzzle:
                ManagersSingleton.Managers.PuzzlePageManager.PreparePage();
                OnFinishedGoingToNewPage = ManagersSingleton.Managers.PuzzlePageManager.DisplayPage;
                //if (OnFinishedGoingToNewPage != null)
                //    OnFinishedGoingToNewPage();
                //return;
                break;
            case Pages.BoardSelection:
                ManagersSingleton.Managers.BoardSelectionPageManager.PreparePage();
                OnFinishedGoingToNewPage = ManagersSingleton.Managers.BoardSelectionPageManager.DisplayPage;
                break;
            case Pages.LevelWon:
                ManagersSingleton.Managers.LevelWonPageManager.PreparePage();
                OnFinishedGoingToNewPage = ManagersSingleton.Managers.LevelWonPageManager.DisplayPage;
                break;
            case Pages.LevelFailed:
                ManagersSingleton.Managers.LevelLostPageManager.PreparePage();
                OnFinishedGoingToNewPage = ManagersSingleton.Managers.LevelLostPageManager.DisplayPage;
                break;
        }

        CurrentPage = page;
        //OnFinishedGoingToNewPage?.Invoke();
        IsSwitchingPages = true;
        ZoomInOut();
        transform.DOKill();
        transform.DOMove(GetPagePinPos(page), 1, false).OnComplete(ZoomInOut);
    }

    public void ZoomInOut()
    {
        ManagersSingleton.Managers.Camera.DOKill();
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
        Vector3 output = default;
        switch (page)
        {
            case Pages.MainMenu:
                output = pinMain.position;
                break;

            case Pages.Puzzle:
                output = pinPuzzle.position;
                break;
            case Pages.BoardSelection:
                output = pinBoard.position;
                break;
            case Pages.LevelWon:
                output = pinLevelCompleted.position;
                break;
            case Pages.LevelFailed:
                output = pinLevelFailed.position;
                break;
        }
        return new Vector3(output.x, output.y, -10);
    }

    private void Shake()
    {
        float intensity = 0.05f;
        float speed = 0.1f;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(transform.position + new Vector3(intensity, 0, 0), speed));
        sequence.Append(transform.DOMove(transform.position + new Vector3(-intensity, 0, 0), speed));
        sequence.Append(transform.DOMove(transform.position + new Vector3(intensity, 0, 0), speed));

        sequence.Play();
    }
}
