using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Ability", order = 1)]
public class ability : ScriptableObject
{
    public string name;
    public List<effect> myEffects;

    public enum type {movement, damage, CC, buff, special}

    public bool targeted;
}
