using System;
using UnityEngine;
using System.Collections.Generic;

public class Profile : MonoBehaviour
{
    public const string PROFILE_JSON_PLAYERREF_KEY = "PROFILE_JSON_PLAYERREF_KEY";

    private UserProfileModel userProfileData;
    public UserProfileModel UserProfileData
    {
        get
        {
            if (userProfileData == null)
                LoadProfile();
            return userProfileData;
        }
        set => userProfileData = value;
    }

    private void SaveProfile()
    {
        string json = JsonUtility.ToJson(userProfileData);
        PlayerPrefs.SetString(PROFILE_JSON_PLAYERREF_KEY, json);
    }

    private void LoadProfile()
    {
        string json = PlayerPrefs.GetString(PROFILE_JSON_PLAYERREF_KEY, null);
        if (string.IsNullOrEmpty(json))
        {
            // presets
            userProfileData = new UserProfileModel();

            userProfileData.RecentBoardType = BoardTypes.Squ5;
            userProfileData.ProgressForEachBoards.Clear();
            
            UserProgressForEachBoard progress5 = new UserProgressForEachBoard();
            progress5.BoardType = BoardTypes.Squ5;
            progress5.LastPuzzleIndexPlayed = -1;
            userProfileData.ProgressForEachBoards.Add(progress5);

            UserProgressForEachBoard progress10 = new UserProgressForEachBoard();
            progress10.BoardType = BoardTypes.Squ10;
            progress10.LastPuzzleIndexPlayed = -1;
            userProfileData.ProgressForEachBoards.Add(progress10);

            UserProgressForEachBoard progress15 = new UserProgressForEachBoard();
            progress15.BoardType = BoardTypes.Squ15;
            progress15.LastPuzzleIndexPlayed = -1;
            userProfileData.ProgressForEachBoards.Add(progress15);

            SaveProfile();
        }
        else
        {
            userProfileData = JsonUtility.FromJson<UserProfileModel>(json);
        }
    }

    public int GetLastPuzzlePlayedForThisBoard(BoardTypes boardType)
    {
        for (int iBoard = 0; iBoard < UserProfileData.ProgressForEachBoards.Count; iBoard++)
        {
            if (UserProfileData.ProgressForEachBoards[iBoard].BoardType == boardType)
                return UserProfileData.ProgressForEachBoards[iBoard].LastPuzzleIndexPlayed;
        }
        return -1;
    }

    public void SavePlayedPuzzle(BoardTypes board, int lastPuzzle)
    {
        UserProfileData.ProgressForEachBoards.Find(x => x.BoardType == board).LastPuzzleIndexPlayed = lastPuzzle;
        SaveProfile();
    }

    public void ResetPlayerProgress()
    {
        userProfileData = null;
        PlayerPrefs.SetString(PROFILE_JSON_PLAYERREF_KEY, null);
    }

    public void SaveRecentBoardType(BoardTypes type)
    {
        UserProfileData.RecentBoardType = type;
        SaveProfile();
    }

    public BoardTypes GetRecentBoardType()
    {
         return UserProfileData.RecentBoardType;
    }

    public bool IsLevelOpen(BoardTypes type, int levelIndex, out bool isTheLastOpenLevel)
    {
        int level = GetLastPuzzlePlayedForThisBoard(type);
        isTheLastOpenLevel = level + 1 == levelIndex; 
        return level + 1 >= levelIndex;
    }
}

[Serializable]
public class UserProgressForEachBoard
{
    public BoardTypes BoardType;
    public int LastPuzzleIndexPlayed = -1;
}

[Serializable]
public class UserProfileModel
{
    public BoardTypes RecentBoardType;
    public List<UserProgressForEachBoard> ProgressForEachBoards = new List<UserProgressForEachBoard>();
}