using UnityEngine.EventSystems;
using UnityEngine;

public class ButtonLevelsHandler : MonoBehaviour, IPointerClickHandler 
{
    public void OnPointerClick (PointerEventData eventData)
    {
        ManagersSingleton.Managers.CameraMovement.GoHere(Pages.BoardSelection);
    }
}
