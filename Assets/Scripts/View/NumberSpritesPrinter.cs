using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberSpritesPrinter : MonoBehaviour
{
    public enum Colors
    {
        Blue, Black
    }

    [SerializeField]
    private Sprite[] BlueNumbers = new Sprite[10];

    [SerializeField]
    private Sprite[] BlackNumbers = new Sprite[10];

    public void Print(int number, Colors outputColor, out Sprite sprite)
    {
        sprite = null;
        if (outputColor == Colors.Blue)
            sprite = BlueNumbers[number];
        else if (outputColor == Colors.Black)
            sprite = BlackNumbers[number];
    }
}
