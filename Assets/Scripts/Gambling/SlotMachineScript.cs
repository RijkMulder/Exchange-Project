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

        [Header("Size")]
        [SerializeField][Range(1,3)] int columns = 3;
        [SerializeField][Range(1, 3)] int rows = 3;

        [Header("Setup")]
        [SerializeField] GameObject[] fishPrefabs;
        [SerializeField] TextMeshProUGUI spinCountText;
        [SerializeField] TextMeshProUGUI output;
        [SerializeField] TMP_InputField inputField;
        [SerializeField] GameObject winParticle;
        [SerializeField] GameObject lineCross1;
        [SerializeField] GameObject lineCross2;
        [SerializeField] GameObject lineStraight1;
        [SerializeField] GameObject lineStraight2;
        [SerializeField] GameObject lineStraight3;

        List<GameObject> fishList = new List<GameObject>();
        int inputAmount;
        GameManager gameManager;
        List<GameObject> row0 = new List<GameObject>();
        List<GameObject> row1 = new List<GameObject>();
        List<GameObject> row2 = new List<GameObject>();
        List<List<GameObject>> rowsList = new List<List<GameObject>>();
        int spinCount = 0;
        int outputAmount;

        private void Awake()
        {
            instance = this;
            gameManager = FindFirstObjectByType<GameManager>().GetComponent<GameManager>();
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
                if (gameManager.chips > 0 && gameManager.chips >= inputAmount)
                {
                    spinCount++;
                    spinCountText.text = "SPINS: " + spinCount.ToString();
                    output.text = "";
                    gameManager.addChips(-inputAmount);
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
                            GameObject newFish = Instantiate(fishPrefabs[randomFishIndex], new Vector2(transform.position.x + (j * 4 / 2) - 2, posY - 1), Quaternion.identity);
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
                    output.text = "NOT ENOUGH CHIPS!";
                }
            }
            else
            {
                output.text = "YOU NEED TO INPUT CHIPS FIRST!";
            }
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
                    givePrize(list[0].GetComponent<SlotFishData>().rarity);
                    if (i == 0) showWin(lineStraight3);
                    else if (i == 1) showWin(lineStraight2);
                    else if (i == 2) showWin(lineStraight1);
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
                givePrize(rowsList[0][0].GetComponent<SlotFishData>().rarity);
                showWin(lineCross2);
            }
            if (rowsList[2][0].GetComponent<SlotFishData>().rarity ==
                rowsList[1][1].GetComponent<SlotFishData>().rarity &&
                rowsList[1][1].GetComponent<SlotFishData>().rarity ==
                rowsList[0][2].GetComponent<SlotFishData>().rarity)
            {
                givePrize(rowsList[2][0].GetComponent<SlotFishData>().rarity);
                showWin(lineCross1);
            }
        }

        void givePrize(int multiplier)
        {
            outputAmount = inputAmount * (multiplier * 2);
            gameManager.addCoins(outputAmount);
            output.text = "YOU WON " + outputAmount.ToString() + " COINS";
            winEffect(outputAmount);
        }
        public void GetChips()
        {
            int amnt = 0;
            FishType[] fish = Inventory.instance.inventoryList.ToArray();
            for (int i = 0; i < fish.Length; i++)
            {
                amnt += fish[i].chipCount;
            }
            gameManager.addChips(amnt);
            Inventory.instance.inventoryList.Clear();
        }
        private void winEffect(int amount)
        {
            GameObject particle = Instantiate(winParticle, new Vector3(5.5f, -1f, 0), Quaternion.identity);
            Destroy(particle, 5f);
        }
        private void showWin(GameObject line)
        {
            GameObject newLine = Instantiate(line);
            Destroy(newLine, 2f);
        }
        public void changeBetAmount(int amount)
        {

        }
    }
}
