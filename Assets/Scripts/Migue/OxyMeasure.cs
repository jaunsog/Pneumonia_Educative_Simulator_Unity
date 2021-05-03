using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxyMeasure : MonoBehaviour
{

    public Transform target;
    public Transform infLimit;
    public Transform supLimit;
    public float t;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void ChangeMeasure()
    {
        Vector3 a = transform.position;
        Vector3 b = target.position;
        while (t < 1)
        {
            t += Time.deltaTime / 20;
            transform.position = Vector3.Lerp(a, b, t);
        }
        
    }
}
