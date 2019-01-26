using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerButtonController : MonoBehaviour
{
    public Text label;
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    public void Join()
    {
        //label.text = "Joined!";
        button.image.color = Color.green;
    }

    public void UnJoin()
    {
        //label.text = "Team " + teamNum;
        button.image.color = Color.white;
    }
}
