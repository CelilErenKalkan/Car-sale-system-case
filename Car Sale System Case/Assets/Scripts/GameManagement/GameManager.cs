using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Transform defaultTransform;

    private void Awake()
    {
        HandleSingletonInstance();
    }

    private void Start()
    {
        Pool.Instance.SpawnObject(defaultTransform.position, PoolItemType.Player, null);
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

    private void LoadGameData()
    {
        // Load player data and map order
    }
}
