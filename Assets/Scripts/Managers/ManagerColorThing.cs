using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerColorThing : MonoBehaviour
{
    // Start is called before the first frame update
    public ColorWall wall;
    public List<Material> texturas;
    Material texturaActual;
    //public bool activa;
    void Start()
    {
        texturaActual = this.gameObject.GetComponent<Renderer>().material;
        
        wall = new ColorWall(texturaActual, texturas, this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void detectColorThing(Material texturaPersonaje)
    {
        wall.compareColor(texturaPersonaje);
    }
  
   /* public void OnTriggerExit(Collider other)
    {
      
        if (other.gameObject.layer == Constans.LAYERPLAYER &&  isActive() == false)
        {
           GetComponent<ManagerColorThing>().Activar();
        }
    }*/
}
