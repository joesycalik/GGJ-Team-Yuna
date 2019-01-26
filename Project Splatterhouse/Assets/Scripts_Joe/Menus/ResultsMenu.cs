using UnityEngine.SceneManagement;
using UnityEngine;

public class ResultsMenu : MonoBehaviour
{
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    } 
}
