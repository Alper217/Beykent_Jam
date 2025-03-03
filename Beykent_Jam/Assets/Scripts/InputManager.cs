using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask placementLayerMask;
    private Vector3 lastPosition;

    public Vector3 GetSelectedMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = _camera.nearClipPlane;
        Ray ray = _camera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,100, placementLayerMask))
        {
            lastPosition = hit.point;
        }
        return lastPosition;
    }
}
