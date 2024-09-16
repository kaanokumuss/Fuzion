using System;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    public bool IsTouching()
    {
        return Input.GetMouseButtonDown(0);
    }

    public Vector3 GetTouchPosition(Camera cam)
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(cam.transform.position.z - transform.position.z); 
        return cam.ScreenToWorldPoint(mousePosition);
    }
}