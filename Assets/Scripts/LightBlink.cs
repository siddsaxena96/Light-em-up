using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlink : MonoBehaviour {

    private Light light;
    private float increment=0.2f;

	// Use this for initialization
	void Start () {
        light = GetComponent<Light> ();
        light.intensity = 0;
	}
	
	// Update is called once per frame
	void Update () {
        
        light.intensity += increment;
        if (light.intensity >= 25)
            increment = -0.2f;
        if (light.intensity <= 0)
            increment = 0.2f;
	}
}
