using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    /*ESPAÑOL
     *Solución por GameDevTraum
    * 
    * Artículo: https://gamedevtraum.com/gdt-short/sistema-de-interaccion-base-para-unity/
    * Página: https://gamedevtraum.com/es/
    * Canal: https://youtube.com/c/GameDevTraum
    * 
    * Visita la página para encontrar más soluciones, Assets y artículos
   */

    /*ENGLISH
    *Solution by GameDevTraum
    * 
    * Article: https://gamedevtraum.com/gdt-short/basic-interaction-system-for-unity/
    * Website: https://gamedevtraum.com/en/
    * Channel: https://youtube.com/c/GameDevTraum
    * 
    * Visit the website to find more articles, solutions and assets
    */

    /*DEUTSCH
    *Lösung von GameDevTraum
    * 
    * Artikel: https://gamedevtraum.com/gdt-short/grundlegendes-interaktionssystem-fuer-unity/
    * Webseite: https://gamedevtraum.com/de/
    * Kanal: https://youtube.com/c/GameDevTraum
    * 
    * Besuch die Website, um weitere Artikel, Lösungen und Hilfsmittel zu finden. 
    *
    */
    [SerializeField]
    private float messageTime;

    [SerializeField]
    private GameObject messageGameObject;

    [SerializeField]
    private GameObject messageTextGameObject;

    [SerializeField]
    private GameObject mensajeSalteableBotonObjeto; //Contenedor
    [SerializeField]
    public Text mensajeSalteableBotonTexto; //Objeto Texto
    private bool mensajeSalteableBotonActivo; //Estado del mensaje
    private bool readInputActivo;

    [SerializeField]
    private GameObject inputMessage;


    private Camera_Controller cam;
    private Aiming aim;
    private Setexts descript;
    private ReadInput read;

    private Text messageText;
    private string inspectext;


    void Start()
    {
        messageText = messageTextGameObject.GetComponent<Text>();
        messageGameObject.SetActive(false);
        inputMessage.SetActive(false);
        mensajeSalteableBotonObjeto.SetActive(false);
        cam = FindObjectOfType<Camera_Controller>();
        aim = FindObjectOfType<Aiming>();
        descript = FindObjectOfType<Setexts>();
        read = FindObjectOfType<ReadInput>();
    }

    private void OnGUI()
    {
        if (mensajeSalteableBotonActivo)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                LimpiarMensajeBoton();
                
            }
        }
    }

    void FixedUpdate()
    {
        clearMessage();
    }

    public void showMessage(string message)
    {
        messageText.text = message;
        messageGameObject.SetActive(true);
    }

    public void clearMessage()
    {
        messageGameObject.SetActive(false);
    }


     public void MostrarMensajeSalteableBoton(string mensaje)
    {
        mensajeSalteableBotonActivo = true;
        Debug.Log("UI entering: " + mensaje);
        descript.getName(mensaje);
        inspectext = descript.chooseDefinition();
        mensajeSalteableBotonTexto.text = inspectext;
        mensajeSalteableBotonObjeto.SetActive(true);
        if (mensaje =="Oxigen")
        {
            Debug.Log("Entrando a Presentar Input");
            MostrarMensajeConInput(inspectext);
        }
    }

    public void MostrarMensajeConInput(string mensaje)
    {
        inputMessage.SetActive(true);
        mensajeSalteableBotonTexto.text = mensaje;

        mensajeSalteableBotonObjeto.SetActive(true);
    }

    public void BotonSaltarMensaje()
    {
        if (readInputActivo)
        {
            Debug.Log("Borrando Oxigeno");
            read.ReadOxygen();
            LimpiarMensajeBoton();
        }else
        if (mensajeSalteableBotonActivo)
        {
            LimpiarMensajeBoton();
            
        }
    }

    private void LimpiarMensajeBoton()
    {
        mensajeSalteableBotonActivo = false;
        mensajeSalteableBotonObjeto.SetActive(false);
        inputMessage.SetActive(false);
        cam.LockMouse();
        aim.Menu();
    }

}
