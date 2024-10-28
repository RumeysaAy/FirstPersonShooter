using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] int currentWeapon = 0; // pistol yani tabanca

    void Start()
    {
        SetWeaponActive();
    }

    void Update()
    {
        int previousWeapon = currentWeapon; // önceki silah tabanca

        // silahın seçilmesi
        ProcessKeyInput(); // klavye ile
        ProcessScrollWhell(); // fare tekerleği ile

        // önceki silah seçilen silaha eşit değilse
        if (previousWeapon != currentWeapon)
        {
            SetWeaponActive(); // seçilen silaha geçilsin
        }
    }

    private void ProcessKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // eğer klavyede 1'e tıklandıysa
            currentWeapon = 0; // pistol seçilsin
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 1; // shotgun seçilsin
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = 2; // carbine seçilsin
        }
    }

    private void ProcessScrollWhell()
    {
        // pozitiften negatife doğru gidiyor
        // sıfırdan küçükse belirli bir yönde kaydırma yapıldığı anlamına gelir
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            // silah sayısı: transform.childCount
            // 2 >= 2 (3-1)
            if (currentWeapon >= transform.childCount - 1)
            {
                // 2 -> 0 seçilir
                currentWeapon = 0;
            }
            else
            {
                // 0 -> 1 seçilir
                // 1 -> 2 seçilir
                currentWeapon++;
            }
        }

        // negatiften pozitife doğru gidiyor
        // sıfırdan büyükse belirli bir yönde kaydırma yapıldığı anlamına gelir
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (currentWeapon <= 0)
            {
                // 0 -> 2 seçilir
                currentWeapon = transform.childCount - 1;
            }
            else
            {
                // 1 -> 0 seçilir
                // 2 -> 1 seçilir
                currentWeapon--;
            }
        }
    }

    private void SetWeaponActive()
    {
        // 0: Pistol
        // 1: Shotgun
        // 2: Carbine
        int weaponIndex = 0;

        // bu bileşeni Weapons nesnesine ekledim ve Weapons nesnesinin çocukları silahlardır
        // tüm silahlar sırasıyla dolaşılır
        foreach (Transform weapon in transform)
        {
            if (weaponIndex == currentWeapon)
            {
                // seçilen silah aktifleştirilir.
                weapon.gameObject.SetActive(true);
            }
            else
            {
                // diğer silahlar devre dışı bırakılır.
                weapon.gameObject.SetActive(false);
            }
            weaponIndex++;
        }
    }
}