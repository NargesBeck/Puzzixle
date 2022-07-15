using UnityEngine.EventSystems;
using UnityEngine;

public class PlayButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("IPOINTERCLICK");

        ManagersSingleton.Managers.CameraMovement.GoHere(Pages.Puzzle);
    }
}
