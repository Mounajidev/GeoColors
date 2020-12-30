﻿using System.Collections;
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
        casillas = GameObject.FindGameObjectsWithTag("CasillaDeColor");        
    }

    // Update is called once per frame
    void Update()
    {
       
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