using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Setexts : MonoBehaviour
{
    private string description;
    [SerializeField]
    private string iname;
    private UI ui;

    void Start()
    {
        ui = FindObjectOfType<UI>();
    }


    public void getName(string name)
    {
        iname = name;
        
        Debug.Log("Setexts code returning: " + iname);
        switch (iname)
        {
            case "Cannula":
                description = "This is a Cannula, it goes in your nostrils.";
                break;
            case "Humidifier":
                description = "This is a Humidifier, it adds humidity to dry Oxigen.";
                break;
            case "ConverterGreen":
                description = "This part of the Venturi Mask is a jet and it goes at the end of the Tube. Its valve delivers 6 liters of Oxygen per minute and sets the Concentration to 35%. ";
                break;
            case "ConverterWhite":
                description = "This part of the Venturi Mask is a jet and it goes at the end of the Tube. Its valve delivers 4 liters of Oxygen per minute and sets the Concentration to 31%.";
                break;
            case "ConverterBlue":
                description = "This part of the Venturi Mask is a jet and it goes at the end of the Tube. Its valve delivers 2 liters of Oxygen per minute and sets the Concentration to 24%.";
                break;
            case "ConverterYellow":
                description = "This part of the Venturi Mask is a jet and it goes at the end of the Tube. Its valve delivers 4 liters of Oxygen per minute and sets the Concentration to 28%.";
                break;
            case "Tube":
                description = "This part of the Venturi Mask is a corrugated tube that allows the Assembly of the mask and the jet.";
                break;
            case "init":
                description = "Welcome to St. Little Jesus Hospital, you will be assisting John Doe, he is 58 years old and is having trouble to breath, please ask him more about his current situation.";
                break;
            case "Oxigen":
                description = "This is the source of Oxygen, you can adjust the Oxygen flow depending on the circumstances. Please select a valid number of Oxygen flow (In liters per minute)";
                break;
            default:
                description = "Getting description...";
                break;
        }
    }
    public string chooseDefinition()
    {
        return description;
    }
}
