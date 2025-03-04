using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{

    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask placementLayerMask;
    private Vector3 lastPosition;
    public event Action OnClicked, OnExit;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            OnClicked?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnExit?.Invoke(); 
        }
        if(Input.GetKeyUp(KeyCode.X))
        {
            transform.rotation = Quaternion.Euler(0,90,0);
        }
        
    }
    public bool isPointerUI()
        => EventSystem.current.IsPointerOverGameObject();

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
