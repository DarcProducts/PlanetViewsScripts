using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class darcProbe : MonoBehaviour
{
    [SerializeField] float probeSpeed = .08f;
    [SerializeField] float probeMaxLife = 20;
    [SerializeField] GameObject explosionEffect;
    float currentLife = 1;
    float distance = 0;
    [SerializeField] GameObject positionTracker = null;
    GameObject tracker = null;
    GameObject explosion = null;

    void Start()
    {
        currentLife = probeMaxLife;
        
        tracker = Instantiate(positionTracker, transform.position, Quaternion.identity);
    }
    void FixedUpdate()
    {
        SearchSpace();

        currentLife -= Time.deltaTime;

        if (currentLife <= 0)
            Explode();
    }

    void OnCollisionEnter(Collision other)
    {
        if (darcPanelManager.Instance != null)
            darcPanelManager.Instance.StartShowingPanels(other.collider.gameObject);
        
        Explode();
    }

    void SearchSpace()
    {
        if (darcVRPlayerController.Instance != null)
        {
            if (darcVRPlayerController.Instance.GetProbeStartLocation() != null && darcVRPlayerController.Instance.GetProbeAimTarget() != null)
            {
                distance = Vector3.Distance(transform.position, darcVRPlayerController.Instance.GetProbeStartLocation().transform.position);
                Vector3 direction = darcVRPlayerController.Instance.GetProbeAimTarget().position - transform.position;
                transform.LookAt(darcVRPlayerController.Instance.GetProbeAimTarget().position, Vector3.up);
                transform.Translate(direction * probeSpeed * Time.deltaTime, Space.World);
            }
        }
    }

    public void Explode()
    {
        if (explosionEffect != null)
            explosion = Instantiate(explosionEffect, transform.position, transform.rotation);

        if (tracker != null)
            Destroy(tracker);
        
        if (darcVRPlayerController.Instance != null)
            darcVRPlayerController.Instance.SetFireProbe(true);
        
        Destroy(gameObject);
    }
}
