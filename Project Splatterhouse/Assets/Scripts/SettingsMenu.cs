using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    GameObject _mainMenuObject;

    public void Close()
    {
        _mainMenuObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
