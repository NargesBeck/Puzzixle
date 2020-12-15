using DG.Tweening;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Transform PlayButton;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpMainMenu()
    {
        PlayButton.DOScale(0.55f, 0.25f).OnComplete(() => PlayButton.DOScale(0.5f, 1));
    }

    public void Play()
    {
        ManagersSingleton.Managers.CameraMovement.GoToAnotherPage(CameraMovement.Pages.Puzzle5);
    }
}
