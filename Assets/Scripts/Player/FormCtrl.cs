using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    public enum formType//formas que puede tomar el personaje
    {
        normal, sphere
    }
    public Rigidbody rb;
    Transform normal, sphere;
   // [HideInInspector]
    public formType actformT;//forma actual del personaje
    Quaternion originalRotation;
    void Start()
    {
        normal = transform.Find("Cuerpo");
        sphere = transform.Find("SphereForm");
        rb = GetComponent<Rigidbody>();
        #region forma inicial y su configuracion
        transform.GetComponent<CapsuleCollider>().enabled = true;
        SphereCollider[] myColliders = gameObject.GetComponents<SphereCollider>();
        foreach (SphereCollider bc in myColliders) bc.enabled = false;    
        actformT = formType.normal;
        normal.gameObject.SetActive(true);
        sphere.gameObject.SetActive(false);
        originalRotation = transform.rotation;
        rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))//hay que cambiarlo para usar un inputmanager
        {
            ChangeForm(actformT);
        }
       // Debug.Log(actformT);
    }
    private void LateUpdate()
    {
         
    }
    public void ChangeForm(formType formt)
    {
        //hay que cambiarlo para que se active una animacion y cambiar de forma a la que elija el jugador
        //por ahora solo voy a probar con la esfera      
            if (formt == formType.normal)
            {
                   transform.GetComponent<CapsuleCollider>().enabled = false;
                   SphereCollider[] myColliders = gameObject.GetComponents<SphereCollider>();
                    foreach (SphereCollider cc in myColliders) cc.enabled = true;
                    normal.gameObject.SetActive(false);
                    sphere.gameObject.SetActive(true);
                    actformT = formType.sphere;
                    //cambiar
                    rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ ;
        }
            else if (formt == formType.sphere)
            {
            actformT = formType.normal;
            transform.GetComponent<CapsuleCollider>().enabled = true;
            SphereCollider[] myColliders = gameObject.GetComponents<SphereCollider>();
            foreach (SphereCollider cc in myColliders) cc.enabled = false;
            normal.gameObject.SetActive(true);
            sphere.gameObject.SetActive(false);
            transform.rotation = originalRotation;
            //cambiar
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
      
    }
}
