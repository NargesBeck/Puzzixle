using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlesDBManager : MonoBehaviour
{
    [SerializeField]
    private PuzzlesPool puzzlesPool;

    private PuzzlesPool PuzzlesPool
    {
        get
        {
            if (puzzlesPool == null)
            {
                PuzzlesScriptableObject puzzlesScriptableObject = Resources.Load<PuzzlesScriptableObject>("Puzzles");
                puzzlesPool = puzzlesScriptableObject?.PuzzlesPool;
            }
            return puzzlesPool;
        }
    }

    //private PuzzleInfo FetchNewPuzzle(BoardTypes boardType)
    //{
    //    int puzzleIndex = ManagersSingleton.Managers.Profile.GetLastPuzzlePlayedForThisBoard(boardType) + 1;

    //    if (puzzleIndex < )
    }
}
