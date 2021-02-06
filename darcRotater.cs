using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class darcRotater : MonoBehaviour
{
    [SerializeField] float _rotateSpeed = 0;
    [SerializeField] Vector3 _rotationVector;
    [SerializeField] GameObject _orbitOrigin = null;

    [SerializeField] float _orbitSpeed = 0;
    [SerializeField] Vector3 _orbitVector;
    float _originalRotateSpeed, _originalOrbitSpeed;
    [SerializeField] bool _isRotating = false;
    bool isRotating = false;
    [SerializeField] bool _isOrbiting = false;
    bool isOrbiting = false;

    private void Start()
    {
        isRotating = _isRotating;
        isOrbiting = _isOrbiting;
        _originalRotateSpeed = _rotateSpeed;
        _originalOrbitSpeed = _orbitSpeed;
    }

    private void FixedUpdate()
    {
        if (darcPanelManager.Instance != null && darcVRPlayerController.Instance != null)
        {
            if (!darcPanelManager.Instance.PanelsActive() && !darcVRPlayerController.Instance.GetIsPaused())
            {
                isRotating = _isRotating;
                isOrbiting = _isOrbiting;
            }

            if (isRotating)
                RotateObject();

            if (isOrbiting)
            {
                if (_orbitOrigin != null)
                    OrbitObject();
            }
        }
    }

    private void RotateObject() => transform.Rotate(_rotationVector * _rotateSpeed * Time.deltaTime, Space.Self);
    private void OrbitObject() => _orbitOrigin.transform.Rotate(_orbitVector * _orbitSpeed * Time.deltaTime, Space.Self);
    private float GetRandomFloat() => Random.Range(2, 6);

    public void SetRotateSpeed(float value) => _rotateSpeed = value;
    public void SetOrbitSpeed(float value) => _orbitSpeed = value;
    public void SetRotating(bool value) => isRotating = value;
    public void SetOrbiting(bool value) => isOrbiting = value;
    public float GetRotateSpeed() => _rotateSpeed;
    public float GetOrbitSpeed() => _orbitSpeed;
    public bool GetIsRotating() => isRotating;
    public bool GetIsOrbiting() => isOrbiting;
}
