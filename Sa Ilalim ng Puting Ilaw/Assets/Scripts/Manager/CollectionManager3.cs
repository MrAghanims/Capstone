using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectionManager3 : MonoBehaviour
{
    public static CollectionManager3 Instance;

    public TextMeshProUGUI collectText;

    public int collected = 0;
    public int total = 3;

    public GameObject trap;

    [TextArea]
    public string nextInstruction = "You notice something there...";

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
        collectText.text = "Collect Flowers for Lola  " + collected + "/" + total;
    }
}

