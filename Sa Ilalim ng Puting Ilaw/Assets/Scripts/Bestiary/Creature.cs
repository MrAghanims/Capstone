using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Creature
{
    public string creatureName;

    [TextArea]
    public string description;

    public Sprite image;
}