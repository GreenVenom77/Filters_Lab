using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchScreen_Controller : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private CinemachineOrbitalTransposer VCamOrbital;
    private Player_Controller Player_Controller;
    private Image camControlArea;
    private string inputAxis = "Mouse X";

    void Start()
    {
        Player_Controller = GetComponentInParent<Player_Controller>();
        VCamOrbital = Player_Controller.Player_VCam.GetCinemachineComponent<CinemachineOrbitalTransposer>();
        camControlArea = GetComponent<Image>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(
            camControlArea.rectTransform,
            eventData.position,
            eventData.enterEventCamera,
            out Vector2 posOut))
        {
            VCamOrbital.m_XAxis.m_InputAxisName = inputAxis;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        VCamOrbital.m_XAxis.m_InputAxisName = null;
        VCamOrbital.m_XAxis.m_InputAxisValue = 0;
    }
}
