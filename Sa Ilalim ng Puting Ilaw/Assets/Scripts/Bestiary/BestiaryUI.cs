using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BestiaryUI : MonoBehaviour
{
    public Image creatureImage;
    public TMP_Text creatureName;
    public TMP_Text creatureDescription;

    public Creature[] creatures;

    private int currentIndex;

    void Start()
    {
        ShowCreature(0);
    }

    void ShowCreature(int index)
    {
        creatureImage.sprite = creatures[index].image;

        creatureName.text =
            creatures[index].creatureName;

        creatureDescription.text =
            creatures[index].description;
    }

    public void NextCreature()
    {
        currentIndex++;

        if (currentIndex >= creatures.Length)
        {
            currentIndex = 0;
        }

        ShowCreature(currentIndex);
    }

    public void PreviousCreature()
    {
        currentIndex--;

        if (currentIndex < 0)
        {
            currentIndex = creatures.Length - 1;
        }

        ShowCreature(currentIndex);
    }
}