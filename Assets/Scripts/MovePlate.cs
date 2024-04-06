using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    // The GameController object that controls the game state
    public GameObject controller;

    // The Chesspiece that was selected to create this MovePlate
    // This is the piece that will be moved or will perform an attack
    GameObject reference = null;

    // The x and y coordinates of this MovePlate on the game board
    int matrixX;
    int matrixY;

    // A flag indicating whether this MovePlate is for an attack (true) or a movement (false)
    public bool attack = false;

    // This method is called at the start of the game
    public void Start()
    {
        // If this MovePlate is for an attack, change its color to red
        if (attack)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }

    // This method is called when the mouse button is released over this MovePlate
    public void OnMouseUp()
    {
        // Find the GameController object
        controller = GameObject.FindGameObjectWithTag("GameController");

        // If this MovePlate is for an attack, destroy the victim Chesspiece
        if (attack)
        {
            // Get the Chesspiece at the MovePlate's position
            GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);

            // If the victim is a king, declare the other player as the winner
            if (cp.name == "white_king") controller.GetComponent<Game>().Winner("black");
            if (cp.name == "black_king") controller.GetComponent<Game>().Winner("white");

            // Destroy the victim Chesspiece
            Destroy(cp);
        }

        // Set the Chesspiece's original location to be empty
        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Chessman>().GetXBoard(),
            reference.GetComponent<Chessman>().GetYBoard());

        // Move the reference Chesspiece to this MovePlate's position
        reference.GetComponent<Chessman>().SetXBoard(matrixX);
        reference.GetComponent<Chessman>().SetYBoard(matrixY);
        reference.GetComponent<Chessman>().SetCoords();

        // Update the game board to reflect the new position of the reference Chesspiece
        controller.GetComponent<Game>().SetPosition(reference);

        // Switch to the other player's turn
        controller.GetComponent<Game>().NextTurn();

        // Destroy all MovePlates, including this one
        reference.GetComponent<Chessman>().DestroyMovePlates();
    }

    // Set the coordinates of this MovePlate on the game board
    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    // Set the reference Chesspiece that will be moved or will perform an attack
    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    // Get the reference Chesspiece
    public GameObject GetReference()
    {
        return reference;
    }
}