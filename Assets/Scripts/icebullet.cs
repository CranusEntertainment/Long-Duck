using UnityEngine;

public class icebullet : MonoBehaviour
{
    public Vector2 moveDirection; // Hareket yönü
    public float moveSpeed = 5f; // Hýz

    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime); // Hareket
    }

    private void Start()
    {
        Destroy(gameObject, 2f); // 2 saniye sonra mermiyi yok et
    }
}
