using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollUpDown : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool GoUp;

    public void OnPointerClick(PointerEventData eventData)
    {
        ManagersSingleton.Managers.BoardSelectionPageManager.ClickedScroll(GoUp);
    }
}
