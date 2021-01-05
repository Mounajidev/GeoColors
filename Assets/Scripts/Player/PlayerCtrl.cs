using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    public Rigidbody rb;
    public float moveH;
    Collider[] coll;
    [Header("Configuration")]
    public float velocity = 5f;
    public float velocityEsphere = 5f;
    public float gravityMultiply = 2;
    public float forceJump;
    Animator anim;
    public float dashForce = 9f;
    public float dashDuration = 0.2f;
    public RaycastDetection raydect;
    FormCtrl formControl;// actformT; 
    public bool activedoubleJump, activeDash = true;
    Character player;
    void Start()
    {
        anim = GetComponent<Animator>();
        player = new Character(this.gameObject);
        rb = GetComponent<Rigidbody>();
        formControl = this.GetComponent<FormCtrl>();
        raydect = GetComponent<RaycastDetection>();
 

    }
    ///Hojo hay que cambiar todos los imput para que funcione en android por ahora esta todo como si fuera un teclado
    ///normal 
    ///
    // Update is called once per frame
    void Update()
    {      
        EscogerColor();
        Jump();
        Dash();
    }


    private void FixedUpdate()
    {
        
        Move();
        Gravity();


    }
    public void Gravity()
    {
        rb.AddForce(-transform.up * gravityMultiply, ForceMode.Acceleration);
    }
   
    public void EscogerColor()
    {
        coll = raydect.RetCollBoxDectected();
        bool chocar = false;
        foreach (Collider c in coll)
        {
            if (c.gameObject.GetComponent<Renderer>().sharedMaterial == this.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial)
            {
                chocar = true;
                break;
            }
        }
        if (Input.GetKeyDown(KeyCode.W) && !chocar && player.tengoColor())
        {
            player.reodenarColor();
            player.escogerColor();
            chocar = false;
        }
 
    }

    public void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Q) && activeDash == true)
        {
            StartCoroutine(Invoke());
            anim.SetBool("Dash", true);
        }

    }
    public IEnumerator Invoke()
    {

        rb.AddForce(transform.forward * dashForce*moveH, ForceMode.VelocityChange);
        activeDash = false;
        yield return new WaitForSeconds(dashDuration);
        rb.velocity = Vector3.zero;
        activeDash = true;
    }
    void Move()
    {
 
        moveH = Input.GetAxis("Horizontal");
        //if (formControl.actformT == FormCtrl.formType.normal)
        //{ 
       /* if (moveH >= 0.1)
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        else if (moveH <= -0.1)
            transform.rotation = Quaternion.Euler(0f, -180f, 0f);*/
        Debug.Log(transform.rotation.eulerAngles.y);
        //hay que crear un animation manager que se encargue de manejar las animaciones podria ser una buena forma para 
        //que maneje las animaciones del mapa        
        anim.SetFloat("MoveHorizontal", moveH * velocity);    
        Vector3 move = new Vector3(0f, 0f, velocity * moveH * Time.deltaTime);       
        rb.MovePosition(transform.position + move);

        // If the input is moving the player right and the player is facing left...
        if (moveH > 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (moveH < 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }

         
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
                    anim.SetBool("Jump",true);

                }
            }
            else if (!detect && activedoubleJump)
            {
                if (Input.GetKeyDown(KeyCode.Space))//cambiar el input
                {

                    float forceJump2 = forceJump + forceJump * 0.1f;
                    rb.AddForce(transform.up * forceJump2, ForceMode.Impulse);
                    anim.SetBool("DobleJump",true);
                    //activedoubleJump = false;
                }
            }

        }


    }

    public void OnTriggerEnter(Collider other)
    {
        //        //cambiar
        //        //hay que hacer un contador de tiempo para que le devuelva el color a la esfera que da el color 
        //        //la condicion del if debe fijarse si se puede tomar el color o no 

        bool existe = player.existeColor(other.gameObject.GetComponent<Renderer>().sharedMaterial);
        if (other.gameObject.layer == Constans.LAYERCOLORSPHERE && !existe)
        {
            player.almacenarColores(other.gameObject.GetComponent<Renderer>().sharedMaterial);
            player.reodenarColor();
            player.escogerColor();
            //  this.gameObject.GetComponentInChildren<Renderer>().sharedMaterial = other.gameObject.GetComponent<Renderer>().sharedMaterial;
        }

    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.z *= -1;
        transform.localScale = theScale;
    }

   
}
