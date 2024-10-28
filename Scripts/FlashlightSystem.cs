using UnityEngine;

public class FlashlightSystem : MonoBehaviour
{
    [SerializeField] float lightDecay = .1f; // ışığın zaman içinde azalma miktarı
    [SerializeField] float angleDecay = 1f; // ışığın zaman içinde küçülme miktarı
    [SerializeField] float minimumAngle = 40f; // minimum açı

    Light myLight; // el fenerimin ışık bileşeni

    private void Start()
    {
        myLight = GetComponent<Light>();
    }

    private void Update()
    {
        // Zaman geçtikçe ışık yoğunluğunu (lightDecay) ve ışık açısını (angleDecay) azaltacağım. 
        // Ancak ışık açısını belirli bir açıya kadar azaltacağım.
        DecreaseLightAngle();
        DecreaseLightIntensity();
    }

    // batarya toplandığında ışığın açısını restoreAngle'a eşitleyeceğim
    public void RestoreLightAngle(float restoreAngle)
    {
        myLight.spotAngle = restoreAngle;
    }

    // batarya toplandıkça ışığın yoğunluğu artıracağım
    public void AddLightIntensity(float intensityAmount)
    {
        myLight.intensity += intensityAmount;
    }

    private void DecreaseLightAngle()
    {
        if (myLight.spotAngle <= minimumAngle)
        {
            return;
        }
        else
        {
            myLight.spotAngle -= angleDecay * Time.deltaTime;
        }
    }

    private void DecreaseLightIntensity()
    {
        myLight.intensity -= lightDecay * Time.deltaTime;
    }
}
