using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Tambahkan ini jika ingin referensi Button

public class IntroScene : MonoBehaviour
{
    [SerializeField] float introDuration = 3f; // durasi intro dalam detik

    private void Start()
    {
        Invoke(nameof(LoadRumahScene), introDuration);
    }

    private void LoadRumahScene()
    {
        musicmanager.Instance.PlayMusic("game");
        SceneManager.LoadScene("SampleScene");
    }
    public void SkipIntroAndPlay()
    {
        musicmanager.Instance.PlayMusic("game");
        SceneManager.LoadScene("SampleScene");
    }
    

    // Fungsi ini dipanggil oleh tombol Skip
    public void OnSkipButtonPressed()
    {
        CancelInvoke(nameof(LoadRumahScene)); // Batalkan invoke jika tombol ditekan
        LoadRumahScene();
    }
}