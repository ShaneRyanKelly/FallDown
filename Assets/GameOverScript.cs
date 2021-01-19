using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public Text score;

    OptionsManagerScript manager;
    public void RetryClick()
    {
        SceneManager.LoadScene("Menu");
    }
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<OptionsManagerScript>();
        score.text = "Score = " + manager.score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
