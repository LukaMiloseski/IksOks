using UnityEngine;

public class PlayerSwapper : MonoBehaviour
{
    public enum Player { X, O};
    
    private Player _player = Player.X;

    public Player ChangePlayer()
    {
        var playerTmp = _player;
        _player= _player == Player.X ? Player.O : Player.X;
        return playerTmp;
    }
}