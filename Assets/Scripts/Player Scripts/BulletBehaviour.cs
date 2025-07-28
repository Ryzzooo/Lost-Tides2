using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour
{
    public float speed, damage, destroyTime;
    public float wait = 0.3f;
    //private bool ilang = false;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        Destroy(gameObject, destroyTime);
    }
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth enemy = collision.GetComponentInParent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            else
            {
                Debug.LogWarning("EnemyHealth tidak ditemukan pada atau di atas " + collision.name);
            }

            StartCoroutine(Hilang());
        }
        else if (collision.CompareTag("Environment"))
        {
            StartCoroutine(Hilang());
        }
        else if (collision.CompareTag("Ground"))
        {
            StartCoroutine(Hilang());
        }
    }

    private IEnumerator Hilang()
    {
        anim.SetTrigger("ilang");
        speed = 0;
        boxCollider.enabled = false;
        yield return new WaitForSeconds(wait);
        Destroy(gameObject);
    }
}
