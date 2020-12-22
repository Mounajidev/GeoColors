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
    Transform normal, sphere;
   // [HideInInspector]
    public formType actformT;//forma actual del personaje

    void Start()
    {
        normal = transform.Find("robotdessin");
        sphere = transform.Find("SphereForm");
        #region forma inicial y su configuracion
        transform.GetComponent<SphereCollider>().enabled = true;
        transform.GetComponent<CapsuleCollider>().enabled = false;
        actformT = formType.normal;
        normal.gameObject.SetActive(true);
        sphere.gameObject.SetActive(false);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))//hay que cambiarlo para usar un inputmanager
        {
            ChangeForm(actformT);
        }
    }
    public void ChangeForm(formType formt)
    {
        //hay que cambiarlo para que se active una animacion y cambiar de forma a la que elija el jugador
        //por ahora solo voy a probar con la esfera      
            if (formt == formType.normal)
            {
                actformT = formType.sphere;
                transform.GetComponent<SphereCollider>().enabled = false;
                transform.GetComponent<CapsuleCollider>().enabled = true;
                normal.gameObject.SetActive(false);
                sphere.gameObject.SetActive(true);
            }
            else if (formt == formType.sphere)
            {             
                transform.GetComponent<SphereCollider>().enabled = true;
                transform.GetComponent<CapsuleCollider>().enabled = false;
                actformT = formType.normal;
                normal.gameObject.SetActive(true);
                sphere.gameObject.SetActive(false);            
            }
        Debug.Log(actformT);
    }
}
