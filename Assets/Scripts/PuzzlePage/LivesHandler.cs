using UnityEngine;
using UnityEngine.UI;

public class LivesHandler : MonoBehaviour
{
    private const int MaxLives = 5;

    [HideInInspector] public int LivesRemaining = 5;

    private Slider slider;
    private Slider Slider
    {
        get
        {
            if (slider == null)
            {
                slider = GetComponent<Slider>();
            }
            return slider;
        }
    }

    /// <summary>
    /// Will return TRUE if player has ran out of lives
    /// </summary>
    public bool ReduceALife()
    {
        Debug.Log("PLAYER IS WRONG");
        if (LivesRemaining == 1)
            return true;
        LivesRemaining--;
        return false;
    }

    public void ResetToFull()
    {
        Slider.value = 1;
        LivesRemaining = MaxLives;
    }

    public void ResetLivesRemaining()
    {
        LivesRemaining = MaxLives;
    }

    private void Update()
    {
        Slider.value = Mathf.Lerp(Slider.value, (float) LivesRemaining / MaxLives, Time.deltaTime);
    }
}
