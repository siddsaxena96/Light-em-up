using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private SpriteRenderer playerSpriteRenderer = null;

    [Header("Player Movement")]
    public float speed = 9f;
    private float vertical, horizontal;
    private Vector2 velocity = Vector2.zero;

    [Header("Torch Light")]
    [SerializeField] private Transform torchTransform = null;
    [SerializeField] private Rigidbody2D crossHair = null;
    [SerializeField] private BoxCollider2D playerLightArea = null;
    [SerializeField] private Light spotLight = null;
    public int torchAmmo = 3;
    public int torchOnTime = 5;
    public int torchReloadTime = 3;
    public float torchOnIntensity = 19.75f;
    public Sprite onSprite = null;
    public Sprite offSprite = null;
    private bool torchOn;
    private float timer = 0f;

    [Header("Battery")]
    [SerializeField] private SpriteRenderer battery;
    public Sprite[] batterystates;

    void Awake()
    {
        if (rb == null)
            rb = gameObject.GetComponent<Rigidbody2D>();
        torchOn = false;
        spotLight.intensity = 0f;
        playerLightArea.enabled = false;
        torchOn = false;
    }

    void FixedUpdate()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        velocity = new Vector2(horizontal, vertical);
        rb.velocity = velocity * speed;
    }

    public void OnCollision(string tag)
    {
        if (tag == "Enemy")
        {
            GameObject.FindGameObjectWithTag("GameOverScreen").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.FindGameObjectWithTag("GameOverScreen").GetComponent<GameOver>().playm();
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        crossHair.position = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        if (!torchOn)
        {
            if (torchAmmo == 0)
            {
                playerSpriteRenderer.sprite = offSprite;
                StartCoroutine(TorchReloadTimer());
            }

            if (Input.GetMouseButtonDown(0) && torchAmmo > 0)
            {
                TurnOnTorch();
            }
        }

        Vector3 diff = (crossHair.transform.position - transform.position);
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        float rotsprite = rot_z < 0 ? 360 + rot_z : rot_z;
        playerSpriteRenderer.flipX = (rotsprite > 90 && rotsprite <= 270) ? true : false;
        torchTransform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90f);
    }

    private void TurnOnTorch()
    {
        spotLight.intensity = torchOnIntensity;
        playerLightArea.enabled = true;
        playerSpriteRenderer.sprite = onSprite;
        torchOn = true;
        StartCoroutine(TorchOnTimer());
    }

    private void TurnOffTorch()
    {
        spotLight.intensity = 0f;
        playerLightArea.enabled = false;
        torchOn = false;
        playerSpriteRenderer.sprite = offSprite;
        torchAmmo -= 1;
        battery.sprite = batterystates[torchAmmo];
    }

    private void ReloadTorch()
    {
        torchAmmo = 3;
        battery.sprite = batterystates[torchAmmo];
    }

    private IEnumerator TorchOnTimer()
    {
        yield return new WaitForSeconds(torchOnTime);
        TurnOffTorch();
    }

    private IEnumerator TorchReloadTimer()
    {
        yield return new WaitForSeconds(torchReloadTime);
        ReloadTorch();
    }
}
