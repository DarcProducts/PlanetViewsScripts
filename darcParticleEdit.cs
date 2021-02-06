using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class darcParticleEdit : MonoBehaviour
{
    [SerializeField] GameObject particleEffect1 = null;
    [SerializeField] GameObject particleEffect2 = null;
    [SerializeField] GameObject particleEffect3 = null;

    void Start() 
    {
        int currentState = Random.Range(0, 3);
        switch (currentState)
        {
            case 0: if (particleEffect1!=null) particleEffect1.SetActive(true);
                    if (particleEffect2!=null) particleEffect2.SetActive(false);
                    if (particleEffect3!=null) particleEffect3.SetActive(false);
            break;
            case 1: if (particleEffect1!=null) particleEffect1.SetActive(false);
                    if (particleEffect2!=null) particleEffect2.SetActive(true);
                    if (particleEffect3!=null) particleEffect3.SetActive(false);
            break;
            case 2: if (particleEffect1!=null) particleEffect1.SetActive(false);
                    if (particleEffect2!=null) particleEffect2.SetActive(false);
                    if (particleEffect3!=null) particleEffect3.SetActive(true);
            break;
            default: if (particleEffect1!=null) particleEffect1.SetActive(false);
                     if (particleEffect2!=null) particleEffect2.SetActive(false);
                     if (particleEffect3!=null) particleEffect3.SetActive(false);
            break;
        }
    }
}
