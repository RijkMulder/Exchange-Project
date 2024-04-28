using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int tokens;
    [SerializeField] TextMeshProUGUI tokensTxt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addTokens(int amount)
    {
        tokens += amount;
        tokensTxt.text = tokens.ToString();
    }
}
