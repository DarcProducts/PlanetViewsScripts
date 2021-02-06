using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class darcLabelFixer : MonoBehaviour
{
    [SerializeField] GameObject thisObject;
    [SerializeField] TMP_Text objectLabel;
    [SerializeField] byte labelHeight = 0;
    GameObject player = null;

    void Start()
    {
        if (darcVRPlayerController.Instance != null && player == null)
            player = darcVRPlayerController.Instance.GetPlayer();
    }

    void LateUpdate()
    {
        if (objectLabel != null && player != null && thisObject != null)
        {
            objectLabel.transform.position = new Vector3(thisObject.transform.position.x, thisObject.transform.position.y + labelHeight, thisObject.transform.position.z);
            FaceTarget(objectLabel);
        }
    }

    void FaceTarget(TMP_Text objectToRotate) => objectToRotate.transform.rotation = Quaternion.LookRotation(objectToRotate.transform.position - player.transform.position);
}
