using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleManager : MonoBehaviour
{
    float horizontal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveReticle();
    }

    void MoveReticle()
    {
        horizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontal, Camera.main.transform);
    }
}
