using System.Collections.Generic;
using UnityEngine;

public class darcMoonStats : MonoBehaviour
{
    [Header("Metric")]
    [SerializeField] string _moonName = "";
    [SerializeField] float _mass = 0;
    [SerializeField] float _diameter = 0;
    [SerializeField] float _density = 0;
    [SerializeField] float _gravity = 0;
    [SerializeField] float _rotationPeriod = 0;
    [SerializeField] float _lengthOfDay = 0;
    [SerializeField] float _meanTemperature = 0;
    [Header("US")] 
    [SerializeField] float mass = 0;
    [SerializeField] float diameter = 0;
    [SerializeField] float density = 0;
    [SerializeField] float gravity = 0;
    [SerializeField] float rotationPeriod = 0;
    [SerializeField] float lengthOfDay = 0;
    [SerializeField] float meanTemperature = 0;   
    List<string> metricStats = new List<string>();
    List<string> usStats = new List<string>();
    [Space(10)] [SerializeField] string[] funFacts = null;
    [SerializeField] List<Sprite> moonImages = new List<Sprite>();
    bool usingMetric = true;

    private void Start()
    {
        _moonName = this.name;
        AddMetricStatStrings();
        AddUSStatStrings();
    }

    void AddMetricStatStrings()
    {
        metricStats.Add(_mass + "\n");
        metricStats.Add(_diameter + "\n");
        metricStats.Add(_density + "\n");
        metricStats.Add(_gravity + "\n");
        metricStats.Add(_rotationPeriod + "\n");
        metricStats.Add(_lengthOfDay + "\n");
        metricStats.Add(_meanTemperature + "\n");
    }

    void AddUSStatStrings()
    {
        usStats.Add(mass + "\n");
        usStats.Add(diameter + "\n");
        usStats.Add(density + "\n");
        usStats.Add(gravity + "\n");
        usStats.Add(rotationPeriod + "\n");
        usStats.Add(lengthOfDay + "\n");
        usStats.Add(meanTemperature + "\n");
    }
    public List<Sprite> GetMoonImages() => moonImages;
    public string[] GetFunFacts => funFacts;
    public string GetName() => _moonName;
    public List<string> GetMetricStats() => metricStats;
    public List<string> GetUSStats() => usStats;
    public bool GetUsingMetric() => usingMetric;
}
