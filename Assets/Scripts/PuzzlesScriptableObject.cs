﻿using System;
using UnityEngine;
using System.Collections.Generic;

public enum CellModes
{
    NA, MarkedAsEmpty, MarkedAsFull
}

public enum BoardTypes
{
    NA, Squ5, Squ10, Squ15
}

[CreateAssetMenu(fileName = "Puzzles", menuName = "Puzzles", order = 0)]
public class PuzzlesScriptableObject : ScriptableObject
{
    public List<PuzzlesPoolForOneBoard> PuzzlesPool = new List<PuzzlesPoolForOneBoard>();
}

[Serializable]
public class PuzzlesPoolForOneBoard
{
    public BoardTypes BoardType = BoardTypes.Squ10;
    public List<PuzzleInfo> PuzzlesList = new List<PuzzleInfo>();
}

[Serializable]
public class PuzzleInfo
{
    public Cell[] Map1D;
    public Cell[,] Map2D;
    public string LevelName;
}

[Serializable]
public class Cell
{
    public Cell()
    {
        CellMode = CellModes.NA;
    }
    public CellModes CellMode;
}