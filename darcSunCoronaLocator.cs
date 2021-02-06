using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class darcSunCoronaLocator : MonoBehaviour
{
    [SerializeField] ParticleSystem myparticleSystem = null;

    public ParticleSystem GetParticleSystem() => myparticleSystem;
}
