using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDetector : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            GameObject hitObject = hit.collider?.gameObject;

            if (hitObject != null)
            {
                Debug.Log(hitObject.name);
                ManagersSingleton.Managers.ActionManager.CallTheRelatedManagerForThisClick(hitObject);
            }
        }
    }
}
