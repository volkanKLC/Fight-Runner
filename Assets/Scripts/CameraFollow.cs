using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance;
    private void Awake()
    {
        Instance = this;
    }

    [Header("CAMERA VARIABLES")]
    public float lerpValue;
    public Vector3 offSet;
    public Transform target;


    void LateUpdate()
    {
        Vector3 desPos = target.position + offSet;
        transform.position = Vector3.Lerp(transform.position, desPos, lerpValue);
    }
}
