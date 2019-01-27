using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class FallingPlayerUpdates : MonoBehaviour
{
    public int damagePerMissle;
    public int numLives;
    public float recoilPerMissle = 30f;
    public float maxSpeed = -50f;
    public float maxFastFallSpeed = -80f;
    public float maxXSpeed = 20f;
    //keeps track of if the player died, is on the ground, or is falling faster than normal
    public bool isDead, isGrounded, isFastFalling, takeCareOfHit;

    public GameObject LifeHolder;
    public GameObject Life;

    private Text endText;
    private Animation playerModelAnimation;
    private float speed;
    private int currentLives;   

    // Start is called before the first frame update
    void Start()
    {
        //endText = transform.Find("EndText").GetComponent<Text>();

        damagePerMissle = 1;
        //umLives = 9;
        currentLives = numLives - 1;

        for (int i = 0; i < numLives; i++) {
            GameObject l = (GameObject) Instantiate(Life, Vector3.zero, Quaternion.identity);
            l.transform.parent = LifeHolder.transform;
        }

        isDead = false;
        isGrounded = false;
        isFastFalling = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        //go to the game over screen and get destroyed
        if (isDead) {
            //endText.text = "Cannon team wins!";
        }

        //goes to the victory screen
        if (isGrounded) {
            //endText.text = "Parachute team wins!";
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
            //playerModelAnimation.CrossFade("fastFall", 0.3f);
        }
        if (!isFastFalling && curSpeed.y < maxSpeed) {
            curSpeed.y = maxSpeed;
            //playerModelAnimation.CrossFade("default", 0.3f);
        }

        //Debug.Log("speed: " + curSpeed);

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
        GameSoundManager.instance.PlayHitSound();
        currentLives -= damagePerMissle;
        GameObject lastLife = LifeHolder.transform.GetChild(LifeHolder.transform.childCount - (numLives - currentLives)).gameObject;
        lastLife.SetActive(false);
        takeCareOfHit = true;
        return currentLives <= 0;
    }

}
