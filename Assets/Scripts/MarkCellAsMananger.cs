using DG.Tweening;
using UnityEngine;

public class MarkCellAsMananger : MonoBehaviour
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

    [SerializeField]
    Sprite Empty, Full;

    public enum Mark
    {
        Empty, Full
    }
    public Mark mark = Mark.Empty;

    public void Click()
    {
        Debug.Log("here");
        if (SpriteRenderer.sprite.name == "MarkAsEmpty")
        {
            mark = Mark.Full;
            SpriteRenderer.DOFade(0, 0);
            SpriteRenderer.sprite = Full;
            SpriteRenderer.DOFade(1, 0.25f);
        }
        else
        {
            mark = Mark.Empty;
            SpriteRenderer.DOFade(0, 0);
            SpriteRenderer.sprite = Empty;
            SpriteRenderer.DOFade(1, 0.25f);
        }
    }
}
