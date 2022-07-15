using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowColViewHandler : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer[] SpriteRenderersArr = new SpriteRenderer[0];

    public void AssignMe(params int[] numbers)
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            SpriteRenderersArr[i].sprite = ManagersSingleton.Managers.NumberSpritesPrinter.Print(numbers[i], NumberSpritesPrinter.Colors.Blue);
        }
    }
}
