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
    public List<PuzzleInfo> PuzzlesList = new List<PuzzleInfo>();
}

[Serializable]
public class PuzzleInfo
{
    public int Size = 10;
    public bool[,] Map;
    public string LevelName;
    public Texture2D Result;
}