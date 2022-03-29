using UnityEngine;

public class LivesHandler : MonoBehaviour
{
    private const int MaxLives = 3;

    [SerializeField]
    private Sprite[] HeartsSpritesArr;

    public int LivesRemaining = 3;

    private SpriteRenderer spriteRenderer;
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
    }

    /// <summary>
    /// Will return TRUE if player has ran out of lives
    /// </summary>
    /// <returns></returns>
    public bool ReduceALife()
    {
        Debug.Log("PLAYER IS WRONG");
        if (LivesRemaining == 1)
            return true;
        LivesRemaining--;
        SpriteRenderer.sprite = HeartsSpritesArr[LivesRemaining - 1];
        return false;
    }

    public void ResetLivesRemaining()
    {
        LivesRemaining = MaxLives;
    }
}
