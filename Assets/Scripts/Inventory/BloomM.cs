using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MaterialesBloom", menuName = "Bloom/BloomList", order = 1)]
public class BloomM: ScriptableObject
{
    [System.Serializable]
    public struct ColoresBloom 
    {         
        public Material bloomM;
    }

    public ColoresBloom[] coloresBloomList;
}
