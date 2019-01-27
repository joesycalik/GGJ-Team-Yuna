using UnityEngine;

public class WrappableObject : MonoBehaviour
{
    Renderer _renderer;
    bool _isWrappingX, _isWrappingY;
    Camera _cam;

    #region Unity Core Methods
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        ScreenWrap();
    }
    #endregion

    #region Object Wrapping
    bool CheckRenderer()
    {
        // If at least one render is visible, return true
        if (_renderer.isVisible)
        {
            return true;
        }

        // Otherwise, the object is invisible
        return false;
    }

    void ScreenWrap()
    {
        bool _isVisible = CheckRenderer();

        if (_isVisible)
        {
            _isWrappingX = false;
            _isWrappingY = false;
            return;
        }

        if (_isWrappingX && _isWrappingY)
        {
            return;
        }

        Vector3 _viewportPosition = _cam.WorldToViewportPoint(transform.position);
        Vector3 _newPosition = transform.position;

        if (!_isWrappingX && (_viewportPosition.x > 1 || _viewportPosition.x < 0))
        {
            _newPosition.x = -_newPosition.x;

            _isWrappingX = true;
        }

        if (!_isWrappingY && (_viewportPosition.y > 1 || _viewportPosition.y < 0))
        {
            _newPosition.y = -_newPosition.y;

            _isWrappingY = true;
        }

        transform.position = _newPosition;
    }
    #endregion
}
