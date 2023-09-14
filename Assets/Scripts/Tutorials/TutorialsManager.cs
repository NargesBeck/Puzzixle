using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TutorialsManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> StepsList = new List<GameObject>();

    [SerializeField] private Sprite ToggleFull, ToggleEmpty;

    private int CurrentStepIndex;

    private void Start()
    {
        ShowTutorials();
    }

    private void GoNextStep()
    {
        if (CurrentStepIndex >= StepsList.Count - 1)
        {
            TutorialFinished();
            return;
        }

        CurrentStepIndex++;
        for (int i = 0; i < StepsList.Count; i++)
        {
            StepsList[i].SetActive(i == CurrentStepIndex);
        }

        switch (CurrentStepIndex)
        {
            case 0: ShowStepZero(); break;
            case 1: ShowStepOne(); break;
            case 2: ShowStepTwo(); break;
            case 7: ShowStepSeven(); break;
            case 9: ShowStepNine(); break;
        }
    }

    public void ShowTutorials()
    {
        CurrentStepIndex = -1;
        GoNextStep();
    }

    public void CellClick(int row, int col, int step)
    {
        if (step == 3)
        {
            StepThreeCellWasFilled(col);
        }
        else if (step == 4)
        {
            StepFourCellWasFilled(col);
        }
        else if (step == 5)
        {
            StepFiveCellWasFilled(col);
        }
        else if (step == 6)
        {
            StepSixCellWasFilled(col);
        }
        else if (step == 8)
        {
            StepEightCellWasFilled(row, col);
        }
        else if (step == 9)
        {
            StepNineCellWasFilled();
        }
    }

    private void TutorialFinished()
    {
        
    }

    #region step 0
    private void ShowStepZero()
    {
        Invoke("GoNextStep", 2);
    }
    #endregion

    #region step 1
    [SerializeField] private Button StepOneButton;

    private void ShowStepOne()
    {
        StepOneButton.onClick.AddListener(GoNextStep);
    }
    #endregion

    #region step 2
    [SerializeField] private Image StepTwoToggle;
    [SerializeField] private Button StepTwoButton;
    
    private void ShowStepTwo()
    {
        StepTwoButton.onClick.AddListener(StepTwoToggleButtonClick);
    }

    private void StepTwoToggleButtonClick()
    {
        StepTwoToggle.sprite = ToggleFull;
        Invoke("GoNextStep", 0.5f);
    }
    #endregion

    #region step 3
    private List<int> StepThreeFilled = new List<int>();

    private void StepThreeCellWasFilled(int col)
    {
        if (!StepThreeFilled.Contains(col))
        {
            StepThreeFilled.Add(col);
            if (StepThreeFilled.Count == 5)
                Invoke("GoNextStep", 0.6f);
        }
    }
    #endregion

    #region step 4
    private List<int> StepFourFilled = new List<int>();

    private void StepFourCellWasFilled(int col)
    {
        if (!StepFourFilled.Contains(col))
        {
            StepFourFilled.Add(col);
            if (StepFourFilled.Count == 4)
                Invoke("GoNextStep", 0.6f);
        }
    }
    #endregion

    #region step 5
    private List<int> StepFiveFilled = new List<int>();

    private void StepFiveCellWasFilled(int col)
    {
        if (!StepFiveFilled.Contains(col))
        {
            StepFiveFilled.Add(col);
            if (StepFiveFilled.Count == 4)
                Invoke("GoNextStep", 0.6f);
        }
    }
    #endregion

    #region step 6
    private List<int> StepSixFilled = new List<int>();

    private void StepSixCellWasFilled(int col)
    {
        if (!StepSixFilled.Contains(col))
        {
            StepSixFilled.Add(col);
            if (StepSixFilled.Count == 4)
                Invoke("GoNextStep", 0.6f);
        }
    }
    #endregion

    #region step 7
    [SerializeField] private Image StepSevenToggle;
    [SerializeField] private Button StepSevenButton;

    private void ShowStepSeven()
    {
        StepSevenButton.onClick.AddListener(StepSevenToggleButtonClick);
    }

    private void StepSevenToggleButtonClick()
    {
        StepSevenToggle.sprite = ToggleEmpty;
        Invoke("GoNextStep", 0.5f);
    }
    #endregion

    #region step 8
    private List<string> StepEightFilled = new List<string>();

    private void StepEightCellWasFilled(int row, int col)
    {
        if (!StepEightFilled.Contains($"{row},{col}"))
        {
            StepEightFilled.Add($"{row},{col}");
            if (StepEightFilled.Count == 7)
                Invoke("GoNextStep", 0.6f);
        }
    }
    #endregion

    #region step 9
    [SerializeField] private Image StepNineToggle;
    [SerializeField] private Button StepNineButton;

    [SerializeField] private TutorialCellHandler LastCell;
    [SerializeField] private GameObject StepNineArrow;

    private void ShowStepNine()
    {
        StepNineButton.onClick.AddListener(StepNineToggleButtonClick);
    }

    private void StepNineToggleButtonClick()
    {
        StepNineToggle.sprite = ToggleFull;

        StepNineArrow.SetActive(false);
        LastCell.ActiveDeactiveGlow(true);
        LastCell.GetComponent<Button>().enabled = true;
    }

    private void StepNineCellWasFilled()
    {
        Invoke("GoNextStep", 0.6f);
    }
    #endregion

    #region step 10
    [SerializeField] private Button StepTenButton;

    private void ShowStepTen()
    {
        StepTenButton.onClick.AddListener(GoNextStep);
    }
    #endregion
}
