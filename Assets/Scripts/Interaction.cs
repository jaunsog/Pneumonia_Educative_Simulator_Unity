using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Interaction : MonoBehaviour
{
    public PulseDataNumberRenderer Bpm;
    public PulseDataNumberRenderer Spo2;
    public PulseDataNumberRenderer Tblood;
    public PulseDataNumberRenderer UNibp;
    public PulseDataNumberRenderer DNibp;
    public PulseDataNumberRenderer MNibp;
    public PulseDataLineRenderer xRangeECG;
    public PulseDataLineRenderer xRangeABP;
    public PulseDataLineRenderer xRangeCO2;
    public float DesiredBpm = 80f;
    public float DesiredSpo2= 89f;
    public float DesiredTblood= 37.2f;
    public float DesiredUNibp= 110f;
    public float DesiredDNibp= 70f;
    public float MBBpm=80f;
    public float MMBpm=110f;
    public float BBpm=80f;
    public float MBSpo2=92f;
    public float MMSpo2=85f;
    public float BSpo2=98f;
    public Aiming ActualStep;
    private bool controlador=false;
    public TMP_Text Text;
    private float tiempoPrevio=10000000;
    private float t;
    public float Spo2Previo,BpmPrevio, previousStep;

    // Start is called before the first frame update
    void Start()
    {
       ActualStep.ActualStep=0;
       xRangeECG.xRange=xRangeECG.xRange*73f/DesiredBpm;
       xRangeCO2.xRange=xRangeCO2.xRange*97f/DesiredSpo2;
       Bpm.multiplier*=DesiredBpm/73f;
       Spo2.multiplier*=DesiredSpo2/97f;
       Tblood.multiplier*=DesiredTblood/33f;
       UNibp.multiplier*=DesiredUNibp/114f;
       DNibp.multiplier*=DesiredDNibp/73f;
       MNibp.multiplier=(UNibp.multiplier+UNibp.multiplier)/2f;
    }
    void Update()
    {   
    
        if(ActualStep.ActualStep==0)
        {
            Text.text="Analiza el estado del paciente";
            previousStep=ActualStep.ActualStep;
            Spo2Previo=Spo2.multiplier;
            BpmPrevio=Bpm.multiplier;
        }
        if(Time.time>5&&ActualStep.ActualStep==0)
        {
            ActualStep.ActualStep=1;
            Text.text="¡Realiza el tratamiento adecuado!";
        }
        if (ActualStep.ActualStep==3)
        {
           Text.text="Analiza el estado del paciente";
           CambioInicialMedioBueno();
        }
        if ((ActualStep.ActualStep>0&&ActualStep.ActualStep<3)||(ActualStep.ActualStep>=4&&ActualStep.ActualStep<=7))
        {
            CambioMedioMalo();
        }
        if (ActualStep.ActualStep>=8)
        {
            CambioBueno();
        }
    }
    void CambioInicialMedioBueno()
    {
        if (previousStep ==2)
        {
            t=0;
            previousStep=3;
            Spo2Previo=Spo2.multiplier;
            BpmPrevio=Bpm.multiplier;
        }
        t+=Time.deltaTime / 50;
        Spo2.multiplier= Mathf.Lerp(Spo2Previo, 100*MBSpo2/97f, (Mathf.Exp(t)/1.359f)-1/1.359f);
        Bpm.multiplier= Mathf.Lerp(BpmPrevio, MBBpm/73f, (Mathf.Exp(t)/1.359f)-1/1.359f);
        if (t>=1)
        {
            t=0;
            ActualStep.ActualStep=4;
            Spo2Previo=Spo2.multiplier;
            BpmPrevio=Bpm.multiplier;
        }  
    }
    void CambioMedioMalo()
    {
        t+=Time.deltaTime / 50;
        Spo2.multiplier= Mathf.Lerp(Spo2Previo, 100*MMSpo2/97f, (Mathf.Exp(t)/2.718f)-1/1.359f);
        Bpm.multiplier= Mathf.Lerp(BpmPrevio, MMBpm/73f, (Mathf.Exp(t)/2.718f)-1/1.359f);
        previousStep=ActualStep.ActualStep;
    }
    void CambioBueno()
    {
        if (previousStep ==7)
        {
            t=0;
            previousStep=100;
            Spo2Previo=Spo2.multiplier;
            BpmPrevio=Bpm.multiplier;
        }
        t+=Time.deltaTime / 50;
        Spo2.multiplier= Mathf.Lerp(Spo2Previo, 100*BSpo2/97f, (Mathf.Exp(t)/1.359f)-1/1.359f);
        Bpm.multiplier= Mathf.Lerp(BpmPrevio, BBpm/73f, (Mathf.Exp(t)/1.359f)-1/1.359f);
    } 
}
