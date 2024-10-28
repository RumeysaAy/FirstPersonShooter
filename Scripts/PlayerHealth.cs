using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f; // oyuncunun sağlığı

    public void TakeDamage(float damage)
    {
        // düşman saldırdığında oyuncunun sağlığı azalacak
        hitPoints -= damage;

        if (hitPoints <= 0)
        {
            // oyuncu oldüğünde oyun bitti ekranı çıksın ve imleç kullanılabilir hale gelsin
            GetComponent<DeathHandler>().HandleDeath();
        }
    }
}
