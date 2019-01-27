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

    public void Init()
    {
        _playerButtons = new List<PlayerButtonController>(panel.GetComponentsInChildren<PlayerButtonController>(true));

        foreach (PlayerButtonController pbc in _playerButtons)
        {
            pbc.gameObject.SetActive(true);
        }

        title.text = "HOLD TO JOIN";
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToServer()
    {
        Application.OpenURL("http://www.airconsole.com/?http=1#http://70ca2d94.ngrok.io/?unity-editor-websocket-port=7843&unity-plugin-version=2.0");
    }

    public void JoinTeam(int activePlayer, int teamChoice)
    {
        _playerButtons[activePlayer].SetButtonImage(teamChoice);
    }
}
