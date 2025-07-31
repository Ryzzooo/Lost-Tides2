using UnityEngine;

public class BossShoot : MonoBehaviour
{
    public GameObject bossBullet;
    public Transform shootPoint;
    public float fireForce = 5f;
    public float fireInterval = 3f;

    private float fireTimer;

    public Transform player;
    public Animator animator;

    private bool isFacingRight = false;

    void Start()
    {
        fireTimer = fireInterval;
    }

    void Update()
    {
        fireTimer -= Time.deltaTime;

        Vector2 direction = player.position - transform.position;

        if (direction.x < 0 && isFacingRight) Flip();
        else if (direction.x > 0 && !isFacingRight) Flip();

        if (fireTimer <= 0)
        {
            animator.SetTrigger("attack");
            fireTimer = fireInterval;
        }
    }

    // Ini dipanggil Animation Event
    public void FireBullet()
    {
        GameObject bulletObj = Instantiate(bossBullet, shootPoint.position, Quaternion.identity);

        Vector2 shootDir = (player.position - shootPoint.position).normalized;

        BossBullet bulletScript = bulletObj.GetComponent<BossBullet>();
        bulletScript.SetDirection(shootDir);
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
