using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void runAbility(GameObject target)
    {
        foreach (effect m in GetComponent<unit>().myAbility.myEffects)
        {
            if(m.self == true)
            {
                m.OnActivated(this.gameObject);
            }
            else
            {
                m.OnActivated(target);
            }
        }
    }
}
