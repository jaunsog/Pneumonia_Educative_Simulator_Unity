using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class PausedMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pauseMenu,endMenu;
    public bool isPaused;
    private Camera_Controller cam;
    public TMP_Text endText;
    void Start()
    {
        pauseMenu.SetActive(false);
        endMenu.SetActive(false);
        cam = FindObjectOfType<Camera_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            EndGame();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            GoodGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void EndGame()
    {
        cam.UnlockMouse();
        endMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        endText.text= "El paciente tuvo que ser llevado a urgencias, el tratamiento no fue efectivo.";
         
        }
    public void GoodGame()
    {
        cam.UnlockMouse();
        endMenu.SetActive(true);
        Time.timeScale=0f;
        isPaused=true;
        endText.text="El tratemiento fue un éxito, el paciente se mantendrá bajo observación, buen trabajo.";
    }
    public void PauseGame()
    {
        cam.UnlockMouse();
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void ResumeGame()
    {
        cam.LockMouse();
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused=false;
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
    public void Exit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
