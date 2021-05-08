using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Aiming : MonoBehaviour
{
    [SerializeField] private Material highlightMaterial;
    private Renderer selectionRenderer, RendererContainer;
    public Material realMaterial, MaterialContainer, Transparent;
    private bool elegido = false;
    private bool equipped = false;
    public Transform gunContainer, fpsCam, selection, Item, TransformContainer, ActualParent, fullVenturi;
    public Vector3 distanceToPlayer;
    private Collider ItemCollider;
    private Rigidbody ItemRigidbody;
    public Int64 ActualStep = 1;
    private String[] NoContainers;
    private Vector3 PreviousPosition;
    private Quaternion PreviousRotation;
    public bool Hitting;
    public GameObject CableInicioCannula, CableFinalCannula, CableInicioVenturi, CableFinalVenturi;
    [SerializeField]
    private string mensajeSalteableBoton;

    private Instructions instr;
    private UI ui;
    private Camera_Controller cam;
    private Player_Controller player;
    private bool menuactive = false;

    // Start is called before the first frame update
    void Start()
    {
        instr = FindObjectOfType<Instructions>();
        ui = FindObjectOfType<UI>();
        cam = FindObjectOfType<Camera_Controller>();
        player = FindObjectOfType<Player_Controller>();
        ui.MostrarMensajeSalteableBoton("init");
        cam.UnlockMouse();
        player.speed = 0f;
        menuactive = true;
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (ActualStep == 0)
        {
            var Activados = GameObject.FindGameObjectsWithTag("Activable.1");
            foreach (GameObject Activado in Activados)
            {
                Activado.gameObject.SetActive(false);
            }
            Activados = GameObject.FindGameObjectsWithTag("Activable.2");
            foreach (GameObject Activado in Activados)
            {
                Activado.gameObject.SetActive(false);
            }

        }
        Hitting = Physics.Raycast(ray, out hit);
        if (Physics.Raycast(ray, out hit))
        {
            if ((selection == hit.collider.transform || selection == null) && !menuactive && hit.collider.transform.tag != "No")
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
                instr.showInspect("Press F to inspect");
                if (Input.GetKeyDown(KeyCode.F))
                {
                    cam.UnlockMouse();
                    player.speed = 0f;
                    ui.MostrarMensajeSalteableBoton(selection.name);
                    menuactive = true;
                }
                if (hit.collider.transform.tag != "Read")
                {
                    instr.showInteract("Press E to interact");

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        PickUp();
                    }
                }

            }
            if (equipped)
            {
                instr.showCancel("Press R to cancel selection");

                if (Input.GetKeyDown(KeyCode.R))
                {
                    Cancel();
                }

            }
            if (equipped && selection.tag == "Container." + Item.tag)
            {
                instr.showInteract("Press Q to place");
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    Place();
                }
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
        player.speed = 8f;
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

                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        cam.UnlockMouse();
                        player.speed = 0f;
                        ui.MostrarMensajeSalteableBoton(selection.name);
                        menuactive = true;
                    }
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
        if (selection.name != "FullVentury")
        {
            fullVenturi.gameObject.layer = 2;
        }
        if (ActualStep == 7)
        {
            CableInicioCannula.SetActive(false);
            CableFinalCannula.SetActive(false);
        }
        Item = selection;
        Item.gameObject.layer = 2;
        PreviousPosition = Item.position;
        PreviousRotation = Item.rotation;
        Item.SetParent(gunContainer);
        Item.position = Item.position - new Vector3(-0.3f, -0.1f, 0);
        ItemCollider.isTrigger = true;
        selection = null;
        if (Item.tag == "5")
        {
            var ColocadoPregunta = GameObject.Find("Final-Tube");
            var Pregunta = ColocadoPregunta.transform;
            if (Pregunta.childCount >0)
            {
                var Containers = GameObject.FindGameObjectsWithTag("Container.5");
                foreach (GameObject Container in Containers)
                {
                    Container.layer = 0;
                    TransformContainer = Container.transform;
                    RendererContainer = Container.GetComponent<Renderer>();
                    MaterialContainer = RendererContainer.material;
                    RendererContainer.material = highlightMaterial;
                }
            }
        }
        else if (Item.tag == "6")
        {
            var ColocadoPregunta = GameObject.Find("Final-Converter");
            var Pregunta = ColocadoPregunta.transform;
            if (Pregunta.childCount >0)
            {
                var Containers = GameObject.FindGameObjectsWithTag("Container."+Item.tag);
                foreach (GameObject Container in Containers)
                {
                    Container.layer = 0;
                    TransformContainer = Container.transform;
                    RendererContainer = Container.GetComponent<Renderer>();
                    MaterialContainer = RendererContainer.material;
                    RendererContainer.material = highlightMaterial;
                }
            }
        }
        else if(Item.tag == "8")
        {
            var ColocadoPregunta = GameObject.Find("Final-Protector");
            var Pregunta1 = ColocadoPregunta.transform;
            ColocadoPregunta = GameObject.Find("Final-Cannula");
            var Pregunta2= ColocadoPregunta.transform;
            if (Pregunta1.childCount >1 && Pregunta2.childCount<=0)
            {
                var Containers = GameObject.FindGameObjectsWithTag("Container."+Item.tag);
                foreach (GameObject Container in Containers)
                {
                    Container.layer = 0;
                    TransformContainer = Container.transform;
                    RendererContainer = Container.GetComponent<Renderer>();
                    MaterialContainer = RendererContainer.material;
                    RendererContainer.material = highlightMaterial;
                }
            }
        }
        else if(Item.tag == "2")
        {
            
            var ColocadoPregunta = GameObject.Find("Final-FullVentury");
            var Pregunta2= ColocadoPregunta.transform;
            if (Pregunta2.childCount<=0)
            {
                var Containers = GameObject.FindGameObjectsWithTag("Container."+Item.tag);
                foreach (GameObject Container in Containers)
                {
                    Container.layer = 0;
                    TransformContainer = Container.transform;
                    RendererContainer = Container.GetComponent<Renderer>();
                    MaterialContainer = RendererContainer.material;
                    RendererContainer.material = highlightMaterial;
                }
            }
        }
        else
        {
            var Containers = GameObject.FindGameObjectsWithTag("Container." + Item.tag);
            foreach (GameObject Container in Containers)
            {
                Container.layer = 0;
                TransformContainer = Container.transform;
                RendererContainer = Container.GetComponent<Renderer>();
                MaterialContainer = RendererContainer.material;
                RendererContainer.material = highlightMaterial;
            }
        }


    }
    private void Cancel()
    {
        fullVenturi.gameObject.layer = 0;
        equipped = !equipped;
        ItemCollider.isTrigger = false;
        
        var Containers = GameObject.FindGameObjectsWithTag("Container." + Item.tag);
        foreach (GameObject Container in Containers)
        {
            Container.layer = 2;
            TransformContainer = Container.transform;
            RendererContainer = Container.GetComponent<Renderer>();
            MaterialContainer = RendererContainer.material;
            RendererContainer.material = Transparent;
        }
        Item.SetParent(null);
        Item.position = PreviousPosition;
        Item.rotation = PreviousRotation;
        Item.gameObject.layer = 0;
        if (ActualStep == 7)
        {
            CableInicioCannula.SetActive(true);
            CableFinalCannula.SetActive(true);
        }
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
        fullVenturi.gameObject.layer = 0;
        equipped = !equipped;
       
        var Containers = GameObject.FindGameObjectsWithTag("Container." + Item.tag);
        foreach (GameObject Container in Containers)
        {
            Container.layer = 2;
            TransformContainer = Container.transform;
            RendererContainer = Container.GetComponent<Renderer>();
            MaterialContainer = RendererContainer.material;
            RendererContainer.material = Transparent;
        }
        if (Item.tag == "5")
        {
            try
            {
                var isChild = selection.GetChild(0);
                if (isChild != null)
                {
                    var lastConverter = selection.GetChild(0);
                    var originalPosition = GameObject.Find("Container-" + lastConverter.name);
                    lastConverter.SetParent(null);
                    originalPosition.layer = 0; 
                    lastConverter.position = originalPosition.transform.position;
                    lastConverter.rotation = originalPosition.transform.rotation;
                }

            }
            catch { }
        }
        Item.SetParent(null);
         if ( selection.gameObject.name!="Final-Tube" && selection.gameObject.name!="Final-Converter" && selection.gameObject.name!="Final-Protector")
        {
            Item.gameObject.layer = 0;
        }
        
        selection.gameObject.layer = 2;
        Item.position = selection.position;
        Item.rotation = selection.rotation;
        ItemCollider.isTrigger = false;
        Item.SetParent(selection);
        ActualStep = ActualStep + 1;
        // if (ActualStep == 3)
        // {
        //     CableInicioCannula.SetActive(true);
        //     CableFinalCannula.SetActive(true);
        // }
        // if (ActualStep == 9)
        // {
        //     CableInicioVenturi.SetActive(true);
        //     CableFinalVenturi.SetActive(true);
        // }
        // if (ActualStep == 7)
        // {
        //     var Viejos = GameObject.FindGameObjectsWithTag("2");
        //     foreach (GameObject Viejo in Viejos)
        //     {
        //         Viejo.tag = "7";
        //     }
        // }
        // if (ActualStep == 8)
        // {
        //     var Activados = GameObject.FindGameObjectsWithTag("8");
        //     foreach (GameObject Activado in Activados)
        //     {
        //         Activado.layer = 0;
        //     }
        //     Activados = GameObject.FindGameObjectsWithTag("Untagged");
        //     foreach (GameObject Activado in Activados)
        //     {
        //         Activado.layer = 2;
        //     }

        //     var Contenedores = GameObject.FindGameObjectWithTag("4");

        //     Contenedores.layer = 2;

        //     Contenedores = GameObject.FindGameObjectWithTag("5");

        //     Contenedores.layer = 2;

        //     Contenedores = GameObject.FindGameObjectWithTag("6");

        //     Contenedores.layer = 2;

        // }
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