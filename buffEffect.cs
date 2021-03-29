using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "effect/buff", order = 1)]
public class buffEffect : effect
{
    public enum statEffected {health, movement, range, damage}
    public statEffected myStatEffected;
    public override void OnActivated(GameObject player)
    {
        switch (myStatEffected)
        {
            case statEffected.health:
                player.GetComponent<unit>().health += amount;
                break;
            case statEffected.movement:
                player.GetComponent<unit>().moveScore += amount;
                break;
            case statEffected.range:
                player.GetComponent<unit>().range += amount;
                break;
            case statEffected.damage:
                player.GetComponent<unit>().damage += amount;
                break;
        }
    }

    public override void OnActivated()
    {
       
    }
}
