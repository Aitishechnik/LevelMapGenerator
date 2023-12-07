using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitControllerHandler : MonoBehaviour
{
    [SerializeField]
    private HashSet<UnitController> _unitControllers = new HashSet<UnitController>();

    private void Update()
    {
        foreach (UnitController controller in _unitControllers)
        {
            controller.ManualUpdate();
        }
    }

    public void Add(UnitController controller)
    {
        _unitControllers.Add(controller);
    }

    public void Remove(UnitController controller)
    {
        _unitControllers.Remove(controller);
    }
}
