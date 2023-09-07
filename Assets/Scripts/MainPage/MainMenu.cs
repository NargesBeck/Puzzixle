using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Page
{
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button LevelsButton;
    [SerializeField] private Button SettingsButton;

    public override void PreparePage()
    {
        ManagersSingleton.Managers.PuzzlePageManager.HidePuzzle();

        PlayButton.onClick.RemoveAllListeners();
        PlayButton.onClick.AddListener(PlayButtonClick);

        LevelsButton.onClick.RemoveAllListeners();
        LevelsButton.onClick.AddListener(LevelsButtonClick);
    }

    private void PlayButtonClick()
    {
        ManagersSingleton.Managers.PageTurner.GoToPage(Pages.Puzzle);
    }

    private void LevelsButtonClick()
    {
        ManagersSingleton.Managers.PageTurner.GoToPage(Pages.BoardSelection);
    }
}
