using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Page
{
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button LevelsButton;
    [SerializeField] private Button SettingsButton;

    public override void PreparePage()
    {
        PlayButton.onClick.RemoveAllListeners();
        PlayButton.onClick.AddListener(PlayButtonClick);

        LevelsButton.onClick.RemoveAllListeners();
        LevelsButton.onClick.AddListener(LevelsButtonClick);

        //RectTransform play = PlayButton.GetComponent<RectTransform>();
        //CrunchUI.CrunchMoveX(play, -700, 0, 3, 10);

        PlayButton.gameObject.SetActive(true);
        SettingsButton.gameObject.SetActive(false);
        LevelsButton.gameObject.SetActive(true);
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
