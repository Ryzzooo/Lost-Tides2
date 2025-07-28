using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    float maxHealth;
    public Image healthUI;

    private void Start()
    {
        maxHealth = health;
    }

public void TakeDamage(float Damage)
    {
        health -= Damage;
        healthUI.fillAmount = health/maxHealth;

        //Knockback code example
        //GetComponent<Rigidbody2D>().AddForce((GetComponent<Rigidbody2D>().position - origin).normalized * 500f, ForceMode2D.Force);
 
        if (health <= 0)
        {
            Destroy(gameObject);
        }
 
    }
}
