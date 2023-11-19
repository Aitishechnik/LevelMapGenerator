using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [SerializeField]
    private int _FPS;
    private void Awake()
    {
        Application.targetFrameRate = _FPS;
    }
}
