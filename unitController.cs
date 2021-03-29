using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitController : MonoBehaviour
{
    public GameObject myTarget;
    public GameObject attacker;
    public Vector3 targetPosition;
    private void Update()
    {
        if (targetPosition != new Vector3(0, 0, 0))
        {
            if (Vector3.Distance(gameObject.transform.position, targetPosition) > 0.2)
            {
                gameObject.transform.LookAt(targetPosition);
                GetComponent<Animator>().SetBool("walking", true);
            }
            else
            {
                gameObject.transform.position = targetPosition;
                GetComponent<Animator>().SetBool("walking", false);

            }
        }
     
    }
    //Hit
    public void Hit(GameObject attacker)
    {
        myTarget.GetComponent<Animator>().SetTrigger("Hit");
        myTarget.GetComponent<unitController>().getHit();
    }

    public void getHit()
    {
        GetComponent<unit>().health = GetComponent<unit>().health - attacker.GetComponent<unit>().damage;
        if (GetComponent<unit>().health < 1)
        {
            GetComponent<Animator>().SetTrigger("Dead");

            GetComponent<unit>().myTile.GetComponent<tile>().removeUnit();
            GetComponent<unit>().myPlayer.GetComponent<player>().Units.Remove(this.gameObject);
            Destroy(this.gameObject);
        }

        attacker = null;
    }
    [PunRPC]
    void destroy(string attackerName)
    {
        GameObject attack = GameObject.Find(attackerName);
    }

    //starts the attack, attackRPC synchs it across the network, once this runs with the target set the rest will follow correctly
    public void attack(GameObject target)
    {
        GetComponent<PhotonView>().RPC("attackRPC", RpcTarget.All, target.transform.name);
    }
    [PunRPC]
    public void attackRPC(string targetName)
    {
        myTarget = GameObject.Find(targetName);
        myTarget.GetComponent<unitController>().attacker = this.gameObject;
        GetComponent<Animator>().SetTrigger("Attack");
    }

}
