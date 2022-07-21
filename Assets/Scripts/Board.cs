using System.Collections.Generic;
using UnityEngine;
using System;

public class Board : MonoBehaviour
{
    public BoardTypes MyType;

    [SerializeField]
    List<RowColViewHandler> RowsList = new List<RowColViewHandler>();

    [SerializeField]
    List<RowColViewHandler> ColumnsList = new List<RowColViewHandler>();

    public PuzzleInfo CurrentPuzzle;

    public void RunLevel(PuzzleInfo puzzleToApply)
    {
        CurrentPuzzle = puzzleToApply;

        List<List<int>> calculatedRowsInfo = GetSequencesLengths(true);
        for (int i = 0; i < RowsList.Count; RowsList[i].AssignMe(calculatedRowsInfo[i]), i++);
        List<List<int>> calculatedColsInfo = GetSequencesLengths(false);
        for (int i = 0; i < ColumnsList.Count; ColumnsList[i].AssignMe(calculatedColsInfo[i]), i++);
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
        CellModes currectCellValue = CurrentPuzzle.Map2D[row, col].CellMode;

        if (ManagersSingleton.Managers.PuzzlePageManager.IsHintActive)
        {
            PlayerUsedHint();
        }
        else if (ManagersSingleton.Managers.PuzzlePageManager.MarkCellAsMananger.Mark != currectCellValue)
        {
            PlayerWasWrong();
        }
        else
        {
            PlayerWasRight();
        }

        callCellToChangeMode(currectCellValue);
    }

    private void PlayerWasWrong()
    {
        if (ManagersSingleton.Managers.GameManager.OnLifeLoss != null)
            ManagersSingleton.Managers.GameManager.OnLifeLoss();

        if (ManagersSingleton.Managers.PuzzlePageManager.LivesHandler.ReduceALife())
        {
            PlayerLost();
        }
    }

    private void PlayerWasRight()
    {
    }

    private void PlayerUsedHint()
    {

    }

    private void PlayerLost()
    {
        ManagersSingleton.Managers.CameraMovement.GoHere(Pages.LevelFailed);
    }
}