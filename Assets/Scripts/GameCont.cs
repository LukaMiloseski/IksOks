using UnityEngine;
using UnityEngine.UI;

public class GameCont : MonoBehaviour
{
    private enum Symbol { X, O, N };
    private Symbol[,] board = new Symbol[3, 3];
    public Button[] xOxSpaces;
    public Sprite[] playerIcon;
    private int player = (int)Symbol.X;
    [SerializeField] private PlayerSwapper playerSwap;

    void Start()
    {
        GameStart();
    }

    void GameStart()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int index = i * 3 + j;
                board[i, j] = Symbol.N;

                xOxSpaces[index].interactable = true;
                xOxSpaces[index].image.sprite = null;

                int btnIndex = index;  
                xOxSpaces[index].onClick.RemoveAllListeners();
                xOxSpaces[index].onClick.AddListener(() => XorOx(btnIndex));
            }
        }
    }

    public void XorOx(int whichButton)
    {
        int row = whichButton / 3;
        int col = whichButton % 3;

        if (board[row, col] != Symbol.N) return;

        board[row, col] = (Symbol)player;
        xOxSpaces[whichButton].image.sprite = playerIcon[player];
        xOxSpaces[whichButton].interactable = false;

        Symbol? winner = CheckWinner();
        if (winner == Symbol.X)
        {
            Debug.Log("Player X wins!");
            DisableAllButtons();
        }
        else if (winner == Symbol.O)
        {
            Debug.Log("Player O wins!");
            DisableAllButtons();
        }
        else if (winner == null)
        {
            Debug.Log("It's a draw!");
            DisableAllButtons();
        }

        player = (player == (int)Symbol.X) ? (int)Symbol.O : (int)Symbol.X;
    }

    private Symbol? CheckWinner()
    {
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] != Symbol.N && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                return board[i, 0];

            if (board[0, i] != Symbol.N && board[0, i] == board[1, i] && board[1, i] == board[2, i])
                return board[0, i];
        }

        if (board[0, 0] != Symbol.N && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            return board[0, 0];

        if (board[0, 2] != Symbol.N && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            return board[0, 2];

        foreach (Symbol cell in board)
        {
        if (cell == Symbol.N) 
            return Symbol.N;
        }

        return null;
        }

    void DisableAllButtons()
    {
        foreach (var button in xOxSpaces)
        {
            button.interactable = false;
        }
    }
}