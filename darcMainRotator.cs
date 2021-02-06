using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class darcMainRotator : MonoBehaviour
{
    [SerializeField] Vector3 _rotationVector = Vector3.zero;
    [SerializeField] float _rotateSpeed = 0f;

    void FixedUpdate() => RotateObject();
    void RotateObject() => transform.Rotate(_rotationVector * _rotateSpeed * Time.deltaTime, Space.Self);
}
