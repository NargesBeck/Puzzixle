using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class CellHandler : MonoBehaviour, IPointerClickHandler
{
    private Image image;
    private Image Image
    {
        get
        {
            if (image == null)
            {
                image = GetComponent<Image>();
            }
            return image;
        }
        set
        {
            image = value;
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

    //[SerializeField]
    private CellModes MyMode;

    //[SerializeField]
    private int myRowIndex;

    //[SerializeField]
    private int myColIndex;

    //private void Awake()
    //{
    //    ManagersSingleton.Managers.PuzzlePageManager.CurrentBoard.OnPuzzleFinished += RevealBlank;
    //}

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
        Animator.Play(toEmptyClip.name);
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