using Events;
using Fishing.Stats;
using Player.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LogBook : MonoBehaviour
{
    public Dictionary<FishType, (FishStats, int)> fishDictionary = new Dictionary<FishType, (FishStats, int)>();

    private void OnEnable()
    {
        EventManager.DayEnd += CheckInventory;
    }
    private void Start()
    {
        FishType[] fish = Resources.LoadAll<FishType>("Data/Fish");

        for (int i = 0; i < fish.Length; i++)
        {
            NewItem(fish[i]);
        }
    }
    private void CheckInventory(int d)
    {
        foreach (var item in Inventory.instance.inventoryDictionary)
        {
            FishType type = item.Key;
            if (fishDictionary.ContainsKey(type)) UpdateItem(type);
        }
    }
    private void NewItem(FishType type)
    {
        FishStats stats = new FishStats();


    }
    private void UpdateItem(FishType key)
    {
        (FishStats, int) currentItem = fishDictionary[key];
        (FishStats, int) newItem = (currentItem.Item1, currentItem.Item2 + 1);
        fishDictionary[key] = newItem;
    }
}
