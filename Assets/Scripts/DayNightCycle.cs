using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI; // UI kullanýmý için gerekli

public class DayNightCycle : MonoBehaviour
{
    public Light2D light2D;
    public float dayOuterRadius = 10f;
    public float nightOuterRadius = 2f;
    public float transitionDuration = 2f;
    public float changeInterval = 10f;

    public Image dayNightIcon; // UI Image (simge)
    public Sprite daySprite;   // Gündüz ikonu
    public Sprite nightSprite; // Gece ikonu

    public Text dayText; // "Day X" yazýsý

    private bool isDay = true;
    private float timer = 0f;
    private float transitionTimer = 0f;
    private bool isTransitioning = false;
    private float startRadius, targetRadius;
    private int currentDay = 1;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= changeInterval && !isTransitioning)
        {
            timer = 0f;
            isDay = !isDay;

            if (isDay)
            {
                currentDay++;
                if (currentDay > 30)
                    currentDay = 1;

                UpdateUI();
            }

            StartTransition();
        }

        if (isTransitioning)
        {
            transitionTimer += Time.deltaTime;
            float t = transitionTimer / transitionDuration;
            light2D.pointLightOuterRadius = Mathf.Lerp(startRadius, targetRadius, t);

            if (t >= 1f)
                isTransitioning = false;
        }
    }

    void StartTransition()
    {
        isTransitioning = true;
        transitionTimer = 0f;
        startRadius = light2D.pointLightOuterRadius;
        targetRadius = isDay ? dayOuterRadius : nightOuterRadius;

        // Ýkonu güncelle
        if (dayNightIcon != null)
        {
            dayNightIcon.sprite = isDay ? daySprite : nightSprite;
        }
    }

    void UpdateUI()
    {
        if (dayText != null)
        {
            dayText.text = "Day " + currentDay;
        }
    }
}
