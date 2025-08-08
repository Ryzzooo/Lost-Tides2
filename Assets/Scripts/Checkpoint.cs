using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RespawnManager.Instance.SetCheckpoint(transform);
            Debug.Log("✅ Checkpoint touched: " + transform.position);
        }
    }
}
