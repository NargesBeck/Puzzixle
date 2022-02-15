using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profile : MonoBehaviour
{
    public List<UserProgressForEachBoard> UserProgressForEachBoardList = new List<UserProgressForEachBoard>();

    public int GetLastPuzzlePlayedForThisBoard(BoardTypes boardType)
    {
        for (int iBoard = 0; iBoard < UserProgressForEachBoardList.Count; iBoard++)
        {
            if (UserProgressForEachBoardList[iBoard].BoardType == boardType)
                return UserProgressForEachBoardList[iBoard].LastPuzzleIndexPlayed;
        }
        return -1;
    }
}
public class UserProgressForEachBoard
{
    public BoardTypes BoardType;
    public int LastPuzzleIndexPlayed = -1;
}