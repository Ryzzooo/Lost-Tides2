using UnityEngine;

public class fallingbox : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasFallen = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D tidak ditemukan!");
        }
        rb.gravityScale = 0f; // Pastikan awalnya 0
    }

    public void ActivateFall()
    {
        if (!hasFallen)
        {
            rb.gravityScale = 7f; // Aktifkan gravitasi
            hasFallen = true;
            Debug.Log("Box mulai jatuh!");
        }
    }
}