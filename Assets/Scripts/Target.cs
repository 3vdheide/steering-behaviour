using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public bool followMouse;

    
    public bool setTargetOnLeftClick;

    public Target(Vector3 position)
    {
        transform.position = position;
    }

    void Update()
    {
        if(followMouse) MoveToMouseCursor();
        if(setTargetOnLeftClick) MoveOnClick();
    }

    private void MoveOnClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            MoveToMouseCursor();
        }
    }

    private void MoveToMouseCursor()
    {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = transform.position.z;
            transform.position = mousePosition;
    }
}
