using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float horizontalMove;
    public float verticalMove;
    public bool dash;
    public bool jump;
    public bool jumpR;
    public bool chooseC;
    public bool enterButton;
    public bool cancelButton;
    public bool actionbutton;
    public KeyCode colorKey;
    void Update()
    {
        DetectHorizonalMove();
        DetectVerticalMove();
        DetectJump();
        DetectDash();
        DetectJumpRealiced();
        DetectEnter();
        DetectChangeColor();
    }
    void DetectDash()
    {
        dash = Input.GetButtonDown("Dash");
    }
    void DetectChangeC()
    {
        dash = Input.GetButtonDown("ChangeColor");
    }
    void DetectJump()
    {
        jump = Input.GetButtonDown("Jump");
    }
    void DetectJumpRealiced()
    {
        jumpR= Input.GetButton("Jump");
    }
    void DetectHorizonalMove()
    {
        horizontalMove = Input.GetAxis("Horizontal");
    }

    void DetectVerticalMove()
    {
        verticalMove = Input.GetAxis("Vertical");
    }
    void DetectEnter()
    {
       
        enterButton = Input.GetButtonDown("Enter");
    }

    //////////////////////////////////////////////////////
    void DetectChangeColor()
    {

        if (Input.GetKeyDown(colorKey) || Input.GetButtonDown("ChangeColor"))
        {
            chooseC = true;
        }
        else
        {
            chooseC = false;
        }
    }

    public void SetKey(string k)
    {
        colorKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), k); 
    }


}
