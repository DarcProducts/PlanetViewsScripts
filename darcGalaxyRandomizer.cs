using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class darcGalaxyRandomizer : MonoBehaviour
{
    [SerializeField] GameObject[] galaxys = null;
    [SerializeField] Vector3 minDistance = Vector3.zero, maxDistance = Vector3.zero;
    [SerializeField] float distanceFfromPlayer = 0;
    GameObject player = null;
    void Awake() => player = GameObject.FindGameObjectWithTag("Player");

    void Start()
    {
        if (galaxys != null && player != null)
        {
            foreach (GameObject galaxy in galaxys)
            {
                do
                { galaxy.transform.position = new Vector3(Random.Range(minDistance.x, maxDistance.x), Random.Range(minDistance.y, maxDistance.y), Random.Range(minDistance.z, maxDistance.z));}
                while (Vector3.Distance(galaxy.transform.position, player.transform.position) < distanceFfromPlayer);
            }
        }
    }
}
