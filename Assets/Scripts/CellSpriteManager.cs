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

    [SerializeField]
    private int myRowIndex;

    [SerializeField]
    private int myColIndex;
    public void Click()
    {
        if (MyMode != CellModes.NA)
            return;
        ManagersSingleton.Managers.PuzzlePageManager.CurrentBoard.OnNACellClicked(myRowIndex, myColIndex, ChangeMode);
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
}