using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data_Management
{
    [Serializable] // Required for JSON serialization
    public struct PlayerData
    {
        // Public fields or properties to be serialized;
        public List<Car> carList;
        public Vector3 position;
        public int moneyAmount;

        // Constructor with default values
        public PlayerData(List<Car> carList, int moneyAmount, Vector3 position)
        {
            this.carList = carList ?? new List<Car>();
            this.moneyAmount = moneyAmount;
            this.position = position;
        }
    }

    public static class PlayerDataManager
    {
        public static PlayerData PlayerData;
        private static Vector3 defaultPos;

        #region Data Management

        /// <summary>
        /// Loads all the data from the files with error handling.
        /// </summary>
        public static void LoadData()
        {
            try
            {
                PlayerData = FileHandler.ReadFromJson<PlayerData>("PlayerData.json");
                if (PlayerData.moneyAmount <= 0) PlayerData.moneyAmount = 2000;
                if (PlayerData.position == Vector3.zero) PlayerData.position = new Vector3(-451, 25, 58.48f);
                RefactorCarList();
                SaveData();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load data: {ex.Message}");
                PlayerData = new PlayerData(new List<Car>(), 2000, defaultPos);
                SaveData();
            }
        }

        /// <summary>
        /// Saves all the data to the files with error handling.
        /// </summary>
        public static void SaveData()
        {
            try
            {
                PlayerData.position = GameManager.Instance.player.position;
                FileHandler.SaveToJson(PlayerData, "PlayerData.json");
                Actions.GameSaved?.Invoke();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to save data: {ex.Message}");
            }
        }

        #endregion

        private static void RefactorCarList()
        {
            PlayerData.carList ??= new List<Car>();
            var carList = PlayerData.carList;
        }

        public static void GetCar(Car car)
        {
            if (PlayerData.moneyAmount < car.price) return;
            
            PlayerData.carList ??= new List<Car>();

            PlayerData.moneyAmount -= car.price;
            PlayerData.carList.Add(car);
            SaveData();
        }
    }
}
