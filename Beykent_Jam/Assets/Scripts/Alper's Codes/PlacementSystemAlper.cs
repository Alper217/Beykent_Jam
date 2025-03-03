using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystemAlper : MonoBehaviour
{
    [SerializeField]
    private GameObject mouseIndicator, cellIndicator;
    [SerializeField]
    private InputManagerAlper InputManagerAlper;
    [SerializeField]
    private Grid grid;

    private void Update()
    {
        Vector3 mousePosition = InputManagerAlper.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        mouseIndicator.transform.position = mousePosition;
        cellIndicator.transform.position = grid.CellToWorld(gridPosition);
    }
}
