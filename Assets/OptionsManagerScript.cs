using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManagerScript : MonoBehaviour
{
    // Start is called before the first frame update

    // basic options are listed in this order:
    // difficulty, item on/off, item%, item effect, move speed, plat speed, hourglass, bolt, freeze, fall speed
    public int[] options = new int[10] { 1, 0, 0, 0, 1, 1, 1, 1, 1, 1 };

    // slider options are listed in this order:
    // difficulty, item%, item effect, move speed, fall speed, plat speed, item on/off, hourglass, bolt, freeze
    public float[] sliderOptions = new float[10] { 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.0f, 1.0f, 1.0f, 1.0f };
    public int score;

    public bool useSliders = false;
    void Start()
    {
        
    }

    private void Awake()
    {
        int numManagers = FindObjectsOfType<OptionsManagerScript>().Length;
        if (numManagers != 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(transform.gameObject);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
