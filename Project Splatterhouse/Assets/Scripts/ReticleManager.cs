using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleManager : MonoBehaviour
{
    float horizontal;

    GameObject _fallingObject;

    [SerializeField]
    float _reticleOffsetY;

    // Start is called before the first frame update
    void Start()
    {
        _fallingObject = GameObject.FindGameObjectWithTag("FallingObject");
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
        MoveReticle();
    }

    void UpdatePosition()
    {
        float yValue;
        if (_fallingObject.transform.localPosition.y - _reticleOffsetY < 5)
        {
            yValue = 5;
        }
        else
        {
            yValue = _fallingObject.transform.localPosition.y - _reticleOffsetY;
        }
        Vector3 newPos = new Vector3(this.transform.localPosition.x,
            yValue,
            this.transform.localPosition.z);

        this.transform.localPosition = newPos;
    }

    void MoveReticle()
    {
        horizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontal, Camera.main.transform);
    }
}
