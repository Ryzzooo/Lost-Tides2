using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public GameObject[] healthUI;

    void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            health = 0;
            print("player dead");
            transform.position = RespawnManager.Instance.respawnPoint.position;
            health = healthUI.Length;
            foreach (GameObject icon in healthUI)
            {
                icon.SetActive(true);
            }

            return;
        }
        healthUI[health].SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemyattack"))
        {
            TakeDamage();
        }
    }
}