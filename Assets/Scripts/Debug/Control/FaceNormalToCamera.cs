using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceNormalToCamera : MonoBehaviour
{
    public Camera target;

    // Start is called before the first frame update
    void Start()
    {
        target = Camera.main;
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    void LateUpdate()
    {
        //transform.LookAt(transform.position + target.transform.rotation * Vector3.up,
        //    target.transform.rotation * Vector3.up);

        //transform.LookAt(target.transform.position);

        transform.rotation = target.transform.rotation;
        transform.Rotate(-90, 0, 90);

    }
}
