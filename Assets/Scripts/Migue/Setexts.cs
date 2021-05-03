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
            case "init":
                description = "Al servicio de urgencia, ingresa masculino, 58 años, con dificultad respiratoria, tos húmeda con expectoración verdosa de cinco (5) días de evolución. Al quinto día presenta fiebre y dificultad respiratoria. Lo observan taquipneico con tirajes subcostales e intercostales, al examen físico, destaca presencia murmullo vesicular disminuido en base pulmonar derecha con crépitos. Analice y atienda al paciente.";

                break;
            case "Character":
                description = "Al servicio de urgencia, ingresa masculino, 58 años, con dificultad respiratoria, tos húmeda con expectoración verdosa de cinco (5) días de evolución. Al quinto día presenta fiebre y dificultad respiratoria. Lo observan taquipneico con tirajes subcostales e intercostales, al examen físico, destaca presencia murmullo vesicular disminuido en base pulmonar derecha con crépitos.";
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
