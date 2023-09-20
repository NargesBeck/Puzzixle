using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Action OnLifeLoss;
    public Action OnAppOpen;

    private enum Scenes { Main = 0, Tutorials = 1}

    private void Start()
    {
        //ManagersSingleton.Managers.Profile.ResetPlayerProgress();
        if (CheckTutorials())
        {
            SceneManager.LoadScene(1);
            return;
        }
        StartCoroutine(DoCreateLevelIcons());
        ManagersSingleton.Managers.PageTurner.GoToPage(Pages.MainMenu);
    }

    private IEnumerator DoCreateLevelIcons()
    {
        int count5x5 = ManagersSingleton.Managers.PuzzlePageManager.GetCountLevels(BoardTypes.Squ5);
        int count10x10 = ManagersSingleton.Managers.PuzzlePageManager.GetCountLevels(BoardTypes.Squ10);
        int count15x15 = ManagersSingleton.Managers.PuzzlePageManager.GetCountLevels(BoardTypes.Squ15);
        int countMax = Mathf.Max(count5x5, count10x10, count15x15);

        ManagersSingleton.Managers.BoardSelectionPageManager.LevelIconsArr = new GameObject[countMax];
        for (int i = 0; i < countMax; i++)
        {
            ManagersSingleton.Managers.BoardSelectionPageManager.LevelIconsArr[i] = Instantiate(ManagersSingleton.Managers.BoardSelectionPageManager.LevelIconPrefab, 
                ManagersSingleton.Managers.BoardSelectionPageManager.LevelIconsParent.transform);

            if (i % 100 == 0)
                yield return null;
        }
    }

    // return true if should show tutorials
    private bool CheckTutorials()
    {
        return !ManagersSingleton.Managers.Profile.UserProfileData.CompletedTutorial;
    }
}
