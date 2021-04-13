using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemScript : MonoBehaviour, IAction
{
    // Start is called before the first frame update
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, gunContainer, fpsCam;
    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;
    public bool equipped=false;
    public bool activated = false;
    public static bool slotFull;
    public Vector3 LastScale;
    public Vector3 LastPosition;
    public Quaternion LastRotation;
    public Transform LastParent;
    void Start()
    {
        if(!equipped)
        {
            rb.isKinematic=false;
            coll.isTrigger = false; 
        }
        if(equipped)
        {
            rb.isKinematic =true;
            coll.isTrigger = true;
            slotFull = true;
        }
        
    }

    // Update is called once per frame
    public void Activate()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull) PickUp();

        if (equipped && Input.GetKeyDown(KeyCode.Mouse0)) Drop();
    }
    void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if(!equipped && distanceToPlayer.magnitude <= pickUpRange&& Input.GetKeyDown(KeyCode.E) && !slotFull) PickUp();

        if(equipped&& Input.GetKeyDown(KeyCode.Mouse0)) Drop();
    }
    private void PickUp()
    {
        //No tomar más objetos
        equipped =true;
        slotFull = true;
        //Child container
        LastPosition=transform.localPosition;
        LastRotation=transform.localRotation;
        LastParent=transform.parent;
        LastScale=transform.localScale;
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        //Crear triggers
        rb.isKinematic=true;
        coll.isTrigger=true;
    }
    private void Drop()
    {
        //No tomar más objetos
        equipped =false;
        slotFull = false;
        //No Child container
        
        transform.SetParent(LastParent);
        //Crear triggers
        rb.isKinematic=false;
        coll.isTrigger=false;
        //Lanzar
        transform.localPosition=LastPosition;
        transform.localRotation=LastRotation;
        transform.localScale=LastScale;
        //rb.AddForce(fpsCam.forward*dropForwardForce, ForceMode.Impulse);
        //rb.AddForce(fpsCam.up*dropUpwardForce, ForceMode.Impulse);

    }
}
