using UnityEngine;
using UnityEngine.UI;

public class LivesHandler : MonoBehaviour
{
    private const int MaxLives = 3;

    [HideInInspector] public int LivesRemaining = 3;

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
        Slider.value = (float)LivesRemaining / MaxLives;
        return false;
    }

    public void ResetLivesRemaining()
    {
        LivesRemaining = MaxLives;
    }
}
