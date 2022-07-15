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
    public Profile Profile;
    public MainMenu MainMenu;
    public GameManager GameManager;
    public CameraMovement CameraMovement;
    public PuzzleGenerator PuzzleGenerator;
    public PuzzlePageManager PuzzlePageManager;
    public NumberSpritesPrinter NumberSpritesPrinter;
    public BoardSelectionPageManager BoardSelectionPageManager;
}
