using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class OptionsScript : MonoBehaviour
{
    public OptionsManagerScript manager;
    public Canvas tooltipCanvas;
    private Canvas currentTooltipCanvas;
    public Text tooltipText;

    private Vector2 tooltipPosition;
    private float xOffset = 10.0f;
    private float yOffset = 10.0f;
    private int[] defaultOptions = new int[] { 1, 0, 0, 0, 1, 1, 1, 1, 1, 1 };

    private int[] selectedOptions = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private int[] originalOptions = new int[10];
    public ToggleGroup difficultyGroup;
    public ToggleGroup itemsGroup;
    public ToggleGroup itemEffectGroup;
    public ToggleGroup itemPercentGroup;
    public Toggle hourToggle;
    public Toggle boltToggle;
    public Toggle freezeToggle;
    public ToggleGroup switchGroup;
    public ToggleGroup speedGroup;
    public ToggleGroup platformGroup;
    public ToggleGroup fallGroup;

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
                currentTooltipCanvas.GetComponentInChildren<Text>().text = "Access sliders options menu for greater control";
                break;
            default:
                break;
        }
        Debug.Log(name);
    }
    public int GetSelectedIndex(ToggleGroup currentGroup)
    {
        Toggle[] toggleArray = currentGroup.GetComponentsInChildren<Toggle>();
        for (int i = 0; i < toggleArray.Length; i++)
        {
            if (toggleArray[i].isOn == true)
                return i;
        }
        return 0;
    }

    public int GetToggleOn(Toggle currentToggle)
    {
        if (currentToggle.isOn)
            return 1;
        else
            return 0;
    }
    public int[] GetOptions()
    {
        int[] returnOptions = new int[10];
        returnOptions[0] = GetSelectedIndex(difficultyGroup);
        returnOptions[1] = GetSelectedIndex(itemsGroup);
        returnOptions[2] = GetSelectedIndex(itemPercentGroup);
        returnOptions[3] = GetSelectedIndex(itemEffectGroup);
        returnOptions[4] = GetSelectedIndex(speedGroup);
        returnOptions[5] = GetSelectedIndex(platformGroup);
        returnOptions[6] = GetToggleOn(hourToggle);
        returnOptions[7] = GetToggleOn(boltToggle);
        returnOptions[8] = GetToggleOn(freezeToggle);
        returnOptions[9] = GetSelectedIndex(fallGroup);
        /**for (int i = 0; i < selectedOptions.Length; i++)
        {
            Debug.Log(selectedOptions[i]);
        }**/
        return returnOptions;
    }

    public void SetOptions()
    {
        Toggle[] difficultyToggleList = difficultyGroup.GetComponentsInChildren<Toggle>();
        difficultyToggleList[originalOptions[0]].isOn = true;
        Toggle[] itemToggleList = itemsGroup.GetComponentsInChildren<Toggle>();
        itemToggleList[originalOptions[1]].isOn = true;
        Toggle[] itemEffectList = itemEffectGroup.GetComponentsInChildren<Toggle>();
        itemEffectList[originalOptions[2]].isOn = true;
        Toggle[] itemPercentList = itemPercentGroup.GetComponentsInChildren<Toggle>();
        itemPercentList[originalOptions[3]].isOn = true;
        Toggle[] speedToggleList = speedGroup.GetComponentsInChildren<Toggle>();
        speedToggleList[originalOptions[4]].isOn = true;
        Toggle[] platformToggleList = platformGroup.GetComponentsInChildren<Toggle>();
        platformToggleList[originalOptions[5]].isOn = true;
        if (originalOptions[6] == 1)
            hourToggle.isOn = true;
        else
            hourToggle.isOn = false;
        if (originalOptions[7] == 1)
            boltToggle.isOn = true;
        else
            boltToggle.isOn = false;
        if (originalOptions[8] == 1)
            freezeToggle.isOn = true;
        else
            freezeToggle.isOn = false;
        Toggle[] fallToggleList = fallGroup.GetComponentsInChildren<Toggle>();
        fallToggleList[originalOptions[9]].isOn = true;
    }

    public void CancelClick()
    {
        SetOptions();
        manager.options = originalOptions;
        ClearGeneric();
        SceneManager.LoadScene("Menu");
    }

    public void ConfirmClick()
    {
        selectedOptions = GetOptions();
        manager.options = selectedOptions;
        ClearGeneric();
        SceneManager.LoadScene("Menu");
    }
    public void DefaultClick()
    {
        Toggle[] difficultyToggleList = difficultyGroup.GetComponentsInChildren<Toggle>();
        difficultyToggleList[1].isOn = true;
        Toggle[] itemToggleList = itemsGroup.GetComponentsInChildren<Toggle>();
        itemToggleList[0].isOn = true;
        Toggle[] itemEffectList = itemEffectGroup.GetComponentsInChildren<Toggle>();
        itemEffectList[0].isOn = true;
        Toggle[] itemPercentList = itemPercentGroup.GetComponentsInChildren<Toggle>();
        itemPercentList[0].isOn = true;
        Toggle[] speedToggleList = speedGroup.GetComponentsInChildren<Toggle>();
        speedToggleList[1].isOn = true;
        Toggle[] platformToggleList = platformGroup.GetComponentsInChildren<Toggle>();
        platformToggleList[1].isOn = true;
        hourToggle.isOn = true;
        boltToggle.isOn = true;
        freezeToggle.isOn = true;
        Toggle[] fallToggleList = fallGroup.GetComponentsInChildren<Toggle>();
        fallToggleList[1].isOn = true;
    }

    public void CustomClick(Button customButton)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Options"){
            SceneManager.LoadScene("SliderOptions");
            manager.useSliders = true;
        }
        else
            SceneManager.LoadScene("Options");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        ShowGenericTip();
        manager = FindObjectOfType<OptionsManagerScript>();
        originalOptions = manager.options;
        SetOptions();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
}
