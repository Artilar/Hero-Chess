using JetBrains.Annotations;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class unit : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject myPlayer;
    public GameObject myTile;
    public PhotonView pView;
    public ability myAbility;
    //variables
    public bool stunned;
    public bool silenced;
    public bool moved;
    public bool attacked;
    public bool seen;
    //stats
    public int health;
    public int damage;
    public int moveScore;
    public int range;

    public int baseHealth;
    public int baseMoveScore;
    public int baseDamage;
    public int baseRange;

    //UI Information
    public Animator myAnimator;
    public TextMeshPro healthText;
    public TextMeshPro attackText;
    public TextMeshPro moveText;
    public Material mine;
    public Material enemy;
    private Material working;
    public Material selected;
    public Material movedMat;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        health = baseHealth;
        damage = baseDamage;
        moveScore = baseMoveScore;
        range = baseRange;
    }
    public void setName(string name, int team)
    { 
        pView.RPC("setNamePun", RpcTarget.All, name, team);
    }
    [PunRPC]
    void setNamePun(string name, int team)
    {
        selectedBool = false;
        if (pView.IsMine)
        {
            //gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = mine;
            working = mine;
        }
        else
        {
            //gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = enemy;
            working = enemy;
        }
        if (team == 1)
        {
            myPlayer = GameObject.Find("Player One");
        }
        else
        {
            myPlayer = GameObject.Find("Player Two");
        }

        gameObject.name = name;
    }

  

    private void Update()
    {
        //myHealth.text = health.ToString();

        healthText.text = health.ToString();
        attackText.text = damage.ToString();
        moveText.text = moveScore.ToString();
        if(moved == true)
        {
           //gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = movedMat;
        }
        else
        {
            selectedMethod();
        }

    }
    public bool selectedBool;
    public void selectedMethod()
    {
        if (selectedBool == true)
        {
            //gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = selected;
        }
        else
        {
            //gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = working;
        }
    }

    public void setTarget()
    {

    }
}
