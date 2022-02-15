using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePageManager : Page
{
    public MarkCellAsMananger MarkCellAsMananger;
    public Sprite EmptyCellSprite;

    private string PrefabsFolderPath = "Boards Prefabs/";

    Board CurrentBoard;

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
        // instantiate
        //      (later check if new level’s board matches the prev’s board. If not, destroy that and instantiate this.)
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

        return Instantiate(Resources.Load(GetPrefabPath(CurrentBoard.CurrentPuzzle.MyPool.BoardType), typeof(GameObject))) as Board;
    }

    private bool IsBoardInstantiatingNeeded(PuzzleInfo nextPuzzleInfo)
    {
        return CurrentBoard.CurrentPuzzle.MyPool.BoardType != nextPuzzleInfo.MyPool.BoardType;
    }
}
