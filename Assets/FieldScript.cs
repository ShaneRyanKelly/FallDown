using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Mathematics;
public class FieldScript : MonoBehaviour
{
    private OptionsManagerScript manager;
    public float startingPosition;
    public float creationTimer;
    public float deathTimer;
    private float countdown;
    private float deathCountdown;
    public float speedFactor;
    public float speedIncrement;
    public float creationIncrement = .0033f;
    private float maxCreationIncrement = .1f;
    private float maxSpeedIncrement = .001f;
    private float maxItemPercent = 9;
    private float maxPickupBounds = 100;
    private float maxSpeedFactor = .01f;
    private float maxItemSpeedIncrement = .05f;
    private float maxItemCreationIncrement = .1f;
    private float maxItemCreationTimer = .99f;
    private int maxMoveSensitivity = 2500;
    private int maxGravity = 100;
    private int maxMass = 20;
    private float maxPlatformSpeed = .33f;
    public int pickupBounds = 1;
    private int Score = 0;
    public bool itemsEnabled;

    public bool gameOver = true;
    public int pickupTimer = 0;
    public int[] options = new int[] { 1, 0, 0, 0, 1, 1, 1, 1, 1, 1 };

    public Text ScoreText;

    public PlatformScript platform;

    public BallScript playerBall;
    //public PlatformScript platformScript;

    private void SetParametersFromOptions()
    {
        Debug.Log("Setting default options");
        // Sets game difficulty
        if (options[0] == 0){
            // Low Difficulty
            speedIncrement = .00025f;
            creationIncrement = .016f;
        }
        else if (options[0] == 1){
            // Medium Difficulty
            speedIncrement = .0005f;
            creationIncrement = .033f;
        }
        else if (options[0] == 2){
            // High Difficulty
            speedIncrement = .001f;
            creationIncrement = .066f;
        }
        // Sets Items on or Off
        if (options[1] == 0){
            // Items Off
            itemsEnabled = false;
        }
        else if (options[1] == 1){
            //Items On
            itemsEnabled = true;
        }
        // Sets Item Percent
        if (options[2] == 0){
            // Low Item Percent
            platform.itemPercent = 3;
            pickupBounds = 90;
        }
        else if (options[2] == 1){
            // Medium Item Percent
            platform.itemPercent = 4;
            pickupBounds = 60;
        }
        else if (options[2] == 2){
            // High Item Percent
            platform.itemPercent = 5;
            pickupBounds = 30;
        }
        // Sets Item Effectiveness
        if (options[3] == 0){
            // Low Item Effect
             playerBall.sFactor = 0.03f;
             playerBall.sIncrement = 0.0005f;
             playerBall.cIncrement = 0.0075f;
             playerBall.cTimer = 0.8f;
        }
        else if (options[3] == 1){
            // Medium Item Effect
            playerBall.sFactor = 0.025f;
            playerBall.sIncrement = 0.0005f;
            playerBall.cIncrement = 0.005f;
            playerBall.cTimer = .9f;
        }
        else if (options[3] == 2){
            // High Item Effect
            playerBall.sFactor = 0.0225f;
            playerBall.sIncrement = 0.0005f;
            playerBall.cIncrement = 0.005f;
            playerBall.cTimer = .99f;
        }
        // Sets Move Speed
        if (options[4] == 0){
            // Low Move Speed
            playerBall.moveSensitivity = 750;
        }
        else if (options[4] == 1){
            // Medium Move Speed
            playerBall.moveSensitivity = 1000;
        }
        else if (options[4] == 2){
            // High Move Speed
            playerBall.moveSensitivity = 2000;
        }
        // Sets Platform Speed
        if (options[5] == 0){
            // Low Platform Speed
            platform.platformSpeed = .075f;
            creationTimer = 1.25f;
        }
        else if (options[5] == 1){
            // Medium Platform Speed
            platform.platformSpeed = .1f;
            creationTimer = 1;
        }
        else if (options[5] == 2){
            // High Platform Speed
            platform.platformSpeed = .2f;
            creationTimer = .5f;
        }
        // Sets Hourglass Switch
        if (options[6] == 0){
            // Off
        }
        else if (options[6] == 1){
            // On
        }
        // Sets Bolt Switch
        if (options[7] == 0){
            // Off
        }
        else if (options[7] == 1){
            // On
        }
        // Sets Freeze Switch
        if (options[8] == 0){
            // Off
        }
        else if (options[8] == 1){
            // On
        }
        // Sets Fall Speed
        if (options[9] == 0){
            // Low Fall Speed
            playerBall.ballBody.gravityScale = 15;
            playerBall.ballBody.mass = 20;
        }
        else if (options[9] == 1){
            // Medium Fall Speed
            playerBall.ballBody.gravityScale = 30;
            playerBall.ballBody.mass = 20;
        }
        else if (options[9] == 2){
            // High Fall Speed
            playerBall.ballBody.gravityScale = 45;
            playerBall.ballBody.mass = 20;
        }
    }

