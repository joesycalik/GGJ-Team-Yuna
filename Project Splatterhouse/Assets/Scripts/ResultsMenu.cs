using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class ResultsMenu : MonoBehaviour
{
    public Text timeText;
    private float timeLeft;

    public GameObject winningTeam1Image, winningTeam2Image;

    public void Awake()
    {
        timeLeft = 5;
    }

    private void Start()
    {
        if (GameManager.instance.winningTeam == 1)
        {
            winningTeam1Image.gameObject.SetActive(true);
            winningTeam2Image.gameObject.SetActive(false);
        }
        else if (GameManager.instance.winningTeam == 2)
        {
            winningTeam1Image.gameObject.SetActive(false);
            winningTeam2Image.gameObject.SetActive(true);
        }
    }

    public void ReturnToMenu()
    {
        GameManager.instance.winningTeam = 0;

        GameManager.instance.startMenu.gameObject.SetActive(true);
        GameManager.instance.ReinitControllers();
        
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        timeText.text = timeLeft.ToString("0");

        timeLeft -= Time.deltaTime;
        if ( timeLeft < 0 )
        {
            ReturnToMenu();
        }
    }
}
