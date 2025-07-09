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

    public float acceleration = 2f; // H�zlanma ivmesi
    public float deceleration = 2f; // Yava�lama ivmesi
    private float currentSpeed = 0f;

    public Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;
    public TrailRenderer dashTrail;
    //  private CameraShake cameraShake; // Kamera sallanma script referans�
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //  cameraShake = Camera.main.GetComponent<CameraShake>(); // Ana kameradan script'i al

        // Di�er ba�lang�� ayarlar�
        if (dashTrail != null)
        {
            dashTrail.emitting = false; // Ba�lang��ta Trail Renderer'� kapal� tut
        }
    }

    void Update()
    {
        // Hareket giri�lerini al
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Yava� yava� h�zlanma ve yava�lama
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

        // Karakterin sa�a/sola d�nmesini sa�la
        if (movement.x > 0)
        {
            spriteRenderer.flipX = false; // Sa�a bak
        }
        else if (movement.x < 0)
        {
            spriteRenderer.flipX = true; // Sola bak
        }

        // Dash i�lemini ba�latma
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing && canDash)
        {
            StartDash();
        }

        // Dash i�leminin s�resini kontrol et
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
        // E�er dash at�l�yorsa dash h�z�nda hareket et, de�ilse normal h�zda
        float speed = isDashing ? dashSpeed : currentSpeed;
        rb.velocity = movement.normalized * speed;
    }

    void StartDash()
    {
        // Kamera sallanmas�n� tetikle
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
        // Trail Renderer'� etkinle�tir
        if (dashTrail != null)
        {
            dashTrail.emitting = true;
        }

    }

    void EndDash()
    {
        isDashing = false;
        // Trail Renderer'� devre d��� b�rak
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
