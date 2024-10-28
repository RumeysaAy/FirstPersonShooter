using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void ReloadGame()
    {
        SceneManager.LoadScene(1); // Asylum (barınak)
        Time.timeScale = 1; // oyun başladığında zaman başlasın
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
