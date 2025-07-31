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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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