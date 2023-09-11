using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class HintHandler : MonoBehaviour
{
    public bool HintIsActive { get; private set; }
    private int NumHintsLeft = 10;

    [SerializeField]
    private Sprite ActiveSprite, DefaultSprite;

    [SerializeField]
    private Text HintLeftText;

    private Image image;
    private Image Image
    {
        get
        {
            if (image == null)
                image = GetComponent<Image>();
            return image;
        }
    }

    public void OnHintClick()
    {
        if (HintIsActive)
        {
            HintIsActive = false;
            Image.DOFade(0, 0);
            Image.sprite = DefaultSprite;
            Image.DOFade(1, 0.25f);
        }
        else if (NumHintsLeft > 0)
        {
            HintIsActive = true;
            Image.DOFade(0, 0);
            Image.sprite = ActiveSprite;
            Image.DOFade(1, 0.25f);
        }
    }

    public void ReduceAHint()
    {
        NumHintsLeft--;
        HintLeftText.text = NumHintsLeft.ToString();
    }

    public void SetHintCount(int count)
    {
        NumHintsLeft = count;
        Image.sprite = DefaultSprite;
        HintLeftText.text = NumHintsLeft.ToString();
    }
}
