using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public PulseDataNumberRenderer heartRate;
    private string heartRateText;
    private float beats, beatsNorm, t, lastT=0;

    public Sound[] sounds;

    // Start is called before the first frame update
    void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    void Update()
    {
        t += Time.deltaTime;
        beats = heartRate.multiplier * 73f;
        beatsNorm = 60f / beats;
        Sound s = Array.Find(sounds, sound => sound.name == "HeartBeat");
        if (t - lastT > beatsNorm)
        {
            lastT = t;
            s.source.Play();
        }
        
    }
}
