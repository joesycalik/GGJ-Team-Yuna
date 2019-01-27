using UnityEngine;

public class DefendingPlayer : MonoBehaviour
{
    // 0.3f with position movement
    // 10 with physics movement
    [SerializeField]
    float _horizontalSpeed;

    GameObject _fallingUnit;
    Rigidbody _unitRigidbody;

    #region Unity Core Methods

    private void Start()
    {
        _fallingUnit = GameObject.FindGameObjectWithTag("FallingObject");
        _unitRigidbody = _fallingUnit.GetComponent<Rigidbody>();
    }
    private void Update()
    {
        CheckInput();
    }
    #endregion

    #region Input Handling
    void CheckInput()
    {
        //float _moveHorizontal = Input.GetAxis("Horizontal");
        float _moveHorizontal = GameManager.instance.defendingPlayerMovement;
        SendInput(_moveHorizontal);
    }

    void SendInput(float _moveHorizontal)
    {
        Vector3 _movement = new Vector3(_moveHorizontal, 0.0f, 0.0f);

        _fallingUnit.transform.localPosition += _movement * _horizontalSpeed;

        // Optional other form of movement using physics instead of direct position adjustment
        //_unitRigidbody.AddForce(_movement * _horizontalSpeed);
    }
    #endregion
}