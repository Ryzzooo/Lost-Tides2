using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GoalManager : MonoBehaviour
{
    public Rigidbody2D bossRb;
    public static GoalManager singleton;
    public int botolNeeded, botolCollected;
    public float bossGravity = 1f;
    public bool canEnter;
    public Text scoreUI;

    private void Awake()
    {
        singleton = this;
    }
    public void CollectBotol()
    {
        botolCollected++;
        scoreUI.text = botolCollected.ToString();
        if (botolCollected >= botolNeeded)
        {
            canEnter = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canEnter)
        {
            Debug.Log("Muncul Boss");
            bossRb.gravityScale = bossGravity;
            bossRb.linearVelocity = new Vector2(0, -10f);
        }
    }
}
