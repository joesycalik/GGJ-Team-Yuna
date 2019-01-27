using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryTurretManager : TurretManager
{
    Vector3 pointDir;

    [SerializeField]
    Transform reticle;

    void Start()
    {
        Init();
        reticle.GetComponent<ReticleManager>().SetInitialPosition(this.transform);
        pointDir = reticle.position - transform.position;
    }

    void Update()
    {
        UpdatePosition();
        RotateBase(reticle.position);
        FireProjectile(pointDir);
    }

    void RotateBase(Vector3 target)
    {
        pointDir = target - transform.position;
        float angle = Mathf.Atan2(pointDir.x, pointDir.y) * -Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
