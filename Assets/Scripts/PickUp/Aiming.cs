﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Aiming : MonoBehaviour
{
    [SerializeField] private Material highlightMaterial;
    private Renderer selectionRenderer, RendererContainer;
    public Material realMaterial, MaterialContainer;
    private bool elegido = false;
    private bool equipped = false;
    public Transform gunContainer, fpsCam, selection, Item, TransformContainer, ActualParent;
    public Vector3 distanceToPlayer;
    private Collider ItemCollider;
    private Rigidbody ItemRigidbody;
    public Int64 ActualStep = 1;
    private String[] NoContainers;
    private Vector3 PreviousPosition;
    private Quaternion PreviousRotation;
    public bool Hitting;

    [SerializeField]
    private string mensajeSalteableBoton;

    private Instructions instr;
    private UI ui;
    private Camera_Controller cam;
    private bool menuactive = false;


    // Start is called before the first frame update
    void Start()
    {
        instr = FindObjectOfType<Instructions>();
        ui = FindObjectOfType<UI>();
        cam = FindObjectOfType<Camera_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Hitting = Physics.Raycast(ray, out hit);
        if (Physics.Raycast(ray, out hit))
        {
            if ((selection == hit.collider.transform || selection == null) && !menuactive)
            {
                
                Aim(hit);
            }
            else if (elegido)
            {
                if (NoContainers[0] != "Container")
                {
                    selectionRenderer.material = realMaterial;
                }
                selection = null;
                elegido = !elegido;
            }
            if (!equipped && selection != null)
            {
                //Debug.Log(selection.name);
                if (selection.tag != ActualStep.ToString())
                {
                    instr.showInspect("Press F to inspect");
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        cam.UnlockMouse();
                        ui.MostrarMensajeSalteableBoton(selection.name);
                        menuactive = true;
                    }
                }
                else
                if (selection.tag == ActualStep.ToString())
                {
                    instr.showInteract("Press E to interact");
                    instr.showInspect("Press F to inspect");
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        PickUp();
                    }
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        cam.UnlockMouse();
                        ui.MostrarMensajeSalteableBoton(selection.name);
                        menuactive = true;
                    }
                }

            }
            if (equipped)
            {
                instr.showCancel("Press R to cancel selection");
                instr.showInteract("Press Q to place");
                if (Input.GetKeyDown(KeyCode.R))
                {
                    Cancel();
                }

            }

            if (equipped && Input.GetKeyDown(KeyCode.Q) && selection.tag == "Container." + ActualStep.ToString())
            {
                Place();
            }

        }
        else if (elegido)
        {
            selection = null;
            elegido = false;
            if (NoContainers[0] != "Container")
            {
                selectionRenderer.material = realMaterial;
            }
        }
        else if (equipped)
        {
            instr.showCancel("Press R to cancel selection");
            if (Input.GetKeyDown(KeyCode.R))
            {
                Cancel();
            }

        }
    }

    public void Menu()
    {
        menuactive = false;
    }

    private void Aim(RaycastHit hit)
    {

        distanceToPlayer = hit.collider.transform.position - transform.position;
        if (distanceToPlayer.magnitude <= 10)
        {
            if (!elegido)
            {
                selection = hit.collider.transform; //saves the aimed object
                selectionRenderer = selection.GetComponent<Renderer>();
                realMaterial = selectionRenderer.material; //saves original color
                elegido = true;
                NoContainers = selection.tag.Split('.');
                if (!equipped && NoContainers[0] != "Container")
                {
                    selectionRenderer.material = highlightMaterial; //changes to "highlight" color
                }
            }
        }
        else if (elegido)
        {
            elegido = !elegido;
            if (NoContainers[0] != "Container")
            {
                selectionRenderer.material = realMaterial;
            }
            selection = null;
        }
    }
    private void PickUp()
    {
        selectionRenderer.material = realMaterial;
        elegido = false;
        equipped = !equipped;
        ItemCollider = selection.GetComponent<Collider>();
        ItemRigidbody = selection.GetComponent<Rigidbody>();
        Item = selection;
        Item.gameObject.layer = 2;
        PreviousPosition = Item.position;
        PreviousRotation = Item.rotation;
        Item.SetParent(gunContainer);
        Item.position = Item.position - new Vector3(-0.3f, -0.1f, 0);
        ItemCollider.isTrigger = true;
        selection = null;
        var Containers = GameObject.FindGameObjectsWithTag("Container." + ActualStep.ToString());
        foreach (GameObject Container in Containers)
        {
            Container.layer = 0;
            TransformContainer = Container.transform;
            RendererContainer = Container.GetComponent<Renderer>();
            MaterialContainer = RendererContainer.material;
            RendererContainer.material = highlightMaterial;
        }

    }
    private void Cancel()
    {
        equipped = !equipped;
        ItemCollider.isTrigger = false;
        RendererContainer.material = MaterialContainer;
        Item.SetParent(null);
        Item.position = PreviousPosition;
        Item.rotation = PreviousRotation;
        Item.gameObject.layer = 0;
    }
    private void Place()
    {
        //if (ActualStep == 4)              Alternativa para convertir un objeto en el siguiente editable al lograr algo
        //{
        //    var Containers = GameObject.FindGameObjectsWithTag("Container." + ActualStep.ToString());
        //    foreach (GameObject Container in Containers)
        //    {
        //        TransformContainer = Container.transform;
        //        RendererContainer = Container.GetComponent<Renderer>();
        //        MaterialContainer = RendererContainer.material;
        //        RendererContainer.material = highlightMaterial;
        //    }
        //    TransformContainer.tag = "5";
        //}
        equipped = !equipped;
        RendererContainer.material = MaterialContainer;
        Item.SetParent(null);
        Item.gameObject.layer = 0;
        selection.gameObject.layer = 2;
        Item.position = selection.position;
        Item.rotation = selection.rotation;
        ItemCollider.isTrigger = false;
        Item.SetParent(selection);
        ActualStep = ActualStep + 1;
        if (ActualStep == 6)
        {
            var Viejos = GameObject.FindGameObjectsWithTag("2");
            foreach (GameObject Viejo in Viejos)
            {
                Viejo.tag = "6";
            }
        }
        if (ActualStep == 7)
        {
            var Activados = GameObject.FindGameObjectsWithTag("7");
            foreach (GameObject Activado in Activados)
            {
                Activado.layer = 0;
            }
        }
        //if (ActualStep == 5)
        //{
        //    var Parents = GameObject.FindGameObjectsWithTag(ActualStep.ToString());
        //    foreach (GameObject Parent in Parents)
        //    {
        //        ActualParent = Parent.transform;
        //    }
        //   for(int i=1;i<ActualStep;i++)
        //    {
        //    var Piezas = GameObject.FindGameObjectsWithTag(i.ToString());
        //    foreach (GameObject Pieza in Piezas)
        //    {
        //        TransformContainer = Pieza.transform;
        //        TransformContainer.SetParent(ActualParent);
        //    }
        //    }
        //}
    }
}