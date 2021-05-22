using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Quiz : MonoBehaviour
{
    public GameObject quizMenu;
    private Camera_Controller cam;
    public TMP_Text Text;
    public Transform mala, buena;
    public int orden;
    public PausedMenu pausedMenu;
    public Slider slider;
    private float timeRemaining;
    private float timerMax=20;
    private float timeSince=31;
    public Toggle IWantQuices;
    public bool pregunta;
    public TMP_Text question, great, wrong;
    public GameObject mensajeSalteableBoton;
    public GameObject GOquices, GOslider;
    private string[] listPreguntas = new string[] {"¿El Spo2 del paciente es alto o bajo?", "¿Las RR del paciente parece mejorar?", "¿El Spo2 del paciente parece mejorar?", "¿El RR del paciente parece mejorar?", "¿Debe tratarse al paciente con el máximo oxígeno disponible?", "¿El Spo2 del paciente parece mejorar?", "¿El paciente requiere tratamiento?", "¿El paciente tiene indicios de neumonía?", "¿El paciente tiene una condición de riesgo?", "¿Cuáles son los signos vitales más importantes para este paciente?", "¿Qué efecto puede tener en el paciente no usar un humidificador?", "¿Es necesario el humidificador?", " ¿Qué efecto tiene el humidificador?", "¿A partir de qué flujo el incremento del efecto de la cánula es insignificante?", "¿Por qué es especialmente ventajoso el venturi?", "¿Puede cambiarse el flujo del venturi arbitrariamente?", "¿Por qué el protector del venturi es importante?"}  ;
    private string[] listRespuestaBuena = new string[] {"Bajo", "Ha mejorado", "Ha mejorado", "Ha empeorado","No, puede tener efectos negativos", "Ha empeorado", "Sí", "Sí", "Sí", "Spo2 y RR", "Daños en la mucosa bronquial", "Sí, por comodidad y para mantener el tratamiento", "Humidifica el oxígeno, haciéndolo más cómodo de recibir", "Aproximadamente 6L/m","Permite asignar un valor específico y estable de apoyo" , "No, el flujo lo define el filtro", "Para evitar que se tapen las vías del filtro por las que entra aire"};
    private string[] listRespuestaMala = new string[] {"Alto","Ha empeorado", "Ha empeorado", "Ha mejorado", "Claro, más es mejor", "Ha mejorado", "No", "No", "No", "BPM y T", "Reducción significativa del FiO2", "No realmente", "Aumenta el FiO2 a recibir", "Aproximadamente 10L/m","Es de alto flujo" , "Sí, depende de lo que se necesite", "Para evitar que ensucien el filtro"};
    public Transform canula, humidificador, venturi;
    private Interaction interaction;
    public int RespuestasBuenas, RespuestasMalas;
    public float tiempoTotal;
    void Start()
    {
        GOquices.SetActive(false);
        GOslider.SetActive(false);
        cam = FindObjectOfType<Camera_Controller>();
        interaction = FindObjectOfType<Interaction>();
    }

    // Update is called once per frame
    public void EmpezarQuiz()
    {
        orden = Random.Range(0,2);
        
        if (orden==1)
        {
            var posicionA=mala.position;
            mala.position=buena.position;
            buena.position=posicionA;
        }
        
        cam.UnlockMouse();
    }
    public void MinimizarQuiz()
    {
        GOquices.SetActive(false);
        cam.LockMouse();
        Text.text="Ingresa C para ver la pregunta nuevamente";
    }
    public void Nice()
    {
        Text.text="La última respuesta al quiz fue correcta";
        RespuestasBuenas+=1;
        Respuesta();
    }
    public void Noup()
    {
        Text.text="La última respuesta al quiz fue incorrecta";
        RespuestasMalas+=1;
        Respuesta();
    }
    public void Respuesta()
    {
        GOquices.SetActive(false);
        GOslider.SetActive(false);
        cam.LockMouse();
        pregunta=false;
        timeSince=0;
    }
    void Update()
    {
        if (GOquices.activeSelf && !pausedMenu.isPaused)
        {
            cam.UnlockMouse();
        }
        if (!pausedMenu.isPaused)
        {
        timeSince+=Time.deltaTime;
        tiempoTotal+=Time.deltaTime;
        if (timeSince>=50&& !mensajeSalteableBoton.activeSelf && !pausedMenu.isPaused && IWantQuices.isOn)
        {
            PreguntaChanger();
            GOquices.SetActive(true);
            GOslider.SetActive(true);
            timeRemaining=timerMax;
            pregunta=true;
            EmpezarQuiz();
        }
        if (Input.GetKeyDown(KeyCode.C) && pregunta && !mensajeSalteableBoton.activeSelf && !pausedMenu.isPaused)
        {
            GOquices.SetActive(true);
            GOslider.SetActive(true);
            cam.UnlockMouse();
        }
        if (pregunta)
        {
            slider.value=CalculateSliderValue();
            Timer();
        }
        }
        
    }
    float CalculateSliderValue()
    {
        return (timeRemaining/timerMax);
    }
    void Timer()
    {
        
        
        if (timeRemaining>0)
        {
            timeRemaining-=Time.deltaTime;
        }
        if (timeRemaining<=0)
        {
            timeRemaining=timerMax;
            if (GOquices.activeSelf)
            {
                cam.LockMouse();
            }
            GOquices.SetActive(false);
            GOslider.SetActive(false);
            pregunta=false;
            
            Text.text="";
        }
        
    }
    void PreguntaChanger()
    {
        timeSince=0;
        orden = Random.Range(0,4);
            if (orden==0)
            {
                elegirPreguntaTiempo();
            }else{
                elegirPreguntaEstado();
            }
    }
    void elegirPreguntaTiempo()
    {
        orden = Random.Range(0,6);
        if (listPreguntas[orden]!=null && ((tiempoTotal<=120&&(orden==0||orden==4))||(Mathf.Sign(interaction.Spo2Cambio)*1>0 && interaction.tempoEstado>30 && (orden==1||orden==2))||(Mathf.Sign(interaction.Spo2Cambio)*1<0 && interaction.tempoEstado>30 && (orden==3||orden==5))))
        {    
            if (orden==1 || orden ==2 || orden==3|| orden==5)
            {
                interaction.tempoEstado=0;
            }
            
            question.text=listPreguntas[orden];
            great.text=listRespuestaBuena[orden];
            wrong.text=listRespuestaMala[orden];
            if (orden==0 || orden==4)
            {
                listPreguntas[orden]=null;
            }
        }else{
            PreguntaChanger();
        }
    }
    void elegirPreguntaEstado()
    {
        orden = Random.Range(6,17);
        if (listPreguntas[orden]!=null && ((orden<=9&&orden>=7)&&tiempoTotal<=120||(orden<=6&&orden>=1&&tiempoTotal<120)||(orden>=14&&orden<=16&&venturi.childCount>0)||((orden==10||orden==11)&&humidificador.childCount<=0&&(canula.childCount>0||venturi.childCount>0))||((canula.childCount>0||venturi.childCount>0)&&humidificador.childCount>0&&orden==12)||(orden==13&&canula.childCount>0)))
        {    
            question.text=listPreguntas[orden];
            great.text=listRespuestaBuena[orden];
            wrong.text=listRespuestaMala[orden];
            listPreguntas[orden]=null;
        }else{
            PreguntaChanger();
        }
    }
}
