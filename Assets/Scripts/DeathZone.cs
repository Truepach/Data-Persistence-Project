using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public MainManager Manager;

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
        var player = GameManager.Instance.GetCurrentPlayer();
        GameManager.Instance.CompareScore(player);
        GameManager.SetGameOver(true);
    }
}
