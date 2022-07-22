using UnityEngine.EventSystems;
using UnityEngine;

public class ButtonPlayHandler : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        ManagersSingleton.Managers.CameraMovement.GoHere(Pages.Puzzle);
    }
}
