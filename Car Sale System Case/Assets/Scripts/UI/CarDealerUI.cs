using Data_Management;
using UnityEngine;
using UnityEngine.UI;

public class CarDealerUI : MonoBehaviour 
{
    public GameObject carButtonPrefab; // Araba butonları için prefab
    public Transform carListParent;   // Butonların ekleneceği yer
    public Button closeButton;        // Pencereyi kapatmak için buton

    public GameObject[] carsAvailable; // Satıştaki arabalar

    private void Start() {
        closeButton.onClick.AddListener(CloseUI);

        // Arabaları listele
        foreach (GameObject car in carsAvailable)
        {
            Car carScript = car.GetComponent<Car>();
            GameObject newButton = Instantiate(carButtonPrefab, carListParent);
            newButton.GetComponent<CarPanel>().SetValues(carScript);
        }
    }

    public void OpenUI()
    {
        gameObject.SetActive(true);
        closeButton.gameObject.SetActive(true);
        EnableCursor(); // Mouse etkinleştirme
    }

    private void CloseUI() 
    {
        DisableCursor();
        Actions.MenuState?.Invoke(false);
        closeButton.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
    
    private void EnableCursor()
    {
        Cursor.lockState = CursorLockMode.None; // Mouse serbest bırakıldı
        Cursor.visible = true; // Mouse görünür
    }

    private void DisableCursor()
    {
        Cursor.lockState = CursorLockMode.Locked; // Mouse FPS kameraya kilitlendi
        Cursor.visible = false; // Mouse görünmez
    }
}