using DG.Tweening;
using UnityEngine;
using System.Collections;

public class MarkCellAsMananger : MonoBehaviour
{
    private const float DOTweenDuration = 0.25f;

    SpriteRenderer spriteRenderer;
    private SpriteRenderer SpriteRenderer
    {
        get
        {
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
            return spriteRenderer;
        }
        set
        {
            spriteRenderer = value;
        }
    }

    [SerializeField]
    Sprite Empty, Full;

    public CellModes Mark = CellModes.MarkedAsEmpty;

    public void Click()
    {
        ManagersSingleton.Managers.TouchDetector.TouchDetectionEnabled = false;
        StartCoroutine(UnlockTouchAfterClickWithDelay());

        if (SpriteRenderer.sprite == Empty)
        {
            Debug.Log("MarkCellAsMananger >> == Empty");
            Mark = CellModes.MarkedAsFull;
            SpriteRenderer.DOFade(0, 0);
            SpriteRenderer.sprite = Full;
            SpriteRenderer.DOFade(1, DOTweenDuration);
        }
        else
        {
            Debug.Log("MarkCellAsMananger >> == Full");
            Mark = CellModes.MarkedAsEmpty;
            SpriteRenderer.DOFade(0, 0);
            SpriteRenderer.sprite = Empty;
            SpriteRenderer.DOFade(1, DOTweenDuration);
        }
    }

    private IEnumerator UnlockTouchAfterClickWithDelay()
    {
        yield return new WaitForSeconds(DOTweenDuration);
        ManagersSingleton.Managers.TouchDetector.TouchDetectionEnabled = true;
    }
}
