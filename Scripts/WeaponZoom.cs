using Cinemachine;
using UnityEngine;
using StarterAssets;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera fpsCamera;
    [SerializeField] float zoomedOutFOV = 30f; // uzaklaştır
    [SerializeField] float zoomedInFOV = 20f; // yakınlaştır
    [SerializeField] float zoomOutSensitivity = 2f; // uzaklaştırdığımdaki hassasiyet
    [SerializeField] float zoomInSensitivity = 0.5f; // yakınlaştırdığımdaki hassasiyet

    FirstPersonController fpsController;

    bool zoomedInToggle = false; // yakınlaştırıldı mı?

    private void OnDisable()
    {
        // Silahı değiştirdiğimde yani devre dışı bırakıldığında, uzaklaştıracağım
        ZoomOut();
    }

    void Start()
    {
        // fpsController = FindObjectOfType<FirstPersonController>();
        fpsController = GetComponentInParent<FirstPersonController>();
    }

    void Update()
    {
        // mouse'un sağ tuşuna tıklanırsa
        if (Input.GetMouseButtonDown(1))
        {
            if (fpsCamera != null)
            {
                if (zoomedInToggle == false) // yakınlaştırılmamışsa
                {
                    ZoomIn();
                }
                else // yakınlaştırılmışsa
                {
                    ZoomOut();
                }
            }
        }
    }

    private void ZoomIn()
    {
        zoomedInToggle = true; // yakınlaştırıldı
        fpsCamera.m_Lens.FieldOfView = zoomedInFOV; // yakınlaştır
        fpsController.RotationSpeed = zoomInSensitivity;
    }

    private void ZoomOut()
    {
        zoomedInToggle = false; // uzaklaştırıldı
        fpsCamera.m_Lens.FieldOfView = zoomedOutFOV; // uzaklaştır
        fpsController.RotationSpeed = zoomOutSensitivity;
    }
}
