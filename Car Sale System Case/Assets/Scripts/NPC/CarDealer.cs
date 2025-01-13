using UnityEngine;

public class CarDealer : MonoBehaviour, IInteractable 
{
    public CarDealerUI dealerUI; // UI Panel'i buraya bağlayacağız
    public void Interact()
    {
        Debug.Log("Araç Satıcısıyla etkileşim başladı!");
        
        // UI Panelini aç
        if (dealerUI != null)
            dealerUI.OpenUI();
    }
}