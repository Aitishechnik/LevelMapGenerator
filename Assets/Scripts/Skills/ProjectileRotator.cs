using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRotator : MonoBehaviour
{
    [SerializeField]
    private Rigidbody body;

    [SerializeField]
    private Transform center;

    [SerializeField]
    private float _radius = 5f;

    [SerializeField]
    private float _rotationSpeed = 1;

    private void Update()
    {
        var offsetVecor = new Vector3(Mathf.Cos(Time.time * _rotationSpeed), 0, Mathf.Sin(Time.time * _rotationSpeed));
        body.MovePosition(center.position + offsetVecor * _radius);

    }

    public void SetCenter(Transform transform)
    {
        center = transform;
    }
}
