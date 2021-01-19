using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BallScript : MonoBehaviour
{
    public float moveSensitivity;
    private Vector3 currentPosition;
    private bool hitWall;
    public Rigidbody2D ballBody;

    public PlatformScript platform;
    public FieldScript field;

    public float sFactor;
    public float sIncrement;
    public float cIncrement;
    public float cTimer;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger");
        if (other.gameObject.tag == "Pickup")
        {
            GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");
            foreach (GameObject pickup in pickups)
            {
                Destroy(pickup);
            }
            field.speedFactor =  sFactor;
            field.speedIncrement += sIncrement;
            field.creationIncrement += cIncrement;
            field.creationTimer = cTimer;
            //field.pickupBounds -= 10;
            field.pickupTimer = 0;
            field.platform.enablePickup = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("ballstart");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentPosition = transform.position;
        float xTranslation = Input.GetAxis("Horizontal") * moveSensitivity * Time.fixedDeltaTime;
        // this makes the ball gain momentum
        //ballBody.velocity += new Vector2(xTranslation, 0);
        // this makes the ball move fluidly
        ballBody.velocity = new Vector2(xTranslation, 0);
        if (currentPosition.y >= 4.8)
        {
            Debug.Log("death, pickup enabled?");
            field.platform.enablePickup = false;
            platform.enablePickup = false;
            field.gameOver = true;
            SceneManager.LoadScene("GameOver");
        }
    }
}
