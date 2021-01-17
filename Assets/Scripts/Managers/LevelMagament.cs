using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class LevelMagament : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject player;
    public GameObject[] casillas;
    float currentTime, maxTime;
    public Text c;
    private InpCanvas impCanvas;
    bool paused;
    // Start is called before the first frame update
    private void OnEnable()
    {
        impCanvas.Enable();
        
    }
    private void OnDisable()
    {
        impCanvas.Disable();
    }

    private void Awake()
    {
        impCanvas = new InpCanvas();
    }
    void Start()
    {
        paused = false;
        impCanvas.InputCanvas.Pause.performed += _ => DetectPause();
        maxTime =450;
        currentTime = maxTime;
        casillas = GameObject.FindGameObjectsWithTag("CasillaDeColor");        
    }

    // Update is called once per frame
    void Update()
    {
        InputSystem.Update();
        currentTime -= Time.deltaTime;
        c.text = "Tiempo Hasta Despertar:"+Mathf.RoundToInt(currentTime).ToString();
        if (currentTime <= 0) {
            currentTime = maxTime;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        Pause();
    }
    private void FixedUpdate()
    {
        ParedesDelMismoColor();
       
    }

    public void ParedesDelMismoColor()
    {
    //    Debug.Log(player.gameObject.transform.GetChild(0).GetComponent < Renderer>().sharedMaterial.name);
    foreach (GameObject casilla in casillas)
    {
        if (player.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial.color == casilla.gameObject.GetComponent<Renderer>().sharedMaterial.color)
        {
             //   Debug.Log("Same Color!");
            casilla.gameObject.GetComponent<Collider>().isTrigger = true;
        }
        else
        {
            casilla.gameObject.GetComponent<Collider>().isTrigger = false;
        }
    }

    }
    public void RestartBu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Pause()
    {
        if (paused)
        {
           
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        else
        {
             
            Time.timeScale = 1;
            pauseMenu.SetActive(false);

        }
    }
    public void DetectPause()
    {
        paused = !paused;        
    }
}