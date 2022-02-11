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

    CellModes MyMode;

    public void Click()
    {
        if (MyMode != CellModes.NA)
            return;
        if (ManagersSingleton.Managers.PuzzlePageManager.MarkCellAsMananger.Mark == CellModes.MarkedAsEmpty)
        {
            SpriteRenderer.sprite = ManagersSingleton.Managers.PuzzlePageManager.EmptyCellSprite;
            MyMode = CellModes.MarkedAsEmpty;
        }
        else
        {
            SpriteRenderer.DOFade(0, 0.5f);
            MyMode = CellModes.MarkedAsFull;
        }
    }

}
