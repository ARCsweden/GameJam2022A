using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelRotation : MonoBehaviour
{
    [SerializeField]
    Transform ObjectTransform;

    [SerializeField]
    Transform PlayerTransform;

    Vector3 startRotation;

    [SerializeField]
    float rotcancel;

    // Start is called before the first frame update
    void Start()
    {
        startRotation = gameObject.transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        rotcancel = PlayerTransform.rotation.eulerAngles.y;
        ObjectTransform.rotation = Quaternion.Euler(startRotation.x, 0, startRotation.z);
        //Quaternion.Euler(startRotation.x, rotcancel, startRotation.z);
    }
}
