using System;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class Board : MonoBehaviour
{
    public BoardTypes MyType;
    public int MyIndexInDB;

    public Action OnPuzzleFinished;

    [SerializeField]
    private List<RowColViewHandler> RowsList = new List<RowColViewHandler>();

    [SerializeField]
    private List<RowColViewHandler> ColumnsList = new List<RowColViewHandler>();

    [SerializeField]
    private GameObject CellsParent, CellPrefab;

    private List<CellHandler> CellsList = new List<CellHandler>();

    public PuzzleInfo CurrentPuzzle;

    private int NumOfFullCellsInPuzzle, NumOfClickedFullCellsInPuzzle;

    public void RunLevel(PuzzleInfo puzzleToApply, int index)
    {
        CreateCells();

        MyIndexInDB = index;
        CurrentPuzzle = puzzleToApply;

        List<List<int>> calculatedRowsInfo = GetSequencesLengths(true);
        for (int i = 0; i < RowsList.Count; RowsList[i].AssignMe(calculatedRowsInfo[i]), i++);
        List<List<int>> calculatedColsInfo = GetSequencesLengths(false);
        for (int i = 0; i < ColumnsList.Count; ColumnsList[i].AssignMe(calculatedColsInfo[i]), i++);

        NumOfFullCellsInPuzzle = NumOfClickedFullCellsInPuzzle = 0;
        for (int row = 0; row < CurrentPuzzle.Map2D.GetLength(0); row++)
        {
            for (int col = 0; col < CurrentPuzzle.Map2D.GetLength(1); col++)
            {
                if (CurrentPuzzle.Map2D[row, col].CellMode == CellModes.MarkedAsFull)
                    NumOfFullCellsInPuzzle++;
            }
        }
    }

    private void CreateCells()
    {
        for (int i = 0; i < CellsList.Count; i++)
        {
            Destroy(CellsList[i].gameObject);
        }
        CellsList.Clear();

        int size = 0;
        switch(MyType)
        {
            case BoardTypes.Squ5: size = 5; break;
            case BoardTypes.Squ10: size = 10; break;
            case BoardTypes.Squ15: size = 15; break;
        }

        for (int r = 0; r < size; r++)
        {
            for (int c = 0; c < size; c++)
            {
                GameObject cellObj = Instantiate(CellPrefab, CellsParent.transform);
                cellObj.SetActive(true);
                CellHandler cell = cellObj.GetComponent<CellHandler>();
                cell.Setup(r, c);
                CellsList.Add(cell);
            }
        }
    }

    private List<List<int>> GetSequencesLengths(bool inRows)
    {
        int iUpperBound = (inRows) ? 0 : 1;
        int jUpperBound = (inRows) ? 1 : 0;

        List<List<int>> allRowsList = new List<List<int>>();
        for (int i = 0; i <= CurrentPuzzle.Map2D.GetUpperBound(iUpperBound); i++)
        {
            List<int> rowInfoIntList = new List<int>();
            int seqLength = 0;
            for (int j = 0; j <= CurrentPuzzle.Map2D.GetUpperBound(jUpperBound); j ++)
            {
                int row = inRows ? i : j;
                int col = inRows ? j : i;
                if (CurrentPuzzle.Map2D[row, col].CellMode == CellModes.MarkedAsFull)
                    seqLength++;
                if ((CurrentPuzzle.Map2D[row, col].CellMode == CellModes.MarkedAsEmpty && seqLength > 0)
                    || (j == CurrentPuzzle.Map2D.GetUpperBound(0) && seqLength > 0))
                {
                    rowInfoIntList.Add(seqLength);
                    seqLength = 0;
                }
            }
            allRowsList.Add(rowInfoIntList);
        }
        return allRowsList;
    }

    public void OnNACellClicked(int row, int col, Action<CellModes> callCellToChangeMode)
    {
        CellModes correctCellValue = CurrentPuzzle.Map2D[row, col].CellMode;

        if (ManagersSingleton.Managers.PuzzlePageManager.IsHintActive)
        {
            PlayerUsedHint();
        }
        else if (ManagersSingleton.Managers.PuzzlePageManager.MarkCellAsMananger.Mark != correctCellValue)
        {
            PlayerWasWrong();
        }
        else
        {
            PlayerWasRight();
        }

        callCellToChangeMode(correctCellValue);
        if (correctCellValue == CellModes.MarkedAsFull)
            NumOfClickedFullCellsInPuzzle++;
        if (IsPuzzleFinished())
            StartCoroutine(PlayerWon());
    }

    private void PlayerWasWrong()
    {
        if (ManagersSingleton.Managers.GameManager.OnLifeLoss != null)
            ManagersSingleton.Managers.GameManager.OnLifeLoss();

        if (ManagersSingleton.Managers.PuzzlePageManager.LivesHandler.ReduceALife())
        {
            StartCoroutine(PlayerLost());
        }

        ManagersSingleton.Managers.PuzzlePageManager.transform.DOShakePosition(0.4f, 10);
    }

    private void PlayerWasRight()
    {
    }

    private void PlayerUsedHint()
    {
        ManagersSingleton.Managers.PuzzlePageManager.HintHandler.ReduceAHint();
    }

    private IEnumerator PlayerLost()
    {
        ManagersSingleton.Managers.PageTurner.GoToPage(Pages.LevelFailed);

        ManagersSingleton.Managers.PuzzlePageManager.ShowLevelEndMessage(false);
        yield return new WaitForSeconds(0.2f);
        if (OnPuzzleFinished != null)
        {
            OnPuzzleFinished();
        }
        yield return new WaitForSeconds(1);
        ManagersSingleton.Managers.PageTurner.GoToPage(Pages.LevelFailed);
    }

    private IEnumerator PlayerWon()
    {
        ManagersSingleton.Managers.Profile.SavePlayedPuzzle(MyType, MyIndexInDB);
        ManagersSingleton.Managers.PuzzlePageManager.ShowLevelEndMessage(true);
        yield return new WaitForSeconds(0.1f);
        if (OnPuzzleFinished != null)
        {
            OnPuzzleFinished();
        }
        yield return new WaitForSeconds(0.1f);
        ManagersSingleton.Managers.PageTurner.GoToPage(Pages.LevelWon);
    }

    private bool IsPuzzleFinished()
    {
        return NumOfClickedFullCellsInPuzzle >= NumOfFullCellsInPuzzle;
    }
}