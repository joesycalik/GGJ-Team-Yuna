using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallingPlayerUpdates : MonoBehaviour
{
    public int damagePerMissle;
    public int numLives;
    public float speed;
    public float recoilPerMissle;
    public float gravity;
    public float maxSpeed, maxFastFallSpeed, maxXSpeed;
    //keeps track of if the player died, is on the ground, or is falling faster than normal
    public bool isDead, isGrounded, isFastFalling, takeCareOfHit;

    private Text endText;
    private Animation playerModelAnimation;

    // Start is called before the first frame update
    void Start()
    {
        //endText = transform.Find("EndText").GetComponent<Text>();

        damagePerMissle = 1;
        numLives = 9;

        gravity = 0.05f;
        recoilPerMissle = 30f;
        maxSpeed = 50f;
        maxFastFallSpeed = 80f;
        maxXSpeed = 20f;

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
        }

        Vector3 curSpeed = gameObject.GetComponent<Rigidbody>().velocity;
        if (takeCareOfHit) {
            curSpeed.y -= recoilPerMissle;
            takeCareOfHit = false;
        }
        //taking care of the player falling speed
        if (isFastFalling && curSpeed.y > maxFastFallSpeed) {
            curSpeed.y = maxFastFallSpeed;
            //playerModelAnimation.CrossFade("fastFall", 0.3f);
        }
        if (!isFastFalling && curSpeed.y > maxSpeed) {
            curSpeed.y = maxSpeed;
            //playerModelAnimation.CrossFade("default", 0.3f);
        }

        //making sure the player isn't moving too fast horizontally
        if (curSpeed.x > maxXSpeed) {
            curSpeed.x = maxXSpeed;
        }

        //should not be moving in the z direction
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(curSpeed.x, curSpeed.y, 0);
    }

    //check if the player has taken a hit or is on the ground
    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case ("missle"):
                isDead = GetHit();
                break;
        }
    }

    void OnTriggerStay(Collider other)
    {
        switch (other.tag)
        {
            case ("ground"):
                isGrounded = true;
                break;
        }
    }

    private bool GetHit() {
        numLives -= damagePerMissle;
        takeCareOfHit = true;
        return numLives <= 0;
    }

}
