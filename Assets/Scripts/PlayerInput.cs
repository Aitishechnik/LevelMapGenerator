using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private Camera _mainCamera;

    private Unit _unit;

    private PlayerUnitController _playerUnitController;

    public void SetCamera(Unit unit)
    {
        if (unit.ThisUnitData.IsControlable)
        {
            _unit = unit;
            _playerUnitController = unit.ThisUnitData.Controller as PlayerUnitController;
        }        
    }

    private void LateUpdate()
    {
        transform.position = _unit.transform.position;
    }

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
                        ClearAllDebugText?.Invoke();
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
