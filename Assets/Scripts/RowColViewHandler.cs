using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowColViewHandler : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer[] SpriteRenderersArr = new SpriteRenderer[0];

    public void AssignMe(List<int> numbers)
    {
        bool isRowOrColEmpty = true;
        for (int i = 0; i < SpriteRenderersArr.Length; i++)
        {
            if (i < numbers.Count)
            {
                isRowOrColEmpty = false;
                SpriteRenderersArr[i].enabled = true;
                SpriteRenderersArr[i].sprite = ManagersSingleton.Managers.NumberSpritesPrinter.Print(numbers[i], NumberSpritesPrinter.Colors.Blue);
            }
            else
            {
                SpriteRenderersArr[i].enabled = false;
            }
        }
        if (isRowOrColEmpty)
        {
            SpriteRenderersArr[0].enabled = true;
            SpriteRenderersArr[0].sprite = ManagersSingleton.Managers.NumberSpritesPrinter.Print(0, NumberSpritesPrinter.Colors.Blue);
        }
    }
}
