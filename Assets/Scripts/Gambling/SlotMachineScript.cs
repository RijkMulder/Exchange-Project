using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Player.Inventory;
using Events;

namespace Gambling
{
    public class SlotMachineScript : MonoBehaviour
    {
        public static SlotMachineScript instance;

        [SerializeField] int columns = 3;
        [SerializeField] int rows = 3;
        [SerializeField] GameObject[] fishPrefabs;
        [SerializeField] TextMeshProUGUI spinCountText;
        [SerializeField] TextMeshProUGUI winCountText;
        [SerializeField] TextMeshProUGUI chipCountText;
        [SerializeField] TMP_InputField inputField;
        [SerializeField] List<GameObject> fishList = new List<GameObject>();
        [SerializeField] int inputAmount;
        [SerializeField] int chips;
        List<GameObject> row0 = new List<GameObject>();
        List<GameObject> row1 = new List<GameObject>();
        List<GameObject> row2 = new List<GameObject>();
        List<List<GameObject>> rowsList = new List<List<GameObject>>();
        int spinCount = 0;
        int winCount = 0;
        int outputAmount;
        GameManager gameManager;

        private void Awake()
        {
            instance = this;
            gameManager = FindFirstObjectByType<GameManager>();
        }
        private void Start()
        {
            UpdateText();
        }

        public void Spin()
        {
            if (inputField.text == "")
            {
                inputAmount = 0;
            }
            else inputAmount = int.Parse(inputField.text);
            if (inputAmount > 0)
            {
                if (chips > 0 && chips >= inputAmount)
                {
                    spinCount++;
                    spinCountText.text = "SPINS: " + spinCount.ToString();
                    winCountText.text = "";
                    chips -= inputAmount;
                    chipCountText.text = "CHIPS: " + chips.ToString();
                    for (int i = 0; i < fishList.Count; i++)
                    {
                        Destroy(fishList[i]);
                    }
                    fishList.Clear();
                    float[] spawnWeights = new float[fishPrefabs.Length];
                    row0.Clear();
                    row1.Clear();
                    row2.Clear();
                    rowsList.Clear();
                    for (int i = 0; i < columns; i++)
                    {
                        float posY = (i * 4 / 2);
                        for (int j = 0; j < rows; j++)
                        {
                            int randomFishIndex = Random.Range(0, fishPrefabs.Length);
                            GameObject newFish = Instantiate(fishPrefabs[randomFishIndex], new Vector2(transform.position.x + (j * 4 / 2), posY), Quaternion.identity);
                            newFish.transform.parent = this.gameObject.transform;
                            fishList.Add(newFish);
                            if (i == 0) row0.Add(newFish);
                            else if (i == 1) row1.Add(newFish);
                            else if (i == 2) row2.Add(newFish);
                        }
                    }
                    rowsList.Add(row0);
                    rowsList.Add(row1);
                    rowsList.Add(row2);

                    CheckRight();
                    CheckAcross();
                }
                else
                {
                    winCountText.text = "NOT ENOUGH CHIPS!";
                }
            }
            else
            {
                winCountText.text = "YOU NEED TO INPUT CHIPS FIRST!";
            }
        }
        private void UpdateText()
        {
            chipCountText.text = "CHIPS: " + chips.ToString();
        }
        private void CheckRight()
        {
            for (int i = 0; i < 3; i++)
            {
                List<GameObject> list = rowsList[i];
                if (list[0].GetComponent<SlotFishData>().rarity ==
                    list[1].GetComponent<SlotFishData>().rarity &&
                    list[1].GetComponent<SlotFishData>().rarity ==
                    list[2].GetComponent<SlotFishData>().rarity)
                {
                    winCount++;
                    winCountText.text = "WINS: " + winCount.ToString();
                    givePrize(list[0].GetComponent<SlotFishData>().rarity);
                }

            }
        }

        private void CheckAcross()
        {
            if (rowsList[0][0].GetComponent<SlotFishData>().rarity ==
                rowsList[1][1].GetComponent<SlotFishData>().rarity &&
                rowsList[1][1].GetComponent<SlotFishData>().rarity ==
                rowsList[2][2].GetComponent<SlotFishData>().rarity)
            {
                winCount++;
                winCountText.text = "WINS: " + winCount.ToString();
                givePrize(rowsList[0][0].GetComponent<SlotFishData>().rarity);
            }
            if (rowsList[2][0].GetComponent<SlotFishData>().rarity ==
                rowsList[1][1].GetComponent<SlotFishData>().rarity &&
                rowsList[1][1].GetComponent<SlotFishData>().rarity ==
                rowsList[0][2].GetComponent<SlotFishData>().rarity)
            {
                winCount++;
                winCountText.text = "WINS: " + winCount.ToString();
                givePrize(rowsList[2][0].GetComponent<SlotFishData>().rarity);
            }
        }

        void givePrize(int multiplier)
        {
            outputAmount = inputAmount * (multiplier * 2);
            gameManager.addCoins(outputAmount);
            winCountText.text = "YOU WON " + outputAmount.ToString() + " COINS";
        }
        public void GetChips()
        {
            int amnt = 0;
            FishType[] fish = Inventory.instance.inventoryList.ToArray();
            for (int i = 0; i < fish.Length; i++)
            {
                amnt += fish[i].chipCount;
            }
            chips = amnt;
            UpdateText();
            Inventory.instance.inventoryList.Clear();
        }
    }
}
