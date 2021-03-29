using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public enum breadcrumbs { mainMenu, MultiplayerMenu, createARoom, inGame }
    public breadcrumbs status;
    public breadcrumbs currentStatus;
    public GameObject Name;
    public GameObject mainMenu;
    public GameObject onlineMenu;
    public GameObject AIMenu;
    public GameObject createARoom;

    public bool FOW;
    public bool attack;
    public bool move;
    void Start()
    {
        currentStatus = breadcrumbs.inGame;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(currentStatus != status)
        {
            if (status == breadcrumbs.mainMenu)
            {
                allOff();
                mainMenu.SetActive(true);

            }
            if (status == breadcrumbs.MultiplayerMenu)
            {
                allOff();
                Name.SetActive(true);

                onlineMenu.SetActive(true);
            }
            if (status == breadcrumbs.createARoom)
            {
                allOff();
                Name.SetActive(true);

                createARoom.SetActive(true);
            }
            if (status == breadcrumbs.inGame)
            {
                allOff();
            }
            currentStatus = status;
        }
      
    }
    public void allOff()
    {
        Name.SetActive(false);
        mainMenu.SetActive(false);
        onlineMenu.SetActive(false);
        //AIMenu.SetActive(false);
        createARoom.SetActive(false);
    }
    public void back()
    {
        if(status == breadcrumbs.MultiplayerMenu)
        {
            status = breadcrumbs.mainMenu;
        }
        if (status == breadcrumbs.createARoom)
        {
            status = breadcrumbs.MultiplayerMenu;
        }
    }

    public void multiplayer()
    {
        status = breadcrumbs.MultiplayerMenu;
        GameObject.Find("networkController").GetComponent<networkController>().Connect();
    }
    public void createRoom()
    {
        status = breadcrumbs.createARoom;
    }
    public void setName()
    {
        GameObject.Find("networkController").GetComponent<networkController>().setName(Name.GetComponent<InputField>().text);

    }
    public void createGame()
    {
        setName();
        GameObject.Find("networkController").GetComponent<networkController>().CreateRoom();
        status = breadcrumbs.inGame;
    }
    public void randomRoom()
    {
        setName();
        GameObject.Find("networkController").GetComponent<networkController>().joinRoom();
        status = breadcrumbs.inGame;
    }
    public void joinTargetRoom(string name)
    {
        setName();
        status = breadcrumbs.inGame;

        GameObject.Find("networkController").GetComponent<networkController>().joinRoom(name);

    }

    public void FOWChange()
    {
        if(FOW == true)
        {
            FOW = false;
        }
        else
        {
            FOW = true;
        }
    }
    public void AttackChange()
    {
        if (attack == true)
        {
            attack = false;
        }
        else
        {
            attack = true;
        }
    }
    public void MoveChange()
    {
        if (move == true)
        {
            move = false;
        }
        else
        {
            move = true;
        }
    }
}
