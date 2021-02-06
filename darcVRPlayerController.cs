using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class darcVRPlayerController : MonoBehaviour
{
    public static darcVRPlayerController Instance {get; private set;}
    [SerializeField] GameObject _player;
    [SerializeField] float _moveSpeed = 10;
    [SerializeField] float _rotationSpeed = 40;
    [SerializeField] GameObject probe = null;
    [SerializeField] GameObject _probeStartLocation;
    [SerializeField] GameObject _probeAimTarget;
    [SerializeField] float exposureDimLevel = .05f;
    [SerializeField] Material asteroidHighlightMaterial = null;
    darcOrbitLines[] orbitLines;
    darcPositionLine[] positionLines;
    darcPlanetStats[] _planets;
    darcMoonStats[] _moons;
    GameObject firedProbe = null;
    bool isBeingHighlighted = false;
    bool _buttonFour = false;
    bool _buttonThree = false;
    bool _buttonTwo = false;
    bool _buttonOne = false;
    float _rightTrigger = 0;
    float _leftTrigger = 0;
    darcRotater[] _rotaters;
    bool _isRotating = true;
    bool _isOrbiting = true;
    float _rightHandTrigger = 0;
    float _leftHandTrigger = 0;
    float _rightIndexTrigger = 0;
    Vector2 _lThumbstick = new Vector2(0, 0);
    Vector2 _rThumbstick = new Vector2(0, 0);
    bool canFireProbe = true;
    bool playedHighlightSound = false;
    bool isPaused = false;

    void Awake()
    {
        Instance = this;
        _player = this.gameObject;
        _rotaters = FindObjectsOfType<darcRotater>();
        _planets = GameObject.FindObjectsOfType<darcPlanetStats>();
        _moons = GameObject.FindObjectsOfType<darcMoonStats>();
        orbitLines = GameObject.FindObjectsOfType<darcOrbitLines>();
        positionLines = GameObject.FindObjectsOfType<darcPositionLine>();
    }

    void FixedUpdate()
    {
        _lThumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        _rThumbstick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        _buttonFour = OVRInput.Get(OVRInput.Button.Four);
        _buttonThree = OVRInput.Get(OVRInput.Button.Three);
        _buttonTwo = OVRInput.Get(OVRInput.Button.Two);
        _rightTrigger = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
        _leftTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        _rightHandTrigger = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);
        _leftHandTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);
        _buttonOne = OVRInput.Get(OVRInput.Button.One);
        _rightIndexTrigger = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

        #region Controls
        if (darcPanelManager.Instance != null)
        {
            if (!darcPanelManager.Instance.PanelsActive())
            {
                if (_rThumbstick.x > .1f)
                    RotatePlayer(new Vector3(0, 1, 0));
                if (_rThumbstick.x < -.1f)
                    RotatePlayer(new Vector3(0, -1, 0));
                if (_rThumbstick.y > .1f)
                    MovePlayer(Vector3.up, _rThumbstick.y / 2);
                if (_rThumbstick.y < -.1f)
                    MovePlayer(Vector3.down, -_rThumbstick.y / 2);
                if (_lThumbstick.y > .1f)
                    MovePlayer(Vector3.forward, _lThumbstick.y);
                if (_lThumbstick.y < -.1f)
                    MovePlayer(Vector3.back, -_lThumbstick.y);
                if (_lThumbstick.x < -.1f)
                    MovePlayer(Vector3.left, -_lThumbstick.x);
                if (_lThumbstick.x > .1f)
                    MovePlayer(Vector3.right, _lThumbstick.x);
            }
            else if (darcPanelManager.Instance.PanelsActive())
            {
                if (_rThumbstick.x < -.1f)
                    darcPanelManager.Instance.RotatePanels(new Vector3(0, _rThumbstick.x, 0));
                if (_rThumbstick.x > .1f)
                    darcPanelManager.Instance.RotatePanels(new Vector3(0, _rThumbstick.x, 0));
            }
        }
        #endregion
    }
    void LateUpdate()
    {
        if (darcPanelManager.Instance != null)
        {
            if (_leftHandTrigger >= 1 && canFireProbe && !darcPanelManager.Instance.PanelsActive())
                HandTriggerActions();
            else if (_leftHandTrigger < 1 && isBeingHighlighted)
                ResetHighlight();

            if (_rightHandTrigger >= 1 && _isOrbiting && _isRotating)
                StopAllRotationAndOrbits();
            else if (_rightHandTrigger < 1 && !_isOrbiting && !_isRotating && !darcPanelManager.Instance.PanelsActive())
                StartAllRotationAndOrbits();

            if (_buttonOne)
                FireProbe();

            if (_buttonTwo)
            {
                if (darcPanelManager.Instance.PanelsActive())
                    darcPanelManager.Instance.StopShowingPanels();
                if (firedProbe != null && !canFireProbe && !_buttonOne)
                    firedProbe.GetComponent<darcProbe>().Explode();
            }

            if (darcPanelManager.Instance.PanelsActive())
            {
                if (_buttonFour)
                    darcPanelRig.Instance.ZoomPanelsInfo();
                if (_buttonThree)
                    darcPanelRig.Instance.ZoomPanelsImage();
            }
        }
    }

    void FireProbe()
    {
        if (darcPanelManager.Instance != null)
        {
            if (canFireProbe && !darcPanelManager.Instance.PanelsActive() && _leftHandTrigger < 1)
            {
                if (probe != null)
                    firedProbe = Instantiate(probe, _probeStartLocation.transform.position, Quaternion.identity);
                canFireProbe = false;
            }
        }
    }

    void ResetHighlight()
    {
        isBeingHighlighted = false;
        playedHighlightSound = false;
        RenderSettings.skybox.SetFloat("_Exposure", .2f);
    }

    public void StopAllRotationAndOrbits()
    {
        isPaused = true;

        if (darcSoundController.Instance.GetCurrentMainMusic().pitch != .5f)
            darcSoundController.Instance.GetCurrentMainMusic().pitch = .5f;

        if (_rotaters != null)
        {
            foreach (darcRotater planet in _rotaters)
            {
                if (planet != null)
                {
                    planet.SetRotating(false);
                    planet.SetOrbiting(false);
                }
            }
            _isRotating = false;
            _isOrbiting = false;
        }
    }

    public void StartAllRotationAndOrbits()
    {
        isPaused = false;
        
        if (darcSoundController.Instance != null)
        {
            if (darcSoundController.Instance.GetCurrentMainMusic().pitch != 1)
                darcSoundController.Instance.GetCurrentMainMusic().pitch = 1;
        }

        if (_rotaters != null)
            foreach (darcRotater planet in _rotaters)
            {
                if (planet != null)
                {
                    planet.SetRotating(true);
                    planet.SetOrbiting(true);
                }
            }
        _isRotating = true;
        _isOrbiting = true;
    }

    public void HandTriggerActions()
    {
        isBeingHighlighted = true;

        RenderSettings.skybox.SetFloat("_Exposure", exposureDimLevel);

        if (!playedHighlightSound && darcSoundController.Instance != null)
        {
            darcSoundController.Instance.PlayHighlightButtonPress();
            playedHighlightSound = true;
        }

        if (orbitLines != null)
        {
            foreach (darcOrbitLines line in orbitLines)
            {
                line.gameObject.SetActive(true);
            }
        }
        if (positionLines != null)
        {
            foreach (darcPositionLine line in positionLines)
            {
                line.gameObject.SetActive(true);
            }
        }
        if (_planets != null)
            foreach (darcPlanetStats planet in _planets)
            {
                darcObjectLabel planetLabel = planet.GetComponent<darcObjectLabel>();

                if (planetLabel != null)
                    planetLabel.TurnOnLabel();
            }
        if (_moons != null)
        {
            foreach (darcMoonStats moon in _moons)
            {
                darcObjectLabel moonLabel = moon.GetComponent<darcObjectLabel>();

                if (moonLabel != null)
                    moonLabel.TurnOnLabel();
            }
        }
    }

    public bool GetIsPaused() => isPaused;
    public bool GetIsBeingHighlighted() => isBeingHighlighted;
    public float GetLeftHandTriggerValue() => _leftHandTrigger;
    public Material GetAsteroidHighlightMaterial() => asteroidHighlightMaterial;
    public GameObject GetPlayer() => _player;
    public Transform GetProbeStartLocation() => _probeStartLocation.transform;
    public Transform GetProbeAimTarget() => _probeAimTarget.transform;
    public void SetFireProbe(bool value) => canFireProbe = value;
    void MovePlayer(Vector3 moveDirection, float value) => _player.transform.Translate(moveDirection * value * _moveSpeed * Time.unscaledDeltaTime);
    void RotatePlayer(Vector3 rotationVector) => _player.transform.Rotate(rotationVector * _rotationSpeed * Time.unscaledDeltaTime);
}
