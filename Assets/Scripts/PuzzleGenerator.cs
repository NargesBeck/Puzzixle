﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PuzzleGenerator : MonoBehaviour
{
    private int MaxSequencesInRowOrColumns = 3 * 2 - 1; // 3 full sequences, with 2 empty sequences between them
    public void Generate(ref PuzzleInfo puzzle, int size)
    {
        int index = 0;
        for (int row = 0; row < size; row++)
        {
            bool[] rowBoolValues = AssignRowWithRandoms(size);
            for (int col = 0; col < size; col++)
            {
                if (puzzle.Map1D[index] == null)
                    puzzle.Map1D[index] = new Cell();
                if (rowBoolValues[col])
                {
                    puzzle.Map1D[index].CellMode = CellModes.MarkedAsFull;
                }
                else
                {
                    puzzle.Map1D[index].CellMode = CellModes.MarkedAsEmpty;
                }
                index++;
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
        for (int i = 0; i < MaxSequencesInRowOrColumns; i++)
        {
            seqLength = Random.Range(1, size - lastSeqEndedAt + 1);
            seqValue = !seqValue;
            
            for (int iRow = lastSeqEndedAt; iRow < lastSeqEndedAt + seqLength; iRow++)
            {
                if (iRow >= size)
                    return row;

                row[iRow] = seqValue;
            }
            lastSeqEndedAt = lastSeqEndedAt + seqLength;
        }

        return row;
    }
}
