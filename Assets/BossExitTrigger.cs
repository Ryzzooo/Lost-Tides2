using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossExitTrigger : MonoBehaviour
{
    public GameObject winnerPanel;         // ← Drag WinnerPanel ke sini
    public Button nextStageButton;         // ← Drag tombol ke sini
    public string nextSceneName;           // ← Nama scene selanjutnya

    private void Start()
    {
        if (winnerPanel != null)
            winnerPanel.SetActive(false);  // Panel tidak muncul di awal
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (winnerPanel != null)
                winnerPanel.SetActive(true); // Munculkan panel

            if (nextStageButton != null)
            {
                nextStageButton.onClick.RemoveAllListeners(); // Bersihkan agar tidak dobel
                nextStageButton.onClick.AddListener(() => LoadNextScene());
            }
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
