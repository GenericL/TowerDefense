using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public InputManager inputManager;

    private void Start()
    {
        inputManager.OnMouseClick += HandleMouseClick;
    }

    private void HandleMouseClick(Vector3Int position)
    {
        Debug.Log(position);
    }
}
