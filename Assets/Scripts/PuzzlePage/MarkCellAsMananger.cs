using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MarkCellAsMananger : MonoBehaviour
{
    private const float DOTweenDuration = 0.25f;

    [SerializeField]
    private Image ToggleImage;

    [SerializeField]
    private Sprite Empty, Full;

    public CellModes Mark = CellModes.MarkedAsEmpty;
    private bool TouchDetectionEnabled = true;

    private IEnumerator UnlockTouchAfterClickWithDelay()
    {
        yield return new WaitForSeconds(DOTweenDuration);
        TouchDetectionEnabled = true;
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
            //SpriteRenderer.DOFade(0, 0);
            //SpriteRenderer.sprite = Full;
            //SpriteRenderer.DOFade(1, DOTweenDuration);
        }
        else
        {
            Debug.Log("MarkCellAsMananger >> == Full");
            Mark = CellModes.MarkedAsEmpty;
            ToggleImage.sprite = Empty;
            //SpriteRenderer.DOFade(0, 0);
            //SpriteRenderer.sprite = Empty;
            //SpriteRenderer.DOFade(1, DOTweenDuration);
        }
    }
}
