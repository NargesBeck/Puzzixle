using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CellHandler : MonoBehaviour, IPointerClickHandler
{
    private SpriteRenderer spriteRenderer;
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

    private Animator animator;
    private Animator Animator
    {
        get
        {
            if (animator == null)
                animator = GetComponent<Animator>();
            return animator;
        }
    }

    [SerializeField]
    private AnimationClip toEmptyClip;
    [SerializeField]
    private AnimationClip toFullClip;

    [SerializeField]
    private Sprite emptySprite;
    [SerializeField]
    private Sprite fullSprite;

    [SerializeField]
    private CellModes MyMode;

    [SerializeField]
    private int myRowIndex;

    [SerializeField]
    private int myColIndex;

    private void Awake()
    {
        ManagersSingleton.Managers.PuzzlePageManager.CurrentBoard.OnPuzzleFinished += RevealBlank;
    }

    private void ChangeMode(CellModes newMode)
    {
        MyMode = newMode;

        if (newMode == CellModes.MarkedAsEmpty)
        {
            Animator.Play(toEmptyClip.name);
        }
        else
        {
            Animator.Play(toFullClip.name);
        }
        StartCoroutine(ChangeSpriteOnChangeModeAnimationDone(0.5f));
    }

    private IEnumerator ChangeSpriteOnChangeModeAnimationDone(float duration)
    {
        yield return new WaitForSeconds(duration);
        Animator.enabled = false;
        if (MyMode == CellModes.MarkedAsEmpty)
        {
            SpriteRenderer.sprite = emptySprite;
        }
        else
        {
            SpriteRenderer.sprite = fullSprite;
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
        Animator.Play(toEmptyClip.name);
        StartCoroutine(ChangeSpriteOnChangeModeAnimationDone(0.5f));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnLevelIconClick();
    }
}