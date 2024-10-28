using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    // oyuncu öldüğünde oyun bitti ekranı çıkacak
    [SerializeField] Canvas gameOverCanvas;

    private void Start()
    {
        // oyun bitti ekranı oyun başladığında gözükmesin
        gameOverCanvas.enabled = false;
    }

    public void HandleDeath()
    {
        // oyuncu öldüğünde ekrana oyun bitti ekranı çıksın
        gameOverCanvas.enabled = true;
        Time.timeScale = 0; // zaman dursun
        FindObjectOfType<WeaponSwitcher>().enabled = false; // silahın değiştirilmesi engellendi
        // oyuncunun imlece erişebilmesini sağlamak için imleç kilitli olmasın (espace tuşuna basılmış gibi)
        Cursor.lockState = CursorLockMode.None;
        // imleç görünür olsun
        Cursor.visible = true;
    }
}
