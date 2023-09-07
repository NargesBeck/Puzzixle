using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RowColViewHandler : MonoBehaviour
{
    [SerializeField]
    private Text SequencesText;

    private enum SequencesTextSeperators { Space, NewLine }

    [SerializeField]
    private SequencesTextSeperators SequencesTextSeperator;
    //private SpriteRenderer[] SpriteRenderersArr = new SpriteRenderer[0];

    public void AssignMe(List<int> numbers)
    {
        string seqText = string.Empty;
        string seperator = SequencesTextSeperator == SequencesTextSeperators.NewLine ? "\n" : " ";
        for (int i = 0; i < numbers.Count; i++)
        {
            seqText = numbers[i].ToString();
            if (i < numbers.Count - 1) 
                seqText += seperator;
        }
        SequencesText.text = seqText;

        //bool isRowOrColEmpty = true;
        //int iNum = numbers.Count - 1;
        //for (int i = SpriteRenderersArr.Length - 1; i >= 0; i--)
        //{
        //    if (iNum < 0)
        //    {
        //        SpriteRenderersArr[i].enabled = false;
        //    }
        //    else
        //    {
        //        isRowOrColEmpty = false;
        //        SpriteRenderersArr[i].enabled = true;
        //        SpriteRenderersArr[i].sprite = ManagersSingleton.Managers.NumberSpritesPrinter.Print(numbers[iNum], NumberSpritesPrinter.Colors.Blue);
        //    }
        //    iNum--; 
        //}
        //if (isRowOrColEmpty)
        //{
        //    SpriteRenderersArr[SpriteRenderersArr.Length - 1].enabled = true;
        //    SpriteRenderersArr[SpriteRenderersArr.Length - 1].sprite = ManagersSingleton.Managers.NumberSpritesPrinter.Print(0, NumberSpritesPrinter.Colors.Blue);
        //}
    }
}
