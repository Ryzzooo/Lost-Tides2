using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroScene : MonoBehaviour
{
    [SerializeField] float introDuration = 6f; // Total durasi intro, 6 detik karena 2 gambar x 3 detik
    [SerializeField] Image imageDisplay; // Drag UI Image ke sini
    [SerializeField] Sprite[] sprites; // Isi 2 gambar di Inspector
    [SerializeField] float changeInterval = 3f; // Ganti tiap 3 detik

    private int currentIndex = 0;

    private void Start()
    {
        InvokeRepeating(nameof(ChangeImage), 0f, changeInterval); // Ganti gambar
        Invoke(nameof(LoadRumahScene), introDuration); // Pindah scene
    }

    private void ChangeImage()
    {
        if (sprites.Length == 0 || imageDisplay == null) return;

        imageDisplay.sprite = sprites[currentIndex];
        currentIndex = (currentIndex + 1) % sprites.Length; // Ulangi terus
    }

    private void LoadRumahScene()
    {
        SceneManager.LoadScene("SampleScene"); // Ganti dengan nama scene kamu
    }

    public void OnSkipButtonPressed()
    {
        CancelInvoke(nameof(LoadRumahScene));
        CancelInvoke(nameof(ChangeImage));
        LoadRumahScene();
    }
}

