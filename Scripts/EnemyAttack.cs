using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    PlayerHealth target; // oyuncunun sağlığı
    [SerializeField] float damage = 40f; // oyuncuya verilen hasar

    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
    }

    public void OnDamageTaken()
    {
        // düşman oyuncudan hasar alırsa çağrılır
        Debug.Log(name + "'ye oyuncu hasar verdi.");
    }

    public void AttackHitEvent()
    {
        if (target == null)
        {
            return;
        }
        // oyuncunun sağlığı azaltılır.
        target.TakeDamage(damage);

        // darbe çizikleri gösterilsin
        target.GetComponent<DisplayDamage>().ShowDamageImpact();
    }
}
