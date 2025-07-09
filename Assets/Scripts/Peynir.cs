using UnityEngine;

public class Peynir : MonoBehaviour
{
    public float healAmount = 20f; // Peynirin vereceði can miktarý
    public int scoreIncreaseAmount = 10; // Peynire temas edince artacak skor miktarý

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // HealthSystem'deki can artýrma fonksiyonunu çaðýr
            HealthSystem.Instance.HealDamage(healAmount);

            // ScoreManager'daki skoru artýrma fonksiyonunu çaðýr
            ScoreManager.Instance.IncreaseScore(scoreIncreaseAmount);

            // Peynir objesini yok et
            Destroy(gameObject);
        }
    }
}
