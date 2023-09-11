using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePageManager : Page
{
    public MarkCellAsMananger MarkCellAsMananger;
    public LivesHandler LivesHandler;
    public HintHandler HintHandler;

    [SerializeField]
    private GameObject CongratsMessage, OhNoMessage;
    [SerializeField]
    private List<GameObject> BoardObjects = new List<GameObject>();
    
    private GameObject BoardObject;
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

    public override void PreparePage()
    {
        if (IsNextSet)
        {
            var puzzle = DB.PuzzlesPool.Find(x => x.BoardType == NextBoardType).PuzzlesList[NextPuzzleIndex];
            StartLevel(NextBoardType, puzzle, NextPuzzleIndex);
        }
        else
        {
            BoardTypes boardType = ManagersSingleton.Managers.Profile.GetRecentBoardType();
            int puzzleIndex = ManagersSingleton.Managers.Profile.GetLastPuzzlePlayedForThisBoard(boardType) + 1;
            var puzzle = DB.PuzzlesPool.Find(x => x.BoardType == boardType).PuzzlesList[puzzleIndex];
            StartLevel(boardType, puzzle, puzzleIndex);
        }
        IsNextSet = false;

        OhNoMessage.SetActive(false);
        CongratsMessage.SetActive(false);
    }

    public override void DisplayPage()
    {    
    }

    public void StartLevel(BoardTypes boardType, PuzzleInfo puzzleInfo, int index)
    {
        ManagersSingleton.Managers.Profile.SaveRecentBoardType(boardType);
        CurrentBoard = ActivateBoard(boardType);
        PrepareLevel2DArray(boardType, ref puzzleInfo);
        LivesHandler.ResetToFull();
        HintHandler.SetHintCount(10);
        CurrentBoard.RunLevel(puzzleInfo, index);
    }

    private Board ActivateBoard(BoardTypes type)
    {
        Destroy(BoardObject);
        for (int iBoardObj = 0; iBoardObj < BoardObjects.Count; iBoardObj++)
        {
            if (BoardObjects[iBoardObj].GetComponent<Board>().MyType == type)
            {
                BoardObject = Instantiate(BoardObjects[iBoardObj], this.transform);
                BoardObject.SetActive(true);
                return BoardObject.GetComponent<Board>();
            }
        }
        return null;
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

    public int GetCountLevels(BoardTypes boardType)
    {
        return DB.PuzzlesPool.Find(x => x.BoardType == boardType).PuzzlesList.Count;
    }

    public void ShowLevelEndMessage(bool won)
    {
        CongratsMessage.SetActive(won);
        OhNoMessage.SetActive(!won);
    }

    public void ResetButtonClick()
    {
        PreparePage();
    }

    public void BackButtonClick()
    {
        ManagersSingleton.Managers.PageTurner.GoToPage(Pages.MainMenu);
    }
}