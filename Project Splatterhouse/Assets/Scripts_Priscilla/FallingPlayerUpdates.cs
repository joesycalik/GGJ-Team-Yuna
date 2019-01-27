using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class FallingPlayerUpdates : MonoBehaviour
{
    public int damagePerMissle = 1;
    public int numLives = 9;
    public float recoilPerMissle = 30f;
    public float maxSpeed = -50f;
    public float maxFastFallSpeed = -80f;
    public float maxXSpeed = 20f;
    //keeps track of if the player died, is on the ground, or is falling faster than normal
    public bool isDead, isGrounded, isFastFalling, takeCareOfHit;

    private Animation playerModelAnimation;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        isGrounded = false;
        isFastFalling = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        //go to the game over screen and get destroyed
        if (isDead) {
        }

        //goes to the victory screen
        if (isGrounded) {
            Debug.Log("grounded");
            gameObject.SetActive(false);
        }

        Vector3 curSpeed = gameObject.GetComponent<Rigidbody>().velocity;
        if (takeCareOfHit) {
            curSpeed.y += recoilPerMissle;
            takeCareOfHit = false;
        }
        //taking care of the player falling speed
        if (isFastFalling && curSpeed.y < maxFastFallSpeed) {
            curSpeed.y = maxFastFallSpeed;
        }
        if (!isFastFalling && curSpeed.y < maxSpeed) {
            curSpeed.y = maxSpeed;
        }

        //making sure the player isn't moving too fast horizontally
        if (Mathf.Abs(curSpeed.x) > maxXSpeed) {
            if (curSpeed.x < 0) {
                curSpeed.x = -maxXSpeed;
            }
            else {
                curSpeed.x = maxXSpeed;
            }
        }

        //should not be moving in the z direction
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(curSpeed.x, curSpeed.y, 0);
    }

    //check if the player has taken a hit or is on the ground
    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case ("Missile"):
                other.gameObject.GetComponent<Projectile>().TriggerDestroyEvent();
                isDead = GetHit();
                if (isDead)
                {
                    GoToResults();
                }
                
                break;
        }
    }

    void OnTriggerStay(Collider other)
    {
        switch (other.tag)
        {
            case ("Ground"):
                isGrounded = true;
                GoToResults();
                break;
        }
    }

    void GoToResults()
    {
        SceneManager.LoadScene(2);
    }

    private bool GetHit() {
        numLives -= damagePerMissle;
        gameObject.GetComponent<AudioSource>().Play(0);
        takeCareOfHit = true;
        SendVibrateToController();
        return numLives <= 0;
    }

    private void SendVibrateToController() {
        int[] ids = GameManager.instance.teamChoices;
        for (int i = 0; i < ids.Length; i++) {
            if (ids[i] == 1) {
                AirConsole.instance.Message(AirConsole.instance.ConvertPlayerNumberToDeviceId(i), "hit");
                Debug.Log("hit sent");
            }
        }
    }

}
