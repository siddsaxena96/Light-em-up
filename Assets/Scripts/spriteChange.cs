using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteChange : MonoBehaviour {


    public Sprite basicSprite;
    public Sprite hitSprite;
    public Material unlit;
    public Material lit;
    public SpriteRenderer[] sprites;

    public void changeMaterial(bool status){
        if (status) {
            for (int i = 0; i < sprites.Length; i++)
                sprites [i].material = lit;
        }
        else {
            for (int i = 0; i < sprites.Length; i++)
                sprites [i].material = unlit;
        }
    }

    public void changeSprite(bool status) {
        if (status)
            GetComponent<SpriteRenderer> ().sprite = hitSprite;
        else
            GetComponent<SpriteRenderer> ().sprite = basicSprite;
    }
}
