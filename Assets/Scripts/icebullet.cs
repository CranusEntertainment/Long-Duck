using UnityEngine;

public class icebullet : MonoBehaviour
{
    public Vector2 moveDirection; // Hareket y�n�
    public float moveSpeed = 5f; // H�z

    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime); // Hareket
    }

    private void Start()
    {
        Destroy(gameObject, 2f); // 2 saniye sonra mermiyi yok et
    }
}
