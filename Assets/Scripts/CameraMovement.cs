using DG.Tweening;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform pinMain, pin5x5;

    public enum Pages
    {
        MainMenu, Puzzle
    }

    public Pages CurrentPage;
    private new Transform transform;
    private bool ZoomedOut = false;

    private void Awake()
    {
        transform = GetComponent<Transform>();
        GoHere(pinMain.position);
    }

    private void GoHere(Vector3 destination)
    {
        ZoomInOut();
        transform.DOMove(destination, 1, false).OnComplete(ZoomInOut);
    }

    public void GoToAnotherPage(Pages newPage)
    {
        GoHere(GetPagePosition(newPage));
        CurrentPage = newPage;
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
                output = pin5x5.position;
                break;
        }
        return output;
    }

    public void ZoomInOut()
    {
        ManagersSingleton.Managers.Camera.DOOrthoSize((ZoomedOut) ? 5 : 5.5f, 0.5f);
        ZoomedOut = !ZoomedOut;
    }
}
