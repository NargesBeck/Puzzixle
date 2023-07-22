using System;
using UnityEngine;

public class BoardSelectionPageManager : Page
{
    public Action OnBoardSelectionPrepare;

    private BoardTypes CurrentBoardType;

    public override void DisplayPage()
    {
        
    }

    public override void PreparePage()
    {
        CurrentBoardType = ManagersSingleton.Managers.Profile.GetRecentBoardType();
        OnBoardSelectionPrepare?.Invoke();
    }

    public void ToggleForward()
    {
        switch(CurrentBoardType)
        {
            case BoardTypes.Squ5: CurrentBoardType = BoardTypes.Squ10; break;
            case BoardTypes.Squ10: CurrentBoardType = BoardTypes.Squ15; break;
            case BoardTypes.Squ15: CurrentBoardType = BoardTypes.Squ5; break;
        }
    }

    public void ToggleBackward()
    {
        switch (CurrentBoardType)
        {
            case BoardTypes.Squ5: CurrentBoardType = BoardTypes.Squ15; break;
            case BoardTypes.Squ10: CurrentBoardType = BoardTypes.Squ5; break;
            case BoardTypes.Squ15: CurrentBoardType = BoardTypes.Squ10; break;
        }
    }

    public void ClickedLevelIcon(int levelIndex)
    {
        ManagersSingleton.Managers.PuzzlePageManager.SetThisLevelNext(CurrentBoardType, levelIndex);
    }
}
