using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class LevelMagament : MonoBehaviour
{
    GameObject SphereC;
    public GameObject pauseMenu;
    public GameObject player;
    public GameObject[] casillas;
    float currentTime, maxTime;
    public Text c;
   // private InpCanvas impCanvas;
    bool paused;
    // Start is called before the first frame update
    public InputManager _inputMan;
    private void Awake()
    {
        //impCanvas = new InpCanvas();
    }
    void Start()
    {
        paused = false;
       // impCanvas.InputCanvas.Pause.performed += _ => DetectPause();
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
        DetectPause();
        
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
            paused = false;
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        else
        {
            paused = true;
            Time.timeScale = 1;
            pauseMenu.SetActive(false);

        }
    }
    public void DetectPause()
    { 
        if (_inputMan.enterButton) {            
            Pause();
        }
               
    }

    public void SphereTakeColor(GameObject playerG, GameObject sphereG)
    {
        sphereG.gameObject.GetComponent<Renderer>().sharedMaterial = playerG.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial;
        if (sphereG.gameObject.GetComponent<Renderer>().sharedMaterial.name == "MaterialYellow")
        {
            //SphereC.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial.name
        }
    }

}