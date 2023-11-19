using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _meshRenderer;

    [SerializeField]
    private MeshFilter _meshFilter;

    public CollectableData ThisCollectableData { get; private set; }

    public void SetData(CollectableData collectableData)
    {
        ThisCollectableData = collectableData;
        transform.position = transform.position + new Vector3(0, collectableData.OffsetY,0);
        _meshRenderer.material = ThisCollectableData.Material;
        _meshFilter.mesh = ThisCollectableData.Mesh;
    }
}
