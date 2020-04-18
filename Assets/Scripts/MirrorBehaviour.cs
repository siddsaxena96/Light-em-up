using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorBehaviour : MonoBehaviour {

    public LineRenderer rayRender;
    public Vector3 finalpos;
    public bool isTouchingCrystal;
    public bool isTouchingSomethingElse;
    public GameObject rayCastPoint;  
    private Transform player;
    private GameObject crystalHit;
    private GameObject winPortalHit;
    private string crystalName;
    private LayerMask portalLayer;
    private bool allowRotate;
    //public GameObject rayHead;
    // Use this for initialization
	void Start () {
        rayRender = GetComponent<LineRenderer> ();
        rayRender.SetPosition(0, transform.position);
        rayRender.SetPosition (1, transform.right * 200 );       
        isTouchingCrystal = false;
        isTouchingSomethingElse = false;
        crystalName = transform.parent.name;       
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        portalLayer = LayerMask.GetMask ("Portals");
        allowRotate = true;
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(),GetComponentInParent<CircleCollider2D>());
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 direction = rayCastPoint.transform.right * 200 + rayCastPoint.transform.position;
        direction.Normalize ();
        RaycastHit2D hit = Physics2D.Raycast (rayCastPoint.transform.position, direction,200f,portalLayer);
        if (hit.collider != null) {
            if (hit.collider.tag == "FinalPortal") {
               
                winPortalHit = hit.collider.gameObject;
                winPortalHit = hit.collider.gameObject;
                finalpos = hit.collider.transform.position;
                isTouchingCrystal = true;
                winPortalHit.GetComponent<WinPortal> ().won = true;
                winPortalHit.GetComponent<WinPortal> ().wincondition ();
                allowRotate = false;
            }
            if (hit.collider.tag == "Crystal" && hit.collider.name!=crystalName) {
                crystalHit = hit.collider.gameObject;
                finalpos = hit.collider.transform.position;
                isTouchingCrystal = true;
                isTouchingSomethingElse = true;
                crystalHit.GetComponentInChildren<spriteChange> ().changeSprite (true);
                crystalHit.GetComponentInChildren<spriteChange> ().changeMaterial (true);
                crystalHit.GetComponentInChildren<LineRenderer> ().enabled = true;
                crystalHit.GetComponentInChildren<MirrorBehaviour> ().enabled = true;              
            } else if (hit.collider.tag != "Crystal"&&hit.collider.tag != "PlayerLightArea") {
                isTouchingCrystal = false;
                isTouchingSomethingElse = true;
                finalpos = hit.point;
                //finalpos = hit.collider.transform.position;
                if (crystalHit != null) {                   
                    crystalHit.GetComponentInChildren<spriteChange> ().changeSprite (false);
                    crystalHit.GetComponentInChildren<spriteChange> ().changeMaterial (false);
                    crystalHit.GetComponentInChildren<MirrorBehaviour> ().enabled = false;
                    crystalHit.GetComponentInChildren<LineRenderer> ().enabled = false;
                }

                if (winPortalHit != null) {
                    winPortalHit.GetComponent<WinPortal> ().won = false; 
                    winPortalHit.GetComponent<WinPortal> ().wincondition ();
                    allowRotate = true;
                }
               
            } 
        } else {
            isTouchingCrystal = false;
            isTouchingSomethingElse = false;           
            if (crystalHit != null) {
                crystalHit.GetComponentInChildren<spriteChange> ().changeSprite (false);
                crystalHit.GetComponentInChildren<spriteChange> ().changeMaterial (false);
                crystalHit.GetComponentInChildren<MirrorBehaviour> ().enabled = false;
                crystalHit.GetComponentInChildren<LineRenderer> ().enabled = false;
            }
        }
        rayRender.SetPosition(0, rayCastPoint.transform.position);
        if (!isTouchingCrystal && !isTouchingSomethingElse) {
            rayRender.SetPosition (1, transform.right * 200);            
        } else {
            rayRender.SetPosition (1, finalpos);               
        }

        if (allowRotate) {
            if (Vector3.Distance (transform.position, player.position) < 4.0f) {
                if (Input.GetKey (KeyCode.Q)) {
                    transform.Rotate (Vector3.forward * 0.5f);
                } else if (Input.GetKey (KeyCode.E)) {
                    transform.Rotate (Vector3.forward * -0.5f);
                }
            }
        }
    }

    void OnDisable() {
        if (crystalHit != null) {
            crystalHit.GetComponentInChildren<spriteChange> ().changeSprite (false);
            crystalHit.GetComponentInChildren<spriteChange> ().changeMaterial (false);
            crystalHit.GetComponentInChildren<MirrorBehaviour> ().enabled = false;
            crystalHit.GetComponentInChildren<LineRenderer> ().enabled = false;
        }
        if (winPortalHit != null) {
            winPortalHit.GetComponent<WinPortal> ().won = false; 
            winPortalHit.GetComponent<WinPortal> ().wincondition ();
            allowRotate = true;
        }
    }
}
