using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
   
    public Rigidbody rb;
    float moveH ;
    [Header("Configuration")]
    public float velocity= 5f;
   
    public float gravityMultiply=0;
    public float forceJump;
    public RaycastDetection raydect;
    FormCtrl formControl;// actformT;  
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        formControl = this.GetComponent<FormCtrl>();
        raydect = GetComponent<RaycastDetection>();                 
        //transform.Find("SphereForm");
       
         
    }
        // Update is called once per frame
        void Update()
        {
        Jump();
        }
    private void FixedUpdate()
    {
        Move();
        
        rb.AddForce(-transform.up * gravityMultiply, ForceMode.Acceleration);
    }
    void Move()
    {
        moveH = Input.GetAxis("Horizontal");      
        if (formControl.actformT == FormCtrl.formType.normal ) 
        {     
            rb.transform.position += transform.forward * velocity* moveH * Time.deltaTime;                     
        }
        else if(formControl.actformT == FormCtrl.formType.sphere  )
        {
            //ver si se puede hacer aca 
           // rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            //rb.AddForce(new Vector3(0f, 0f, moveH) * velocityEsphere);
           // rb.AddTorque(new Vector3(moveH, 0f, 0f) * velocityEsphere);
            //rb.AddTorque(transform.right * moveH * velocityEsphere);
            
             
        }
    }
    void Jump()
    {
        
        bool detect= this.raydect.ifRaycast();
       
         if ((formControl.actformT == FormCtrl.formType.normal))
         {
            if (detect)
            {

                if (Input.GetKeyDown(KeyCode.Space))//cambiar el input
                {
                    rb.AddForce(transform.up * forceJump, ForceMode.Impulse);
                }
            }
            else
                Debug.Log("no piso");
        }


    }

    public void OnTriggerEnter(Collider other)
    {
        //cambiar
        //hay que hacer un contador de tiempo para que le devuelva el color a la esfera que da el color 
        //la condicion del if debe fijarse si se puede tomar el color o no 
        if (other.gameObject.layer == Constans.LAYERCOLORSPHERE)
            this.gameObject.GetComponentInChildren<Renderer>().sharedMaterial = other.gameObject.GetComponent<Renderer>().sharedMaterial;
    }
}



