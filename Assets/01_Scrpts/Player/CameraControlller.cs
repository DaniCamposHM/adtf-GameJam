using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlller : MonoBehaviour
{
    public Transform target;
    public Vector3 offSet = new Vector3 (0, 20, 0);
    
    void Update()
    {
        transform.position = target.position + offSet;
    }
}
