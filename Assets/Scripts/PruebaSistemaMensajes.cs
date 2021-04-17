//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PruebaSistemaMensajes : MonoBehaviour
//{
    /* ---- Mostrar mensajes en pantalla por GameDevTraum ----
     * 
     * Esta solución consiste en el Script "InterfazDeUsuario" y el prefabricado "Solucion Mostrar Mensajes - GameDevTraum".
     * El Script "InterfazDeUsuario" provee tres métodos públicos para mostrar mensajes en pantalla.
     * 
     * InterfazDeUsuario.MostrarMensajeSimple(string mensaje)
     * 
     *      Muestra un mensaje por un frame, por lo tanto para que el mensaje se muestre de manera continua, 
     *      debemos ejecutar ese método en cada frame que lo necesitemos. Esto puede servir para mostrar mensajes
     *      cuando el personaje se encuentra en determinado lugar o está mirando un objeto con el que se puede interactuar.
     * 
     * InterfazDeUsuario.MostrarMensajeSalteableTecla(string mensaje) 
     * 
     *      Se ejecuta una vez y muestra el mensaje en pantalla hasta que el
     *      jugador pulse cualquier tecla.
     * 
     * InterfazDeUsuario.MostrarMensajeSalteableBoton(string mensaje) 
     * 
     *      Se ejecuta una vez y muestra un mensaje hasta que el jugador
     *      pulse el botón "Entendido"
     * 
     * -------------------------------------------------------------------------------------------- *
     * Página: https://gamedevtraum.com/es/
     * Canal: https://youtube.com/c/GameDevTraum
     * Instagram: @gamedevtraum
     * 
     * Visita la página para encontrar más soluciones, Assets y artículos informativos
     * -------------------------------------------------------------------------------------------- *
     * 
     */


//    [SerializeField]
//    private InterfazDeUsuario interfazDeUsuario;

//    [SerializeField]
//    private string mensajeSimple;

//    [SerializeField]
//    private string mensajeSalteableTecla;

//    [SerializeField]
//    private string mensajeSalteableBoton;

//    void Start()
//    {
//        interfazDeUsuario = FindObjectOfType<InterfazDeUsuario>();
//    }


//    void Update()
//    {
        
//        if (Input.GetKey(KeyCode.Alpha1))
//        {
//            Debug.Log("Tecla 1 pulsada");
//            interfazDeUsuario.MostrarMensajeSimple(mensajeSimple);
//        }

//        if (Input.GetKeyUp(KeyCode.Alpha2))
//        {
//            Debug.Log("Tecla 2 pulsada");
//            interfazDeUsuario.MostrarMensajeSalteableTecla(mensajeSalteableTecla);
//        }

//        if (Input.GetKeyDown(KeyCode.Alpha3))
//        {
//            Debug.Log("Tecla 3 pulsada");
//            interfazDeUsuario.MostrarMensajeSalteableBoton(mensajeSalteableBoton);
//        }

//        if (Input.anyKeyDown)
//        {
//            Debug.Log(Input.inputString);
//        }

//    }
//}
