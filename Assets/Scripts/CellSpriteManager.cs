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

    enum Modes
    {
        Default, Full, Empty 
    }
    Modes mode = Modes.Default;

    public void Click()
    {
        if (mode != Modes.Default)
            return;
        if (ManagersSingleton.Managers.PuzzlePageManager.MarkCellAsMananger.mark == MarkCellAsMananger.Mark.Empty)
        {
            SpriteRenderer.sprite = ManagersSingleton.Managers.PuzzlePageManager.EmptyCellSprite;
            mode = Modes.Empty;
        }
        else
        {
            SpriteRenderer.DOFade(0, 0.5f);
            mode = Modes.Full;
        }
    }

}
