using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class ResultsMenu : MonoBehaviour
{
    public Text timeText;
    private float timeLeft;

    public void Awake()
    {
        timeLeft = 5;
    }

    public void ReturnToMenu()
    {
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
