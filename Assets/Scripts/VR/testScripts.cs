using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScripts : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Material mat = null;
    void Start()
    {
        Renderer[] renderer = GetComponentsInChildren<Renderer>();

        foreach(Renderer r in renderer)
        {
            r.material = mat;
        }
    }

    
}
