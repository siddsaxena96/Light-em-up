using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMover : MonoBehaviour {

    public Rigidbody2D crossHair;
    public SpriteRenderer sp;
    public int torchAmmo=3;
    public Sprite[] batterystates;
    public SpriteRenderer battery;
    public Sprite onSprite;
    public Sprite offSprite;
    private Light spotLight;
    private BoxCollider2D playerLightArea;
    private bool torchon;
    private float timer=0f;

    void Start(){        
        torchon = false;
        spotLight = GetComponentInChildren<Light>();
        playerLightArea = GameObject.FindGameObjectWithTag ("PlayerLightArea").GetComponent<BoxCollider2D> ();
        spotLight.intensity = 0f;
        playerLightArea.enabled = false;
        torchon = false;
    }

    void Update(){
        crossHair.position = Camera.main.ScreenToWorldPoint (new Vector2 (Input.mousePosition.x, Input.mousePosition.y));
        if (torchAmmo > 0 && !torchon) {
            if (Input.GetMouseButtonDown (0)) {
                spotLight.intensity = 19.25f;
                playerLightArea.enabled = true;
                torchon = true;
                sp.sprite = onSprite;
            }
        } else if (torchon) {
            timer += Time.deltaTime;
            int seconds = (int)timer % 60;
            if (seconds > 5) {
                torchAmmo -= 1;
                battery.sprite = batterystates [torchAmmo];
                spotLight.intensity = 0f;
                playerLightArea.enabled = false;
                torchon = false;
                timer = 0f;
                sp.sprite = offSprite;
            }
        } else {
            sp.sprite = offSprite;
            timer += Time.deltaTime;
            int seconds = (int)timer % 60;
            if (seconds > 3) {
                torchAmmo = 3;
                battery.sprite = batterystates [torchAmmo];
                timer = 0f;
            }
        }


        Vector3 diff = (crossHair.transform.position - transform.position);
        diff.Normalize ();
        float rot_z = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;
        float rotsprite=rot_z<0?360+rot_z:rot_z;
        if (rotsprite > 90 && rotsprite <= 270) {
            sp.flipX = true;
        } else {
            sp.flipX = false;               
        }
        transform.rotation = Quaternion.Euler (0f,0f, rot_z-90f);          
    }
}
