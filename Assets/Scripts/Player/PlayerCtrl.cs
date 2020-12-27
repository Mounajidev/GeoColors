﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    Animator anim;

    public Rigidbody rb;
    public float moveH;
    Collider[] coll;
    [Header("Configuration")]
    public float velocity = 5f;
    public float velocityEsphere = 5f;
    public float gravityMultiply = 0;
    public float forceJump;
    public RaycastDetection raydect;
    FormCtrl formControl;// actformT;  


    void Start()
    {

        rb = GetComponent<Rigidbody>();
        formControl = this.GetComponent<FormCtrl>();
        raydect = GetComponent<RaycastDetection>();
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
      
        Jump();
       DetectColorThing();//detecta por ahora las cosas que quiera traspasar
    }
    private void FixedUpdate()
    {

        Move();
        rb.AddForce(-transform.up * gravityMultiply, ForceMode.Acceleration);
    }
    private void DetectColorThing()
    {
            coll=raydect.RetCollBoxDectected();
        
        foreach (Collider c in coll)
        {   
            Material mat=this.GetComponentInChildren<Renderer>().sharedMaterial;
            c.gameObject.GetComponent<ManagerColorThing>().detectColorThing(mat);
        }
   
    }
    void Move()
    {
        moveH = Input.GetAxis("Horizontal");
        if (formControl.actformT == FormCtrl.formType.normal)
        {
            //  rb.transform.position += transform.forward * velocity * moveH * Time.deltaTime;
            Vector3 move = new Vector3(0f, 0f, velocity * moveH * Time.deltaTime);
            rb.MovePosition(transform.position + move);
            anim.SetFloat("Horizontal", moveH);

            //Debug.Log(anim);
        }
        else if (formControl.actformT == FormCtrl.formType.sphere)
        {
            rb.AddForce(new Vector3(0f, 0f, moveH) * velocityEsphere);
            rb.AddTorque(new Vector3(moveH, 0f, 0f) * velocityEsphere);
            rb.AddTorque(transform.right * moveH * velocityEsphere);
        }
    }
    void Jump()
    {

        bool detect = this.raydect.ifRaycast();

        if ((formControl.actformT == FormCtrl.formType.normal))
        {
            if (detect)
            {
                if (Input.GetKeyDown(KeyCode.Space))//cambiar el input
                {
                    rb.AddForce(transform.up * forceJump, ForceMode.Impulse);
                }
            }

        }


    }

    public void OnTriggerEnter(Collider other)
    {
        //cambiar
        //hay que hacer un contador de tiempo para que le devuelva el color a la esfera que da el color 
        //la condicion del if debe fijarse si se puede tomar el color o no 
        if (other.gameObject.layer == Constans.LAYERCOLORSPHERE)
        {
            this.gameObject.GetComponentInChildren<Renderer>().sharedMaterial = other.gameObject.GetComponent<Renderer>().sharedMaterial;           
        }
     
    }
   /* public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == Constans.LAYERCOLORTHING)
        {
            Material r = this.GetComponentInChildren<Renderer>().sharedMaterial;
            collision.gameObject.GetComponent<ManagerColorThing>().Desactivar(r);//aca me quede!!!
        }
    }*/

}