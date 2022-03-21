using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintHandler : MonoBehaviour
{
    public void Click()
    {
        ManagersSingleton.Managers.PuzzlePageManager.OnHintClicked();
    }
}
