using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class abilityDropdown : MonoBehaviour
{
    // Start is called before the first frame update
    public champDropdown myDropdown;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void populate(List<string> abilities)
    {
        GetComponent<Dropdown>().ClearOptions();
        GetComponent<Dropdown>().AddOptions(abilities);
    }
}
