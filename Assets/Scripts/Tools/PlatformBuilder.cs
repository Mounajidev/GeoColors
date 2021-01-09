using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlatformBuilder : MonoBehaviour
{
    
    [Header("Horizontal")]

    [Header("Transform Positions")]
    [Range(-200,200)]
    public float hPoint1 = 0;
    [Range(-200, 200)]
    public float hPoint2 = 1;
    [Range(-100, 100)]
    public float hSpeed = 0;
    
    [Header("Vertical")]
    [Range(-200, 200)]
    public float vPoint1 = 0;
    [Range(-200, 200)]
    public float vPoint2 = 1;
    [Range(-100, 100)]
    public float vSpeed = 0;


    [Header("OnTriggerEnters")]
    public Collider OnStandup;

    [Header("Change Color")]

    
    
    public float DelayTime;
    public Color color1;
    public Color color2;

    Rigidbody rbPlatform;

    // Start is called before the first frame update
    void Start()
    {
        rbPlatform = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
