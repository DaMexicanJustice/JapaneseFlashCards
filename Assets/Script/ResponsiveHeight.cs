using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsiveHeight : MonoBehaviour
{

    Transform myTransform;
    Vector3 newPositionVector;
    public float divider = 0f;
    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();
        newPositionVector = myTransform.position;
        newPositionVector.y = Screen.height / divider;
        myTransform.position = newPositionVector;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
