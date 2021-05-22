using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class Interaction : MonoBehaviour
{
    public PulseDataNumberRenderer Bpm;
    public PulseDataNumberRenderer Spo2;
    public PulseDataNumberRenderer Tblood;
    public PulseDataNumberRenderer UNibp;
    public PulseDataNumberRenderer DNibp;
    public PulseDataNumberRenderer MNibp;
    public PulseDataNumberRenderer RR;
    public PulseDataLineRenderer xRangeECG;
    public PulseDataLineRenderer xRangeABP;
    public PulseDataLineRenderer xRangeCO2;
    public float DesiredBpm = 80f;
    public float DesiredSpo2 = 89f;
    public float DesiredTblood = 37.2f;
    public float DesiredUNibp = 110f;
    public float DesiredDNibp = 70f;
    public float DesiredRR = 27f;
    public float MBBpm = 80f;
    public float MMBpm = 110f;
    public float BBpm = 80f;
    public float MBSpo2 = 92f;
    public float MMSpo2 = 85f;
    public float BSpo2 = 98f;
    public Aiming ActualStep;
    public TMP_Text Text;
    private float t = 0;
    private float TiempoMuerto;
    public float Spo2Previo, BpmPrevio, previousStep;
    public PausedMenu pausedMenu;
    public GameObject C_InicioCannula, C_FinalCannula, C_InicioVenturi, C_FinalVenturi, CH_InicioCannula, CH_FinalCannula, CH_InicioVenturi, CH_FinalVenturi;
    // Start is called before the first frame update
    public ReadInput oxiFlow;
    public Transform containerCannula, containerVenturi, containerHumidifier;
    private bool Cannula, Venturi, Malu;
    public float PorcentConverter, entrada,empeorar, tratGood,tratMedium, tratBad,entradaOld;
    public float difer=1;
    public float Spo2Cambio, lastSpo2Cambio, tempoEstado;
    public Quiz quiz;
    void Start()
    {
        quiz=FindObjectOfType<Quiz>();
        
        Bpm.multiplier *= DesiredBpm / 73f;
        Spo2.multiplier *= DesiredSpo2 / 97f;
        Tblood.multiplier *= DesiredTblood / 33f;
        UNibp.multiplier *= DesiredUNibp / 114f;
        DNibp.multiplier *= DesiredDNibp / 73f;
        MNibp.multiplier = (UNibp.multiplier + UNibp.multiplier) / 2f;
        RR.multiplier= 27f/12f;
        xRangeECG.xRange = 8 * Bpm.multiplier;
        xRangeABP.xRange = 8 * Bpm.multiplier;
        xRangeCO2.xRange = 8 * (RR.multiplier);
        C_InicioCannula.SetActive(false);
        C_FinalCannula.SetActive(false);
        C_InicioVenturi.SetActive(false);
        C_FinalVenturi.SetActive(false);
        CH_InicioCannula.SetActive(false);
        CH_FinalCannula.SetActive(false);
        CH_InicioVenturi.SetActive(false);
        CH_FinalVenturi.SetActive(false);
        previousStep = ActualStep.ActualStep;
        Spo2Previo = Spo2.multiplier;
        BpmPrevio = Bpm.multiplier;
        previousStep = 0;
        empeorar=3f;
    }
    void Update()
    {
        Spo2Previo=Spo2.multiplier;
        xRangeECG.xRange = 8 * Bpm.multiplier;
        xRangeABP.xRange = 8 * Bpm.multiplier;
        xRangeCO2.xRange = 8 * RR.multiplier;
        var Colocado = GameObject.Find("Green");
        var ask = Colocado.transform;
        var padre = ask.parent.gameObject.name;
        
        if((Spo2.multiplier*97/100<= 92 && containerCannula.transform.childCount > 0 &&oxiFlow.value == 3 && Spo2Cambio>=0)|| (containerVenturi.transform.childCount > 0 && oxiFlow.value == 6 && padre=="Final-Converter" && Spo2Cambio>=0 && Spo2.multiplier*97/100> 92) )
        {
            tratGood+=Time.deltaTime;
            //Debug.Log("111111 "+tratGood);
        }else if (Spo2Cambio<0)
        {
            tratBad+=Time.deltaTime;
            //Debug.Log("22222 "+tratBad);
        }else{
            tratMedium+=Time.deltaTime;
            //Debug.Log("33333 "+tratMedium);
        }
        if (!pausedMenu.isPaused)
        {
        TiempoMuerto += Time.deltaTime;
        if (containerCannula.transform.childCount > 0 && oxiFlow.value >= 1)
        {
            var oxigeno=oxiFlow.value;
            if (oxiFlow.value>6)
            {
                oxigeno = 6;
            }
            entrada = oxigeno * 0.03f + 0.21f;
        }
        else if (containerVenturi.transform.childCount > 0 && oxiFlow.value >= 1)
        {
            var ColocadoPregunta = GameObject.Find("Final-Converter");
            var Pregunta = ColocadoPregunta.transform;
            Pregunta = Pregunta.GetChild(0);
            var GOPregunta = Pregunta.gameObject;
            switch (GOPregunta.name)
            {
                case "Green":
                    PorcentConverter = 0.35f;
                    entrada=0.17f+(oxiFlow.value*0.03f);
                    if (oxiFlow.value>6)
                    {
                        entrada=0.35f;
                    }
                    break;
                case "Blue":
                    PorcentConverter = 0.24f;
                    entrada=0.18f+(oxiFlow.value*0.03f);
                    if (oxiFlow.value>2)
                    {
                        entrada=0.24f;
                    }
                    break;
                case "Yellow":
                    PorcentConverter = 0.28f;
                    entrada=0.06f+(oxiFlow.value*0.03f);
                    if (oxiFlow.value>4)
                    {
                        entrada=0.28f;
                    }
                    break;
                case "White":
                    PorcentConverter = 0.31f;
                    entrada=0.08f+(oxiFlow.value*0.03f);
                    if (oxiFlow.value>4)
                    {
                        entrada=0.31f;
                    }
                    break;
                case "Red":
                    PorcentConverter = 0.4f;
                    entrada=0.16f+(oxiFlow.value*0.03f);
                    if (oxiFlow.value>8)
                    {
                        entrada=0.4f;
                    }
                    break;
                case "Orange":
                    PorcentConverter = 0.5f;
                    entrada=0.24f+(oxiFlow.value*0.03f);
                    if (oxiFlow.value>12)
                    {
                        entrada=0.5f;
                    }
                    break;
            }
        }else{
            entrada=0;
        }
        if (entrada<0 && oxiFlow.value<1)
        {
            entrada=0f;
        }
        if (TiempoMuerto>180)
        {

            empeorar*=2;
            if (empeorar>12)
            {
                if (quiz.tiempoTotal<=930)
                {
                    empeorar=12;
                }else{
                    empeorar*=2;
                }
                
            }
            TiempoMuerto=0;
        }
        //if (entrada <=0.21f)
        //{
        //   entrada=0.21f;
        //}
        PorcentConverter=Spo2.multiplier;
        //t+=Time.deltaTime/(5000*difer);
        //Spo2.multiplier = Mathf.Lerp(Spo2.multiplier, (0.0302f*(float)Math.Pow(100*entrada,2f)-0.763f*100*entrada+87.667f)*100/97, t);
        //if (entrada!=entradaOld)
        //{
        //    t=0;
        //    difer=Mathf.Abs(Spo2.multiplier*97/100-(0.0302f*(float)Math.Pow(100*entrada,2f)-0.763f*100*entrada+87.667f));
        //}
        //entradaOld=entrada;
        Spo2.multiplier += (40*entrada - empeorar)/6000;
        if (Spo2.multiplier*97/100>= 92 && entrada<0.3)
        {
                Spo2.multiplier=PorcentConverter-empeorar/6000;
        }
        if (Spo2.multiplier*97/100>=98)
        {
            pausedMenu.GoodGame();
        }else if (Spo2.multiplier*97/100<=85)
        {
            pausedMenu.EndGame();
        }
        if (Spo2.multiplier *97/100<=88)
        {
            Bpm.multiplier= (80+(88-Spo2.multiplier*97/100)*10)/73f;
        }
        RR.multiplier=(-86.4f*Mathf.Log(Spo2.multiplier*97/100)+414.11f)/12;
        if (containerCannula.transform.childCount > 0)
        {
            if (containerHumidifier.transform.childCount > 0)
            {
                CH_InicioCannula.SetActive(true);
                CH_FinalCannula.SetActive(true);
                C_InicioCannula.SetActive(false);
                C_FinalCannula.SetActive(false);
            }
            else
            {
                CH_InicioCannula.SetActive(false);
                CH_FinalCannula.SetActive(false);
                C_InicioCannula.SetActive(true);
                C_FinalCannula.SetActive(true);
            }
            if (Cannula == false)
            {
                //  CambioInicialMedioBueno();
            }

        }
        if (containerVenturi.transform.childCount > 0)
        {
            if (containerHumidifier.transform.childCount > 0)
            {
                CH_InicioVenturi.SetActive(true);
                CH_FinalVenturi.SetActive(true);
                C_InicioVenturi.SetActive(false);
                C_FinalVenturi.SetActive(false);
            }
            else
            {
                CH_InicioVenturi.SetActive(false);
                CH_FinalVenturi.SetActive(false);
                C_InicioVenturi.SetActive(true);
                C_FinalVenturi.SetActive(true);
            }
            Cannula = false;
            //CambioBueno();
        }
        if (containerVenturi.transform.childCount == 0 && containerCannula.transform.childCount == 0)
        {
            C_InicioCannula.SetActive(false);
            C_FinalCannula.SetActive(false);
            C_InicioVenturi.SetActive(false);
            C_FinalVenturi.SetActive(false);
            CH_InicioCannula.SetActive(false);
            CH_FinalCannula.SetActive(false);
            CH_InicioVenturi.SetActive(false);
            CH_FinalVenturi.SetActive(false);
        }
        if (TiempoMuerto > 30)
        {
            //CambioMedioMalo();
        }
        Spo2Cambio=Spo2.multiplier-Spo2Previo;
        Debug.Log("111111 "+Mathf.Sign(Spo2Cambio)*1);
        if (Mathf.Sign(Spo2Cambio)*1==Mathf.Sign(lastSpo2Cambio)*1)
        {
            tempoEstado+=Time.deltaTime;
            Debug.Log("22222 "+tempoEstado);
        }else{
            tempoEstado=0;
        }
        lastSpo2Cambio=Spo2Cambio;
        }
    }
    void CambioInicialMedioBueno()
    {
        if (previousStep != 1)
        {
            t = 0;
            previousStep = 1;
            Spo2Previo = Spo2.multiplier;
            BpmPrevio = Bpm.multiplier;
        }
        t += Time.deltaTime * oxiFlow.value / 100;
        Spo2.multiplier = Mathf.Lerp(Spo2Previo, 100 * MBSpo2 / 97f, (Mathf.Exp(t) / 1.359f) - 1 / 1.359f);
        Bpm.multiplier = Mathf.Lerp(BpmPrevio, MBBpm / 73f, (Mathf.Exp(t) / 1.359f) - 1 / 1.359f);
        if (t >= 1)
        {
            t = 0;
            ActualStep.ActualStep = 4;
            Cannula = true;
            Spo2Previo = Spo2.multiplier;
            BpmPrevio = Bpm.multiplier;
        }
    }
    void CambioMedioMalo()
    {
        if (previousStep != 2)
        {
            t = 0;
            previousStep = 2;
            Spo2Previo = Spo2.multiplier;
            BpmPrevio = Bpm.multiplier;
        }

        t += Time.deltaTime / 200;
        Spo2.multiplier = Mathf.Lerp(Spo2Previo, 100 * MMSpo2 / 97f, t);
        Bpm.multiplier = Mathf.Lerp(BpmPrevio, MMBpm / 73f, t);
        previousStep = ActualStep.ActualStep;

        if (Spo2.multiplier <= 97 * MMSpo2 / 100)
        {
            pausedMenu.EndGame();
        }
    }
    void CambioBueno()
    {
        if (previousStep != 3)
        {
            t = 0;
            previousStep = 3;
            Spo2Previo = Spo2.multiplier;
            BpmPrevio = Bpm.multiplier;
        }
        var ColocadoPregunta = GameObject.Find("Final-Converter");
        var Pregunta = ColocadoPregunta.transform;
        Pregunta = Pregunta.GetChild(0);
        var GOPregunta = Pregunta.gameObject;
        switch (GOPregunta.name)
        {
            case "Green":
                PorcentConverter = 0.35f;
                break;
            case "Blue":
                PorcentConverter = 0.24f;
                break;
            case "Yellow":
                PorcentConverter = 0.28f;
                break;
            case "White":
                PorcentConverter = 0.31f;
                break;
            case "Red":
                PorcentConverter = 0.4f;
                break;
            case "Orange":
                PorcentConverter = 0.5f;
                break;
        }


        t += Time.deltaTime * oxiFlow.value * PorcentConverter / 100;
        Spo2.multiplier = Mathf.Lerp(Spo2Previo, 100 * BSpo2 / 97f, (Mathf.Exp(t) / 1.359f) - 1 / 1.359f);
        Bpm.multiplier = Mathf.Lerp(BpmPrevio, BBpm / 73f, (Mathf.Exp(t) / 1.359f) - 1 / 1.359f);
        if (Spo2.multiplier >= 97 * BSpo2 / 100)
        {
            pausedMenu.GoodGame();
        }
    }
}
