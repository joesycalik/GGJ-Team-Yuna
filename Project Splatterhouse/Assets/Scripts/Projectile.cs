using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    float duration;

    public float startTime { get; set; }

    public event Action<Projectile> destroyEvent;

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime > duration)
        {
            if (destroyEvent != null)
            {
                destroyEvent(this);
            }
        }
    }

    public void TriggerDestroyEvent()
    {
        if (destroyEvent != null)
        {
            destroyEvent(this);
        }
    }

    //check if the player has taken a hit or is on the ground
    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case ("Ground"):
                if (destroyEvent != null)
                {
                    destroyEvent(this);
                }
                break;
        }
    }
}
