using UnityEngine;

public class CatIdleAnimator : MonoBehaviour
{
    private Animator animator;
    public AudioClip jumpSoundEffect;
    private AudioSource audioSource;
    public Transform player; // Oyuncu pozisyonunu almak i�in referans
    public float followSpeed = 2f; // Kedinin oyuncuyu takip etme h�z�
    public float verticalOffsetRange = 0.5f; // Rastgele yukar�-a�a�� sapma aral���
    private float timeBetweenAnimations = 4f; // Idle animasyonlar� aras� s�re
    private float timer;
    private Vector2 randomVerticalOffset;

    public float catSpeed = 0f; // Kedinin h�z de�erini belirlemek i�in

    private string[] idleAnimations = { "Idle1", "Idle2", "Idle3", "Idle4", "Idle5", "Idle6" };
    private bool canFollowPlayer = false; // 2 saniye bekleme durumu

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        timer = timeBetweenAnimations;
        PlayRandomIdleAnimation();
        GenerateNewVerticalOffset(); // �lk takip i�in rastgele yukar�-a�a�� sapma �ret

        // 2 saniye sonra hareket etmeye ba�la
        Invoke("EnableFollowPlayer", 2f);
    }

    void Update()
    {
        // E�er 2 saniye sonra oyuncuyu takip etmeye ba�lad�ysa
        if (canFollowPlayer)
        {
            // Oyuncuya olan mesafeyi kontrol et
            if (Vector2.Distance(transform.position, player.position) > 0.1f)
            {
                catSpeed = followSpeed; // Kedinin h�z�n� takip h�z�na ayarla
                FollowPlayerWithOffset(); // Oyuncuyu takip et

                // Run animasyonu �al��t�rma
                if (!animator.GetBool("IsRunning")) // E�er kedi �u anda ko�muyor ise
                {
                    animator.SetTrigger("RunTrigger"); // Run animasyonunu ba�lat
                    animator.SetBool("IsRunning", true); // Ko�uyor durumunu g�ncelle
                }

                // Sa�a veya sola d�nerken flip yapmak
                if (transform.position.x > player.position.x && transform.localScale.x > 0) // Sa�dan sola hareket
                {
                    Flip();
                }
                else if (transform.position.x < player.position.x && transform.localScale.x < 0) // Soldan sa�a hareket
                {
                    Flip();
                }
            }
            else
            {
                // Oyuncu durduysa Idle animasyonlar� �al��s�n
                catSpeed = 0; // Kedinin h�z�n� s�f�rla
                timer -= Time.deltaTime;

                if (timer <= 0f)
                {
                    PlayRandomIdleAnimation();
                    timer = timeBetweenAnimations;
                }

                // Kedi durduysa, run animasyonunu s�f�rla ve idle animasyonuna ge�
                if (animator.GetBool("IsRunning"))
                {
                    animator.SetBool("IsRunning", false); // Ko�ma durumunu s�f�rla
                }
            }
        }

        // 2 saniyelik bekleme durumunda idle animasyonu oynatma
        if (!canFollowPlayer)
        {
            catSpeed = 0; // Kedinin h�z�n� s�f�rla
            animator.SetBool("IsRunning", false); // Ko�ma durumunu s�f�rla
            PlayRandomIdleAnimation();
        }
    }

    void FollowPlayerWithOffset()
    {
        // Oyuncuya do�ru rastgele yukar�-a�a�� sapmayla hareket
        Vector2 targetPosition = new Vector2(player.position.x, player.position.y) + randomVerticalOffset;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, catSpeed * Time.deltaTime);

        // Belirli aral�klarla yeni bir sapma �ret
        if (Vector2.Distance(transform.position, targetPosition) < 0.2f)
        {
            GenerateNewVerticalOffset();
        }
    }

    void PlayRandomIdleAnimation()
    {
        int randomIndex = Random.Range(0, idleAnimations.Length);
        string randomIdle = idleAnimations[randomIndex];
        animator.Play(randomIdle);
    }

    private void GenerateNewVerticalOffset()
    {
        randomVerticalOffset = new Vector2(0, Random.Range(-verticalOffsetRange, verticalOffsetRange));
    }

    private void EnableFollowPlayer()
    {
        canFollowPlayer = true; // 2 saniye sonra kedi takip etmeye ba�las�n
    }

    private void Flip()
    {
        // Kedi sa�a-sola d�nerken x ekseninde flip (ters) yapacak
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            animator.ResetTrigger("JumpTrigger");
            animator.SetTrigger("JumpTrigger");

            if (jumpSoundEffect != null && audioSource != null)
            {
                audioSource.PlayOneShot(jumpSoundEffect);
            }

            Invoke("PlayIdleAfterJump", 0.4f);
        }
    }

    void PlayIdleAfterJump()
    {
        PlayRandomIdleAnimation();
    }
}
