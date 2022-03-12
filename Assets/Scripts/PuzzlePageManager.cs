using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePageManager : Page
{
    public MarkCellAsMananger MarkCellAsMananger;
    public Sprite EmptyCellSprite;

    private string PrefabsFolderPath = "Boards Prefabs/";

    public Board CurrentBoard;

    public override void DisplayPage()
    {
        //StartLevel();

    }

    public void Click()
    {
        switch(name)
        {
            case "MarkAs":
                MarkCellAsMananger?.Click();
                break;
        }
    }

    public void StartLevel(PuzzleInfo puzzleInfo)
    {
        CurrentBoard = InstantiateNewBoardIfNeeded(puzzleInfo);

        CurrentBoard.RunLevel(puzzleInfo);
    }

    private string GetPrefabPath(BoardTypes boardType)
    {
        switch (boardType)
        {
            case BoardTypes.Squ5:
                return PrefabsFolderPath + "Puzzle5x5";
            
            case BoardTypes.Squ10:
            default:
                return PrefabsFolderPath + "Puzzle10x10";
        }
    }

    private Board InstantiateNewBoardIfNeeded(PuzzleInfo nextPuzzleInfo)
    {
        if (!IsBoardInstantiatingNeeded(nextPuzzleInfo))
            return CurrentBoard;

        Destroy(CurrentBoard.gameObject);
        return Instantiate(Resources.Load(GetPrefabPath(ManagersSingleton.Managers.BoardSelectionPageManager.CurrentBoardType), typeof(GameObject))) as Board;
    }

    private bool IsBoardInstantiatingNeeded(PuzzleInfo nextPuzzleInfo)
    {
        return ManagersSingleton.Managers.BoardSelectionPageManager.CurrentBoardType != ManagersSingleton.Managers.BoardSelectionPageManager.PrevBoardType;
    }
}