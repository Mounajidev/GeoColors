using System.Collections;
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

    public float dashForce;
    public float dashDuration;
    public RaycastDetection raydect;
    FormCtrl formControl;// actformT; 
    public bool activedoubleJump,activeDash;
    Character player;
    void Start()
    {
        player = new Character(this.gameObject);
        rb = GetComponent<Rigidbody>();
        formControl = this.GetComponent<FormCtrl>();
        raydect = GetComponent<RaycastDetection>();
        anim = GetComponent<Animator>();
        
    }
    ///Hojo hay que cambiar todos los imput para que funcione en android por ahora esta todo como si fuera un teclado
    ///normal 
    ///
    // Update is called once per frame
    void Update()
    {
      escogerColor();
      Jump();
      Dash();
    }


    private void FixedUpdate()
    {

        Move();
        rb.AddForce(-transform.up * gravityMultiply, ForceMode.Acceleration);
       

    }
    public void escogerColor()
    {       
        coll = raydect.RetCollBoxDectected();
        bool chocar=false;
        foreach (Collider c in coll)
        {
            if (c.gameObject.GetComponent<Renderer>().sharedMaterial == this.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial)
            {
                chocar = true;
                break;
            }
        }
         if (Input.GetKeyDown(KeyCode.W) && !chocar)
         {
           player.reodenarColor();
           player.escogerColor();
            chocar = false;
         }
        Debug.Log(chocar);
    }
    
    public void Dash() {
        if (Input.GetKeyDown(KeyCode.Q) && activeDash == true) {
            StartCoroutine(Invoke());
        }
       
    }
    public IEnumerator Invoke() 
    {
        
        rb.AddForce(transform.forward * dashForce, ForceMode.VelocityChange);
        activeDash = false;                 
        yield return new WaitForSeconds(dashDuration);
        rb.velocity = Vector3.zero;
        activeDash = true;
    }
    void Move()
    {
        moveH = Input.GetAxis("Horizontal");
        //if (formControl.actformT == FormCtrl.formType.normal)
        Vector3 move = new Vector3(0f, 0f, velocity * moveH * Time.deltaTime);
        rb.MovePosition(transform.position + move);
        if (moveH > 0.1)
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        else if (moveH < -0.1)
            transform.rotation = Quaternion.Euler(0f, -180f, 0f);
        anim.SetFloat("PlayerMove", moveH);
        //}
        //else if (formControl.actformT == FormCtrl.formType.sphere)
        //{
        //    rb.AddForce(new Vector3(0f, 0f, moveH) * velocityEsphere);
        //    rb.AddTorque(new Vector3(moveH, 0f, 0f) * velocityEsphere);
        //    rb.AddTorque(transform.right * moveH * velocityEsphere);
        //}
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
                  //  anim.Play("Jump");
                }
            } else if(!detect && activedoubleJump){
                if (Input.GetKeyDown(KeyCode.Space))//cambiar el input
                {

                    float forceJump2 = forceJump + forceJump * 0.5f;
                    rb.AddForce(transform.up * forceJump2, ForceMode.Impulse);
                    //  anim.Play("Jump");
                    activedoubleJump = false;
                }
            }

        }


    }

    public void OnTriggerEnter(Collider other)
    {
        //        //cambiar
        //        //hay que hacer un contador de tiempo para que le devuelva el color a la esfera que da el color 
        //        //la condicion del if debe fijarse si se puede tomar el color o no 
        
        bool existe= player.existeColor(other.gameObject.GetComponent<Renderer>().sharedMaterial);
        if (other.gameObject.layer == Constans.LAYERCOLORSPHERE && !existe)
        {
            player.almacenarColores(other.gameObject.GetComponent<Renderer>().sharedMaterial);
            player.reodenarColor();
            player.escogerColor();
            //  this.gameObject.GetComponentInChildren<Renderer>().sharedMaterial = other.gameObject.GetComponent<Renderer>().sharedMaterial;
        }

    }

    /* private void DetectColorThing()
 {
     coll = raydect.RetCollBoxDectected();

     foreach (Collider c in coll)
     {
         Material mat = this.GetComponentInChildren<Renderer>().sharedMaterial;
         c.gameObject.GetComponent<ManagerColorThing>().detectColorThing(mat);
     }

 }*/
}
