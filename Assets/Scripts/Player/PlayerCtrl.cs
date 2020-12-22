using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
   
    public Rigidbody rb;
    float moveH;
    [Header("Configuration")]
    public float velocity = 5f;
    public float gravityMultiply=0;
    public float angularSpeed;
    public float forceJump;
     
    public RaycastDetection raydect;
   
    FormCtrl formControl;// actformT;

    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        formControl = new FormCtrl();// actformT = formCtrla.actformT;
        raydect = GetComponent<RaycastDetection>();
         
         
        //transform.Find("SphereForm");
    }
        // Update is called once per frame
        void Update()
            {
                Move();               
                Jump();
        Debug.Log(formControl.actformT);
            }
    private void FixedUpdate()
    {
        rb.AddForce(-transform.up * gravityMultiply, ForceMode.Acceleration);
    }
   
    void Move()
    {
        moveH = Input.GetAxis("Horizontal");
       
        if (formControl.actformT == FormCtrl.formType.normal) {         
            rb.transform.position += transform.forward * velocity * moveH * Time.deltaTime;          
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
        else if(formControl.actformT == FormCtrl.formType.sphere)
        {
            velocity = 15f;
            rb.AddForce(Vector3.forward * moveH * velocity);                         
            rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
    }
    void Jump()
    {
        Collider[] coll= this.raydect.SphereDectected();
        
        if (formControl.actformT == FormCtrl.formType.normal)
            forceJump = 8f;
        else if (formControl.actformT == FormCtrl.formType.sphere)
            forceJump = 12f;

        if (coll.Length>0)
        {
         foreach(Collider c in coll)
            {
                if (c.gameObject.layer == Constans.LAYERFLOOR && Input.GetKeyDown(KeyCode.Space))//cambiar el input
                {
                    Debug.Log("salte");
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
            this.gameObject.GetComponentInChildren<Renderer>().sharedMaterial = other.gameObject.GetComponent<Renderer>().sharedMaterial;

    }
}



