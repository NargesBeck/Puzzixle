using UnityEngine;
using UnityEngine.UI;

public class BoardSelectionPageManager : Page
{
    [SerializeField] private Text BoardTypeText;

    public BoardTypes CurrentBoardType;
    public GameObject LevelIconPrefab;
    public GameObject LevelIconsParent;
    public GameObject[] LevelIconsArr;

    public override void PreparePage()
    {
        CurrentBoardType = ManagersSingleton.Managers.Profile.GetRecentBoardType();
        UpdateScrollableLevelIcons();
    }

    public void ToggleForward()
    {
        switch (CurrentBoardType)
        {
            case BoardTypes.Squ5: CurrentBoardType = BoardTypes.Squ10; break;
            case BoardTypes.Squ10: CurrentBoardType = BoardTypes.Squ15; break;
            case BoardTypes.Squ15: CurrentBoardType = BoardTypes.Squ5; break;
        }
        SetTypeText();
        UpdateScrollableLevelIcons();
    }

    public void ToggleBackward()
    {
        switch (CurrentBoardType)
        {
            case BoardTypes.Squ5: CurrentBoardType = BoardTypes.Squ15; break;
            case BoardTypes.Squ10: CurrentBoardType = BoardTypes.Squ5; break;
            case BoardTypes.Squ15: CurrentBoardType = BoardTypes.Squ10; break;
        }
        SetTypeText();
        UpdateScrollableLevelIcons();
    }

    private void SetTypeText()
    {
        switch (CurrentBoardType)
        {
            case BoardTypes.Squ5: BoardTypeText.text = "Tiny"; break;
            case BoardTypes.Squ10: BoardTypeText.text = "Medium"; break;
            case BoardTypes.Squ15: BoardTypeText.text = "Huge"; break;
        }
    }

    public void ClickedLevelIcon(int levelIndex)
    {
        ManagersSingleton.Managers.PuzzlePageManager.SetThisLevelNext(CurrentBoardType, levelIndex);
        ManagersSingleton.Managers.PageTurner.GoToPage(Pages.Puzzle);
    }

    private void UpdateScrollableLevelIcons()
    {
        int count = ManagersSingleton.Managers.PuzzlePageManager.GetCountLevels(CurrentBoardType);
        for (int i = 0; i < LevelIconsArr.Length; i++)
        {
            if (i < count)
            {
                LevelIconsArr[i].SetActive(true);

                int lastlevel = ManagersSingleton.Managers.Profile.GetLastPuzzlePlayedForThisBoard(CurrentBoardType);
                bool isOpen = lastlevel + 1 >= i;

                LevelIconsArr[i].GetComponent<LevelIcons>().AssignMe(i + 1, isOpen);
            }
            else
            {
                LevelIconsArr[i].SetActive(false);
            }
        }
    }
}
