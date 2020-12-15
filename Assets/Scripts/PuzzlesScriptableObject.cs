using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Puzzles", menuName = "Puzzles", order = 0)]
public class PuzzlesScriptableObject : ScriptableObject
{
    public PuzzlesPool PuzzlesPool = new PuzzlesPool();
}

[Serializable]
public class PuzzlesPool
{
    public List<PuzzleInfo5> SmallPuzzles = new List<PuzzleInfo5>();
    public List<PuzzleInfo10> MediumPuzzles = new List<PuzzleInfo10>();
    public List<PuzzleInfo15> BigPuzzles = new List<PuzzleInfo15>();
}

[Serializable]
public class PuzzleInfo5
{
    public bool[,] Map = new bool[5, 5];
    public Texture2D Result;
}

[Serializable]
public class PuzzleInfo10
{
    public bool[,] Map = new bool[10, 10];
    public Texture2D Result;
}

public class PuzzleInfo15
{
    public bool[,] Map = new bool[15, 15];
    public Texture2D Result;
}