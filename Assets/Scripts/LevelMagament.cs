using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMagament : MonoBehaviour
{
    public GameObject player;
    public GameObject[] casillas;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ParedesDelMismoColor();
    }


public void ParedesDelMismoColor()
{
    player = GameObject.FindGameObjectWithTag("Player");
    casillas = GameObject.FindGameObjectsWithTag("Casilla");
    
    foreach (GameObject casilla in casillas)
    {
        if (player.gameObject.GetComponentInChildren<Renderer>().sharedMaterial.color == casilla.gameObject.GetComponent<Renderer>().sharedMaterial.color)
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