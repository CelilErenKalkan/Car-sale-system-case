using UnityEngine;

public class CarInteractable : MonoBehaviour, IInteractable
{
    private GameObject _hoodCamera, _wheelCamera;
    private RCC_CarControllerV3 _controllerV3;

    void Update()
    {
        if (_controllerV3.enabled)
            HandleInteraction();
    }
    
    private void OnEnable()
    {
        if (transform.TryGetComponent(out RCC_CarControllerV3 controller)) _controllerV3 = controller;
        _wheelCamera = transform.GetChild(transform.childCount - 1).gameObject;
        _hoodCamera = transform.GetChild(transform.childCount - 2).gameObject;
    }

    public void Interact() {
        Debug.Log("Araca binme tetiklendi!");
        // Araca binme mekanikleri burada başlatılır.
        
        SetCameras(true);
        SetControllerState(true);
    }
    
    // Etkileşim tuşu (E) ile tetikleme
    private void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector3 playerNewPos = transform.position;
            playerNewPos.x -= 2;
            Pool.Instance.SpawnObject(playerNewPos, PoolItemType.Player, null);

            SetCameras(false);
            SetControllerState(false);
        }
    }

    private void SetControllerState(bool state)
    {
        _controllerV3.enabled = state;
    }

    private void SetCameras(bool state)
    {
        _wheelCamera.SetActive(state);
        _hoodCamera.SetActive(state);
    }
}