using System;
using StarterAssets;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private FirstPersonController controller;
    [SerializeField] private float interactionRange = 5f; // Etkileşim mesafesi
    [SerializeField] private LayerMask interactionLayers; // Etkileşim yapılabilir objelerin Layer'ı
    [SerializeField] private GameObject interactionUI; // "E'ye basın" UI'si

    private IInteractable currentInteractable;

    void Update()
    {
        CheckForInteractable();
        HandleInteraction();
    }

    // Raycast ile etkileşim yapılabilir objeyi kontrol et
    private void CheckForInteractable()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange, interactionLayers))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null) {
                currentInteractable = interactable;
                if (interactionUI != null)
                    interactionUI.SetActive(true);
                return;
            }
        }

        // Eğer bir şey algılanmazsa
        currentInteractable = null;
        if (interactionUI != null)
            interactionUI.SetActive(false); // UI'yi gizle
    }

    // Etkileşim tuşu (E) ile tetikleme
    private void HandleInteraction()
    {
        if (currentInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable.Interact();
            
            // Araba ile etkileşim kurulduğunda Player'ı devre dışı bırak
            if (currentInteractable is CarInteractable)
            {
                Pool.Instance.DeactivateObject(transform.parent.gameObject, PoolItemType.Player);
            }
            else if (currentInteractable is CarDealer)
            {
                Actions.MenuState?.Invoke(true);
            }
        }
    }

    private void SetControllerState(bool state)
    {
        controller.enabled = !state;
    }

    private void OnEnable()
    {
        Actions.MenuState += SetControllerState;
    }
    
    private void OnDisable()
    {
        Actions.MenuState -= SetControllerState;
    }
}

public interface IInteractable
{
    void Interact();
}