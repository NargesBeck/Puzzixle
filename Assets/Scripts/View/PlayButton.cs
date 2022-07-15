using UnityEngine.EventSystems;
using UnityEngine;

public class PlayButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        ManagersSingleton.Managers.CameraMovement.GoHere(Pages.Puzzle);
    }
}
