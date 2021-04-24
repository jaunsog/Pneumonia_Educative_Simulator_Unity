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
