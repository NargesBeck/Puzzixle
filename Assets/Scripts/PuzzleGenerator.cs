using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGenerator : MonoBehaviour
{
    public void Generate(ref PuzzleInfo puzzle, int size)
    {
        for (int row = 0; row < size; row++)
        {
            bool[] rowBoolValues = AssignRowWithRandoms(size);
            for (int col = 0; col < size; col++)
            {
                if (puzzle.Map[row, col] == null)
                    puzzle.Map[row, col] = new Cell();
                if (rowBoolValues[col])
                {
                    puzzle.Map[row, col].CellMode = CellModes.MarkedAsFull;
                }
                else
                {
                    puzzle.Map[row, col].CellMode = CellModes.MarkedAsEmpty;
                }
            }
        }
    }

    private bool RandomBool ()
    {
        float chance = Random.Range(1, 100);
        float randomNumber = Random.Range(1, 100);
        return randomNumber <= chance;
    }

    private bool[] AssignRowWithRandoms(int size)
    {
        bool[] row = new bool[size];
        int lastSeqEndedAt = 0, seqLength;
        bool seqValue = RandomBool();
        // worst case scenario, we have 10 sequences for a row[10]
        for (int i = 0; i < size; i++)
        {
            seqLength = Random.Range(1, size - lastSeqEndedAt + 1);
            seqValue = !seqValue;
            for (int iRow = lastSeqEndedAt; iRow < lastSeqEndedAt + seqLength; iRow++)
            {
                if (iRow >= size - 1)
                    return row;

                row[iRow] = seqValue;
            }
            lastSeqEndedAt = lastSeqEndedAt + seqLength;
        }

        return row;
    }
}
