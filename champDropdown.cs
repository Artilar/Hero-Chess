using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class champDropdown : MonoBehaviour
{
    public gameController myController;
    public List<string> champNames;
    // Start is called before the first frame update
    public abilityDropdown dropdownOne;
    public abilityDropdown dropDownTwo;
    void Start()
    {
        myController = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>();

        GetComponent<Dropdown>().ClearOptions();
        foreach (GameObject champ in myController.champs)
        {
            champNames.Add(champ.name);
        }
        GetComponent<Dropdown>().AddOptions(champNames);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void heroSelected()
    {
        GameObject selected = myController.champs[GetComponent<Dropdown>().value];
        Debug.Log(selected.name);
        if (selected != null)
        {
            //dropdownOne.GetComponent<abilityDropdown>().populate(selected.ability);
           // dropDownTwo.GetComponent<abilityDropdown>().populate(selected.ability);
        }
    }
}
