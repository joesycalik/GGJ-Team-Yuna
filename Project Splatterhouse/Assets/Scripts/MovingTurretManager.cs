using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTurretManager : TurretManager
{
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
        float joystick = GameManager.instance.GetAttackMovementInput(playerNum);
        transform.Translate(Vector3.right * joystick, Camera.main.transform);
    }
}
