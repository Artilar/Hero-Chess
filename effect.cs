using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class effect : ScriptableObject
{
    public ability.type myType;
    public bool self;
    public int amount;

    public abstract void OnActivated(); // all abilities must implement this method
    public abstract void OnActivated(GameObject player); // all abilities must implement this method

}
