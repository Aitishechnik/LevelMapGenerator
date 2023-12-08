using System;
using System.Collections;
using System.Drawing;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private CollectablesPool _pool;

    [SerializeField]
    private MeshRenderer _meshRenderer;

    [SerializeField]
    private MeshFilter _meshFilter;

    [SerializeField]
    private Collider _collider;

    public CollectableData ThisCollectableData { get; private set; }


    public void Collect()
    {
        //Game.Instance.CollectablesAccounting.
        OnCollectEvent?.Invoke(this);
    }
    public void SetData(CollectableData collectableData)
    {
        ThisCollectableData = collectableData;
        transform.localScale = ThisCollectableData.Size;
        _meshRenderer.material = ThisCollectableData.Material;
        _meshFilter.mesh = ThisCollectableData.Mesh;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER))
            Collect();
    }

    public void SetPool(CollectablesPool pool)
    {
        _pool = pool;
    }

    public void ReturnToPool()
    {
        _pool.Return(this);
    }

    public event Action<Collectable> OnCollectEvent;
}
