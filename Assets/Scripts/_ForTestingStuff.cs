using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class _ForTestingStuff : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ManagersSingleton.Managers.Profile.ResetPlayerPrefs();
    }
}