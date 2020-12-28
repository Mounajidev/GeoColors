using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Wall  
{

    protected Material texturaActual;   
    protected List<Material> texturas;
    protected Transform tf;
    protected GameObject gobj;
   // protected bool habilitada;

  //  public bool Habilitada { get => habilitada;}

    public virtual void cambiarcolor(Material texturaActual, Material texturaNueva) { }
    public virtual void movewall() { }
    //ver que mas hace la pared
    public virtual void desactivar(){         
            gobj.GetComponent<Collider>().isTrigger = true;
         //   habilitada = false;
                
    }
    public virtual void activar()
    {
           // habilitada = true;
            gobj.GetComponent<Collider>().isTrigger = false;
    }
    public virtual void compareColor( Material texturaPersonaje)
    {
      //  Debug.Log(texturaPersonaje);
        if (texturaActual.color == texturaPersonaje.color)
            desactivar();
        else
            activar();

    }

}
