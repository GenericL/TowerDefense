using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildManager : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    private Tile[] tiles;
    private List<GameObject> UITiles;

    private int selectedTile;

    [SerializeField] Transform tileGridUI;

    private void Start()
    {
        int position = 0;
        foreach (Tile tile in tiles)
        {
        }
    }
}
