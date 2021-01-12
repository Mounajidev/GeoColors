using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<InventoryItem> slotItems;
    public  ItemData itemdata;

    public void Start()
    {
        foreach (InventoryItem slot in slotItems)
        {
            slot.image.sprite = itemdata.coloresEnInventario[0].sprite;
        }
        
    }
    public void addItem(List<Material> mat) 
    {
        int count = 0;
        foreach(Material m in mat)
        {
            for (int i = 0; i < itemdata.coloresEnInventario.Length; i++)
            {
                if ((string.Compare(itemdata.coloresEnInventario[i].nombre, m.name) == 0)) {
                      slotItems[count].image.sprite= itemdata.coloresEnInventario[i].sprite;
                    break;
                }
                    
            }
            count++;    
        }
       
    }



}
