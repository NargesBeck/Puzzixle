using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagersSingleton : MonoBehaviour
{
    private static ManagersSingleton ManagersLinker;

    public static ManagersSingleton Managers
    {
        get
        {
            if (ManagersLinker == null)
            {
                ManagersLinker = FindObjectOfType<ManagersSingleton>();
            }
            return ManagersLinker;
        }
    }

    public Camera Camera;
    public MainMenu MainMenu;
    public GameManager GameManager;
    public ActionManager ActionManager;
    public TouchDetector TouchDetector;
    public CameraMovement CameraMovement;
    public PuzzlePageManager PuzzlePageManager;
}
