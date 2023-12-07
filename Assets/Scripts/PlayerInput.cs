using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private Camera _mainCamera;

    [SerializeField]
    private float _cameraYOffset;

    private Unit _unit;

    private PlayerUnitController _playerUnitController;

    public void SetUnit(Unit unit, PlayerUnitController playerUnitController)
    {
        _unit = unit;
        _playerUnitController = playerUnitController;
    }

    private void LateUpdate()
    {
        if (_unit != null)
            transform.position = _unit.transform.position + new Vector3(0, _cameraYOffset, 0);
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
