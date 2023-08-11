using UnityEngine;
using System.Collections.Generic;

public class LevelEndPageManager : Page
{
    [SerializeField]
    private List<SpriteRenderer> NumberSpriteRenderers = new List<SpriteRenderer>();

    [SerializeField]
    private bool GameOver;

    public override void PreparePage()
    {
        BoardTypes boardType = ManagersSingleton.Managers.Profile.GetRecentBoardType();
        int levelNumber = ManagersSingleton.Managers.Profile.GetLastPuzzlePlayedForThisBoard(boardType) + 1;
        if (GameOver) levelNumber++;

        int levelNumView = levelNumber + 1;
        int n100 = levelNumView / 100;
        int n10 = (levelNumView - n100 * 100) / 10;
        int n1 = levelNumView - n100 * 100 - n10 * 10;

        if (n100 > 0)
            NumberSpriteRenderers[2].sprite = ManagersSingleton.Managers.NumberSpritesPrinter.Print(n100, NumberSpritesPrinter.Colors.Blue);
        else
            NumberSpriteRenderers[2].sprite = ManagersSingleton.Managers.NumberSpritesPrinter.Print(0, NumberSpritesPrinter.Colors.Blue); ;

        if (n10 > 0 || n100 > 0)
            NumberSpriteRenderers[1].sprite = ManagersSingleton.Managers.NumberSpritesPrinter.Print(n10, NumberSpritesPrinter.Colors.Blue);
        else
            ManagersSingleton.Managers.NumberSpritesPrinter.Print(0, NumberSpritesPrinter.Colors.Blue);

        NumberSpriteRenderers[0].sprite = ManagersSingleton.Managers.NumberSpritesPrinter.Print(n1, NumberSpritesPrinter.Colors.Blue);
    }

    public void NextLevelClick()
    {
        ManagersSingleton.Managers.CameraMovement.GoHere(Pages.Puzzle);
    }

    public void LevelsClick()
    {
        ManagersSingleton.Managers.CameraMovement.GoHere(Pages.BoardSelection);
    }
}
