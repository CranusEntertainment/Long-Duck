using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public static HealthSystem Instance;

    public Slider currentHealthSlider;
    public Text healthText;
    public float hitPoint = 100f;
    public float maxHitPoint = 100f;

    public bool Regenerate = true;
    public float regen = 0.1f;
    private float timeleft = 0.0f;
    public float regenUpdateInterval = 1f;

    private float displayedHealthRatio = 1f; // Slider'da gösterilen geçici oran
    public float sliderLerpSpeed = 5f;
    private float displayedHealth = 100f; // Slider'da gösterilecek geçici can
    public bool GodMode;

    public Animator Duckanimator;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateGraphics();
        timeleft = regenUpdateInterval;
    }

    void Update()
    {
        if (Regenerate)
            Regen();
    }

    private void Regen()
    {
        timeleft -= Time.deltaTime;

        if (timeleft <= 0.0f)
        {
            if (GodMode)
            {
                HealDamage(maxHitPoint);
            }
            else
            {
                HealDamage(regen);
            }

            UpdateGraphics();
            timeleft = regenUpdateInterval;
        }
    }

    private void UpdateHealthBar()
    {
        currentHealthSlider.value = hitPoint;
        healthText.text = hitPoint.ToString("0") + "/" + maxHitPoint.ToString("0");
    }




    public void TakeDamage(float Damage)
    {
        hitPoint -= Damage;
        if (hitPoint < 1)
            hitPoint = 0;

        UpdateGraphics();
        StartCoroutine(PlayerHurts());
    }

    public void HealDamage(float Heal)
    {
        hitPoint += Heal;
        if (hitPoint > maxHitPoint)
            hitPoint = maxHitPoint;

        UpdateGraphics();
    }

    public void SetMaxHealth(float max)
    {
        maxHitPoint += (int)(maxHitPoint * max / 100);
        UpdateGraphics();
    }

    private void UpdateGraphics()
    {
        UpdateHealthBar();
    }

    IEnumerator PlayerHurts()
    {
        PopupText.Instance.Popup("Ouch!", 1f, 1f);

        if (hitPoint < 1)
        {
            yield return StartCoroutine(PlayerDied());
        }
        else
        {
            yield return null;
        }
    }

    IEnumerator PlayerDied()
    {
        Debug.Log("ördek öldü !");
        Duckanimator.SetTrigger("ded");
        PopupText.Instance.Popup("You have died!", 1f, 1f);

        yield return null;
    }
}
