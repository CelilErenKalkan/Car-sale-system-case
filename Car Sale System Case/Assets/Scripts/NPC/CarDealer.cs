using UnityEngine;

public class CarDealer : MonoBehaviour, IInteractable {
    public void Interact() {
        Debug.Log("Araç Satıcısıyla etkileşim başladı!");
        // Burada araç satış UI'sini açabilirsiniz.
    }
}