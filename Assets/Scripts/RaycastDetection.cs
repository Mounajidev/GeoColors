using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastDetection : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 offset,offset2,offset3;

    RaycastHit hit,hitC,hitIz,hitDe;
    public float rayDist;
    public float rayDistFront;
    public LayerMask lmask;
    public LayerMask lmask2;
    public float detectionRadiusX, detectionRadiusY, detectionRadiusZ,detectRadiusSphere;
     
    void Start()
    {
        detectRadiusSphere = 0.6f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool ifRaycast()
    {

        Physics.Raycast(transform.position + offset3, -transform.up, out hitC, rayDist, lmask);         
        if (hit.collider != null )
        {       
            return true;
        }
        else
        {
            return false;
        }
    }

    public GameObject returnGameObj()
    {

        Physics.Raycast(transform.position + offset, transform.forward, out hit, rayDistFront, lmask2);
 
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.layer);
            return hit.collider.gameObject;
        }
            
        else
            return null;

    }
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
    public Collider[] RetCollBoxDectected()
    {
        Collider[] detected;
        detected = Physics.OverlapBox(transform.position + offset, new Vector3(detectionRadiusX, detectionRadiusY, detectionRadiusZ), transform.rotation, lmask2);
        return detected;
       
    }
    public Collider[] RetCollSphereDectected()
    {
        Collider[] detected;
        detected = Physics.OverlapSphere(transform.position + offset2, detectRadiusSphere,lmask2);
        return detected;         
    }

    private void OnDrawGizmos()
    {
       // Gizmos.color = Color.red;
       // Gizmos.DrawLine(transform.position + offset3, -transform.up * rayDist + transform.position + offset3);
      //  Gizmos.color = Color.green;
        //Gizmos.DrawLine(transform.position + offset, transform.forward * rayDistFront + transform.position + offset);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position+ offset, new Vector3(detectionRadiusX, detectionRadiusY, detectionRadiusZ) * 2f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position + offset2, detectRadiusSphere);
    }
}
