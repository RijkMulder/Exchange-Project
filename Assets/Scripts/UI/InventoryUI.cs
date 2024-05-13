using Events;
using Player.Inventory;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.DayEnd += Initialize;
    }
    private void OnDisable()
    {
        EventManager.DayEnd -= Initialize;

    }
    private void Initialize(int day)
    {
        //for (int i = 0; i < length; i++)
        //{

        //}
    }
}
