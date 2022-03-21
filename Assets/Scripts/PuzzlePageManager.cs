using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePageManager : Page
{
    public MarkCellAsMananger MarkCellAsMananger;
    public Sprite EmptyCellSprite;

    private int NumHintsLeft;
    public bool HintIsActive;
    //private string PrefabsFolderPath = "Boards Prefabs/";

    [SerializeField]
    private List<Board> BoardObjects = new List<Board>();
    
    public Board CurrentBoard;

    public override void DisplayPage()
    {
        var DB = Resources.Load<PuzzlesScriptableObject>("Puzzles");

        StartLevel(DB.PuzzlesPool[0].BoardType, DB.PuzzlesPool[0].PuzzlesList[0]);
    }

    public void StartLevel(BoardTypes boardType, PuzzleInfo puzzleInfo)
    {
        //CurrentBoard = InstantiateNewBoardIfNeeded(puzzleInfo);
        CurrentBoard = ActivateBoard(boardType);
        CurrentBoard.RunLevel(puzzleInfo);
    }

    public void OnHintClicked()
    {
        if (HintIsActive)
        {
            HintIsActive = false;
        }
        else if (NumHintsLeft > 0)
        {
            HintIsActive = true;
        }
    }

    private Board ActivateBoard(BoardTypes type)
    {
        int indexToReturn = 0;
        for (int iBoardObj = 0; iBoardObj < BoardObjects.Count; iBoardObj++)
        {
            if (BoardObjects[iBoardObj].MyType == type)
            {
                indexToReturn = iBoardObj;
                BoardObjects[iBoardObj].gameObject.SetActive(true);
            }
            else
            {
                BoardObjects[iBoardObj].gameObject.SetActive(false);
            }
        }
        return BoardObjects[indexToReturn];
    }
}