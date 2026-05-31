using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectionManager4 : MonoBehaviour
{
    public static CollectionManager4 Instance;

    public TextMeshProUGUI collectText;

    public int collected = 0;
    public int total = 3;

    public GameObject trap;

    [TextArea]
    public string nextInstruction = "Get Ready and wait for Nightfall";

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateUI();
        trap.SetActive(false);
    }

    public void AddCollectible()
    {
        collected++;

        if (collected >= total)
        {
            collectText.text = nextInstruction;
            trap.SetActive(true);
        }
        else
        {
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        collectText.text = "Prepare the pots around the Lake  " + collected + "/" + total;
    }
}

