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
    private RedAlarm limit;
    void Start()
    {
        ui = FindObjectOfType<UI>();
        limit=FindObjectOfType<RedAlarm>();
    }


    public void getName(string name)
    {
        iname = name;
        
        Debug.Log("Setexts code returning: " + iname);
        switch (iname)
        {
            case "Cannula":
                description = "Esto es una cánula, dispositivo de bajo flujo de oxígeno, va en las fosas nasales.";
                break;
            case "Humidifier":
                description = "Esto es un humidificador, añade humedad al oxígeno seco.";
                break;
            case "Character":
                description = "Al servicio de urgencia, ingresa masculino, 58 años, con dificultad respiratoria, tos húmeda con expectoración verdosa de cinco (5) días de evolución. Al quinto día presenta fiebre y dificultad respiratoria. Lo observan taquipneico con tirajes subcostales e intercostales, al examen físico, destaca presencia murmullo vesicular disminuido en base pulmonar derecha con crépitos.";
                break;
            case "Green":
                description = "Esta parte de la máscara de Venturi es un filtro y va al final del tubo. Su válvula entrega 6 litros de oxígeno por minuto a una concentración de 35%. ";
                break;
            case "White":
                description = "Esta parte de la máscara de Venturi es un filtro y va al final del tubo. Su válvula entrega 4 litros de oxígeno por minuto a una concentración de 31%.";
                break;
            case "Blue":
                description = "Esta parte de la máscara de Venturi es un filtro y va al final del tubo. Su válvula entrega 2 litros de oxígeno por minuto a una concentración de 24%.";
                break;
            case "Yellow":
                description = "Esta parte de la máscara de Venturi es un filtro y va al final del tubo. Su válvula entrega 4 litros de oxígeno por minuto a una concentración de 28%.";
                break;
            case "Orange":
                description = "Esta parte de la máscara de Venturi es un filtro y va al final del tubo. Su válvula entrega 12 litros de oxígeno por minuto a una concentración de 50%.";
                break;
            case "Red":
                description = "Esta parte de la máscara de Venturi es un filtro y va al final del tubo. Su válvula entrega 8 litros de oxígeno por minuto a una concentración de 40%.";
                break;
             case "Tube":
                description = "Esta parte de la máscara de Venturi es un tubo corrugado que permite la unión de la máscara y el filtro.";
                break;
            case "init":
                description = "Bienvenido al Hospital Niño Jesús, estará asistiendo a José Pérez, tiene 58 años y ha tenido problemas para respirar, acércate y conoce más sobre su situación actual.";
                break;
            case "PlaneScreen":
                description = "Este es un monitor de signos vitales, tiene una alarma para cuando el % de SpO2 es bajo, puedes asignar el % para que la alarma se dispare (por defecto es 0%). El último valor de alarma asignado fue: "+limit.triggerAlarm+"%";
                break;
            case "Oxigen":
                description = "Esta es una fuente de oxígeno, puedes ajustar el flujo de oxígeno dependiendo de la situación de 0 litros por minuto a 10 litros por minuto, por favor selecciona un número válido de flujo de oxígeno (en litros por minuto)";
                break;
            case "Protector":
                description = "Esta parte de la máscara de Venturi es un protector para el filtro que se asigne. Evita que se tapen las entradas de aire del filtro, impidiendo que existan cambios en la concentración de oxígeno recibida por el paciente.";
                break;
            case "FullVentury":
                var ColocadoPregunta = GameObject.Find("Final-Converter");
                var Pregunta = ColocadoPregunta.transform;
                var concentracion="";
                var flujodebido="";
                if (Pregunta.childCount>0)
                {
                Pregunta = Pregunta.GetChild(0);
                var GOPregunta = Pregunta.gameObject;
                switch (GOPregunta.name){
                    case "Green":
                        concentracion="35%";
                        flujodebido= "6L/m";
                        break;
                    case "Blue":
                        concentracion="24%";
                        flujodebido= "2L/m";
                        break;
                    case "Yellow":
                        concentracion="28%";
                        flujodebido= "4L/m";
                        break;
                    case "White":
                        concentracion="31%";
                        flujodebido= "4L/m";
                        break;
                    case "Red":
                        concentracion="40%";
                        flujodebido= "8L/m";
                        break;
                    case "Orange":
                        concentracion="50%";
                        flujodebido= "12L/m";
                        break;
                }
                }else{
                    concentracion="-";
                    flujodebido="-";
                }
                description = "Esto es un Venturi. Dispositivo de alto flujo de oxígeno, que permite entregar una concentración y flujos definidos al paciente (dependiente del filtro asignado). En este caso se tiene el filtro de concentración: "+concentracion+" y flujo deseado: "+flujodebido;
                break;
            default:
                description = "Obteniendo descripción...";
                break;
        }
    }
    public string chooseDefinition()
    {
        return description;
    }
}
