using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ruleSet : MonoBehaviour
{
    // Move Once no Attack
    // Move multiple times no attack
    // move once and attack
    // move multiple times and attack

    public PhotonView pview;
    public bool FOWActive;
    public bool multiMoves;
    public bool attackAswell;

    void Start()
    {
        pview = GetComponent<PhotonView>();

        FOWActive = GameObject.Find("MainMenu").GetComponent<mainMenuController>().FOW;
        
        multiMoves = GameObject.Find("MainMenu").GetComponent<mainMenuController>().move;
        attackAswell = GameObject.Find("MainMenu").GetComponent<mainMenuController>().attack;

        if (pview.Controller.IsMasterClient)
        {
            if(multiMoves == true)
            {
                GetComponent<PhotonView>().RPC("multiRPC", RpcTarget.AllBuffered);

            }
            if (attackAswell == true)
            {
                GetComponent<PhotonView>().RPC("attackRPC", RpcTarget.AllBuffered);

            }
            if (FOWActive == true)
            {
                GetComponent<PhotonView>().RPC("FOWRPC", RpcTarget.AllBuffered);
            }
        }
    }
           
    [PunRPC]
    void multiRPC()
    {
            multiMoves = true;
    }

    [PunRPC]
    void attackRPC()
    {
            attackAswell = true;

    }

    [PunRPC]
    void FOWRPC()
    {
            FOWActive = true;
    }
}
