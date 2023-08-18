using UnityEngine;
using UnityEngine.EventSystems;

public class LevelIcons : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private int LevelNumber;

    [SerializeField]
    private int iconRowIndex, iconColIndex;

    [SerializeField]
    private Sprite played, toPlay, locked;

    [SerializeField]
    private SpriteRenderer[] numberSpriteRenderers;

    [SerializeField]
    private bool isSelectable;

    [SerializeField]
    private SpriteRenderer LevelStateSpriteRenderer;

    private void Awake()
    {
        ManagersSingleton.Managers.BoardSelectionPageManager.OnReAssignLevelIcons += AssignMe;
    }

    private void AssignMe()
    {
        LevelNumber = (iconRowIndex * 5 + iconColIndex) + ManagersSingleton.Managers.BoardSelectionPageManager.ScrolledViewLevelStartingFrom;
        int levelNumView = LevelNumber + 1;

        if (levelNumView <= ManagersSingleton.Managers.GameManager.MaxLevelNumber)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
            return;
        }

        int n100 = levelNumView / 100;
        int n10 = (levelNumView - n100 * 100) / 10;
        int n1 = levelNumView - n100 * 100 - n10 * 10;

        if (n100 > 0)
            numberSpriteRenderers[2].sprite = ManagersSingleton.Managers.NumberSpritesPrinter.Print(n100, NumberSpritesPrinter.Colors.Black);
        else
            numberSpriteRenderers[2].sprite = null;

        if (n10 > 0 || n100 > 0)
            numberSpriteRenderers[1].sprite = ManagersSingleton.Managers.NumberSpritesPrinter.Print(n10, NumberSpritesPrinter.Colors.Black);
        else
            numberSpriteRenderers[1].sprite = null;

        numberSpriteRenderers[0].sprite = ManagersSingleton.Managers.NumberSpritesPrinter.Print(n1, NumberSpritesPrinter.Colors.Black);

        SetStateSprite();
    }

    private void SetStateSprite()
    {
        bool open = ManagersSingleton.Managers.Profile.IsLevelOpen(ManagersSingleton.Managers.BoardSelectionPageManager.CurrentBoardType, LevelNumber, out bool shine);

        if (shine)
            LevelStateSpriteRenderer.sprite = toPlay;
        else if (open)
            LevelStateSpriteRenderer.sprite = played;
        else
            LevelStateSpriteRenderer.sprite = locked;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isSelectable)
        {
            ManagersSingleton.Managers.BoardSelectionPageManager.ClickedLevelIcon(LevelNumber);
        }
    }
}
