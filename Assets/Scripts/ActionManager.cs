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
        if (cellSpriteManager != null)
            return;

        MarkCellAsMananger markCell = clicked.GetComponent<MarkCellAsMananger>();
        markCell?.Click();
    }
}
