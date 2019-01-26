using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlayerUpdates : MonoBehaviour
{
    public int damagePerMissle;
    public int numLives;
    public float speed;
    public float recoilPerMissle;
    public float gravity;
    public float maxSpeed, maxFastFallSpeed, maxXSpeed;
    //keeps track of if the player died, is on the ground, or is falling faster than normal
    public boolean isDead, isGrounded, isFastFalling, takeCareOfHit;

    private Text endText;

    // Start is called before the first frame update
    void Start()
    {
        endText = transform.Find("EndText").child.GetComponent<Text>();

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
            endText.text = "Cannon team wins!";
        }

        //goes to the victory screen
        if (isGrounded) {
            endText.text = "Parachute team wins!";
        }

        Vector3 curSpeed = GameObject.Rigidbody.velocity;
        if (takeCareOfHit) {
            curSpeed.y -= recoilPerMissle;
            takeCareOfHit = false;
        }
        //taking care of the player falling speed
        if (isFastFalling && curSpeed.y > maxFastFallSpeed) {
            curSpeed.y = maxFastFallSpeed;
        }
        if (!isFastFalling && curSpeed.y > maxSpeed) {
            curSpeed.y = maxSpeed;
        }

        //making sure the player isn't moving too fast horizontally
        if (curSpeed.x > maxXSpeed) {
            curSpeed.x = maxXSpeed;
        }

        //should not be moving in the z direction
        GameObject.Rigidbody.velocity = new Vector3(curSpeed.x, curSpeed.y, 0);
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

    private boolean GetHit() {
        numLives -= damagePerMissle;
        takeCareOfHit = true;
        return numLives <= 0;
    }

}
