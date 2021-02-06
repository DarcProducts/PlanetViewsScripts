using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class darcDontDestroy : MonoBehaviour
{
    void Awake() => DontDestroyOnLoad(this.gameObject);
}
