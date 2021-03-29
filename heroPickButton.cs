using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class heroPickButton : MonoBehaviour
{
    // Start is called before the first frame update
    public player myPlayer;

    public gameController controller;
    public champDropdown optionOne;
    public champDropdown optionTwo;
    public champDropdown optionThree;
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void champLockIn()
    {
        GameObject heroOne = controller.champs[optionOne.gameObject.GetComponent<Dropdown>().value];

        GameObject heroTwo = controller.champs[optionTwo.gameObject.GetComponent<Dropdown>().value];

        GameObject heroThree = controller.champs[optionThree.gameObject.GetComponent<Dropdown>().value];


        myPlayer.setHeroes(heroOne, heroTwo, heroThree);
    }

}
