using UnityEngine;

public class CamController : MonoBehaviour
{
    #region Serialized variables
    // This variable is the base difference in y value between the falling object and the camera
    // The camera will always be y units below the falling object
    [SerializeField]
    int _cameraYOffset;

    // The camera needs to maintain a specific distance from the falling object
    [SerializeField]
    int cameraZOffset;
    #endregion

    GameObject _fallingObject;

    #region Unity Core Methods
    private void Start()
    {
       _fallingObject = GameObject.FindGameObjectWithTag("FallingObject");
    }

    private void Update()
    {
        FollowFallingUnit();
    }
    #endregion

    #region Camera updates
    void FollowFallingUnit()
    {
        float yValue;
        if (_fallingObject.transform.localPosition.y + _cameraYOffset < 5)
        {
            yValue = 5;
        }
        else
        {
            yValue = _fallingObject.transform.localPosition.y + _cameraYOffset;
        }
        Vector3 newPos = new Vector3(0.0f,
            yValue, 
            _fallingObject.transform.localPosition.z + cameraZOffset);

        this.transform.localPosition = newPos;
    }
    #endregion


}
