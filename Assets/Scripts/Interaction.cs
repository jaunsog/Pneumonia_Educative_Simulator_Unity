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
    public float DesiredSpo2 = 89f;
    public float DesiredTblood = 37.2f;
    public float DesiredUNibp = 110f;
    public float DesiredDNibp = 70f;
    public float MBBpm = 80f;
    public float MMBpm = 110f;
    public float BBpm = 80f;
    public float MBSpo2 = 92f;
    public float MMSpo2 = 85f;
    public float BSpo2 = 98f;
    public Aiming ActualStep;
    private bool controlador = false;
    public TMP_Text Text;
    private float tiempoPrevio = 10000000;
    private float t = 0;
    private float TiempoMuerto;
    public float Spo2Previo, BpmPrevio, previousStep;
    public PausedMenu pausedMenu;
    public GameObject C_InicioCannula, C_FinalCannula, C_InicioVenturi, C_FinalVenturi, CH_InicioCannula, CH_FinalCannula, CH_InicioVenturi, CH_FinalVenturi;
    // Start is called before the first frame update
    public ReadInput oxiFlow;
    public Transform containerCannula, containerVenturi, containerHumidifier;
    void Start()
    {
        ActualStep.ActualStep = 0;
        xRangeECG.xRange = xRangeECG.xRange * 73f / DesiredBpm;
        xRangeCO2.xRange = xRangeCO2.xRange * 97f / DesiredSpo2;
        Bpm.multiplier *= DesiredBpm / 73f;
        Spo2.multiplier *= DesiredSpo2 / 97f;
        Tblood.multiplier *= DesiredTblood / 33f;
        UNibp.multiplier *= DesiredUNibp / 114f;
        DNibp.multiplier *= DesiredDNibp / 73f;
        MNibp.multiplier = (UNibp.multiplier + UNibp.multiplier) / 2f;
        C_InicioCannula.SetActive(false);
        C_FinalCannula.SetActive(false);
        C_InicioVenturi.SetActive(false);
        C_FinalVenturi.SetActive(false);
        CH_InicioCannula.SetActive(false);
        CH_FinalCannula.SetActive(false);
        CH_InicioVenturi.SetActive(false);
        CH_FinalVenturi.SetActive(false);
    }
    void Update()
    {
        TiempoMuerto += Time.deltaTime;
        if (ActualStep.ActualStep == 0)
        {
            previousStep = ActualStep.ActualStep;
            Spo2Previo = Spo2.multiplier;
            BpmPrevio = Bpm.multiplier;
            ActualStep.ActualStep = 1;
            //Text.text="¡Realiza el tratamiento adecuado!";
        }

        if (containerCannula.transform.childCount >0)
        {
            if (containerHumidifier.transform.childCount >0)
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
            CambioInicialMedioBueno();
            TiempoMuerto = 0;
        }
        else if (containerVenturi.transform.childCount >0)
        {
            if (containerHumidifier.transform.childCount >0)
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
            CambioBueno();
            TiempoMuerto = 0;
        }
        else
        {
            C_InicioCannula.SetActive(false);
            C_FinalCannula.SetActive(false);
            C_InicioVenturi.SetActive(false);
            C_FinalVenturi.SetActive(false);
            CH_InicioCannula.SetActive(false);
            CH_FinalCannula.SetActive(false);
            CH_InicioVenturi.SetActive(false);
            CH_FinalVenturi.SetActive(false);
            if (TiempoMuerto>60)
            {
                CambioMedioMalo();
            }
            
        }
        // if (TiempoMuerto>60&&ActualStep.ActualStep<=7)
        // {
        //     CambioMedioMalo();
        // }
        // if (ActualStep.ActualStep>=8)
        // {
        //     CambioBueno();
        // }
    }
    void CambioInicialMedioBueno()
    {
        if (previousStep == 2)
        {
            t = 0;
            previousStep = 3;
            Spo2Previo = Spo2.multiplier;
            BpmPrevio = Bpm.multiplier;
        }
        t += Time.deltaTime / 50;
        Spo2.multiplier = Mathf.Lerp(Spo2Previo, 100 * MBSpo2 / 97f, (Mathf.Exp(t) / 1.359f) - 1 / 1.359f);
        Bpm.multiplier = Mathf.Lerp(BpmPrevio, MBBpm / 73f, (Mathf.Exp(t) / 1.359f) - 1 / 1.359f);
        if (t >= 1)
        {
            t = 0;
            ActualStep.ActualStep = 4;
            Spo2Previo = Spo2.multiplier;
            BpmPrevio = Bpm.multiplier;
        }
    }
    void CambioMedioMalo()
    {
        t += Time.deltaTime / 10;
        Spo2.multiplier = Mathf.Lerp(Spo2Previo, 100 * MMSpo2 / 97f, t);
        Bpm.multiplier = Mathf.Lerp(BpmPrevio, MMBpm / 73f, t);
        previousStep = ActualStep.ActualStep;
        if (t >= 1)
        {
            pausedMenu.EndGame();
        }
    }
    void CambioBueno()
    {
        if (previousStep == 7)
        {
            t = 0;
            previousStep = 100;
            Spo2Previo = Spo2.multiplier;
            BpmPrevio = Bpm.multiplier;
        }
        t += Time.deltaTime / 50;
        Spo2.multiplier = Mathf.Lerp(Spo2Previo, 100 * BSpo2 / 97f, (Mathf.Exp(t) / 1.359f) - 1 / 1.359f);
        Bpm.multiplier = Mathf.Lerp(BpmPrevio, BBpm / 73f, (Mathf.Exp(t) / 1.359f) - 1 / 1.359f);
        if (t >= 1)
        {
            pausedMenu.GoodGame();
        }
    }
}
