using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class materialStandard : MonoBehaviour {

    public Material unlit;
    public Material lit;
    public SpriteRenderer[] sprites;

    public void changematerial(bool stat){
        if (stat) {
            for (int i = 0; i < sprites.Length; i++)
                sprites [i].material = lit;
        }
        else {
            for (int i = 0; i < sprites.Length; i++)
                sprites [i].material = lit;
        }
    }
}
