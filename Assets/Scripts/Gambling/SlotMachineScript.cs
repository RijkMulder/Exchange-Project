using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Player.Inventory;
using Events;
using System.Collections;

namespace Gambling
{
    public class SlotMachineScript : MonoBehaviour
    {

        public static SlotMachineScript instance;

        [Header("Size")]
        [SerializeField][Range(1,3)] int columns = 3;
        [SerializeField][Range(1, 3)] int rows = 3;

        [Header("Bets")]
        [SerializeField]
        List<int> bets = new List<int>();

        [Header("Setup")]
        [SerializeField] GameObject[] fishPrefabs;
        [SerializeField] TextMeshProUGUI spinCountText;
        [SerializeField] TextMeshProUGUI output;
        [SerializeField] TextMeshProUGUI betText;
        [SerializeField] GameObject winParticle;
        [SerializeField] GameObject lineCross1;
        [SerializeField] GameObject lineCross2;
        [SerializeField] GameObject lineStraight1;
        [SerializeField] GameObject lineStraight2;
        [SerializeField] GameObject lineStraight3;
        [SerializeField] Animator anim;

        List<GameObject> fishList = new List<GameObject>();
        int inputAmount;
        GameManager gameManager;
        List<GameObject> row0 = new List<GameObject>();
        List<GameObject> row1 = new List<GameObject>();
        List<GameObject> row2 = new List<GameObject>();
        List<List<GameObject>> rowsList = new List<List<GameObject>>();
        int spinCount = 0;
        int outputAmount;
        int currentBet = 0;
        bool canSpin;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            gameManager = GameManager.Instance;
            betText.text = inputAmount.ToString();
            canSpin = true;
        }

        // Spin function
        public void Spin()
        {
            if (canSpin) StartCoroutine(startSpin());
            else return;
        }

        // Chech straight for wins
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

        // Check across for wins
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

        // Calculate the win amount
        void givePrize(int multiplier)
        {
            outputAmount = inputAmount * (multiplier * 2);
            gameManager.addCoins(outputAmount);
            output.text = "YOU WON " + outputAmount.ToString() + " COINS";
            winEffect(outputAmount);
        }

        // Calculate the amount of chips you get
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

        // Instantiate a particle effect on win
        private void winEffect(int amount)
        {
            GameObject particle = Instantiate(winParticle, new Vector3(5.5f, -1f, 0), Quaternion.identity);
            particle.GetComponent<ParticleSystem>().maxParticles = amount / 100 + 5;
            Destroy(particle, 5f);
        }

        // Instantiate a line on top of the winning line
        private void showWin(GameObject line)
        {
            GameObject newLine = Instantiate(line);
            Destroy(newLine, 2f);
        }

        // Change the bet amount fractually
        public void changeBetAmount(int amount)
        {
            currentBet += amount;
            if (currentBet < 0) currentBet = 0;
            if (currentBet > bets.Count) currentBet = bets.Count;
            inputAmount = bets[currentBet];
            betText.text = inputAmount.ToString();
        }

        private IEnumerator startSpin()
        {
            canSpin = false;
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
                    anim.gameObject.SetActive(true);
                    toggleAnimation();
                    yield return new WaitForSeconds(2);
                    toggleAnimation();
                    anim.gameObject.SetActive(false);
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
            canSpin = true;
        }

        private void toggleAnimation()
        {
            if (!anim.GetBool("spin")) anim.SetBool("spin", true);
            else anim.SetBool("spin", false);
        }
    }
}
