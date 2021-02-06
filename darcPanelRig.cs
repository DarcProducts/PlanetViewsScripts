using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class darcPanelRig : MonoBehaviour
{
    public static darcPanelRig Instance { get; private set; }
    [SerializeField] TMP_Text nameText = null;
    [SerializeField] TMP_Text labelText = null;
    [SerializeField] TMP_Text statText = null;
    [SerializeField] TMP_Text lineText = null;
    [SerializeField] SpriteRenderer image1 = null;
    [SerializeField] SpriteRenderer image2 = null;
    [SerializeField] SpriteRenderer image3 = null;
    [SerializeField] SpriteRenderer image4 = null;
    [SerializeField] TMP_Text fact1Text = null;
    [SerializeField] TMP_Text fact2Text = null;
    [SerializeField] TMP_Text fact3Text = null;
    List<string> labelsPlanet = new List<string>();
    List<string> labelsMoon = new List<string>();
    [SerializeField] bool usingMetric = true;
    [SerializeField] GameObject statPanel = null;
    [SerializeField] GameObject factPanel = null;
    [SerializeField] GameObject infoPanel1 = null;
    [SerializeField] GameObject infoPanel2 = null;
    [SerializeField] float minPanelDistance = 32f;
    [SerializeField] float maxPanelDistance = 128f;

    void Awake() => Instance = this;

    void Start()
    {
        labelsPlanet.Add("Mass:(10^24kg)\n");
        labelsPlanet.Add("Diameter:(km)\n");
        labelsPlanet.Add("Density:(kg/m^3)\n");
        labelsPlanet.Add("Gravity:(m/s^2)\n");
        labelsPlanet.Add("Escape Velocity:(km/s)\n");
        labelsPlanet.Add("Rotation Period:(hours)\n");
        labelsPlanet.Add("Length of Day:(hours)\n");
        labelsPlanet.Add("Distance from Sun:(10^6km)\n");
        labelsPlanet.Add("Perihelion:(10^6km)\n");
        labelsPlanet.Add("Aphelion:(10^6km)\n");
        labelsPlanet.Add("Orbit Period:(days)\n");
        labelsPlanet.Add("Orbit Velocity:(km/s)\n");
        labelsPlanet.Add("Orbit Inclination:(degrees)\n");
        labelsPlanet.Add("Orbit Eccentricity\n");
        labelsPlanet.Add("Obliquity to Orbit:(degrees)\n");
        labelsPlanet.Add("Mean Temperature:(C)\n");
        labelsPlanet.Add("Surface Pressure:(bars)\n");
        labelsPlanet.Add("Number of Moons\n");
        labelsPlanet.Add("Has Ring System\n");
        labelsPlanet.Add("Has MagneticField");

        labelsMoon.Add("Mass:(10^21tons)\n");
        labelsMoon.Add("Diameter:(miles)\n");
        labelsMoon.Add("Density:(lbs/ft^3)\n");
        labelsMoon.Add("Gravity:(ft/s^2)\n");
        labelsMoon.Add("Rotation Period:(hours)\n");
        labelsMoon.Add("Length Of Day:(hours)\n");
        labelsMoon.Add("Mean Temperature:(C)\n");

        SetUpLabels(labelsPlanet);
    }

    void OnEnable()
    {
        if (darcSoundController.Instance != null)
            darcSoundController.Instance.GetCurrentMainMusic().volume = .01f;
    }
    void OnDisable()
    {
        if (darcSoundController.Instance != null && darcSoundController.Instance.GetCurrentMainMusic().volume != .5f)
            darcSoundController.Instance.GetCurrentMainMusic().volume = .5f;
    }

    public void StartPanels(GameObject targetHit)
    {
        if (darcPanelManager.Instance != null)
        {
            ResetTexts();

            nameText.text = targetHit.name;

            darcPlanetStats darcPlanet = targetHit.GetComponent<darcPlanetStats>();
            darcMoonStats darcMoon = targetHit.GetComponent<darcMoonStats>();

            if (darcPlanet != null)
            {
                infoPanel1.SetActive(true);
                infoPanel2.SetActive(true);

                SetUpImages(darcPlanet, darcMoon);

                SetUpLabels(labelsPlanet);
                SetUpLines(darcPanelManager.Instance.GetNumberOfLinesPlanet());

                if (usingMetric)
                    SetUpStats(darcPlanet.GetMetricStats());
                else
                    SetUpStats(darcPlanet.GetUSStats());
            }
            if (darcMoon != null)
            {
                infoPanel1.SetActive(false);
                infoPanel2.SetActive(false);

                SetUpImages(darcPlanet, darcMoon);

                SetUpLabels(labelsMoon);
                SetUpLines(darcPanelManager.Instance.GetNumberOfLinesMoon());

                if (usingMetric)
                    SetUpStats(darcMoon.GetMetricStats());
                else
                    SetUpStats(darcMoon.GetUSStats());
            }
        }
    }

    void SetUpImages(darcPlanetStats targetPlanet, darcMoonStats targetMoon)
    {
        int ranNum = 0;
        if (targetPlanet != null)
        {
            List<Sprite> planetImages = new List<Sprite>(targetPlanet.GetPlanetImages());
            if (image1 == null && planetImages.Count > 0)
            {
                ranNum = Random.Range(0, planetImages.Count - 1);
                image1.sprite = planetImages[ranNum];
                planetImages.RemoveAt(ranNum);
            }
            if (image2 == null && planetImages.Count > 0)
            {
                ranNum = Random.Range(0, planetImages.Count - 1);
                image2.sprite = planetImages[ranNum];
                planetImages.RemoveAt(ranNum);
            }
            if (image3 == null && planetImages.Count > 0)
            {
                ranNum = Random.Range(0, planetImages.Count - 1);
                image3.sprite = planetImages[ranNum];
                planetImages.RemoveAt(ranNum);
            }
            if (image4 == null && planetImages.Count > 0)
            {
                ranNum = Random.Range(0, planetImages.Count - 1);
                image4.sprite = planetImages[ranNum];
                planetImages.RemoveAt(ranNum);
            }
        }
        if (targetMoon != null)
        {
            List<Sprite> moonImages = new List<Sprite>(targetMoon.GetMoonImages());
            if (image1 == null && moonImages.Count > 0)
            {
                ranNum = Random.Range(0, moonImages.Count - 1);
                image1.sprite = moonImages[ranNum];
                moonImages.RemoveAt(ranNum);
            }
            if (image2 == null && moonImages.Count > 0)
            {
                ranNum = Random.Range(0, moonImages.Count - 1);
                image2.sprite = moonImages[ranNum];
                moonImages.RemoveAt(ranNum);
            }
            if (image3 == null && moonImages.Count > 0)
            {
                ranNum = Random.Range(0, moonImages.Count - 1);
                image3.sprite = moonImages[ranNum];
                moonImages.RemoveAt(ranNum);
            }
            if (image4 == null && moonImages.Count > 0)
            {
                ranNum = Random.Range(0, moonImages.Count - 1);
                image4.sprite = moonImages[ranNum];
                moonImages.RemoveAt(ranNum);
            }
        }
    }

    public void ResetTexts()
    {
        if (nameText != null)
            nameText.text = "";
        if (labelText != null)
            labelText.text = "";
        if (statText != null)
            statText.text = "";
        if (lineText != null)
            lineText.text = "";
    }

    void SetUpLabels(List<string> darcList)
    {
        foreach (string label in darcList)
        {
            if (labelText != null)
                labelText.text += label;
        }
    }

    void SetUpStats(List<string> darcList)
    {
        foreach (string stat in darcList)
        {
            if (statText != null)
                statText.text += stat;
        }
    }

    void SetUpLines(int numberOfLines)
    {
        int currentLines = 0;
        while (currentLines < numberOfLines)
        {
            if (lineText != null)
                lineText.text += "_";

            currentLines++;
        }
    }
    public void ZoomPanelsInfo()
    {
        if (statPanel.transform.localPosition.z > minPanelDistance)
            TranslateZ(statPanel, false, maxPanelDistance);
        if (factPanel.transform.localPosition.z < -minPanelDistance)
            TranslateZ(factPanel, true, maxPanelDistance);
        if (infoPanel1.transform.localPosition.x < -minPanelDistance)
            TranslateX(infoPanel1, true, maxPanelDistance);
        if (infoPanel2.transform.localPosition.x > minPanelDistance)
            TranslateX(infoPanel2, false, maxPanelDistance);

        if (image1.transform.localPosition.x > -maxPanelDistance)
            TranslateX(image1.gameObject, false, maxPanelDistance);
        if (image2.transform.localPosition.x < maxPanelDistance)
            TranslateX(image2.gameObject, true, maxPanelDistance);
        if (image3.transform.localPosition.x < maxPanelDistance)
            TranslateX(image3.gameObject, true, maxPanelDistance);
        if (image4.transform.localPosition.x > -maxPanelDistance)
            TranslateX(image4.gameObject, false, maxPanelDistance);

        if (image1.transform.localPosition.z < maxPanelDistance)
            TranslateZ(image1.gameObject, true, maxPanelDistance);
        if (image2.transform.localPosition.z < maxPanelDistance)
            TranslateZ(image2.gameObject, true, maxPanelDistance);
        if (image3.transform.localPosition.z > -maxPanelDistance)
            TranslateZ(image3.gameObject, false, maxPanelDistance);
        if (image4.transform.localPosition.z > -maxPanelDistance)
            TranslateZ(image4.gameObject, false, maxPanelDistance);
    }

    public void ZoomPanelsImage()
    {
        if (statPanel.transform.localPosition.z < maxPanelDistance)
            TranslateZ(statPanel, true, maxPanelDistance);
        if (factPanel.transform.localPosition.z > -maxPanelDistance)
            TranslateZ(factPanel, false, maxPanelDistance);
        if (infoPanel1.transform.localPosition.x > -maxPanelDistance)
            TranslateX(infoPanel1, false, maxPanelDistance);
        if (infoPanel2.transform.localPosition.x < maxPanelDistance)
            TranslateX(infoPanel2, true, maxPanelDistance);

        if (image1.transform.localPosition.x < -minPanelDistance)
            TranslateX(image1.gameObject, true, maxPanelDistance);
        if (image2.transform.localPosition.x > minPanelDistance)
            TranslateX(image2.gameObject, false, maxPanelDistance);
        if (image3.transform.localPosition.x > minPanelDistance)
            TranslateX(image3.gameObject, false, maxPanelDistance);
        if (image4.transform.localPosition.x < -minPanelDistance)
            TranslateX(image4.gameObject, true, maxPanelDistance);

        if (image1.transform.localPosition.z > minPanelDistance)
            TranslateZ(image1.gameObject, false, maxPanelDistance);
        if (image2.transform.localPosition.z > minPanelDistance)
            TranslateZ(image2.gameObject, false, maxPanelDistance);
        if (image3.transform.localPosition.z < -minPanelDistance)
            TranslateZ(image3.gameObject, true, maxPanelDistance);
        if (image4.transform.localPosition.z < -minPanelDistance)
            TranslateZ(image4.gameObject, true, maxPanelDistance);
    }

    void TranslateX(GameObject obj, bool isAdding, float value)
    {
        if (isAdding)
            obj.transform.localPosition = new Vector3(obj.transform.localPosition.x + value * Time.deltaTime, obj.transform.localPosition.y, obj.transform.localPosition.z);
        else
            obj.transform.localPosition = new Vector3(obj.transform.localPosition.x - value * Time.deltaTime, obj.transform.localPosition.y, obj.transform.localPosition.z);
    }

    void TranslateZ(GameObject obj, bool isAdding, float value)
    {
        if (isAdding)
            obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, obj.transform.localPosition.y, obj.transform.localPosition.z + value * Time.deltaTime);
        else
            obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, obj.transform.localPosition.y, obj.transform.localPosition.z - value * Time.deltaTime);
    }
}
