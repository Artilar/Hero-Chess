using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class roomButton : MonoBehaviour
{
    // Start is called before the first frame update
    public string roomName;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void joinRoom()
    {
        GameObject.Find("MainMenu").GetComponent<mainMenuController>().joinTargetRoom(name);
    }
}
