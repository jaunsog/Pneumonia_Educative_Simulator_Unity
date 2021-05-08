using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxyMeasure : MonoBehaviour
{

    public Transform target;
    public Transform infLimit;
    public Transform supLimit;
    public float t;
    public int listo;
    private Vector3 a,b;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        if (listo==1)
        {
            t+=Time.deltaTime;
            transform.position = Vector3.Lerp(a, b, t);
            if (t>=1)
            {
                listo=0;
            }
        }
       
    }
    // Update is called once per frame
    public void ChangeMeasure()
    {
        a = transform.position;
        b = target.position;
        listo=1;
        t=0;
    }
}
