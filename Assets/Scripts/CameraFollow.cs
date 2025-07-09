using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Takip edilecek nesne (karakter)
    public float smoothSpeed = 0.125f; // Kamera hareketinin yumuþaklýðý
    public Vector3 offset; // Kameranýn karaktere göre pozisyon farký

    public BoxCollider2D boundBox;
    private Vector3 minBounds;
    private Vector3 maxBounds;
    private Camera theCamera;
    private float halfheight;
    private float halfwidth;


    private void Start()
    {
        minBounds = boundBox.bounds.min;
        maxBounds = boundBox.bounds.max;
        theCamera = GetComponent<Camera>();
        halfheight = theCamera.orthographicSize;
        halfwidth = halfheight * Screen.width / Screen.height;
    }

    void LateUpdate()
    {
        // Hedef pozisyonu belirle (karakterin pozisyonu + ofset)
        Vector3 desiredPosition = target.position + offset;

        // Kameranýn geçiþini yumuþat
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Kameranýn pozisyonunu güncelle
        transform.position = smoothedPosition;


        float clampedX = Mathf.Clamp(transform.position.x, minBounds.x + halfwidth, maxBounds.x - halfwidth);
        float clampedY = Mathf.Clamp(transform.position.y, minBounds.y + halfheight, maxBounds.y - halfheight);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
