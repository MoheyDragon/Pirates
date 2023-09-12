using UnityEngine;
using Cinemachine;
using StarterAssets;
public class ZoomController : Singleton<ZoomController>
{
    [SerializeField] CinemachineVirtualCamera vCam;
    [SerializeField] float minZoom, maxZoom, defaultZoom;
    float currentZoom;
    Cinemachine3rdPersonFollow cinemachine3RdPersonFollow;
    void Start()
    {
        cinemachine3RdPersonFollow = vCam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        currentZoom = defaultZoom;
        cinemachine3RdPersonFollow.CameraDistance = currentZoom;
    }
    public void ChangeInputListner(StarterAssetsInputs input)
    {
        input.OnZoomAction += Zoom;
    }
    public void Zoom(float newZoom)
    {
        ClampZoom(newZoom);
        cinemachine3RdPersonFollow.CameraDistance = currentZoom;
    }
    private void ClampZoom(float newZoom)
    {
        currentZoom += newZoom;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
    }
}
