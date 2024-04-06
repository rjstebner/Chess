using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    // Reference to the chesspiece prefab in the Unity IDE
    public GameObject chesspiece;

    // 2D array to store the positions of each of the GameObjects on the chess board
    private GameObject[,] positions = new GameObject[8, 8];

    // Arrays to store the chess pieces for each player
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];

    // String to keep track of the current player's turn
    private string currentPlayer = "white";

    // Boolean to track if the game has ended
    private bool gameOver = false;

    // Unity calls this method when the game starts
    public void Start()
    {
        // Initialize the white and black players' chess pieces
        playerWhite = new GameObject[] { Create("white_rook", 0, 0), Create("white_knight", 1, 0),
            Create("white_bishop", 2, 0), Create("white_queen", 3, 0), Create("white_king", 4, 0),
            Create("white_bishop", 5, 0), Create("white_knight", 6, 0), Create("white_rook", 7, 0),
            Create("white_pawn", 0, 1), Create("white_pawn", 1, 1), Create("white_pawn", 2, 1),
            Create("white_pawn", 3, 1), Create("white_pawn", 4, 1), Create("white_pawn", 5, 1),
            Create("white_pawn", 6, 1), Create("white_pawn", 7, 1) };
        playerBlack = new GameObject[] { Create("black_rook", 0, 7), Create("black_knight",1,7),
            Create("black_bishop",2,7), Create("black_queen",3,7), Create("black_king",4,7),
            Create("black_bishop",5,7), Create("black_knight",6,7), Create("black_rook",7,7),
            Create("black_pawn", 0, 6), Create("black_pawn", 1, 6), Create("black_pawn", 2, 6),
            Create("black_pawn", 3, 6), Create("black_pawn", 4, 6), Create("black_pawn", 5, 6),
            Create("black_pawn", 6, 6), Create("black_pawn", 7, 6) };

        // Set all piece positions on the positions board
        for (int i = 0; i < playerBlack.Length; i++)
        {
            SetPosition(playerBlack[i]);
            SetPosition(playerWhite[i]);
        }
    }

    // Method to create a chess piece
    public GameObject Create(string name, int x, int y)
    {
        // Instantiate a new chess piece
        GameObject obj = Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity);

        // Get the Chessman script from the new chess piece
        Chessman cm = obj.GetComponent<Chessman>();

        // Set the name and board position of the new chess piece
        cm.name = name;
        cm.SetXBoard(x);
        cm.SetYBoard(y);

        // Activate the new chess piece
        cm.Activate();

        return obj;
    }

    // Method to set the position of a chess piece on the board
    public void SetPosition(GameObject obj)
    {
        Chessman cm = obj.GetComponent<Chessman>();

        // Overwrite either the empty space or whatever was there
        positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }

    // Method to set a position on the board to be empty
    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }

    // Method to get the chess piece at a position on the board
    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    // Method to check if a position is on the board
    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        return true;
    }

    // Method to get the current player
    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    // Method to check if the game is over
    public bool IsGameOver()
    {
        return gameOver;
    }

    // Method to switch to the next player's turn
    public void NextTurn()
    {
        if (currentPlayer == "white")
        {
            currentPlayer = "black";
        }
        else
        {
            currentPlayer = "white";
        }
    }

    // Unity calls this method once per frame
    public void Update()
    {
        // If the game is over and the mouse button is clicked, restart the game
        if (gameOver == true && Input.GetMouseButtonDown(0))
        {
            gameOver = false;
            SceneManager.LoadScene("Game");
        }
    }

    // Method to declare the winner of the game
    public void Winner(string playerWinner)
    {
        gameOver = true;

        // Display the winner text
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().text = playerWinner + " is the winner";

        // Display the restart text
        GameObject.FindGameObjectWithTag("RestartText").GetComponent<Text>().enabled = true;
    }
}