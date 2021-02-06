using System.Collections.Generic;
using UnityEngine;

public class darcPlanetStats : MonoBehaviour
{
    [SerializeField] private bool _usingMetric = true;
    [Header("METRIC")]
    [SerializeField] string _planetName = "";
    [SerializeField] float _mass = 0; //in 10^24kg
    [SerializeField] float _diameter = 0; //in km
    [SerializeField] float _density = 0; //in kg/m^3
    [SerializeField] float _gravity = 0; //in m/s^2
    [SerializeField] float _escapeVelocity = 0; //in km/s
    [SerializeField] float _rotationPeriod = 0; //in hours
    [SerializeField] float _lengthOfDay = 0; //in hours
    [SerializeField] float _distanceFromSun = 0; //in millions km
    [SerializeField] float _perihelion = 0; //in 10^6km
    [SerializeField] float _aphelion = 0; //in 10^6km
    [SerializeField] float _orbitalPeriod = 0; //in days
    [SerializeField] float _orbitalVelocity = 0; //in km/s
    [SerializeField] float _orbitalInclination = 0; //in degrees
    [SerializeField] float _orbitalEccentricity = 0;
    [SerializeField] float _obliquityToOrbit = 0; //in degrees
    [SerializeField] float _meanTemperature = 0; //in C
    [SerializeField] bool _unknownSurfacePressure = false; //true if unknown
    [SerializeField] float _surfacePressure = 0; //in bars
    [SerializeField] int _numberOfMoons = 0;
    [SerializeField] bool _hasRingSystem = false;
    [SerializeField] bool _unknownGlobalMagneticField = false;
    [SerializeField] bool _hasGlobalMagneticField = false;
    [Header("US")]
    [SerializeField] float mass = 0; //in 10^21ton
    [SerializeField] float diameter = 0; //in miles
    [SerializeField] float density = 0; //in Lbs/ft^3
    [SerializeField] float gravity = 0; //in ft/s^2
    [SerializeField] float escapeVelocity = 0; //in miles/s
    [SerializeField] float distanceFromSun = 0; //in millions miles
    [SerializeField] float perihelion = 0; //in 10^6miles
    [SerializeField] float aphelion = 0; //in 10^6miles
    [SerializeField] float orbitalVelocity = 0; //in miles/s
    [SerializeField] float meanTemperature = 0; //in C
    [SerializeField] float surfacePressure = 0; //in bars
    [Space(10)] [SerializeField] string[] funFacts = null;
    [SerializeField] List<Sprite> planetImages = new List<Sprite>();
    List<string> _myMetricStatStrings = new List<string>();
    List<string> _myUsStatStrings = new List<string>();

    void Start()
    {
        _planetName = this.name;
        AddMetricStrings();
        AddUSStrings();
    }

    void AddMetricStrings()
    {
        _myMetricStatStrings.Add(_mass + "\n");
        _myMetricStatStrings.Add(_diameter + "\n");
        _myMetricStatStrings.Add(_density + "\n");
        _myMetricStatStrings.Add(_gravity + "\n");
        _myMetricStatStrings.Add(_escapeVelocity + "\n");
        _myMetricStatStrings.Add(_rotationPeriod + "\n");
        _myMetricStatStrings.Add(_lengthOfDay + "\n");
        _myMetricStatStrings.Add(_distanceFromSun + "\n");
        _myMetricStatStrings.Add(_perihelion + "\n");
        _myMetricStatStrings.Add(_aphelion + "\n");
        _myMetricStatStrings.Add(_orbitalPeriod + "\n");
        _myMetricStatStrings.Add(_orbitalVelocity + "\n");
        _myMetricStatStrings.Add(_orbitalInclination + "\n");
        _myMetricStatStrings.Add(_orbitalEccentricity + "\n");
        _myMetricStatStrings.Add(_obliquityToOrbit + "\n");
        _myMetricStatStrings.Add(_meanTemperature + "\n");
        _myMetricStatStrings.Add(GetSurfacePressure() + "\n");
        _myMetricStatStrings.Add(_numberOfMoons + "\n");
        _myMetricStatStrings.Add(GetRingSystem() + "\n");
        _myMetricStatStrings.Add(GetGlobalMagneticField());
    }

    void AddUSStrings()
    {
        _myUsStatStrings.Add(mass + "\n");
        _myUsStatStrings.Add(diameter + "\n");
        _myUsStatStrings.Add(density + "\n");
        _myUsStatStrings.Add(gravity + "\n");
        _myUsStatStrings.Add(escapeVelocity + "\n");
        _myUsStatStrings.Add(_rotationPeriod + "\n");
        _myUsStatStrings.Add(_lengthOfDay + "\n");
        _myUsStatStrings.Add(distanceFromSun + "\n");
        _myUsStatStrings.Add(perihelion + "\n");
        _myUsStatStrings.Add(aphelion + "\n");
        _myUsStatStrings.Add(_orbitalPeriod + "\n");
        _myUsStatStrings.Add(orbitalVelocity + "\n");
        _myUsStatStrings.Add(_orbitalInclination + "\n");
        _myUsStatStrings.Add(_orbitalEccentricity + "\n");
        _myUsStatStrings.Add(_obliquityToOrbit + "\n");
        _myUsStatStrings.Add(meanTemperature + "\n");
        _myUsStatStrings.Add(GetSurfacePressureBars() + "\n");
        _myUsStatStrings.Add(_numberOfMoons + "\n");
        _myUsStatStrings.Add(GetRingSystem() + "\n");
        _myUsStatStrings.Add(GetGlobalMagneticField());
    }

    public string GetSurfacePressure()
    {
        if (_unknownSurfacePressure)
            return "Unknown";
        else
            return _surfacePressure.ToString();
    }

    public string GetSurfacePressureBars()
    {
        if (_unknownSurfacePressure)
            return "Unknown";
        else
            return surfacePressure.ToString();
    }

    private string GetRingSystem()
    {
        if (_hasRingSystem)
            return "Yes";
        else if (!_hasRingSystem)
            return "No";
        else
            return "Unknown";
    }

    private string GetGlobalMagneticField()
    {
        if (!_unknownGlobalMagneticField)
        {
            if (_hasGlobalMagneticField)
            {
                return "Yes";
            }
            else if (!_hasGlobalMagneticField)
            {
                return "No";
            }
        }
        return "Unknown";
    }

    public List<Sprite> GetPlanetImages() => planetImages;
    public string[] GetFunFacts() => funFacts;
    public bool GetUsingMetric() => _usingMetric;
    public List<string> GetMetricStats() => _myMetricStatStrings;
    public List<string> GetUSStats() => _myUsStatStrings;
    public string GetName() => _planetName;
}
