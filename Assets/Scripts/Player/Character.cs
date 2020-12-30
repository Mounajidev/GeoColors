using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    GameObject character;
    float hp;
    float velocity;
    Material materialActual;
    List<Material> materiales;
    
    public Character(GameObject charac) {
        this.character = charac;
        materiales = new List<Material>();
    }
    public bool existeColor(Material mat)
    {

        return materiales.Exists(x => x == mat);
    }
    public void almacenarColores(Material mat)
    {
            if (materiales.Count < 2)
            {
                materiales.Add(mat);
            }

            else
            {
                materiales[0] = mat;
            }
         
        
           
    }
    public void escogerColor() {
        character.GetComponentInChildren<Renderer>().sharedMaterial = materiales[0];
    }
    public void  reodenarColor() 
    {
        //por ahora solo cambia entre 2 colores habria que hacer otra logica si tenemos mas colores 
        if (materiales.Count == 2) {
            Material aux = materiales[0];
            materiales[0] = materiales[1];
            materiales[1] = aux;
        }        
    }
    
}
