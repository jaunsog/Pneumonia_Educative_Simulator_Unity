using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
public class Quiz : MonoBehaviour
{
    public GameObject quizMenu;
    private Camera_Controller cam;
    public TMP_Text Text;
    void Start()
    {
        quizMenu.SetActive(false);
        cam = FindObjectOfType<Camera_Controller>();
    }

    // Update is called once per frame
    public void EmpezarQuiz()
    {
        quizMenu.SetActive(true);
        cam.UnlockMouse();
    }
    public void MinimizarQuiz()
    {
        quizMenu.SetActive(false);
        cam.LockMouse();
        Text.text="Ingresa P para ver la pregunta nuevamente";
    }
    public void Respuesta()
    {
        quizMenu.SetActive(false);
        cam.LockMouse();
        Text.text="Respondiste: "+EventSystem.current.currentSelectedGameObject.name;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            EmpezarQuiz();
            
        }
    }
}
