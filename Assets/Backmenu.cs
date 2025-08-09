using UnityEngine;
using UnityEngine.SceneManagement;

public class Backmenu : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu"); // Ganti "Menu" sesuai nama scene kamu
    }
}
