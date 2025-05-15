using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private enum Symbol { X, O, N };
    
    private PlayerSwapper _playerSwap;
    [SerializeField] private Button[] xOxSpaces;
    [SerializeField] private Sprite[] playerIcon;
    
    private readonly Symbol[,] _board = new Symbol[3, 3];

    private void Awake()
    {
        _playerSwap =GetComponentInParent<PlayerSwapper>();
    }
    
    private void Start()
    {
        GameStart();
    }

    private void GameStart()
    {
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                var index = i * 3 + j;
                _board[i, j] = Symbol.N;

                xOxSpaces[index].interactable = true;
                xOxSpaces[index].image.sprite = null;

                xOxSpaces[index].onClick.RemoveAllListeners();
                xOxSpaces[index].onClick.AddListener(() => XorOx(index));
            }
        }
    }

    private void XorOx(int whichButton)
    {
        var player=_playerSwap.ChangePlayer();
        var row = whichButton / 3;
        var col = whichButton % 3;

        if (_board[row, col] != Symbol.N) return;

        _board[row, col] = (Symbol)player;
        xOxSpaces[whichButton].image.sprite = playerIcon[(int)player];
        xOxSpaces[whichButton].interactable = false;

        var winner = CheckWinner();
        switch (winner)
        {
            case Symbol.X:
                Debug.Log("Player X wins!");
                DisableAllButtons();
                break;
            case Symbol.O:
                Debug.Log("Player O wins!");
                DisableAllButtons();
                break;
            case null:
                Debug.Log("It's a draw!");
                DisableAllButtons();
                break;
        }
    }

    private Symbol? CheckWinner()
    {
        for (var i = 0; i < 3; i++)
        {
            if (_board[i, 0] != Symbol.N && _board[i, 0] == _board[i, 1] && _board[i, 1] == _board[i, 2])
                return _board[i, 0];

            if (_board[0, i] != Symbol.N && _board[0, i] == _board[1, i] && _board[1, i] == _board[2, i])
                return _board[0, i];
        }

        if (_board[0, 0] != Symbol.N && _board[0, 0] == _board[1, 1] && _board[1, 1] == _board[2, 2])
            return _board[0, 0];

        if (_board[0, 2] != Symbol.N && _board[0, 2] == _board[1, 1] && _board[1, 1] == _board[2, 0])
            return _board[0, 2];

        foreach (var cell in _board)
        {
            if (cell == Symbol.N) 
                return Symbol.N;
        }

        return null;
    }

    private void DisableAllButtons()
    {
        foreach (var button in xOxSpaces)
        {
            button.interactable = false;
        }
    }
}