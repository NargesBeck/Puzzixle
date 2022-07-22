using System;
using UnityEngine;

public class BoardSelectionPageManager : Page
{
    public Action OnBoardSelectionPrepare;

    public BoardTypes CurrentBoardType { get; private set; }

    public BoardTypes PrevBoardType { get; private set; }

    public override void DisplayPage()
    {
        
    }

    public override void PreparePage()
    {
        OnBoardSelectionPrepare?.Invoke();
    }
}
