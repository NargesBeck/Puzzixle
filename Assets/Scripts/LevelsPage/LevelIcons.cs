using UnityEngine;
using UnityEngine.UI;

public class LevelIcons : MonoBehaviour
{
    [SerializeField]
    private int LevelNumber;

    [SerializeField]
    private int iconRowIndex, iconColIndex;

    [SerializeField]
    private Sprite played, locked;

    //[SerializeField]
    //private SpriteRenderer[] numberSpriteRenderers;

    [SerializeField]
    private Text LevelIconText;

    //[SerializeField]
    private bool isSelectable;

    [SerializeField]
    private Image LevelStateImage;

    private int Value;

    //private void Awake()
    //{
    //    ManagersSingleton.Managers.BoardSelectionPageManager.OnReAssignLevelIcons += AssignMe;
    //}

    //private void AssignMe()
    //{
    //    LevelNumber = (iconRowIndex * 5 + iconColIndex) + ManagersSingleton.Managers.BoardSelectionPageManager.ScrolledViewLevelStartingFrom;
    //    int levelNumView = LevelNumber + 1;

    //    if (levelNumView <= ManagersSingleton.Managers.PuzzlePageManager.GetCountLevels(ManagersSingleton.Managers.BoardSelectionPageManager.CurrentBoardType))
    //    {
    //        gameObject.SetActive(true);
    //    }
    //    else
    //    {
    //        gameObject.SetActive(false);
    //        return;
    //    }

    //    LevelIconText.text = levelNumView.ToString();
    //    SetStateSprite();
    //}

    public void AssignMe(int value, bool isLevelOpen)
    {
        LevelNumber = value - 1;
        LevelIconText.text = value.ToString();
        SetStateSprite(isLevelOpen);
    }

    private void SetStateSprite(bool open)
    {
        isSelectable = open;
        if (open)
            LevelStateImage.sprite = played;
        else
            LevelStateImage.sprite = locked;
    }

    public void OnLevelIconClick()
    {
        if (isSelectable)
        {
            ManagersSingleton.Managers.BoardSelectionPageManager.ClickedLevelIcon(LevelNumber);
        }
    }
}