    private void SetParametersFromSliders()
    {
        Debug.Log("Setting Options from Sliders");
        Debug.Log(maxSpeedIncrement);
        // Sets game difficulty
        speedIncrement = maxSpeedIncrement * manager.sliderOptions[0];
        creationIncrement = maxCreationIncrement * manager.sliderOptions[0];
        speedFactor = maxSpeedFactor * manager.sliderOptions[0];
        // Sets Item Percent
        platform.itemPercent -= Mathf.RoundToInt(maxItemPercent * manager.sliderOptions[1]);
        pickupBounds = 100 -Mathf.RoundToInt(maxPickupBounds * manager.sliderOptions[1]);
        // Sets Item Effectiveness
        playerBall.sFactor += playerBall.sFactor * manager.sliderOptions[2];
        playerBall.sIncrement += playerBall.sIncrement * manager.sliderOptions[2];
        playerBall.cIncrement += playerBall.cIncrement * manager.sliderOptions[2];
        playerBall.cTimer = maxItemCreationTimer;
        // Sets Move Speed
        playerBall.moveSensitivity = maxMoveSensitivity * manager.sliderOptions[3];
        // Sets Fall Speed
        playerBall.ballBody.gravityScale = maxGravity * manager.sliderOptions[4];
        playerBall.ballBody.mass = 0.1f;
        // Sets Platform Speed
        platform.platformSpeed = maxPlatformSpeed * manager.sliderOptions[5];
        // Sets Items on or Off
        if (manager.sliderOptions[6] == 0){
            // Items Off
            itemsEnabled = false;
        }
        else if (manager.sliderOptions[6] == 1){
            //Items On
            itemsEnabled = true;
        }
        // Sets Hourglass Switch
        if (manager.sliderOptions[7] == 0){
            // Off
        }
        else if (manager.sliderOptions[7] == 1){
            // On
        }
        // Sets Bolt Switch
        if (manager.sliderOptions[8] == 0){
            // Off
        }
        else if (manager.sliderOptions[8] == 1){
            // On
        }
        // Sets Freeze Switch
        if (manager.sliderOptions[9] == 0){
            // Off
        }
        else if (manager.sliderOptions[9] == 1){
            // On
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        platform.enablePickup = false;
        manager = FindObjectOfType<OptionsManagerScript>();
        options = manager.options;
        if (playerBall != null){
            if (!manager.useSliders)
                SetParametersFromOptions();
            else
                SetParametersFromSliders();
        }
        manager.score = 0;
        countdown = creationTimer;
        deathCountdown = deathTimer;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        countdown -= Time.deltaTime;
        deathCountdown -= Time.deltaTime;
        if (itemsEnabled && pickupTimer > pickupBounds && !gameOver)
        {
            platform.enablePickup = true;
        }
        if (countdown <= 0)
        {
            var currentPlatform = Instantiate(platform, new Vector3(0.0f, startingPosition, 0.0f), Quaternion.identity);
            currentPlatform.IncreaseSpeed(speedFactor);
            currentPlatform.transform.parent = gameObject.transform;
            countdown = creationTimer;
            Score++;
            manager.score++;
            pickupTimer++;
            ScoreText.text = "Score: " + Score;
        }
        if (deathCountdown <= 0)
        {
            speedFactor += speedIncrement;
            creationTimer -= creationIncrement;
            deathCountdown = deathTimer;
        }
    }
}
