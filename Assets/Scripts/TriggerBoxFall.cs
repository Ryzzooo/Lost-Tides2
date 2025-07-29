using UnityEngine;

public class TriggerBoxFall : MonoBehaviour
{
    public fallingbox boxToFall; // Ganti MonoBehaviour ke FallingBoxController

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered by: " + other.name);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected, activating box fall");
            boxToFall.ActivateFall();
        }
    }

    private void Update()
    {
        Debug.Log("TriggerBoxFall aktif");
    }
}