using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePageManager : MonoBehaviour
{
    public MarkCellAsMananger MarkCellAsMananger;
    public Sprite EmptyCellSprite;
    
    public void Click()
    {
        switch(name)
        {
            case "MarkAs":
                MarkCellAsMananger?.Click();
                break;
        }
    }

    
}
