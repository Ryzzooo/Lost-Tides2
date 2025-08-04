using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public float health;
    float maxHealth;
    public Image healthUI;

    public GameObject exitTriggerZone; // Zona ke scene berikutnya

    private void Start()
    {
        maxHealth = health;

        if (exitTriggerZone != null)
            exitTriggerZone.SetActive(false); // Nonaktif di awal
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (healthUI != null)
            healthUI.fillAmount = health / maxHealth;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (exitTriggerZone != null)
            exitTriggerZone.SetActive(true); // Aktifkan portal

        Destroy(gameObject);
    }
}
