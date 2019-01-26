using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    Vector3 pointDir;
    float elapsedTimeSinceFire;

    [SerializeField]
    float projectileVelocity = 1f;

    [SerializeField]
    int projectileNum = 1;

    [SerializeField]
    Transform reticle;

    [SerializeField]
    Transform projectilePrefab;

    Transform ejectionPoint;
    Queue<Rigidbody> magazine;

    // Start is called before the first frame update
    void Start()
    {
        ejectionPoint = transform.Find("Projectiles");
        magazine = new Queue<Rigidbody>();
        LoadMagazine(projectileNum);
        pointDir = reticle.position - transform.position;
        elapsedTimeSinceFire = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        RotateBase(reticle.position);
        FireProjectile();
        elapsedTimeSinceFire += Time.deltaTime;
    }

    void FireProjectile ()
    {
        if (Input.GetButtonDown("Fire1") && elapsedTimeSinceFire >= 1f)
        {
            if (magazine.Count > 0)
            {
                Rigidbody projectileRigidbody = magazine.Dequeue();
                Projectile projectile = projectileRigidbody.GetComponent<Projectile>();
                projectile.destroyEvent += OnProjectileDestroyed;
                projectile.startTime = Time.time;
                projectileRigidbody.gameObject.SetActive(true);
                projectileRigidbody.AddForce(pointDir * projectileVelocity, ForceMode.Impulse);
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

    void RotateBase (Vector3 target)
    {
        pointDir = target - transform.position;
        float angle = Mathf.Atan2(pointDir.x, pointDir.y) * -Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
