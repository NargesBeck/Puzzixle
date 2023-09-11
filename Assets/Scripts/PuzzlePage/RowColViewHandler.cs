using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RowColViewHandler : MonoBehaviour
{
    [SerializeField]
    private Text SequencesText;

    private enum SequencesTextSeperators { Space, NewLine }

    [SerializeField]
    private SequencesTextSeperators SequencesTextSeperator;

    public void AssignMe(List<int> numbers)
    {
        string seqText = string.Empty;
        string seperator = SequencesTextSeperator == SequencesTextSeperators.NewLine ? "\n" : " ";
        for (int i = 0; i < numbers.Count; i++)
        {
            seqText += numbers[i].ToString();
            if (i < numbers.Count - 1) 
                seqText += seperator;
        }
        SequencesText.text = seqText;
    }
}
