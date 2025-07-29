using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float lifetime = 5f;

    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = -transform.right * speed;
        Destroy(gameObject, lifetime); // Hancurkan otomatis setelah waktu habis
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            Destroy(gameObject);
    }
}