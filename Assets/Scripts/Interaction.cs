using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public PulseDataNumberRenderer Bpm;
    public PulseDataNumberRenderer Spo2;
    public PulseDataNumberRenderer Tblood;
    public PulseDataNumberRenderer UNibp;
    public PulseDataNumberRenderer DNibp;
    public PulseDataNumberRenderer MNibp;
    public float DesiredBpm = 80f;
    public float DesiredSpo2= 89f;
    public float DesiredTblood= 37.2f;
    public float DesiredUNibp= 110f;
    public float DesiredDNibp= 70f;

    // Start is called before the first frame update
    void Start()
    {
       Bpm.multiplier*=DesiredBpm/73f;
       Spo2.multiplier*=DesiredSpo2/97f;
       Tblood.multiplier*=DesiredTblood/33f;
       UNibp.multiplier*=DesiredUNibp/114f;
       DNibp.multiplier*=DesiredDNibp/73f;
       MNibp.multiplier=(UNibp.multiplier+UNibp.multiplier)/2f;
    }

}
