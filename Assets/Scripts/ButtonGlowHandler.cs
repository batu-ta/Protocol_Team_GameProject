using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonGlowHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
  
    public GameObject glowObject;

    void Start()
    {
        if (glowObject != null)
            glowObject.SetActive(false);
    }

    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (glowObject != null)
            glowObject.SetActive(true); // Iþýðý aç
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        if (glowObject != null)
            glowObject.SetActive(false); // Iþýðý kapat
    }
}