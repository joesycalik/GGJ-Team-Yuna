using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool movingLeft;
    public bool movingRight;

    public bool player1TurningLeft;
    public bool player1TurningRight;
    public bool player1Firing;

    public bool player2TurningLeft;
    public bool player2TurningRight;
    public bool player2Firing;

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
    }
}
