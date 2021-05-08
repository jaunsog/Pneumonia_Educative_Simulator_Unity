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
    private float value;
    private UI ui;
    private OxyMeasure oxi;


    // Start is called before the first frame update
    void Start()
    {
        ui = FindObjectOfType<UI>();
        oxi = FindObjectOfType<OxyMeasure>();
        textToDisplay.SetActive(false);
        displayTextObject.SetActive(false);
        
    }

    public void ReadOxygen()
    {
        Vector3 init = oxi.infLimit.position;
        input = writeField.GetComponent<Text>().text;
        if (float.TryParse(input, out value)) 
        {
            if (value >= 0 && value < 11)
            {
                Debug.Log("Oxygen value is " + value);
                textToDisplay.GetComponent<Text>().text = "Oxygen flow value is " + value + "L/s";
                textToDisplay.SetActive(true);
                displayTextObject.SetActive(true);
                Vector3 current = init + new Vector3(0f, (value * 0.0545f), 0f);
                oxi.target.position = current;
                oxi.ChangeMeasure();
            }else
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
}
