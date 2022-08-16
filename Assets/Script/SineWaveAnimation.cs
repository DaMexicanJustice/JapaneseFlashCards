using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWaveAnimation : MonoBehaviour
{

    private Vector3 startPosition;
    public float sineWaveHeight;
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Factor controls the height of the sine wave. Time.delta time controls the speed.
        transform.position = startPosition + new Vector3(0f, Mathf.Sin(Time.time * speed)*sineWaveHeight, 0f);
    }
}
