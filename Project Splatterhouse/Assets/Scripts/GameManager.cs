using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public const int MAX_PLAYERS = 4;
    public const int MAX_TEAMS = 2;

    public bool movingLeft;
    public bool movingRight;

    public bool player1TurningLeft;
    public bool player1TurningRight;
    public bool player1Firing;

    public bool player2TurningLeft;
    public bool player2TurningRight;
    public bool player2Firing;

    public int[] teamChoices;
    public GameObject airConsoleGO;

    public StartMenu startMenu;

    private static GameManager m_instance = null;
    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                var prefab = Resources.Load<GameObject>("GameManager");
                if (prefab == null) Debug.LogError("Can't load GameManager from Resources");
                var instance = Instantiate(prefab);
                if (instance == null) Debug.LogError("Instance of GameManager prefab is null");
                m_instance = instance.GetComponent<GameManager>();
                if (m_instance == null) Debug.LogError("No GameManager found on prefab instance.");
            }
            return m_instance;
        }
    }

    public void Awake()
    {
        if (m_instance != null && m_instance != this)
        {
            DestroyObject(gameObject);
            return;
        }
        m_instance = this;

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(airConsoleGO);

        AddCallbacks();
        teamChoices = new int[MAX_PLAYERS] {0, 0, 0, 0};
    }

    void OnConnect (int deviceId) 
    {
		if (AirConsole.instance.GetActivePlayerDeviceIds.Count == 0 &&
            AirConsole.instance.GetControllerDeviceIds ().Count >= MAX_PLAYERS)
        {
			AirConsole.instance.SetActivePlayers(4);
            Debug.LogWarning("Ready!");

            startMenu.Init();
		}
	}

    void OnMessage(int deviceId, JToken data) 
    {
        if (teamChoices == null)
        {
            return;
        }

        int activePlayer = AirConsole.instance.ConvertDeviceIdToPlayerNumber(deviceId);
		if (data["join"] != null)
        {
            ProcessJoin(activePlayer, (int)data["join"]);
        }
        if (data["move"] != null)
        {
            ProcessMove(activePlayer, (int)data["move"]);
        }
        if (data["fire"] != null)
        {
            ProcessFire(activePlayer, (int)data["fire"]);
        }   
	}

    private void ProcessMove(int activePlayer, int amount)
    {

    }

    private void ProcessFire(int activePlayer, int amount)
    {

    }

    private void ProcessJoin(int activePlayer, int teamChoice)
    {
        Debug.LogWarning("Player: " + activePlayer + " Team: " + teamChoice);

        if (CanJoinTeam(teamChoice))
        {
            teamChoices[activePlayer] = teamChoice;

            startMenu.JoinTeam(activePlayer, teamChoice);
        }

        if (ShouldStart())
        {
            AssignControllers();
            RemoveCallbacks();
            
            SceneManager.LoadScene("Joe");
        }
    }

    private bool ShouldStart()
    {
        int team1Count = 0;
        int team2Count = 0;
        foreach (int teamNum in teamChoices)
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
        foreach (int teamNum in teamChoices)
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

    private void AssignControllers()
    {
        bool team1Assignment = false;
        for (int i = 0; i < teamChoices.Length; i++)
        {
            int controllerId = AirConsole.instance.GetControllerDeviceIds()[i];
            int teamChoice = teamChoices[i];

            if (teamChoice == 1 && team1Assignment == false)
            {
                AirConsole.instance.Message(controllerId, "team_1_1");
                team1Assignment = true;
            }
            else if (teamChoice == 1 && team1Assignment == true)
            {
                AirConsole.instance.Message(controllerId, "team_1_2");
            }
            else if (teamChoice == 2)
            {
                AirConsole.instance.Message(controllerId, "team_2");
            }
        }
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
