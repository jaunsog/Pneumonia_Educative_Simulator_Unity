using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadInput : MonoBehaviour
{

    private string input;
    public GameObject writeField;
    public GameObject textToDisplay;
    public GameObject displayTextObject;
    [HideInInspector]
    public float value;
    private UI ui;
    private OxyMeasure oxi;
    private RedAlarm alarm;
    public GameObject inputField;
    [HideInInspector]
    public float limit;

    // Start is called before the first frame update
    void Start()
    {
        ui = FindObjectOfType<UI>();
        oxi = FindObjectOfType<OxyMeasure>();
        alarm = FindObjectOfType<RedAlarm>();
        textToDisplay.SetActive(false);
        displayTextObject.SetActive(false);
        
    }

    public void Read()
    {
        switch(ui.inspectName)
        {
            case "PlaneScreen":
                ReadAlarm();
                break;
            case "Oxigen":
                ReadOxygen();
                break;
            default:
                break;
        }
        Deletedis();



        
    }

    public void ReadOxygen()
    {
        Vector3 init = oxi.infLimit.position;
        input = writeField.GetComponent<Text>().text;
        if (float.TryParse(input, out value))
        {
            if (value >= 0 && value < 13)
            {
                Debug.Log("Oxygen value is " + value);
                textToDisplay.GetComponent<Text>().text = "El flujo de oxígeno es " + value + "L/m";
                textToDisplay.SetActive(true);
                displayTextObject.SetActive(true);
                Vector3 current = init + new Vector3(0f, (value * 0.048f), 0f);
                oxi.target.position = current;
                oxi.ChangeMeasure();
            }
            else
            {
                writeField.GetComponent<Text>().text = "Not a valid number";
            }

        }
        else
        {
            textToDisplay.SetActive(false);
            displayTextObject.SetActive(false);

        }
    }


    public void ReadAlarm()
    {
        Debug.Log("Alarm will be set.");
        input = writeField.GetComponent<Text>().text;
        if (float.TryParse(input, out limit))
        {
            if (limit >= 0 && limit < 100)
            {
                Debug.Log("Alarm is set to " + limit + "%");
                alarm.triggerAlarm = limit;
            }
        }
    }
    public void Deletedis()
    {
        InputField cuText = inputField.GetComponent<InputField>();
        Debug.Log("cuText returning: "+cuText.text);
        cuText.text = "";
    }
}
