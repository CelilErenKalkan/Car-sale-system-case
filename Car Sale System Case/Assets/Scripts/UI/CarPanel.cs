using System;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.UI.Slider;

public class CarPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text modelName, price;
    [SerializeField] private Slider topSpeed, condition;

    public void SetValues(string newModelName, int newPrice, float newTopSpeed, int newCondition)
    {
        modelName.text = newModelName;
        price.text = newPrice + "$";
        
        topSpeed.value = newTopSpeed / 400;
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

        condition.value = newCondition / 100;
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
        
    }
}
