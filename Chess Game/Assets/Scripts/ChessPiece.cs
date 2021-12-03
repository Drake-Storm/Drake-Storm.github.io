using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChessPiece : MonoBehaviour
{
    public int CurrentXPos { get; set; }
    public int CurrentYPos { get; set; }
    public bool checkChecking;
    public bool player1;
    public string colour;

    /// <summary>
    /// This sets the position for the chess piece
    /// </summary>
    /// <param name="x">The column of the chessboard</param>
    /// <param name="y">The row of the chessboard</param>
    public void SetPos(int x, int y)
    {
        CurrentXPos = x;
        CurrentYPos = y;
    }

    public virtual bool[,] CanMove()
    {
        return new bool[8, 8];
    }
}
