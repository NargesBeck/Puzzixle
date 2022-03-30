using System.Collections.Generic;
using UnityEngine;
using System;

public class Board : MonoBehaviour
{
    public BoardTypes MyType;

    [SerializeField]
    List<TextMesh> RowsInfo = new List<TextMesh>();

    public PuzzleInfo CurrentPuzzle;

    public void RunLevel(PuzzleInfo puzzleToApply)
    {
        CurrentPuzzle = puzzleToApply;
        List<string> calculatedRowsInfo = SequencesInRowsToString();
        for (int i = 0; i < RowsInfo.Count; i++)
        {
            RowsInfo[i].text = calculatedRowsInfo[i];
        }
    }

    private List<string> SequencesInRowsToString()
    {
        List<string> output = new List<string>();
        for (int row = 0; row <= CurrentPuzzle.Map2D.GetUpperBound(0); row++)
        {
            string rowInfo = "";
            int seqLength = 0;
            for (int col = 0; col <= CurrentPuzzle.Map2D.GetUpperBound(1); col ++)
            {
                if (CurrentPuzzle.Map2D[row, col].CellMode == CellModes.MarkedAsFull)
                    seqLength++;
                if ((CurrentPuzzle.Map2D[row, col].CellMode == CellModes.MarkedAsEmpty && seqLength > 0)
                    || (col == CurrentPuzzle.Map2D.GetUpperBound(0) && seqLength > 0))
                {
                    if (rowInfo != "")
                        rowInfo += " ";
                    rowInfo += seqLength;
                    seqLength = 0;
                }
            }
            if (rowInfo == "")
                rowInfo = "0";
            output.Add(rowInfo);
        }
        return output;
    }

    public void OnNACellClicked(int row, int col, Action<CellModes> callCellToChangeMode)
    {
        CellModes currectCellValue = CurrentPuzzle.Map2D[row, col].CellMode;

        if (ManagersSingleton.Managers.PuzzlePageManager.HintIsActive)
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