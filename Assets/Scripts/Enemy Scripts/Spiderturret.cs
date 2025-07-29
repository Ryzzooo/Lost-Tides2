using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Animations;

public class SpiderTurret : MonoBehaviour
{
    [SerializeField] float scanRadius = 5f;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float fireDelay = 1f;

    private Collider2D target;
    private void Start()
    {
        InvokeRepeating("Fire", 0f, fireDelay);
    }

    private void Update()
    {
        CheckEnviroment();
        LookAtTarget();
    }


    private void CheckEnviroment() { 
        target = Physics2D.OverlapCircle(transform.position, scanRadius, playerLayer);
    }

    private void LookAtTarget()
    {
        if (target != null)
        {
            Vector2 direction = target.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void Fire()
    {
        if (target != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, scanRadius);
    }
}
