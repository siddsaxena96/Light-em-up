using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MirrorBehaviour : MonoBehaviour
{
    [Header("Line Rendering")]
    [SerializeField] private LineRenderer rayRender = null;
    [SerializeField] private Vector3 finalpos = Vector3.zero;
    [SerializeField] private LayerMask portalLayer = 0;
    [SerializeField] private GameObject rayCastPoint = null;
    [SerializeField] private LightBlink nodeLightHandler = null;
    public bool isTouchingCrystal = false;
    public bool isTouchingSomethingElse = false;
    public bool allowRotate = false;
    public bool forceOn = false;

    [Space]
    [SerializeField] private SpriteChange spriteChanger = null;

    private Transform player = null;
    private GameObject crystalHit = null;
    private GameObject winPortalHit = null;
    private string crystalName = null;
    public UnityEngine.Events.UnityEvent onEnableEvent = null;


    void Awake()
    {
        if (rayRender == null)
            rayRender = GetComponentInChildren<LineRenderer>();
        if (spriteChanger == null)
            spriteChanger = GetComponentInChildren<SpriteChange>();
        rayRender.SetPosition(0, transform.position);
        rayRender.SetPosition(1, transform.right * 200);
        isTouchingCrystal = false;
        isTouchingSomethingElse = false;
        crystalName = transform.name;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        allowRotate = true;
        Physics2D.IgnoreCollision(GetComponentInChildren<BoxCollider2D>(), GetComponentInParent<CircleCollider2D>());
    }

    private void OnEnable()
    {
        spriteChanger.ChangeSprite(true);
        spriteChanger.ChangeMaterial(true);
        rayRender.enabled = true;
        onEnableEvent?.Invoke();
        onEnableEvent?.RemoveAllListeners();
        nodeLightHandler.IncreaseRange();
    }

    void OnDisable()
    {
        if (forceOn)
        {
            this.enabled = true;
            return;
        }
        spriteChanger.ChangeSprite(false);
        spriteChanger.ChangeMaterial(false);
        rayRender.enabled = false;
        nodeLightHandler.DecreaseRange();
        if (crystalHit != null)
        {
            crystalHit.GetComponentInChildren<MirrorBehaviour>().enabled = false;
        }
        if (winPortalHit != null)
        {
            winPortalHit.GetComponent<WinPortal>().ResetWinCondition();
            allowRotate = true;
        }
    }

    void Update()
    {
        Vector3 direction = rayCastPoint.transform.right;
        RaycastHit2D hit = Physics2D.Raycast(rayCastPoint.transform.position, direction, 200f, portalLayer);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Crystal" && hit.collider.name != crystalName)
            {
                crystalHit = hit.collider.gameObject;
                finalpos = hit.collider.transform.position;
                isTouchingCrystal = true;
                crystalHit.GetComponent<MirrorBehaviour>().enabled = true;
            }
            else if (crystalHit != null)
            {
                crystalHit.GetComponent<MirrorBehaviour>().enabled = false;
            }

            if (hit.collider.tag == "FinalPortal")
            {
                winPortalHit = hit.collider.gameObject;
                finalpos = hit.collider.transform.position;
                isTouchingCrystal = true;
                winPortalHit.GetComponent<WinPortal>().OnGameWon();
            }
            else if (winPortalHit != null)
            {
                winPortalHit.GetComponent<WinPortal>().ResetWinCondition();
            }

            if (hit.collider.tag == "map")
            {
                finalpos = hit.point;
                isTouchingSomethingElse = true;
            }
        }

        rayRender.SetPosition(0, rayCastPoint.transform.position);
        rayRender.SetPosition(1, (!isTouchingCrystal && !isTouchingSomethingElse) ? transform.right * 200 : finalpos);

        if (allowRotate && player != null)
        {
            if (Vector3.Distance(transform.position, player.position) < 5.0f)
            {
                if (Input.GetKey(KeyCode.Q))
                {
                    transform.Rotate(Vector3.forward * 0.5f);
                }
                else if (Input.GetKey(KeyCode.E))
                {
                    transform.Rotate(Vector3.forward * -0.5f);
                }
            }
        }
    }

    public void StopRotation()
    {
        allowRotate = false;
    }
}
