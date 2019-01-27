using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StartMenu : MonoBehaviour
{
    private List<PlayerButtonController> _playerButtons;

    public GameObject panel;
    public Text title;

    public Sprite attackCat, chuteCat, defaultButton;

    void Awake() 
    {
        title.text = "Connect More Controllers!";
	}

    public void Init()
    {
        _playerButtons = new List<PlayerButtonController>(panel.GetComponentsInChildren<PlayerButtonController>(true));

        foreach (PlayerButtonController pbc in _playerButtons)
        {
            pbc.gameObject.SetActive(true);
        }

        title.text = "Hold to Join!";
    }

    public void JoinTeam(int activePlayer, int teamChoice)
    {
        _playerButtons[activePlayer].SetButtonImage(teamChoice);
    }
}
