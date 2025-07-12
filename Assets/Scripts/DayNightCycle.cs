using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour
{
    public Light2D light2D;
    public float dayOuterRadius = 10f;
    public float nightOuterRadius = 2f;
    public float transitionDuration = 2f;
    public float changeInterval = 10f;

    public Image dayNightIcon;
    public Sprite daySprite;
    public Sprite nightSprite;
    public Text dayText;

    public Image freezingIcon;
    public HealthSystem playerHealth;

    [Header("Hunger System")]
    public Slider hungerSlider;
    public int maxHunger = 50;
    public int currentHunger = 50;
    public float hungerDecreaseInterval = 1f;
    public int hungerDecreaseAmount = 1;
    private float hungerTimer = 0f;

    public int hungerRegenHealth = 2;
    public float regenInterval = 1f;
    private float regenTimer = 0f;

    public int starvationDamage = 3;

    private bool isDay = true;
    private float timer = 0f;
    private float transitionTimer = 0f;
    private bool isTransitioning = false;
    private float startRadius, targetRadius;
    private int currentDay = 1;

    private bool isPlayerInWarmZone = false;
    private float freezeTimer = 0f;
    public float freezeDamageInterval = 1f;
    public int freezeDamageAmount = 5;

    void Update()
    {
        timer += Time.deltaTime;

        // GÜNDÜZ BOYUNCA AÇLIK AZALIR
        if (isDay)
        {
            hungerTimer += Time.deltaTime;
            if (hungerTimer >= hungerDecreaseInterval)
            {
                hungerTimer = 0f;
                currentHunger = Mathf.Max(currentHunger - hungerDecreaseAmount, 0);
                UpdateHungerUI();
            }
        }

        // Gün/Gece geçiş kontrolü
        if (timer >= changeInterval && !isTransitioning)
        {
            timer = 0f;
            isDay = !isDay;

            if (isDay)
            {
                currentDay++;
                if (currentDay > 30) currentDay = 1;
                UpdateUI();
            }

            StartTransition();
        }

        // Işık geçişi
        if (isTransitioning)
        {
            transitionTimer += Time.deltaTime;
            float t = transitionTimer / transitionDuration;
            light2D.pointLightOuterRadius = Mathf.Lerp(startRadius, targetRadius, t);
            if (t >= 1f) isTransitioning = false;
        }

        // GECEDE DIŞARIDA İSE DONMA HASARI
        if (!isDay && !isPlayerInWarmZone)
        {
            if (freezingIcon != null) freezingIcon.enabled = true;

            freezeTimer += Time.deltaTime;
            if (freezeTimer >= freezeDamageInterval)
            {
                freezeTimer = 0f;
                if (playerHealth != null)
                    playerHealth.TakeDamage(freezeDamageAmount);
            }
        }
        else
        {
            freezeTimer = 0f;
            if (freezingIcon != null) freezingIcon.enabled = false;
        }

        // SADECE GECEYKEN AÇLIK ETKİSİ OLUŞUR
        if (!isDay)
        {
            regenTimer += Time.deltaTime;
            if (regenTimer >= regenInterval)
            {
                regenTimer = 0f;

                if (currentHunger > 25)
                {
                    if (playerHealth != null)
                        playerHealth.HealDamage(hungerRegenHealth); // Can yenileme (Heal varsa)
                }
                else if (currentHunger <= 0)
                {
                    if (playerHealth != null)
                        playerHealth.TakeDamage(starvationDamage); // Açlıktan hasar
                }
            }
        }
    }

    void StartTransition()
    {
        isTransitioning = true;
        transitionTimer = 0f;
        startRadius = light2D.pointLightOuterRadius;
        targetRadius = isDay ? dayOuterRadius : nightOuterRadius;
        if (dayNightIcon != null)
            dayNightIcon.sprite = isDay ? daySprite : nightSprite;
    }

    void UpdateUI()
    {
        if (dayText != null)
            dayText.text = "Day " + currentDay;
    }

    void UpdateHungerUI()
    {
        if (hungerSlider != null)
        {
            hungerSlider.maxValue = maxHunger;
            hungerSlider.value = currentHunger;
        }
    }

    public void EnterWarmZone()
    {
        isPlayerInWarmZone = true;
    }

    public void ExitWarmZone()
    {
        isPlayerInWarmZone = false;
    }

    public void IncreaseHunger(int amount)
    {
        currentHunger += amount;
        if (currentHunger > maxHunger)
            currentHunger = maxHunger;
        UpdateHungerUI();

        Debug.Log("Açlık arttı: " + currentHunger);
    }
}
