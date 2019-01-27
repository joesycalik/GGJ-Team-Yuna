using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTurretManager : TurretManager
{
    [SerializeField]
    int playerNum;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
        FireProjectile(Vector3.up);
        MoveTurret();
    }

    void MoveTurret()
    {
        float joystick = 0f;
        //left
        if (playerNum == 1)
        {
            joystick = Input.GetAxis("Horizontal");
            //TODO: new input from Airconsole
            //Input from airconsole needs to be a float between -1 and 1
        }
        //right
        else
        {
            //TODO: new input from Airconsole
            //Input from airconsole needs to be a float between -1 and 1
            joystick = Input.GetAxis("Vertical");
        }
        transform.Translate(Vector3.right * joystick, Camera.main.transform);
    }
}
