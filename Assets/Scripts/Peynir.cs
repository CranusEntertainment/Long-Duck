using UnityEngine;

public class Peynir : MonoBehaviour
{
    public float healAmount = 20f; // Peynirin verece�i can miktar�
    public int scoreIncreaseAmount = 10; // Peynire temas edince artacak skor miktar�

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // HealthSystem'deki can art�rma fonksiyonunu �a��r
            HealthSystem.Instance.HealDamage(healAmount);

            // ScoreManager'daki skoru art�rma fonksiyonunu �a��r
            ScoreManager.Instance.IncreaseScore(scoreIncreaseAmount);

            // Peynir objesini yok et
            Destroy(gameObject);
        }
    }
}
