using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMagament : MonoBehaviour
{
    public GameObject player;
    public GameObject[] casillas;
    float currentTime, maxTime;
    public Text c;
    // Start is called before the first frame update
    void Start()
    {
        maxTime =480;
        currentTime = maxTime;
        casillas = GameObject.FindGameObjectsWithTag("CasillaDeColor");        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        c.text = "Tiempo Hasta Despertar:"+Mathf.RoundToInt(currentTime).ToString();
        if (currentTime <= 0) {
            currentTime = maxTime;
        }
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
        if (player.gameObject.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial.color == casilla.gameObject.GetComponent<Renderer>().sharedMaterial.color)
        {
            casilla.gameObject.GetComponent<Collider>().isTrigger = true;
        }
        else
        {
            casilla.gameObject.GetComponent<Collider>().isTrigger = false;
        }
    }


}

}