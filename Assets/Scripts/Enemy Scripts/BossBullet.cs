using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed, damage, destroyTime;
    private Vector2 moveDirection;

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    public void SetDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Enemy Hit hrsnya ilang");
            Destroy(gameObject);
        }
    }
}
