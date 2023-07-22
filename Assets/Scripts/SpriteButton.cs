using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SpriteButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] 
    private UnityEvent OnClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke();
    }
}
