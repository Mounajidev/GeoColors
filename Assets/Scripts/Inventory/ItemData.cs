using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Items", menuName = "Inventory/List", order = 1)]
public class ItemData: ScriptableObject
{
    [System.Serializable]//lo muestra en el inspector
    public struct ColoresenInventario
    {
        public int id;
        public string nombre;
        public Sprite sprite;
    }
    public ColoresenInventario[] coloresEnInventario;

}


