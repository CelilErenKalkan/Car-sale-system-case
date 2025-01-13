using System;
using Data_Management;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text modelName, price;
    [SerializeField] private Slider topSpeed, condition;
    public Button buyButton;
    private Car car;

    public void SetValues(Car carScript)
    {
        buyButton.onClick.AddListener(BuyCar);

        car = carScript;
        modelName.text = car.modelName;
        price.text = car.price + "$";
        
        topSpeed.value = car.topSpeed / 400;
        Debug.Log(topSpeed.value);
        if (topSpeed.transform.GetChild(1).GetChild(0).TryGetComponent(out Image topSpeedFill))
        {
            if (topSpeed.value <= 0.25f)
                topSpeedFill.color = Color.red;
            else if (topSpeed.value <= 0.60f)
                topSpeedFill.color = Color.yellow;
            else
                topSpeedFill.color = Color.green;
        }

        condition.value = car.condition / 100;
        if (condition.transform.GetChild(1).GetChild(0).TryGetComponent(out Image conditionFill))
        {
            if (condition.value <= 0.25f)
                conditionFill.color = Color.red;
            else if (condition.value <= 0.60f)
                conditionFill.color = Color.yellow;
            else
                conditionFill.color = Color.green;
        }
        
        if (transform.GetChild(1).TryGetComponent(out Button button)) button.onClick.AddListener(BuyCar);
    }

    private void BuyCar()
    {
        Debug.Log($"{car.modelName} satın alındı! Fiyat: {car.price}");
        PlayerDataManager.GetCar(car);
    }
}
