using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance;

    public Transform respawnPoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Bisa diisi default posisi player saat start
        if (respawnPoint == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                respawnPoint = player.transform;
        }
    }

    public void SetCheckpoint(Transform checkpoint)
    {
        respawnPoint = checkpoint;
        Debug.Log("✅ Checkpoint updated to: " + checkpoint.position);
    }

    public void Respawn(GameObject player)
    {
        player.transform.position = respawnPoint.position;
        Debug.Log("🔁 Player respawned to: " + respawnPoint.position);
    }
}
