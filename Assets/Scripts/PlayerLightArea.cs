using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightArea : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D col){
        Debug.Log (col.gameObject.tag);
    }
}
