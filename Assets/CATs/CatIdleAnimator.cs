using UnityEngine;

public class CatIdleAnimator : MonoBehaviour
{
    private Animator animator;
    public AudioClip jumpSoundEffect;
    private AudioSource audioSource;
    public Transform player; // Oyuncu pozisyonunu almak için referans
    public float followSpeed = 2f; // Kedinin oyuncuyu takip etme hýzý
    public float verticalOffsetRange = 0.5f; // Rastgele yukarý-aþaðý sapma aralýðý
    private float timeBetweenAnimations = 4f; // Idle animasyonlarý arasý süre
    private float timer;
    private Vector2 randomVerticalOffset;

    public float catSpeed = 0f; // Kedinin hýz deðerini belirlemek için

    private string[] idleAnimations = { "Idle1", "Idle2", "Idle3", "Idle4", "Idle5", "Idle6" };
    private bool canFollowPlayer = false; // 2 saniye bekleme durumu

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        timer = timeBetweenAnimations;
        PlayRandomIdleAnimation();
        GenerateNewVerticalOffset(); // Ýlk takip için rastgele yukarý-aþaðý sapma üret

        // 2 saniye sonra hareket etmeye baþla
        Invoke("EnableFollowPlayer", 2f);
    }

    void Update()
    {
        // Eðer 2 saniye sonra oyuncuyu takip etmeye baþladýysa
        if (canFollowPlayer)
        {
            // Oyuncuya olan mesafeyi kontrol et
            if (Vector2.Distance(transform.position, player.position) > 0.1f)
            {
                catSpeed = followSpeed; // Kedinin hýzýný takip hýzýna ayarla
                FollowPlayerWithOffset(); // Oyuncuyu takip et

                // Run animasyonu çalýþtýrma
                if (!animator.GetBool("IsRunning")) // Eðer kedi þu anda koþmuyor ise
                {
                    animator.SetTrigger("RunTrigger"); // Run animasyonunu baþlat
                    animator.SetBool("IsRunning", true); // Koþuyor durumunu güncelle
                }

                // Saða veya sola dönerken flip yapmak
                if (transform.position.x > player.position.x && transform.localScale.x > 0) // Saðdan sola hareket
                {
                    Flip();
                }
                else if (transform.position.x < player.position.x && transform.localScale.x < 0) // Soldan saða hareket
                {
                    Flip();
                }
            }
            else
            {
                // Oyuncu durduysa Idle animasyonlarý çalýþsýn
                catSpeed = 0; // Kedinin hýzýný sýfýrla
                timer -= Time.deltaTime;

                if (timer <= 0f)
                {
                    PlayRandomIdleAnimation();
                    timer = timeBetweenAnimations;
                }

                // Kedi durduysa, run animasyonunu sýfýrla ve idle animasyonuna geç
                if (animator.GetBool("IsRunning"))
                {
                    animator.SetBool("IsRunning", false); // Koþma durumunu sýfýrla
                }
            }
        }

        // 2 saniyelik bekleme durumunda idle animasyonu oynatma
        if (!canFollowPlayer)
        {
            catSpeed = 0; // Kedinin hýzýný sýfýrla
            animator.SetBool("IsRunning", false); // Koþma durumunu sýfýrla
            PlayRandomIdleAnimation();
        }
    }

    void FollowPlayerWithOffset()
    {
        // Oyuncuya doðru rastgele yukarý-aþaðý sapmayla hareket
        Vector2 targetPosition = new Vector2(player.position.x, player.position.y) + randomVerticalOffset;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, catSpeed * Time.deltaTime);

        // Belirli aralýklarla yeni bir sapma üret
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
        canFollowPlayer = true; // 2 saniye sonra kedi takip etmeye baþlasýn
    }

    private void Flip()
    {
        // Kedi saða-sola dönerken x ekseninde flip (ters) yapacak
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
