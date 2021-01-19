using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour
{
    public GameObject button;
    private bool hasManager = false;
    public OptionsManagerScript options;

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Options()
    {
        if (options.useSliders == false)
            SceneManager.LoadScene("Options");
        else
            SceneManager.LoadScene("SliderOptions");
    }
    // Start is called before the first frame update
    void Start()
    {
        options = FindObjectOfType<OptionsManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            Debug.Log("Button Pressed");
            SceneManager.LoadScene("Game");
        }
    }
}
