using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorWall : Wall
{
    public  ColorWall(Material texturaActual, List<Material> texturas,GameObject gobj)
    {
        this.texturaActual = texturaActual;
        this.texturas = texturas;
        this.gobj = gobj;
       // this.habilitada = habilitada;
    }
    public override void cambiarcolor(Material texturaActual, Material texturaNueva) 
    { 
        
    }
   // public override void desactivar(Material texturaActual, Material texturaPersonaje) =>base.desactivar(texturaActual, texturaPersonaje);
   public override void compareColor(Material texturaPersonaje)
    {
        base.compareColor(texturaPersonaje);
    }

}
  