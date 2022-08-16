using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectorOnly : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!Application.isEditor)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    
    void Update()
    {
        
    }
    
}
