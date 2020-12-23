using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastDetection : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 offset;
    RaycastHit hit;
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

        Physics.Raycast(transform.position + offset, -transform.up, out hit, rayDist, lmask);

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
    public bool BoxDetect()
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
    public Collider[] SphereDectected()
    {
        Collider[] detected;
        detected = Physics.OverlapSphere(transform.position + offset, detectRadiusSphere);

        if (detected.Length > 0)
        {
            return detected;
        }
        else
        {
            return detected;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + offset, -transform.up * rayDist + transform.position + offset);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position + offset, transform.forward * rayDistFront + transform.position + offset);
        //Gizmos.color = Color.cyan;
        //Gizmos.DrawWireCube(transform.position+ offset, new Vector3(detectionRadiusX, detectionRadiusY, detectionRadiusZ));
        //Gizmos.DrawSphere(transform.position + offset, detectRadiusSphere);
    }
}
