using UnityEngine;

public class CamController : MonoBehaviour
{
    #region Serialized variables
    // This variable is the base difference in y value between the falling object and the camera
    // The camera will always be y units below the falling object
    [SerializeField]
    int cameraPlayerYDiff;

    // The camera needs to maintain a specific distance from the falling object
    [SerializeField]
    int cameraZOffset;
    #endregion

    GameObject _fallingUnit;

    #region Unity Core Methods
    private void Start()
    {
       _fallingUnit = GameObject.FindGameObjectWithTag("FallingObject");
    }

    private void Update()
    {
        FollowFallingUnit();
    }
    #endregion

    #region Camera updates
    void FollowFallingUnit()
    {
        Vector3 newPos = new Vector3(0.0f,
            _fallingUnit.transform.localPosition.y + cameraPlayerYDiff, 
            _fallingUnit.transform.localPosition.z + cameraZOffset);

        this.transform.localPosition = newPos;
    }
    #endregion


}
