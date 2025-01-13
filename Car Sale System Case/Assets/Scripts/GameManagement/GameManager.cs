using System;
using System.Collections;
using Data_Management;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private int counter;
    [HideInInspector] public Transform player;

    private void Awake()
    {
        HandleSingletonInstance();
        PlayerDataManager.LoadData();
    }

    private void Start()
    {
        StartCoroutine(AutoSave());
        player = Pool.Instance.SpawnObject(PlayerDataManager.PlayerData.position, PoolItemType.Player, null).transform;
    }

    private void HandleSingletonInstance()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    
    private void OnEnable()
    {
        Actions.GameSaved += ResetCounter;
    }
    
    private void OnDisable()
    {
        Actions.GameSaved -= ResetCounter;
    }

    private void ResetCounter()
    {
        counter = 60;
    }

    private IEnumerator AutoSave()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            counter--;
            if (counter <= 0)
            {
                PlayerDataManager.SaveData();
                counter = 60;
            }
        }
    }
}
