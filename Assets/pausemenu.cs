using UnityEngine;

public class Pausemenu : MonoBehaviour
{
    [Header("Panel Utama")]
    public GameObject pauseMenuUI;
    public GameObject optionPanel;
    public GameObject volumePanel;
    public GameObject creditPanel;
    public GameObject controlPanel;

    private bool isPaused = false;

    void Update()
    {
        // Tombol keyboard M untuk pause / resume
        if (Input.GetKeyDown(KeyCode.M))
        {
            TogglePause();
        }
    }

    // Bisa dipanggil dari keyboard atau tombol UI Pause
    public void TogglePause()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;

        // Tampilkan pause menu dan sembunyikan panel lainnya
        pauseMenuUI.SetActive(true);
        optionPanel.SetActive(false);
        volumePanel.SetActive(false);
        creditPanel.SetActive(false);
        controlPanel.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;

        // Tutup semua panel
        pauseMenuUI.SetActive(false);
        optionPanel.SetActive(false);
        volumePanel.SetActive(false);
        creditPanel.SetActive(false);
        controlPanel.SetActive(false);
    }

    // --- Navigasi antar panel ---
    public void OpenOptionPanel()
    {
        pauseMenuUI.SetActive(false);
        optionPanel.SetActive(true);
        volumePanel.SetActive(false);
        creditPanel.SetActive(false);
        controlPanel.SetActive(false);
    }

    public void OpenVolumePanel()
    {
        optionPanel.SetActive(false);
        volumePanel.SetActive(true);
    }

    public void OpenCreditPanel()
    {
        pauseMenuUI.SetActive(false);
        creditPanel.SetActive(true);
    }

    public void OpenControlPanel()
    {
        optionPanel.SetActive(false);
        controlPanel.SetActive(true);
    }

    // Back button dari semua panel (kecuali PauseMenu)

}
