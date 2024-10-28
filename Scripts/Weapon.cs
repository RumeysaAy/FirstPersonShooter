using System.Collections;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera; // ışının başlangıç noktası
    [SerializeField] float range = 100f; // ışının ne kadar ileri gidebileceği
    [SerializeField] float damage = 30f;
    [SerializeField] ParticleSystem muzzleFlash; // silah ateşlendiğinde namludan çıkan effect
    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] TextMeshProUGUI ammoText; // silaha göre mermi sayısını gösterecek

    bool canShoot = true;

    private void OnEnable() // bu sınıfın örneği etkinleştirildiğinde
    {
        // başka bir silaha geçtiğimde ateş edebilmek için
        canShoot = true;
    }

    void Update()
    {
        DisplayAmmo();

        // sol fare düğmesine basıldı mı?
        if (Input.GetMouseButtonDown(0) && canShoot == true)
        {
            StartCoroutine(Shoot()); // ateş et
        }
    }

    private void DisplayAmmo()
    {
        // kullanılan silaha göre mermi sayısının gösterilmesi
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = currentAmmo.ToString();
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0) // mermi varsa
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo(ammoType); // her ateş ettiğimde mermi sayısı azalacak
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        // ışının başlangıç noktası, yönü, vurulan nesne, ışının uzunluğu
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit)) // bir nesneye çarptı mı?
        {
            // Debug.Log("Vurulan nesne: " + hit.transform.name); // vurulan nesnenin ismi

            // vurulan nesne üzerinde efekt oluşturacağım
            CreateHitImpact(hit);

            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>(); // vurulan nesnenin sağlık bileşenini aldım

            if (target == null)
            {
                // vurulan nesne düşmansa sağlık bileşeni vardır
                return;
            }

            // EnemyHealth'te düşmanın sağlığını azaltan bir yöntem çağırır
            target.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        // vurulan nesnenin üzerinde efekt oluşsun
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 0.1f);
    }
}
