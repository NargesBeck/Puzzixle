using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class CellHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image Image;
    
    [SerializeField]
    private UIAnimator animator;

    [SerializeField]
    private Sprite emptySprite;
    [SerializeField]
    private Sprite fullSprite;

    private CellModes MyMode;

    private int myRowIndex;

    private int myColIndex;

    private void ChangeMode(CellModes newMode)
    {
        MyMode = newMode;
        float duration = 0;
        if (newMode == CellModes.MarkedAsEmpty)
        {
            duration = animator.PlayAnimation("empty");
        }
        else
        {
            duration = animator.PlayAnimation("full");
        }
        StartCoroutine(ChangeSpriteOnChangeModeAnimationDone(duration));
    }

    private IEnumerator ChangeSpriteOnChangeModeAnimationDone(float duration)
    {
        yield return new WaitForSeconds(duration);
        if (MyMode == CellModes.MarkedAsEmpty)
        {
            Image.sprite = emptySprite;
        }
        else
        {
            Image.sprite = fullSprite;
        }
    }

    public void OnLevelIconClick()
    {
        if (MyMode != CellModes.NA)
            return;
        ManagersSingleton.Managers.PuzzlePageManager.CurrentBoard.OnNACellClicked(myRowIndex, myColIndex, ChangeMode);
    }

    private void RevealBlank()
    {
        if (MyMode != CellModes.NA)
            return;
        MyMode = CellModes.MarkedAsEmpty;
        animator.PlayAnimation("empty");
        StartCoroutine(ChangeSpriteOnChangeModeAnimationDone(0.5f));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnLevelIconClick();
    }

    public void Setup(int rowIndex, int colIndex)
    {
        ManagersSingleton.Managers.PuzzlePageManager.CurrentBoard.OnPuzzleFinished += RevealBlank;
        myRowIndex = rowIndex;
        myColIndex = colIndex;
    }
}