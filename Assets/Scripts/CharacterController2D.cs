using UnityEngine;

public class TopDownCharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    private bool isDashing = false;
    private bool canDash = true;
    private float dashTime;

    public float acceleration = 2f; // Hýzlanma ivmesi
    public float deceleration = 2f; // Yavaþlama ivmesi
    private float currentSpeed = 0f;

    public Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;
    public TrailRenderer dashTrail;
    //  private CameraShake cameraShake; // Kamera sallanma script referansý
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //  cameraShake = Camera.main.GetComponent<CameraShake>(); // Ana kameradan script'i al

        // Diðer baþlangýç ayarlarý
        if (dashTrail != null)
        {
            dashTrail.emitting = false; // Baþlangýçta Trail Renderer'ý kapalý tut
        }
    }

    void Update()
    {
        // Hareket giriþlerini al
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Yavaþ yavaþ hýzlanma ve yavaþlama
        if (movement.magnitude > 0)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, moveSpeed, acceleration * Time.deltaTime);
            animator.SetBool("isRunning", true);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, deceleration * Time.deltaTime);
            animator.SetBool("isRunning", false);
        }

        // Animasyon parametrelerini ayarla
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // Karakterin saða/sola dönmesini saðla
        if (movement.x > 0)
        {
            spriteRenderer.flipX = false; // Saða bak
        }
        else if (movement.x < 0)
        {
            spriteRenderer.flipX = true; // Sola bak
        }

        // Dash iþlemini baþlatma
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing && canDash)
        {
            StartDash();
        }

        // Dash iþleminin süresini kontrol et
        if (isDashing)
        {
            dashTime -= Time.deltaTime;
            if (dashTime <= 0)
            {
                EndDash();
            }
        }
    }

    void FixedUpdate()
    {
        // Eðer dash atýlýyorsa dash hýzýnda hareket et, deðilse normal hýzda
        float speed = isDashing ? dashSpeed : currentSpeed;
        rb.velocity = movement.normalized * speed;
    }

    void StartDash()
    {
        // Kamera sallanmasýný tetikle
      /*  if (cameraShake != null)
        {
            cameraShake.TriggerShake();
        }
      */
        isDashing = true;
        canDash = false;
        dashTime = dashDuration;
        animator.SetTrigger("Dash"); // Dash animasyonunu tetikle
        Invoke(nameof(ResetDash), dashCooldown);
        // Trail Renderer'ý etkinleþtir
        if (dashTrail != null)
        {
            dashTrail.emitting = true;
        }

    }

    void EndDash()
    {
        isDashing = false;
        // Trail Renderer'ý devre dýþý býrak
        if (dashTrail != null)
        {
            dashTrail.emitting = false;
        }
    }

    void ResetDash()
    {
        canDash = true;
    }
}
