using System.Collections;
using UnityEngine;

public class DisplayDamage : MonoBehaviour
{
    [SerializeField] Canvas impactCanvas; // darbe çizgileri
    [SerializeField] float impactTime = 0.3f; // ekranda durma süresi

    void Start()
    {
        // zombi saldırdığında aktifleştireceğim
        impactCanvas.enabled = false;
    }

    public void ShowDamageImpact()
    {
        StartCoroutine(ShowSplatter());
    }

    IEnumerator ShowSplatter() // Sıçramayı Göster
    {
        impactCanvas.enabled = true;
        yield return new WaitForSeconds(impactTime);
        impactCanvas.enabled = false;
    }
}
