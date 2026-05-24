using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Týklama olaylarýný algýlamak için gerekli

public class ButtonGlowHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Buraya sahnedeki "ButtonGlow" objesini sürükleyip býrakacaðýz.
    public GameObject glowObject;

    void Start()
    {
        // Oyun baþladýðýnda ýþýk kapalý olsun.
        if (glowObject != null)
            glowObject.SetActive(false);
    }

    // Fareyle butona týklandýðý an (basýlý tutulurken)
    public void OnPointerDown(PointerEventData eventData)
    {
        if (glowObject != null)
            glowObject.SetActive(true); // Iþýðý aç
    }

    // Fare týký býrakýldýðý an
    public void OnPointerUp(PointerEventData eventData)
    {
        if (glowObject != null)
            glowObject.SetActive(false); // Iþýðý kapat
    }
}