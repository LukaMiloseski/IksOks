using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSwapper : MonoBehaviour
{
    private enum Player { X, O};
    
    private int player = (int)Player.X;

    void ChangePlayer()
    {
        player = (player == (int)Player.X) ? (int)Player.O : (int)Player.X;
    }
}