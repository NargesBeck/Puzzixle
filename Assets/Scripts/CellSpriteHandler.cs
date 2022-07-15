using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class CellSpriteHandler : MonoBehaviour, IPointerClickHandler
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

    private void ChangeMode(CellModes newMode)
    {
        MyMode = newMode;

        if (newMode == CellModes.MarkedAsEmpty)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(SpriteRenderer.DOFade(0, 0.1f));
            sequence.Append(SpriteRenderer.DOFade(1, 0.25f));

            StartCoroutine(ChangeSpriteWithDelay());
            sequence.Play();
        }
        else
        {
            SpriteRenderer.DOFade(0, 0.5f);
        }
    }

    private IEnumerator ChangeSpriteWithDelay()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        SpriteRenderer.sprite = ManagersSingleton.Managers.PuzzlePageManager.EmptyCellSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (MyMode != CellModes.NA)
            return;
        ManagersSingleton.Managers.PuzzlePageManager.CurrentBoard.OnNACellClicked(myRowIndex, myColIndex, ChangeMode);
    }
}