using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGenerator : MonoBehaviour
{
    public bool[,] Generate(int size)
    {
        bool[,] arr = new bool[size, size];

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                arr[row, col] = RandomBool();
            }
        }

        return arr;
    }

    private bool RandomBool (float chancePercentage = 0)
    {
        float randomNumber = Random.Range(1, 2);
        return randomNumber == 1;
    }
}
