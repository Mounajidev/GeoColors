﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{

    public bool lateralDetect = true;
    public float forceJumpImpulse = 0.2f;
    public bool isGrounded;
    public Vector3 moveDirection;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    public Rigidbody rb;
    public float moveH;
    public float moveY;
    public float climbingMove = 10f;
    Collider[] coll;
    [Header("Configuration")]
    public float velocity = 5f;
    public float velocityEsphere = 5f;
    public float gravityMultiply = 2;
    float gravityMultiplyAux;
    public float fallAfterJump = 2f;
    public float forceJump;
    Animator anim;
    public float dashForce = 9f;
    public float dashDuration = 0.2f;
    public RaycastDetection raydect;
    FormCtrl formControl;// actformT; 
    public bool activedoubleJump, activeDash;
    public Character player;
    private int facing;
    [Header("Configuration for Climbing")]
    public bool activeClimb;
    public float timeClimbing, timefalling;
    public float maxtimeClimbing, maxtimeFalling;
    bool falling;
    float auxV;
    [Header("Configuration for Item")]
     public InventoryManager invM;
    void Start()
    {
        
        anim = GetComponent<Animator>();
        player = new Character(this.gameObject);
        rb = GetComponent<Rigidbody>();
        formControl = this.GetComponent<FormCtrl>();
        raydect = GetComponent<RaycastDetection>();
        activeClimb = false;
        //gravityMultiplyAux = gravityMultiply;
        timeClimbing = maxtimeClimbing;
        timefalling = maxtimeFalling;
        falling = false;
        auxV = velocity;
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
        Climb();


    }

    // Opcion 1  -   Climb Con Raycast ( el que estaba )
    //-------------------------------------------------------------------------------------


    //public void Climb() {

    //    if (!falling)
    //        activeClimb = (raydect.ifRaycast(m_FacingRight,this.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial));
    //    else
    //        activeClimb = false;
    //   // Debug.Log(activeClimb);
    //     if (activeClimb)
    //     {
    //        transform.GetComponent<CapsuleCollider>().height = 1f;
    //        //rb.useGravity = false;
    //        //gravityMultiply = 0;
    //        rb.isKinematic = true;
    //        moveH = Input.GetAxis("Horizontal");
    //        //Debug.Log("entro");

    //        float new_velocity =3f;
    //        Vector3 move = new Vector3(0f, new_velocity * Mathf.Abs(moveH) * Time.deltaTime,0);
    //        rb.MovePosition(transform.position + move);
    //        if (moveH > 0 && !m_FacingRight)
    //        {
    //            // gravityMultiply = gravityMultiplyAux; 
    //            // rb.useGravity = true;
    //            // activeClimb = false;
    //            rb.isKinematic = false;
    //            timeClimbing = maxtimeClimbing;
    //            Flip();

    //        }
    //        // Otherwise if the input is moving the player left and the player is facing right...
    //        else if (moveH < 0 && m_FacingRight)
    //        {
    //            // ... flip the player.
    //            // gravityMultiply = gravityMultiplyAux;
    //            //rb.useGravity = true;
    //            timeClimbing = maxtimeClimbing;
    //            activeClimb = false;
    //            Flip();
    //        }
    //        timeClimbing -= Time.deltaTime;
    //        if (timeClimbing <= 0 ||(timeClimbing > 0 && raydect.ifRaycastTop(this.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial)))
    //        {
    //            falling = true;
    //            activeClimb = false;
    //            rb.isKinematic = false;
    //        }         
    //    }
    //    else
    //    {
    //        rb.isKinematic = false;
    //        if(!falling)
    //            timefalling = maxtimeFalling;
    //        else
    //        {
    //            velocity = 0;
    //            timefalling -= Time.deltaTime;
    //        }

    //        if (timefalling <= 0)
    //        {
    //            falling = false;
    //            velocity = auxV;
    //            timeClimbing = maxtimeClimbing;
    //        }
    //        //  rb.useGravity = true;
    //        //gravityMultiply = gravityMultiplyAux;
    //         transform.GetComponent<CapsuleCollider>().height = 1.83f;
    //    }
    //}
    //private void LateUpdate()
    //{
    //   // Debug.Log(rb.velocity.y);
    //   // if(rb.velocity.y>0 )
    //  //  Debug.Log("aciendo");
    //  //  else if(rb.velocity.y < 0 )
    //  //      Debug.Log("deciendo");      
    //    anim.SetFloat("VerticalSpeed", rb.velocity.y);

    //    climbAnimation();
    //}
    //private void climbAnimation() {
    //    if (activeClimb)
    //    {
    //        anim.SetBool("CollisionWithWall", true);
    //    }
    //    else
    //    {
    //        anim.SetBool("CollisionWithWall", false);
    //    }
    //}

    // ---------------------------------------------------------------------------------------------------------

    // Opcion 2  Climb con enter Collider ---------------------------------------------
    //---------------------------------------------------------------
    public void OnCollisionEnter(Collision collision)
    {
        
        bool detect = this.raydect.ifBoxDetect();
        isGrounded = true;

        if (detect)
        {


            //rb.isKinematic = false;
            anim.SetBool("CollisionWithWall", false);
            Debug.Log("Wall off");

        }
        else if ( !raydect.ifRaycastTop(this.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial))
        {
            Debug.Log("Collision Wall");
            activeClimb = true;
            anim.SetBool("CollisionWithWall", true);

        }

    }
    public void OnCollisionStay(Collision collision)
    {
        lateralDetect = raydect.ifRaycastLateral(m_FacingRight);
        bool detect = this.raydect.ifBoxDetect();
        if (!detect && lateralDetect)
        {
            moveH = Input.GetAxis("Horizontal");

            rb.velocity = Vector3.up * moveH * climbingMove * facing;
            //   //Vector3 move = new Vector3(0f, velocity * moveY * Time.deltaTime, 0f);
            //Vector3 moveDirection = new Vector3(0f, velocity * Mathf.Abs(moveH) * Time.deltaTime, 0);
            //moveDirection.y = moveH * Time.deltaTime;
            //rb.MovePosition(transform.position.y * moveDirection);
            Debug.Log("Collision Stay");

        }
    }
    public void OnCollisionExit(Collision collision)
    {
        
        isGrounded = false;
        //debug.log("collision exit");
    }
    //public void Climb()
    //{
    //    if (activeClimb)
    //    {
    //        transform.GetComponent<CapsuleCollider>().height = 1f;
    //        //        //rb.useGravity = false;
    //        //        //gravityMultiply = 0;
    //        //rb.isKinematic = true;
    //        moveH = Input.GetAxis("Horizontal");
    //        //        //Debug.Log("entro");

    //        float new_velocity = 3f;
    //        Vector3 move = new Vector3(0f, new_velocity * Mathf.Abs(moveH) * Time.deltaTime, 0);
    //        rb.MovePosition(transform.position + move);
    //        if (moveH > 0 && !m_FacingRight)
    //        {
    //            // gravityMultiply = gravityMultiplyAux; 
    //            // rb.useGravity = true;
    //            // activeClimb = false;
    //            //rb.isKinematic = false;
    //            //timeClimbing = maxtimeClimbing;
    //            Flip();

    //        }
    //        //        // Otherwise if the input is moving the player left and the player is facing right...
    //        else if (moveH < 0 && m_FacingRight)
    //        {
    //            //            // ... flip the player.
    //            //            // gravityMultiply = gravityMultiplyAux;
    //            //            //rb.useGravity = true;
    //            //timeClimbing = maxtimeClimbing;
    //            //activeClimb = false;
    //            Flip();
    //        }
    //    timeClimbing -= Time.deltaTime;
    //    if (timeClimbing <= 0 || (timeClimbing > 0 && raydect.ifRaycastTop(this.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial)))
    //    {
    //        falling = true;
    //        activeClimb = false;
    //        rb.isKinematic = false;
    //    }
    //}
    //else
    //{
    //    rb.isKinematic = false;
    //    if (!falling)
    //        timefalling = maxtimeFalling;
    //    else
    //    {
    //        velocity = 0;
    //        timefalling -= Time.deltaTime;
    //    }

    //    if (timefalling <= 0)
    //    {
    //        falling = false;
    //        velocity = auxV;
    //        timeClimbing = maxtimeClimbing;
    //    }
    //    //  rb.useGravity = true;
    //    //        //gravityMultiply = gravityMultiplyAux;
    //    transform.GetComponent<CapsuleCollider>().height = 1.83f;
    // }
    //}





    private void FixedUpdate()
    {
        
        Move();
        Climb();
        Gravity();


    }
    public void Gravity()
    {
        ////  Opcion 1:  Gravedad todo el tiempo
        rb.AddForce(-transform.up * gravityMultiply, ForceMode.Acceleration);

        // Opcion 2:  Gravedad agregada solo cuando velocity.y es menor a 0
        /*if (rb.velocity.y < 0)
        {
            rb.velocity = Vector2.up * Physics.gravity.y * (gravityMultiply - 1) * Time.deltaTime;
        }*/
        if (rb.velocity.y > 0f && !Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.Joystick1Button0))
        {
            // Opcopn 1 -- la que usamos por el momento -----
            rb.AddForce(-transform.up * gravityMultiply * fallAfterJump, ForceMode.Acceleration);
            //rb.velocity = Vector2.up * fallAfterJump * -1 * Time.deltaTime;

            // opcion 2 ---


        }
        else if (rb.velocity.y == 0f || activeClimb)
        {
            activedoubleJump = true;
        }
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
        if (Input.GetKeyDown(KeyCode.W) || (Input.GetAxis("JoystickUp") > 0 && !chocar && player.tengoColor()))
        {
            player.reodenarColor();
            player.escogerColor();
            invM.addItem(player.materiales);
            chocar = false;
        }
 
    }

    public void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Q) && activeDash == true)
        {
            StartCoroutine(Invoke());
            anim.SetBool("Dash", true);
            SoundManager.PlaySound("Dash");
        }

    }
    public IEnumerator Invoke()
    {
        // Dash Opcion 1 ----
        //rb.AddForce(transform.forward * dashForce * facing, ForceMode.VelocityChange);

        // Dash Opcion 2 ----
        //rb.velocity = Vector3.forward * dashForce * facing;

        // Dash Opcion 3 ---
        rb.velocity = Vector3.forward * dashForce * facing;



        activeDash = false;
        yield return new WaitForSeconds(dashDuration);
        rb.velocity = Vector3.zero;
        activeDash = true;
    }
    void Move()
    {
        //if (!activeClimb )
        {
            timeClimbing = maxtimeClimbing;
            moveH = Input.GetAxis("Horizontal");
            //if (formControl.actformT == FormCtrl.formType.normal)
            //{ 
            /* if (moveH >= 0.1)
                 transform.rotation = Quaternion.Euler(0f, 0f, 0f);
             else if (moveH <= -0.1)
                 transform.rotation = Quaternion.Euler(0f, -180f, 0f);*/

            //hay que crear un animation manager que se encargue de manejar las animaciones podria ser una buena forma para 
            //que maneje las animaciones del mapa      
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
            anim.SetFloat("MoveHorizontal", moveH * velocity);
            Vector3 moveDirection = new Vector3(0f, 0f, velocity * moveH * Time.deltaTime);
            rb.MovePosition(transform.position + moveDirection);

            // If the input is moving the player right and the player is facing left...



            //}
            //else if (formControl.actformT == FormCtrl.formType.sphere)
            //{
            //    rb.AddForce(new Vector3(0f, 0f, moveH) * velocityEsphere);
            //    rb.AddTorque(new Vector3(moveH, 0f, 0f) * velocityEsphere);
            //    rb.AddTorque(transform.right * moveH * velocityEsphere);
            //}
        }
        //else
        //{
        //    moveY = Input.GetAxis("Horizontal");


        //    //   //Vector3 move = new Vector3(0f, velocity * moveY * Time.deltaTime, 0f);
            
        //    Vector3 move = new Vector3(0f, 0f, 0f);
        //    rb.MovePosition(transform.position + move);
            

        //}

    }

    // ----CLIMB  EN PROCESO --------------------
    //-------------------------------
    public void Climb()
    {
        lateralDetect = raydect.ifRaycastLateral(m_FacingRight);
        //bool lateralDetect = this.raydect.ifRaycastLateral(m_FacingRight, this.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial);


        

        if (raydect.ifRaycastLateral(m_FacingRight))
        {
            activeClimb = true;
            //moveY = Input.GetAxis("Horizontal");
            //Vector3 move = new Vector3(0f, velocity * moveY * facing * Time.deltaTime, 0f);
            //rb.MovePosition(transform.position + move);
            

            Debug.Log("Climb");
        }
        else
        {
            activeClimb = false;
        }



    }
    void Jump()
    {

        bool detect = this.raydect.ifBoxDetect();
        
        if (formControl.actformT == FormCtrl.formType.normal)
        {
            if (isGrounded == true || detect)
            {

                if (Input.GetKeyDown(KeyCode.Space) || (Input.GetKeyDown(KeyCode.Joystick1Button0))) //|| (Input.GetButtonDown("Joystick1Button0")))*/ //cambiar el input
                {
                    if (activeClimb)
                    {
                        activeClimb = false;
                        isGrounded = false;
                        rb.AddForce(transform.forward * forceJumpImpulse * -facing, ForceMode.Impulse);
                        //rb.velocity = Vector3.forward * forceJump, AddForce
                        anim.SetTrigger("Jump");
                        SoundManager.PlaySound("Jump");
                        Debug.Log("WallJump!");



                        rb.velocity = Vector2.up * forceJump;

                        //anim.SetTrigger("Jump");
                        anim.SetFloat("VerticalSpeed", rb.velocity.y);

                    }

                    // --------------------     Prueba de Formas de Salto          ---------------------------------
                    // Opcion1 

                    //rb.AddForce(transform.up * forceJump, ForceMode.Impulse);
                    //anim.SetBool("Jump",true);

                    // Opcion 2
                    //Updated upstream
                    else
                    {
                        activeClimb = false;
                        rb.velocity = Vector3.up * forceJump;
                        anim.SetTrigger("Jump");
                        SoundManager.PlaySound("Jump");
                        isGrounded = false;



                        //rb.velocity = Vector2.up * forceJump;

                        //anim.SetTrigger("Jump");
                        anim.SetFloat("VerticalSpeed", rb.velocity.y);
                    }
                }
              
            }
            else if (!detect && activedoubleJump && !activeClimb)
            {
                if (Input.GetKeyDown(KeyCode.Space) || (Input.GetKeyDown(KeyCode.Joystick1Button0)))//cambiar el input
                {
                    //  Opcion 1 ----

                    //float forceJump2 = forceJump + forceJump * 0.8f;
                    //rb.AddForce(transform.up * forceJump2, ForceMode.Impulse);

                    // Opcion 2 ----

                    rb.velocity = Vector2.up * forceJump;
                    anim.SetTrigger("Jump");
                    SoundManager.PlaySound("Jump");

                    //  anim.SetBool("DobleJump",true);
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

        bool existe = player.existeColor(other.gameObject.GetComponent<Renderer>().sharedMaterial);
        if (other.gameObject.layer == Constans.LAYERCOLORSPHERE && !existe)
        {
            player.almacenarColores(other.gameObject.GetComponent<Renderer>().sharedMaterial) ;
            player.escogerColor();
            invM.addItem(player.materiales);
            if (other.gameObject.tag == "Finish")
            {
                Destroy(other.gameObject);
            }
            //  this.gameObject.GetComponentInChildren<Renderer>().sharedMaterial = other.gameObject.GetComponent<Renderer>().sharedMaterial;
        }

    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;
        if (m_FacingRight)
        {
            facing = 1;
            this.GetComponent<RaycastDetection>().offset.z = -0.07f;//arregla el bug que cuando reescalo cambia el centro
        }

        else
        {
            facing = -1;
            this.GetComponent<RaycastDetection>().offset.z = 0.06f;
        }
            
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.z *= -1;
        transform.localScale = theScale;
         
    }

   
}
