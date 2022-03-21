using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public void CallTheRelatedManagerForThisClick (GameObject clicked)
    {
        CellSpriteManager cellSpriteManager = clicked.GetComponent <CellSpriteManager>();
        cellSpriteManager?.Click();
        if (cellSpriteManager != null)
            return;

        MainMenu mainMenu = clicked.GetComponent <MainMenu>();
        mainMenu?.Click();
        if (mainMenu != null)
            return;

        MarkCellAsMananger markCell = clicked.GetComponent<MarkCellAsMananger>();
        markCell?.Click();
        if (markCell == null)
            return;

        HintHandler hint = clicked.GetComponent<HintHandler>();
        hint?.Click();
        if (hint == null)
            return;
    }
}
