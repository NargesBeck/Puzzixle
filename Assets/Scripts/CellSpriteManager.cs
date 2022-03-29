using DG.Tweening;
using UnityEngine;
using System.Collections;

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
}