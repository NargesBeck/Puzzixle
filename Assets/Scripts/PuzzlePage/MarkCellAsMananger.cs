using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

public class MarkCellAsMananger : MonoBehaviour
{
    private const float DOTweenDuration = 0.25f;

    [SerializeField] private Image ToggleImage;
    [SerializeField] private RectTransform EmptyImageTransform;
    [SerializeField] private RectTransform FullImageTransform;

    [SerializeField] private Sprite Empty, Full;

    public CellModes Mark = CellModes.MarkedAsFull;
    private bool TouchDetectionEnabled = true;

    private IEnumerator UnlockTouchAfterClickWithDelay()
    {
        yield return new WaitForSeconds(DOTweenDuration);
        TouchDetectionEnabled = true;
    }

    public void ResetToDefault()
    {
        Mark = CellModes.MarkedAsFull;
        ToggleImage.sprite = Full;
        EmptyImageTransform.localScale = Vector3.one;
        FullImageTransform.localScale = new Vector3(2, 2, 2);
    }

    public void OnToggleClick()
    {
        Debug.Log("MarkAS");

        if (!TouchDetectionEnabled)
            return;

        TouchDetectionEnabled = false;
        StartCoroutine(UnlockTouchAfterClickWithDelay());

        if (ToggleImage.sprite == Empty)
        {
            Debug.Log("MarkCellAsMananger >> == Empty");
            Mark = CellModes.MarkedAsFull;
            ToggleImage.sprite = Full;

            EmptyImageTransform.DOScale(Vector3.one, 0.2f);
            FullImageTransform.DOScale(new Vector3(2, 2, 2), 0.4f);
        }
        else
        {
            Debug.Log("MarkCellAsMananger >> == Full");
            Mark = CellModes.MarkedAsEmpty;
            ToggleImage.sprite = Empty;

            FullImageTransform.DOScale(Vector3.one, 0.2f);
            EmptyImageTransform.DOScale(new Vector3(2, 2, 2), 0.4f);
        }
    }
}
