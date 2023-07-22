using System;
using UnityEngine;

public class BoardSelectionPageManager : Page
{
    public Action OnReAssignLevelIcons;
    public int ScrolledViewLevelStartingFrom {get; private set;}
    private int LevelIncrementPerScrollClick = 35;

    public BoardTypes CurrentBoardType;

    [SerializeField]
    private SpriteRenderer TypeSpriteRenderer;

    [SerializeField]
    private Sprite Tiny, Medium, Huge;

    public override void DisplayPage()
    {
        
    }

    public override void PreparePage()
    {
        CurrentBoardType = ManagersSingleton.Managers.Profile.GetRecentBoardType();
        OnReAssignLevelIcons?.Invoke();
    }

    public void ToggleForward()
    {
        switch(CurrentBoardType)
        {
            case BoardTypes.Squ5: CurrentBoardType = BoardTypes.Squ10; break;
            case BoardTypes.Squ10: CurrentBoardType = BoardTypes.Squ15; break;
            case BoardTypes.Squ15: CurrentBoardType = BoardTypes.Squ5; break;
        }
        SetTypeSprite();
    }

    public void ToggleBackward()
    {
        switch (CurrentBoardType)
        {
            case BoardTypes.Squ5: CurrentBoardType = BoardTypes.Squ15; break;
            case BoardTypes.Squ10: CurrentBoardType = BoardTypes.Squ5; break;
            case BoardTypes.Squ15: CurrentBoardType = BoardTypes.Squ10; break;
        }
        SetTypeSprite();
    }

    private void SetTypeSprite()
    {
        switch (CurrentBoardType)
        {
            case BoardTypes.Squ5: TypeSpriteRenderer.sprite = Tiny ; break;
            case BoardTypes.Squ10: TypeSpriteRenderer.sprite = Medium; break;
            case BoardTypes.Squ15: TypeSpriteRenderer.sprite = Huge; break;
        }
    }

    public void ClickedLevelIcon(int levelIndex)
    {
        ManagersSingleton.Managers.PuzzlePageManager.SetThisLevelNext(CurrentBoardType, levelIndex);
        ManagersSingleton.Managers.CameraMovement.GoHere(Pages.Puzzle);
    }

    public void ClickedScroll(bool upward)
    {
        int increment = (upward ? -1 : 1) * LevelIncrementPerScrollClick;
        ScrolledViewLevelStartingFrom += increment;
        OnReAssignLevelIcons?.Invoke();
    }
}
