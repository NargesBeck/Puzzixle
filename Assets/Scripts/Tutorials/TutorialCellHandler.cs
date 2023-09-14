using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCellHandler : MonoBehaviour
{
    [SerializeField] private int Row, Col, Step;
    [SerializeField] private GameObject Glow;
    [SerializeField] private bool MarkAsFull;
    [SerializeField] private bool IsGlowing;

    private void OnEnable()
    {
        ActiveDeactiveGlow(IsGlowing);
    }

    public void Click()
    {
        FindObjectOfType<TutorialsManager>().CellClick(Row, Col, Step);
        GetComponent<UIAnimator>().PlayAnimation(MarkAsFull ? "full" : "empty");
    }

    public void ActiveDeactiveGlow(bool activeGlow)
    {
        Glow.SetActive(activeGlow);
    }
}
