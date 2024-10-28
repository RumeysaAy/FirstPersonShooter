using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    [SerializeField] float restoreAngle = 90f;
    [SerializeField] float addIntensity = 1f;

    private void OnTriggerEnter(Collider other)
    {
        // eğer Player etiketli nesneyle çarpıştıysa
        if (other.gameObject.tag == "Player")
        {
            // oyuncu pili topladığında ışığın açısı
            other.GetComponentInChildren<FlashlightSystem>().RestoreLightAngle(restoreAngle);
            // oyuncu her pili topladığında ışığın yoğunluğu artar
            other.GetComponentInChildren<FlashlightSystem>().AddLightIntensity(addIntensity);

            Destroy(gameObject);
        }
    }
}
