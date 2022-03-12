using DG.Tweening;
using UnityEngine;

public class CellSpriteManager : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    private SpriteRenderer SpriteRenderer
    {
        get
        {
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
            return spriteRenderer;
        }
        set
        {
            spriteRenderer = value;
        }
    }

    private CellModes MyMode;

    private int myRowIndex = -1;
    private int MyRowIndex
    {
        get
        {
            if (myRowIndex < 0)
            {
                AssignMyRowAndColumnIndex();
            }
            return myRowIndex;
        }
    }

    private int myColIndex;
    private int MyColIndex
    {
        get
        {
            if (myColIndex < 0)
            {
                AssignMyRowAndColumnIndex();
            }
            return myColIndex;
        }
    }

    public void Click()
    {
        if (MyMode != CellModes.NA)
            return;
        ManagersSingleton.Managers.PuzzlePageManager.CurrentBoard.OnNACellClicked(MyRowIndex, MyColIndex, ChangeMode);
    }

    private void ChangeMode(CellModes newMode)
    {
        MyMode = newMode;

        if (newMode == CellModes.MarkedAsEmpty)
        {
            SpriteRenderer.sprite = ManagersSingleton.Managers.PuzzlePageManager.EmptyCellSprite;
            ChangeMode(CellModes.MarkedAsEmpty);
        }
        else
        {
            SpriteRenderer.DOFade(0, 0.5f);
            ChangeMode(CellModes.MarkedAsFull);
        }
    }

    private void AssignMyRowAndColumnIndex()
    {
        if (ManagersSingleton.Managers.BoardSelectionPageManager.CurrentBoardType == BoardTypes.Squ5)
        {
            int myObjectName = int.Parse(name);
            myRowIndex = myObjectName / 5;
            myColIndex = myObjectName - (5 * myRowIndex);
        }
    }
}