using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsSlidersScript : MonoBehaviour
{
    public OptionsManagerScript manager;
    public Canvas tooltipCanvas;
    private Canvas currentTooltipCanvas;
    public Text tooltipText;

    private Vector2 tooltipPosition;
    private float xOffset = 10.0f;
    private float yOffset = 10.0f;
    private float[] defaultOptions = new float[] { 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0, 1, 1, 1 };
    private float[] selectedOptions = new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private float[] originalOptions = new float[10];

    public Slider difficultySlider;
    public ToggleGroup itemGroup;
    public Slider itemPercentSlider;
    public Slider itemEffectSlider;
    public Toggle hourToggle;
    public Toggle boltToggle;
    public Toggle freezeToggle;
    public Slider moveSlider;
    public Slider fallSlider;
    public Slider platformSlider;
    public GameObject content;

    public void ToolTipDestroy()
    {
        currentTooltipCanvas.GetComponentInChildren<Text>().text = "";
        Destroy(currentTooltipCanvas.gameObject);
        ShowGenericTip();
    }

    public void ClearGeneric()
    {
        currentTooltipCanvas.GetComponentInChildren<Text>().text = "";
        Destroy(currentTooltipCanvas.gameObject);
    }

    private void FormatText()
    {
        currentTooltipCanvas.GetComponentInChildren<Text>().fontSize = 14;
        currentTooltipCanvas.GetComponentInChildren<Text>().color = UnityEngine.Color.black;
    }

    private void InstantiateToolTip()
    {
        tooltipPosition = new Vector2(Input.mousePosition.x + xOffset, Input.mousePosition.y + yOffset);
        currentTooltipCanvas = Instantiate(tooltipCanvas, tooltipPosition, new Quaternion(0,0,0,0));
        FormatText();
    }

    private void ShowGenericTip()
    {
        InstantiateToolTip();
        currentTooltipCanvas.GetComponentInChildren<Text>().text = "Change the game settings, hover over option to see description";
    }

    public void ToolTipHandler(int name)
    {
        ClearGeneric();
        InstantiateToolTip();
        switch (name)
        {
            case 0:
                currentTooltipCanvas.GetComponentInChildren<Text>().text = "Difficulty effects the rate at which game speed increases";
                break;
            case 1:
                currentTooltipCanvas.GetComponentInChildren<Text>().text = "Sets whether or not items will appear";
                break;
            case 2:
                currentTooltipCanvas.GetComponentInChildren<Text>().text = "Sets how often items appear, set lower for a harder game";
                break;
            case 3:
                currentTooltipCanvas.GetComponentInChildren<Text>().text = "Sets effectiveness of items, set lower for a harder game";
                break;
            case 4:
                currentTooltipCanvas.GetComponentInChildren<Text>().text = "Turn various items on or off, hover over item for description";
                break;
            case 5:
                currentTooltipCanvas.GetComponentInChildren<Text>().text = "The hourglass will reset platform speed to initial conditions";
                break;
            case 6:
                currentTooltipCanvas.GetComponentInChildren<Text>().text = "The lightning bolt briefly increases move speed";
                break;
            case 7:
                currentTooltipCanvas.GetComponentInChildren<Text>().text = "The snowflake briefly pauses platform movement";
                break;
            case 8:
                currentTooltipCanvas.GetComponentInChildren<Text>().text = "Sets how fast the ball moves, set lower for a harder game";
                break;
            case 9:
                currentTooltipCanvas.GetComponentInChildren<Text>().text = "Sets how fast platforms move at start, set higher for a harder game";
                break;
            case 10:
                currentTooltipCanvas.GetComponentInChildren<Text>().text = "Cancel settings change reverting settings to previous values";
                break;
            case 11:
                currentTooltipCanvas.GetComponentInChildren<Text>().text = "Confirm settings and return to main menu";
                break;
            case 12:
                currentTooltipCanvas.GetComponentInChildren<Text>().text = "Reset to default settings";
                break;
            case 13:
                currentTooltipCanvas.GetComponentInChildren<Text>().text = "Sets how fast the ball falls";
                break;
            case 14:
                currentTooltipCanvas.GetComponentInChildren<Text>().text = "Access basic options menu for presets";
                break;
            default:
                break;
        }
        Debug.Log(name);
    }

    public float GetSelectedIndex(ToggleGroup currentGroup)
    {
        Toggle[] toggleArray = currentGroup.GetComponentsInChildren<Toggle>();
        for (int i = 0; i < toggleArray.Length; i++)
        {
            if (toggleArray[i].isOn == true)
                return i;
        }
        return 0;
    }
    public float GetToggleOn(Toggle CurrentToggle)
    {
        if (CurrentToggle.isOn == true)
            return 1.0f;
        else
            return 0.0f;
    }
    public float[] GetOptions()
    {
        float[] returnOptions = new float[10];
        returnOptions[0] = difficultySlider.value;
        returnOptions[1] = itemPercentSlider.value;
        returnOptions[2] = itemEffectSlider.value;
        returnOptions[3] = moveSlider.value;
        returnOptions[4] = fallSlider.value;
        returnOptions[5] = platformSlider.value;
        returnOptions[6] = GetSelectedIndex(itemGroup);
        returnOptions[7] = GetToggleOn(hourToggle);
        returnOptions[8] = GetToggleOn(boltToggle);
        returnOptions[9] = GetToggleOn(freezeToggle);

        return returnOptions;
    }
    
    public void SetOptions()
    {
        difficultySlider.value = originalOptions[0];
        itemPercentSlider.value = originalOptions[1];
        itemEffectSlider.value = originalOptions[2];
        moveSlider.value = originalOptions[3];
        fallSlider.value = originalOptions[4];
        platformSlider.value = originalOptions[5];
        Toggle[] itemToggleList = itemGroup.GetComponentsInChildren<Toggle>();
        if (originalOptions[6] == 1.0f)
            itemToggleList[1].isOn = true;
        else
            itemToggleList[0].isOn = true;
        if (originalOptions[7] == 1.0f)
            hourToggle.isOn = true;
        else
            hourToggle.isOn = false;
        if (originalOptions[8] == 1.0f)
            boltToggle.isOn = true;
        else
            boltToggle.isOn = false;
        if (originalOptions[9] == 1.0f)
            freezeToggle.isOn = true;
        else
            freezeToggle.isOn = false;
    }

    public void CancelClick()
    {
        SetOptions();
        manager.sliderOptions = originalOptions;
        ClearGeneric();
        SceneManager.LoadScene("Menu");
    }

    public void ConfirmClick()
    {
        selectedOptions = GetOptions();
        manager.sliderOptions = selectedOptions;
        ClearGeneric();
        SceneManager.LoadScene("Menu");
    }
    public void DefaultClick()
    {
        Button[] buttonList = content.GetComponentsInChildren<Button>();
        foreach (Button button in buttonList)
        {
            Text displayText = GetObjectWithTag(button.transform, "Display");
            if (displayText != null)
            {
                Slider currentSlider = button.GetComponentInChildren<Slider>();
                currentSlider.value = .5f;
                displayText.text = currentSlider.value.ToString("n2");
            }
        }
        hourToggle.isOn = true;
        boltToggle.isOn = true;
        freezeToggle.isOn = true;
        Toggle[] itemToggles = itemGroup.GetComponentsInChildren<Toggle>();
        itemToggles[0].isOn = true;
    }

    public void CustomClick(Button customButton)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Options")
            SceneManager.LoadScene("SliderOptions");
            
        else
        {
            SceneManager.LoadScene("Options");
            manager.useSliders = false;
        }
    }

    private Text GetObjectWithTag(Transform parent, string tag)
    {
        Text tagChild = null;
        Text[] textChildren = parent.GetComponentsInChildren<Text>();
        for (int i = 0; i < textChildren.Length; i++)
        {
            if (textChildren[i].tag == tag)
            {
                tagChild = textChildren[i];
                break;
            }
            else
                tagChild = null;
        }
        return tagChild;
    }
    public void SetDisplays()
    {
        Button[] buttonList = content.GetComponentsInChildren<Button>();
        foreach (Button button in buttonList)
        {
            Text displayText = GetObjectWithTag(button.transform, "Display");
            if (displayText != null)
            {
                Slider currentSlider = button.GetComponentInChildren<Slider>();
                displayText.text = currentSlider.value.ToString("n2");
            }
        }
    }
    
    public void SliderChange(Button parent)
    {
            Text displayText = GetObjectWithTag(parent.transform, "Display");
            if (displayText != null)
            {
                Slider currentSlider = parent.GetComponentInChildren<Slider>();
                displayText.text = currentSlider.value.ToString("n2");
            }
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("ScriptActive");
        ShowGenericTip();
        manager = FindObjectOfType<OptionsManagerScript>();
        originalOptions = manager.sliderOptions;
        SetOptions();
        SetDisplays();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
}
