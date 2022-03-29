using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController Instance;

    private void Awake()
    {
        if (Instance!=null)
        {
            Destroy(Instance);
        }

        Instance = this;

    }

    public Transform xAxis;
    public float speed;
    public float controlSpeed;
    public float minClamp, maxClamp;
    float power = 5;
    public CharacterController cControl;

    private float _mousePos;



    void Update()
    {
        #region PLAYER CONTROLLER
        cControl.Move(new Vector3(0, 0, speed * Time.deltaTime));

        if (Input.GetMouseButton(0))
        {
            _mousePos += Input.GetAxis("Mouse X") * controlSpeed * Time.deltaTime;
        }

        xAxis.position = new Vector3(_mousePos, xAxis.position.y, xAxis.position.z);

        float ClampX = Mathf.Clamp(xAxis.position.x, minClamp, maxClamp);

        xAxis.position = new Vector3(ClampX, xAxis.position.y, xAxis.position.z);
        #endregion
    }
}
