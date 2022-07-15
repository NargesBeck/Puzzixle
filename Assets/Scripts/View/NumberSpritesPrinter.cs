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

    public Sprite Print(int number, Colors outputColor)
    {
        if (outputColor == Colors.Blue)
            return BlueNumbers[number];
        else if (outputColor == Colors.Black)
            return BlackNumbers[number];
        return null;
    }
}
