using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class darcExposure : MonoBehaviour
{
    [SerializeField] float minExposure = 2f;
    [SerializeField] float maxExposure = 2.10f;
    [SerializeField] float rate = .01f;
    float currentExposure = 0f;
    bool goingHigher = false;
    // Start is called before the first frame update
    void FixedUpdate() {
        currentExposure = RenderSettings.skybox.GetFloat("_Exposure");
        
        if (goingHigher)
            SetHigher();
        else if (!goingHigher)
            SetLower();
        
        if (currentExposure>maxExposure)
            goingHigher = false;
        if (currentExposure<minExposure)
            goingHigher = true;
    }

    void SetHigher() => RenderSettings.skybox.SetFloat("_Exposure", currentExposure + rate * Time.deltaTime);
    void SetLower() =>  RenderSettings.skybox.SetFloat("_Exposure", currentExposure - rate * Time.deltaTime);
}
