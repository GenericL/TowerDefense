using CodeMonkey.Utils;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private Grid<bool> grid;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grid = new Grid<bool>(4, 2, 4f, new Vector3(-20,0), () => true);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.SetGridObject(UtilsClass.GetMouseWorldPositionWithZ(), true);
        }
    }
}
