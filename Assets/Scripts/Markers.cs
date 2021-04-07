using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Markers : MonoBehaviour
{
    // Start is called before the first frame update
    private Image iconimg;
    private Text distanceText;

    public Transform player;
    public Transform target;
    public Camera cam;
    public float closeEnoughDist;
    void Start()
    {
        iconimg = GetComponent<Image>();
        distanceText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            GetDistance();
            CheckOnScreen();
        }
    }
    private void GetDistance()
    {
        float dist = Vector3.Distance(player.position, target.position);
        distanceText.text = dist.ToString("f1")+"m";

        if (dist<closeEnoughDist)
        {
            ToggleUI(false);
        }
    }
    private void CheckOnScreen()
    {
        float thing = Vector3.Dot((target.position - cam.transform.position).normalized, cam.transform.forward);
        float dist = Vector3.Distance(player.position, target.position);
        if (thing< 0)
        {
            ToggleUI(false);
        }
        else if (dist>closeEnoughDist)
        {
            ToggleUI(true);
            transform.position = cam.WorldToScreenPoint(target.position);
        }
    }
    private void ToggleUI(bool _value)
    {
        iconimg.enabled =_value;
        distanceText.enabled =_value;
    }
}
