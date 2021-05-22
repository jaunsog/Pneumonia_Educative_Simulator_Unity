using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedAlarm : MonoBehaviour
{

    public Light redAlarm;
    public AudioSource AlarmSound;
    public AudioClip LightAudio;
    public float triggerAlarm = 0f;

    private float beats, beatsNorm, t, lastT = 0;

    public PulseDataNumberRenderer spO2;
    // Start is called before the first frame update
    void Start()
    {
        redAlarm.enabled = false;
        triggerAlarm = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        BlinkingLight();
    }

    void BlinkingLight()
    {
        //if (Timer >0)
        //{
        //    Timer -= Time.deltaTime;
        //}
        //if (Timer <=0)
        //{
        //    redAlarm.enabled = !redAlarm.enabled;
        //    Timer = Random.Range(MinTime, MaxTime);

        //}
        t += Time.deltaTime;
        beats = spO2.multiplier * 97f/100f;
        beatsNorm = 100f / beats;
        if (t - lastT > beatsNorm)
        {
            lastT = t;
            if (beats<triggerAlarm)
            {
                redAlarm.enabled = !redAlarm.enabled;
                AlarmSound.PlayOneShot(LightAudio);
            }else
            {
                redAlarm.enabled = false;
            }
            
        }
    }
}
