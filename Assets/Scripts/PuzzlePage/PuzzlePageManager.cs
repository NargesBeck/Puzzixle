﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePageManager : Page
{
    public MarkCellAsMananger MarkCellAsMananger;
    public LivesHandler LivesHandler;
    public Sprite EmptyCellSprite;

    [SerializeField]
    private HintHandler HintHandler;
    [SerializeField]
    private List<Board> BoardObjects = new List<Board>();
    
    public Board CurrentBoard;

    public bool IsHintActive
    {
        get
        {
            if (HintHandler == null)
                return false;
            return HintHandler.HintIsActive;
        }
    }

    private PuzzlesScriptableObject db;
    private PuzzlesScriptableObject DB
    {
        get
        {
            if (db == null)
                db = Resources.Load<PuzzlesScriptableObject>("Puzzles");
            return db;
        }
    }

    private bool IsNextSet = false;
    private int NextPuzzleIndex;
    private BoardTypes NextBoardType;

    public void SetThisLevelNext(BoardTypes boardType, int index)
    {
        IsNextSet = true;
        NextPuzzleIndex = index;
        NextBoardType = boardType;
    }

    public override void DisplayPage()
    {
        //StartLevel(DB.PuzzlesPool[0].BoardType, DB.PuzzlesPool[0].PuzzlesList[0]);
        if (IsNextSet)
        {
            var puzzle = DB.PuzzlesPool.Find(x => x.BoardType == NextBoardType).PuzzlesList[NextPuzzleIndex];
            StartLevel(NextBoardType, puzzle, NextPuzzleIndex);
        }
        else
        {
            BoardTypes boardType = ManagersSingleton.Managers.Profile.GetRecentBoardType();
            int puzzleIndex = ManagersSingleton.Managers.Profile.GetLastPuzzlePlayedForThisBoard(boardType);
            var puzzle = DB.PuzzlesPool.Find(x => x.BoardType == boardType).PuzzlesList[puzzleIndex];
            StartLevel(boardType, puzzle, puzzleIndex);
        }
        IsNextSet = false;
    }

    public void StartLevel(BoardTypes boardType, PuzzleInfo puzzleInfo, int index)
    {
        CurrentBoard = ActivateBoard(boardType);
        PrepareLevel2DArray(boardType, ref puzzleInfo);
        CurrentBoard.RunLevel(puzzleInfo, index);
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

    private void PrepareLevel2DArray(BoardTypes boardType, ref PuzzleInfo puzzleInfo)
    {
        int boardSize = GetBoardSizeFromBoardEnum(boardType);

        puzzleInfo.Map2D = new Cell[boardSize, boardSize];

        int index = 0;
        for (int row = 0; row < boardSize; row++)
        {
            for (int col = 0; col < boardSize; col++)
            {
                puzzleInfo.Map2D[row, col] = puzzleInfo.Map1D[index];
                index++;
            }
        }
    }

    public static int GetBoardSizeFromBoardEnum(BoardTypes board)
    {
        switch (board)
        {
            case BoardTypes.Squ5:
                return 5;
            case BoardTypes.Squ10:
                return 10;
            case BoardTypes.Squ15:
                return 15;
            default:
                return 10;
        }
    }
}