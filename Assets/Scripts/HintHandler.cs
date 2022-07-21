using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class HintHandler : MonoBehaviour, IPointerClickHandler
{
    public bool HintIsActive { get; private set; }
    private int NumHintsLeft = 10;

    [SerializeField]
    private Sprite ActiveSprite, DefaultSprite;

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer SpriteRenderer
    {
        get
        {
            if (spriteRenderer == null)
                spriteRenderer = GetComponent<SpriteRenderer>();
            return spriteRenderer;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (HintIsActive)
        {
            HintIsActive = false;
            SpriteRenderer.DOFade(0, 0);
            SpriteRenderer.sprite = DefaultSprite;
            SpriteRenderer.DOFade(1, 0.25f);
        }
        else if (NumHintsLeft > 0)
        {
            HintIsActive = true;
            SpriteRenderer.DOFade(0, 0);
            SpriteRenderer.sprite = ActiveSprite;
            SpriteRenderer.DOFade(1, 0.25f);
        }
    }

    
}
