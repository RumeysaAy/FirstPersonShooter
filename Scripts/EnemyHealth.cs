using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    bool isDead = false;

    public bool IsDead()
    {
        return isDead;
    }

    // hitPoints'i hasar/damage miktarına göre azaltan genel bir yöntem
    public void TakeDamage(float damage) // düşman hasar aldığında
    {
        // düşmanın sağlığı azalırsa oyuncu saldırmıştır yani düşman kışkırtılır
        // GetComponent<EnemyAI>().OnDamageTaken();
        // bunu BroadcastMessage() ile yapabilirim
        BroadcastMessage("OnDamageTaken"); // aynı nesnedeki bütün OnDamageTaken() fonksiyonlarını çağırır

        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;
        // sağlık sıfır olunca animatörde die durumuna/animasyonuna geç
        GetComponent<Animator>().SetTrigger("die");
    }
}
