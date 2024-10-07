using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int targetsInScene;
    public int targetHit = 0;

    private void Start()
    {
        targetsInScene = GameObject.FindGameObjectsWithTag("Target").Length;
    }

    public void TargetHit() 
    {
        targetHit++;
        if(targetHit >= targetsInScene) 
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        Debug.Log("you win!!!");
    }
}
