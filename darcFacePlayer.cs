using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class darcFacePlayer : MonoBehaviour
{
    GameObject player = null;
    void Start() => player = GameObject.FindGameObjectWithTag("Player");

    void FixedUpdate()
    {
        if (player != null)
            transform.rotation = Quaternion.LookRotation(transform.position - player.transform.position);
    }
}
