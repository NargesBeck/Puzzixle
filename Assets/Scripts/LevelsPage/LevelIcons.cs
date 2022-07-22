using UnityEngine;
using UnityEngine.EventSystems;

public class LevelIcons : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private int levelNumber;

    [SerializeField]
    private int iconRowIndex, iconColIndex;

    [SerializeField]
    private Sprite played, toPlay, locked;

    [SerializeField]
    private SpriteRenderer[] numberSpriteRenderers;

    [SerializeField]
    private bool isSelectable;

    private void Awake()
    {
        ManagersSingleton.Managers.BoardSelectionPageManager.OnBoardSelectionPrepare += AssignMe;
        // temp:
        levelNumber = iconRowIndex * 5 + iconColIndex + 1;
    }

    private void AssignMe()
    {
        levelNumber = iconRowIndex * 5 + iconColIndex + 1;
        int n100 = levelNumber / 100;
        int n10 = (levelNumber - n100 * 100) / 10;
        int n1 = levelNumber - n100 * 100 - n10 * 10;

        if (n100 > 0)
            numberSpriteRenderers[2].sprite = ManagersSingleton.Managers.NumberSpritesPrinter.Print(n100, NumberSpritesPrinter.Colors.Black);
        else
            numberSpriteRenderers[2].sprite = null;

        if (n10 > 0)
            numberSpriteRenderers[1].sprite = ManagersSingleton.Managers.NumberSpritesPrinter.Print(n10, NumberSpritesPrinter.Colors.Black);
        else
            numberSpriteRenderers[1].sprite = null;

        numberSpriteRenderers[0].sprite = ManagersSingleton.Managers.NumberSpritesPrinter.Print(n1, NumberSpritesPrinter.Colors.Black);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isSelectable)
        {
            // blah blah
        }
    }
}
