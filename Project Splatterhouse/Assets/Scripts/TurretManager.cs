using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    protected float elapsedTimeSinceFire;
    public float projectileVelocity = 1f;

    public int projectileNum = 1;

    public Transform projectilePrefab;

    Transform ejectionPoint;

    Queue<Rigidbody> magazine;

    GameObject _fallingObject;

    public int playerNum;

    [SerializeField]
    float _turretYOffset;

    protected void Init()
    {
        _fallingObject = GameObject.FindGameObjectWithTag("FallingObject");
        ejectionPoint = transform.Find("Projectiles");
        magazine = new Queue<Rigidbody>();
        LoadMagazine(projectileNum);
        elapsedTimeSinceFire = 0f;
    }

    protected void UpdatePosition()
    {
        float yValue;
        if (_fallingObject.transform.localPosition.y - _turretYOffset < 0)
        {
            yValue = 0;
        }
        else
        {
            yValue = _fallingObject.transform.localPosition.y - _turretYOffset;
        }
        Vector3 newPos = new Vector3(this.transform.localPosition.x,
            yValue,
            this.transform.localPosition.z);

        this.transform.localPosition = newPos;
        elapsedTimeSinceFire += Time.deltaTime;
    }

    protected void FireProjectile (Vector3 dir)
    {
        int fire = GameManager.instance.GetAttackFireInput(playerNum);
        if (fire != 0 && elapsedTimeSinceFire >= 1f)
        {
            if (magazine.Count > 0)
            {
                GameSoundManager.instance.PlayCannonFire();
                Rigidbody projectileRigidbody = magazine.Dequeue();
                Projectile projectile = projectileRigidbody.GetComponent<Projectile>();
                projectile.destroyEvent += OnProjectileDestroyed;
                projectile.startTime = Time.time;
                projectileRigidbody.gameObject.SetActive(true);
                projectileRigidbody.AddForce(dir * projectileVelocity, ForceMode.Impulse);
                elapsedTimeSinceFire = 0f;
                projectileRigidbody.transform.parent = transform.parent;
            }
        }
    }

    void LoadMagazine (int ammoCount)
    {
        for (int i = 0; i < ammoCount; i++)
        {
            Transform projectileTransform = CreateProjectile();
            Rigidbody projectileRigidbody = projectileTransform.gameObject.GetComponent<Rigidbody>();
            magazine.Enqueue(projectileRigidbody);
        }
    }

    Transform CreateProjectile ()
    {
        Transform projectile = Object.Instantiate(projectilePrefab, ejectionPoint.position, transform.rotation);
        projectile.parent = ejectionPoint;
        projectile.gameObject.SetActive(false);
        return projectile;
    }

    void OnProjectileDestroyed (Projectile projectile)
    {
        Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectile.gameObject.SetActive(false);
        magazine.Enqueue(projectileRigidbody);
        projectileRigidbody.velocity = Vector3.zero;
        projectileRigidbody.angularVelocity = Vector3.zero;
        projectile.transform.parent = ejectionPoint;
        projectile.transform.position = ejectionPoint.position;
        projectile.transform.rotation = ejectionPoint.rotation;
    }
}
