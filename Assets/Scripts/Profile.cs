using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profile : MonoBehaviour
{
    public const string RECENT_BOARD_TYPE = "RecentBoard";

    private List<UserProgressForEachBoard> userProgressForEachBoardList = new List<UserProgressForEachBoard>();
    public List<UserProgressForEachBoard> UserProgressForEachBoardList
    {
        get
        {
            userProgressForEachBoardList.Clear();
            UserProgressForEachBoard progress5 = new UserProgressForEachBoard();
            progress5.BoardType = BoardTypes.Squ5;
            progress5.LastPuzzleIndexPlayed = PlayerPrefs.GetInt(BoardTypes.Squ5.ToString());
            userProgressForEachBoardList.Add(progress5);

            UserProgressForEachBoard progress10 = new UserProgressForEachBoard();
            progress10.BoardType = BoardTypes.Squ10;
            progress10.LastPuzzleIndexPlayed = PlayerPrefs.GetInt(BoardTypes.Squ10.ToString());
            userProgressForEachBoardList.Add(progress10);

            UserProgressForEachBoard progress15 = new UserProgressForEachBoard();
            progress15.BoardType = BoardTypes.Squ15;
            progress15.LastPuzzleIndexPlayed = PlayerPrefs.GetInt(BoardTypes.Squ15.ToString());
            userProgressForEachBoardList.Add(progress15);

            return userProgressForEachBoardList;
        }
    }

    public int GetLastPuzzlePlayedForThisBoard(BoardTypes boardType)
    {
        for (int iBoard = 0; iBoard < UserProgressForEachBoardList.Count; iBoard++)
        {
            if (UserProgressForEachBoardList[iBoard].BoardType == boardType)
                return UserProgressForEachBoardList[iBoard].LastPuzzleIndexPlayed;
        }
        return -1;
    }

    public void SavePlayedPuzzle(BoardTypes board, int lastPuzzle)
    {
        PlayerPrefs.SetInt(board.ToString(), lastPuzzle);
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.SetInt(BoardTypes.Squ5.ToString(), -1);
        PlayerPrefs.SetInt(BoardTypes.Squ10.ToString(), -1);
        PlayerPrefs.SetInt(BoardTypes.Squ15.ToString(), -1);
        PlayerPrefs.SetString(RECENT_BOARD_TYPE, BoardTypes.Squ5.ToString());
    }

    public void SaveRecentBoardType(BoardTypes type)
    {
        PlayerPrefs.SetString(RECENT_BOARD_TYPE, type.ToString());
    }

    public BoardTypes GetRecentBoardType()
    {
        string recentBoard = PlayerPrefs.GetString(RECENT_BOARD_TYPE, BoardTypes.Squ5.ToString());
        if (recentBoard == BoardTypes.Squ5.ToString())
            return BoardTypes.Squ5;
        if (recentBoard == BoardTypes.Squ10.ToString())
            return BoardTypes.Squ10;
        if (recentBoard == BoardTypes.Squ15.ToString())
            return BoardTypes.Squ15;

        throw new System.Exception("mismatch of somoe sort at GetRecentBoardType - " + recentBoard);
    }

    public bool IsLevelOpen(BoardTypes type, int levelIndex)
    {
        return GetLastPuzzlePlayedForThisBoard(type) + 1 >= levelIndex;
    }
}
public class UserProgressForEachBoard
{
    public BoardTypes BoardType;
    public int LastPuzzleIndexPlayed = -1;
}