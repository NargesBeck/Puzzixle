using UnityEngine;
using UnityEngine.EventSystems;

public class PrevNextType : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool GoForward;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GoForward)
            ManagersSingleton.Managers.BoardSelectionPageManager.ToggleForward();
        else
            ManagersSingleton.Managers.BoardSelectionPageManager.ToggleBackward();
    }
}
