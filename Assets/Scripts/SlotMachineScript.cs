using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SlotMachineScript : MonoBehaviour
{
    [SerializeField] int columns = 3;
    [SerializeField] int rows = 3;
    [SerializeField] GameObject[] fishPrefabs;
    [SerializeField] TextMeshProUGUI spinCountText;
    [SerializeField] TextMeshProUGUI winCountText;
    [SerializeField] List<GameObject> fishList = new List<GameObject>();
    [SerializeField] List<GameObject> list0 = new List<GameObject>();
    [SerializeField] List<GameObject> list1 = new List<GameObject>();
    [SerializeField] List<GameObject> list2 = new List<GameObject>();
    [SerializeField] List<List<GameObject>> listOfListsOfGameObjects = new List<List<GameObject>>();
    int spinCount = 0;
    int winCount = 0;

    public void Spin()
    {
        spinCount++;
        spinCountText.text = "SPINS: " + spinCount.ToString();
        for (int i = 0; i < fishList.Count; i++)
        {
            Destroy(fishList[i]);
        }
        fishList.Clear();
        float[] spawnWeights = new float[fishPrefabs.Length];
        list0.Clear();
        list1.Clear();
        list2.Clear();
        listOfListsOfGameObjects.Clear();
        for (int i = 0; i < columns; i++)
        {
            float posY = (i * 4 / 2);
            for (int j = 0; j < rows; j++)
            {
                int randomFishIndex = Random.Range(0, fishPrefabs.Length);
                GameObject newFish = Instantiate(fishPrefabs[randomFishIndex], new Vector2(transform.position.x + (j * 4 / 2), posY), Quaternion.identity);
                fishList.Add(newFish);
                if (i == 0) list0.Add(newFish);
                else if (i == 1) list1.Add(newFish);
                else if (i == 2) list2.Add(newFish);
            }
        }
        listOfListsOfGameObjects.Add(list0);
        listOfListsOfGameObjects.Add(list2);
        listOfListsOfGameObjects.Add(list1);

        checkWins();
    }
    void checkWins()
    {
        int worth = 0;
        worth += CheckRight(listOfListsOfGameObjects[0]);
        worth += CheckAcross(listOfListsOfGameObjects[0]);
        print(worth);
    }
    private int CheckRight(List<GameObject> list)
    {
        SlotFishData data = list[0].GetComponent<SlotFishData>();
        List<GameObject> list2 = listOfListsOfGameObjects[1];
        List<GameObject> list3 = listOfListsOfGameObjects[2];
        int worth = data.rarity;
        for (int i = 0; i < list.Count; i++)
        {
            if (list2[i].TryGetComponent(out SlotFishData otherData) == data)
            {
                worth += otherData.rarity;
            }
            if (list3[i].TryGetComponent(out SlotFishData otherData2) == data)
            {
                worth += otherData2.rarity;
            }
        }
        return worth;
    }
    private int CheckAcross(List<GameObject> list)
    {
        int worth = 0;
        for (int i = 0; i < list.Count - 1; i++)
        {
            print(list[i]);
            if (i == 0 || i == 2)
            {
                int multiplier = i == 0 ? -1 : 1;
                SlotFishData thisData = list[i].GetComponent<SlotFishData>();
                SlotFishData secondData = listOfListsOfGameObjects[1][i + multiplier].GetComponent<SlotFishData>();
                SlotFishData thirdData = listOfListsOfGameObjects[2][i + (multiplier + 1)].GetComponent<SlotFishData>();

                if (secondData == thisData) worth += secondData.rarity;
                if (thirdData == thisData) worth += thirdData.rarity;
            }
        }
        return worth;
    }
}