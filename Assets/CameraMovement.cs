using DG.Tweening;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform pinMain, pin5x5;
    private new Transform transform;

    private void Awake()
    {
        transform = GetComponent<Transform>();
        GoHere(pin5x5.position);
    }

    private void GoHere(Vector3 destination)
    {
        transform.DOMove(destination, 3, false);
    }
}
