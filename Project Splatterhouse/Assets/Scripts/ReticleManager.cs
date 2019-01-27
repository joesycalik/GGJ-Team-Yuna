using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleManager : MonoBehaviour
{
    float horizontal;
    float vertical;

    GameObject _fallingObject;

    [SerializeField]
    float _reticleOffsetY;

    [SerializeField]
    int playerNum;

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

    public void SetInitialPosition(Transform turretPosition)
    {
        transform.SetPositionAndRotation(new Vector3(turretPosition.position.x, turretPosition.position.y - _reticleOffsetY, turretPosition.position.z), transform.rotation);
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
        float joystick = 0f;
        transform.Translate(Vector3.right * joystick, Camera.main.transform);
    }
}
