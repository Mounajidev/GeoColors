using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastDetection : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 offset,offsetEsfera,offset3,frontOffset,upOffset, behinfOffset;

    RaycastHit hit,hitC,hitFront;
    public float rayDistFront, rayDistBehind, rayDistTop;
    public LayerMask lmask;
    public LayerMask lmask2;
    public LayerMask maskClimb;
    public float detectionRadiusX, detectionRadiusY, detectionRadiusZ,detectRadiusSphere;
    private float detectionRadiusX1, detectionRadiusY1, detectionRadiusZ1;
    void Start()
    {
        detectRadiusSphere = 0.6f;
        detectionRadiusX1 = 0.32f;
        detectionRadiusY1 = 0.61f;
        detectionRadiusZ1 = 0.32f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool ifRaycastTop(Material mat) {
        RaycastHit hitUp;
        Physics.Raycast(transform.position + upOffset, transform.up, out hitUp, rayDistTop, maskClimb);
        if (hitUp.collider != null && mat.color != hitFront.collider.GetComponent<Renderer>().sharedMaterial.color)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool ifRaycast(bool facing,Material mat)
    {
        
        if (facing)
        {          
            Physics.Raycast(transform.position + frontOffset, transform.forward, out hitFront, rayDistFront, maskClimb);
           // Debug.Log("front" + hitFront.collider.name);
        }

        else
        {
           
            Physics.Raycast(transform.position + behinfOffset, -transform.forward, out hitFront, rayDistBehind, maskClimb);
         //  Debug.Log("behind" + hitFront.collider.name);
        }          
        //Physics.Raycast(transform.position + behinfOffset, -transform.forward, out hitBehind, rayDistBehind, maskClimb);
        
        if (hitFront.collider != null && mat.color != hitFront.collider.GetComponent<Renderer>().sharedMaterial.color)
        {
           // Debug.Log("hit");
            return true;
        }
        else
        {
           // Debug.Log("nohit");
            return false;
        }
    }
    
   /* public GameObject returnGameObj()
    {

        Physics.Raycast(transform.position + offset, transform.forward, out hit, rayDistFront, lmask2);
 
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.layer);
            return hit.collider.gameObject;
        }
            
        else
            return null;

    }*/
    public bool ifBoxDetect()
    {
        Collider[] detected;
        detected = Physics.OverlapBox(transform.position+ offset, new Vector3(detectionRadiusX, detectionRadiusY, detectionRadiusZ), transform.rotation, lmask);
     
        if (detected.Length > 0 )
        {            
            return true;
        }
        else
        {             
            return false;
        }
            
    }
    public Collider[] RetCollBoxDectected()//para detectar que golpeo paredes pisos o lo que sea del mismo color y no me permita cambiarlo si estoy atravesandolo ojo no sacarlo!!!!
    {
        Collider[] detected;
        detected = Physics.OverlapBox(transform.position + offset3, new Vector3(detectionRadiusX1, detectionRadiusY1, detectionRadiusZ1), transform.rotation, lmask2);
        return detected;
       
    }
    public Collider[] RetCollSphereDectected()
    {
        Collider[] detected;
        detected = Physics.OverlapSphere(transform.position + offsetEsfera, detectRadiusSphere,lmask2);
        return detected;         
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position + upOffset, transform.up * rayDistTop + transform.position + frontOffset);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position + frontOffset, transform.forward * rayDistFront + transform.position + frontOffset);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + behinfOffset, -transform.forward * rayDistBehind + transform.position + behinfOffset);
        Gizmos.color = Color.green;
         Gizmos.DrawWireCube(transform.position+ offset, new Vector3(detectionRadiusX, detectionRadiusY, detectionRadiusZ) * 2f);
       // Gizmos.color = Color.green;
       // Gizmos.DrawWireCube(transform.position + offset3, new Vector3(detectionRadiusX1, detectionRadiusY1, detectionRadiusZ1) * 2f);
        //   Gizmos.color = Color.blue;
        //  Gizmos.DrawSphere(transform.position + offset2, detectRadiusSphere);
    }
}
