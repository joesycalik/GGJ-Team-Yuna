using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private const int MAX_PLAYERS = 4;

    private int[] _teamChoices;
    public GameObject airConsoleGO;

    void Awake() 
    {
        AirConsole.instance.onConnect += OnConnect;
		AirConsole.instance.onMessage += OnMessage;

        Object.DontDestroyOnLoad(airConsoleGO);
        _teamChoices = new int[MAX_PLAYERS] {0, 0, 0, 0};
	}

    void OnConnect (int deviceId) 
    {
		if (AirConsole.instance.GetActivePlayerDeviceIds.Count == 0 &&
            AirConsole.instance.GetControllerDeviceIds ().Count >= MAX_PLAYERS)
        {
			AirConsole.instance.SetActivePlayers(4);
            Debug.LogWarning("Ready!");
		}
	}

    void OnMessage(int deviceId, JToken data) 
    {
		int activePlayer = AirConsole.instance.ConvertDeviceIdToPlayerNumber(deviceId);
        int teamChoice = (int)data["join"];

        Debug.LogWarning("Player: " + activePlayer + " Team: " + teamChoice);

        _teamChoices[activePlayer] = teamChoice;
	}		

    // Update is called once per frame
    void Update()
    {
        if (ShouldStart())
        {
            RemoveCallbacks();
            SceneManager.LoadScene("GameScene");
        }
    }

    private bool ShouldStart()
    {
        if (_teamChoices == null)
        {
            return false;
        }

        int team1Count = 0;
        int team2Count = 0;
        foreach (int teamNum in _teamChoices)
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

    private void RemoveCallbacks()
    {
        AirConsole.instance.onConnect -= OnConnect;
		AirConsole.instance.onMessage -= OnMessage;
    }
}
