using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndPageManager : Page
{
    public void NextLevelClick()
    {
        ManagersSingleton.Managers.CameraMovement.GoHere(Pages.Puzzle);
    }

    public void LevelsClick()
    {
        ManagersSingleton.Managers.CameraMovement.GoHere(Pages.BoardSelection);
    }
}
