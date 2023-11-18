using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private Camera _mainCamera;

    private void Update()
    {
        if (IsTapPressed())
        {
            var initialPoint = _mainCamera.transform.position;
            var directionRay = _mainCamera.ViewportPointToRay(Input.mousePosition);

            if (Physics.Raycast(directionRay ,out var hitInfo))
            {
                var tile = hitInfo.collider.gameObject.GetComponent<Tile>();
                if (tile != null)
                {
                    tile.ProcessTileClick();
                }
            }
            
        }
    }

    private bool IsTapPressed()
    {
        return Input.GetMouseButtonDown(0) || Input.GetMouseButton(0);
    }
}
