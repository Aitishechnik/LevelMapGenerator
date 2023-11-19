using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private Camera _mainCamera;

    [SerializeField]
    private PlayerUnitController _playerUnitController;

    private void Update()
    {
        if (IsTapPressed())
        {
            var directionRay = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(directionRay, out var hitInfo))
            {
                var tile = hitInfo.collider.gameObject.GetComponent<Tile>();
                if (tile != null)
                {
                    if (tile != _playerUnitController.CurrentTarget)
                        ClearAllDebugText?.Invoke(); //явл€етс€ ли подписка каждого тайла на event хорошей практикой или лучше делать в цикле?
                    tile.ProcessTileClick();
                }
            }           
        }
    }

    public static event Action ClearAllDebugText;

    private bool IsTapPressed()
    {
        return Input.GetMouseButtonDown(0) || Input.GetMouseButton(0);
    }
}
