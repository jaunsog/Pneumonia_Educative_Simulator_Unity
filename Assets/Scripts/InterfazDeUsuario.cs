//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class InterfazDeUsuario : MonoBehaviour
//{

//    /* ---- Mostrar mensajes en pantalla por GameDevTraum ----
// * 
// * Esta solución consiste en el Script "InterfazDeUsuario" y el prefabricado "Solucion Mostrar Mensajes - GameDevTraum".
// * El Script "InterfazDeUsuario" provee tres métodos públicos para mostrar mensajes en pantalla.
// * 
// * InterfazDeUsuario.MostrarMensajeSimple(string mensaje)
// * 
// *      Muestra un mensaje por un frame, por lo tanto para que el mensaje se
// *      muestre de manera continua, debemos ejecutar ese método en cada frame que lo necesitemos. Esto puede servir para mostrar mensajes
// *      cuando el personaje se encuentra en determinado lugar o está mirando un objeto con el que se puede interactuar.
// * 
// * InterfazDeUsuario.MostrarMensajeSalteableTecla(string mensaje) 
// * 
// *      Se ejecuta una vez y muestra el mensaje en pantalla hasta que el
// *      jugador pulse cualquier tecla.
// * 
// * InterfazDeUsuario.MostrarMensajeSalteableBoton(string mensaje) 
// * 
// *      Se ejecuta una vez y muestra un mensaje hasta que el jugador
// *      pulse el botón "Entendido"
// * 
// * -------------------------------------------------------------------------------------------- *
// * Página: https://gamedevtraum.com/es/
// * Canal: https://youtube.com/c/GameDevTraum
// * Instagram: @gamedevtraum
// * 
// * Visita la página para encontrar más soluciones, Assets y artículos informativos
// * -------------------------------------------------------------------------------------------- *
// * 
// */

//    #region CAMPOS

//    //[Header("Mensaje Simple (se muestra mientras la orden persista)")]

//    //[SerializeField]
//    //private GameObject mensajeSimpleObjeto; //Contenedor
//    //[SerializeField]
//    //private Text mensajeSimpleTexto; //Objeto Texto

//    //private bool mensajeSimpleActivo; //Estado del mensaje


//    //[Header("Mensaje Salteable (cualquier tecla)")]


//    //[SerializeField]
//    //private GameObject mensajeSalteableTeclaObjeto; //Contenedor
//    //[SerializeField]
//    //private Text mensajeSalteableTeclaTexto; //Objeto Texto
//    //private bool mensajeSalteableTeclaActivo; //Estado del mensaje


//    [Header("Mensaje Salteable (botón)")]


//    [SerializeField]
//    private GameObject mensajeSalteableBotonObjeto; //Contenedor
//    [SerializeField]
//    private Text mensajeSalteableBotonTexto; //Objeto Texto
//    private bool mensajeSalteableBotonActivo; //Estado del mensaje

//    #endregion

//    #region MÉTODOS


//    void Start()
//    {
//        //mensajeSimpleObjeto.SetActive(false);
//        //mensajeSalteableTeclaObjeto.SetActive(false);
//        mensajeSalteableBotonObjeto.SetActive(false);
//    }


//    private void OnGUI()
//    {
        

//        //if (mensajeSalteableTeclaActivo)
//        //{
//        //    if (Input.anyKeyDown)
//        //    {
//        //        LimpiarMensajeTecla();
//        //    }
//        //}

//        if (mensajeSalteableBotonActivo)
//        {
//            if (Input.GetKeyDown(KeyCode.Escape))
//            {
//                LimpiarMensajeBoton();
//            }
//        }

//        //LimpiarMensajeSimple();

//    }


//    //public void MostrarMensajeSimple(string mensaje)
//    //{
//    //    mensajeSimpleActivo = true;
//    //    mensajeSimpleTexto.text = mensaje;
//    //    mensajeSimpleObjeto.SetActive(true);
//    //}

//    //public void MostrarMensajeSalteableTecla(string mensaje)
//    //{
//    //    mensajeSalteableTeclaActivo = true;
//    //    mensajeSalteableTeclaTexto.text = mensaje;
//    //    mensajeSalteableTeclaObjeto.SetActive(true);
//    //}

//    public void MostrarMensajeSalteableBoton(string mensaje)
//    {
//        mensajeSalteableBotonActivo = true;
//        mensajeSalteableBotonTexto.text = mensaje;
//        mensajeSalteableBotonObjeto.SetActive(true);
//    }

//    public void BotonSaltarMensaje()
//    {
//        if (mensajeSalteableBotonActivo)
//        {
//            LimpiarMensajeBoton();
//        }
//    }

//    //private void LimpiarMensajeSimple()
//    //{
//    //    mensajeSimpleActivo = false;
//    //    mensajeSimpleObjeto.SetActive(false);
//    //}

//    //private void LimpiarMensajeTecla()
//    //{
//    //    mensajeSalteableTeclaActivo = false;
//    //    mensajeSalteableTeclaObjeto.SetActive(false);
//    //}

//    private void LimpiarMensajeBoton()
//    {
//        mensajeSalteableBotonActivo = false;
//        mensajeSalteableBotonObjeto.SetActive(false);
//    }

//    #endregion

//}
