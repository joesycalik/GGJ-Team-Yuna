using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private List<PlayerButtonController> _playerButtons;

    public GameObject panel;
    public Text title;

    public Sprite attackCat, chuteCat;

    void Awake() 
    {
        AddCallbacks();

        title.text = "Connect More Controllers!";
        _playerButtons = new List<PlayerButtonController>(panel.GetComponentsInChildren<PlayerButtonController>(true));
	}

    void OnConnect (int deviceId) 
    {
		if (AirConsole.instance.GetActivePlayerDeviceIds.Count == 0 &&
            AirConsole.instance.GetControllerDeviceIds ().Count >= GameManager.GetMaxPlayers())
        {
			AirConsole.instance.SetActivePlayers(4);
            Debug.LogWarning("Ready!");

            foreach (PlayerButtonController pbc in _playerButtons)
            {
                pbc.gameObject.SetActive(true);
            }

            title.text = "Hold to Join!";
		}
	}

    void OnMessage(int deviceId, JToken data) 
    {
        if (GameManager.instance.teamChoices == null)
        {
            return;
        }

		int activePlayer = AirConsole.instance.ConvertDeviceIdToPlayerNumber(deviceId);
        int teamChoice = (int)data["join"];

        Debug.LogWarning("Player: " + activePlayer + " Team: " + teamChoice);

        if (CanJoinTeam(teamChoice))
        {
            GameManager.instance.teamChoices[activePlayer] = teamChoice;

            if (teamChoice > 0)
            {
                _playerButtons[activePlayer].Join();
                _playerButtons[activePlayer].SetButtonImage(teamChoice);
            }
            else
            {
                _playerButtons[activePlayer].UnJoin();
            }
        }

        if (ShouldStart())
        {
            RemoveCallbacks();
            SceneManager.LoadScene("GameScene");
        }
	}

    private bool ShouldStart()
    {
        int team1Count = 0;
        int team2Count = 0;
        foreach (int teamNum in GameManager.instance.teamChoices)
        {
            if (teamNum == 1)
            {
                team1Count++;
            }
            else if (teamNum == 2)
            {
                team2Count++;
            }
        }

        return team1Count == 2 && team2Count == 2;
    }

    private bool CanJoinTeam(int teamChoice)
    {
        if (teamChoice == 0)
        {
            return true;
        }

        int team1Count = 0;
        int team2Count = 0;
        foreach (int teamNum in GameManager.instance.teamChoices)
        {
            if (teamNum == 1)
            {
                team1Count++;
            }
            else if (teamNum == 2)
            {
                team2Count++;
            }
        }

        if (teamChoice == 1)
        {
            return team1Count < 2;
        }
        else if (teamChoice == 2)
        {
            return team2Count < 2;
        }

        return false;
    }

    private void AddCallbacks()
    {
        AirConsole.instance.onConnect += OnConnect;
		AirConsole.instance.onMessage += OnMessage;
    }

    private void RemoveCallbacks()
    {
        AirConsole.instance.onConnect -= OnConnect;
		AirConsole.instance.onMessage -= OnMessage;
    }
}
