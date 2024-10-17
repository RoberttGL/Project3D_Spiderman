using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] Transform container;
    [SerializeField] Rigidbody rb_player;
    float ejeX;
    float ejeY;
    [SerializeField] int sens;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb_player = GetComponent<Rigidbody>();
        container.eulerAngles = new Vector3(10, 0, 0);
    }


    void Update()
    {

        ejeX = Input.GetAxis("Mouse X");
        ejeY = Input.GetAxis("Mouse Y") * -1;

        transform.Rotate(Vector3.up, ejeX * sens);
        container.eulerAngles = fixedCamera();


        if (Input.GetMouseButtonDown(0))
        {
            onMauseDown();
        }
    }


    //Para fijar la camara y que no se pueda mover mas 70 grados arriba y -50 grados abajo
    Vector3 fixedCamera()
    {
        print(container.eulerAngles.x);
        //print("BBBBBBBBBBBB"+ container.eulerAngles.y);
        //if (container.eulerAngles.x > 70)
        //{
        //    container.eulerAngles = new Vector3(360, 0, 0);
        //}
        Vector3 angle = container.eulerAngles;
        angle.x += ejeY * sens;
        //print(angle);
        if (angle.x < 361 && angle.x > 70)
        {
            if (angle.x < 310 && angle.x > 290)
            {
                angle.x = 310;
            }
            else if (angle.x > 70 && angle.x < 90)
            {
                angle.x = 70;
            }
        }

        return angle;
    }

    void onMauseDown()
    {
        RaycastHit hit;
        if (Physics.Raycast(container.position, container.GetChild(0).transform.forward, out hit, 50f))
        {
            rb_player.AddForce(container.GetChild(0).transform.forward * 500f);
        }
    }


}
