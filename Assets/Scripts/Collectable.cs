using System.Collections;
using System.Drawing;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _meshRenderer;

    [SerializeField]
    private MeshFilter _meshFilter;

    [SerializeField]
    private Collider _collider;

    public CollectableData ThisCollectableData { get; private set; }

    public void SetData(CollectableData collectableData)
    {
        ThisCollectableData = collectableData;
        transform.localScale = ThisCollectableData.Size;
        _meshRenderer.material = ThisCollectableData.Material;
        _meshFilter.mesh = ThisCollectableData.Mesh;
    }

    private void State(bool state)
    {
        _collider.enabled = state;
        _meshRenderer.enabled = state;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collected");
    }
}
