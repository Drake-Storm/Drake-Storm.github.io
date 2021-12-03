using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessPiece
{
    /// <summary>
    /// This checks all possible moves for the Pawn.
    /// Checking if there is an oppenent Diagonal from the selected piece,
    /// if it is the first move (meaning it can go up two spaces instead of just one),
    /// and if there is a piece directly in front of it.
    /// </summary>
    /// <returns>A 2D array of boolean values representing each space on the board and if they are a valid move.</returns>
    public override bool[,] CanMove()
    {
        bool[,] possibleMoves = new bool[8, 8];
        ChessPiece piece1, piece2;

        if (player1)
        {
            //Diagonals
            if(CurrentXPos !=0 && CurrentYPos != 7)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos - 1, CurrentYPos + 1];
                if(piece1 != null && !piece1.player1)
                {
                    possibleMoves[CurrentXPos - 1, CurrentYPos + 1] = true;
                }
                else if(piece1 != null && checkChecking)
                {
                    possibleMoves[CurrentXPos - 1, CurrentYPos + 1] = true;
                }
            }

            if (CurrentXPos != 7 && CurrentYPos != 7)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos + 1, CurrentYPos + 1];
                if (piece1 != null && !piece1.player1)
                {
                    possibleMoves[CurrentXPos + 1, CurrentYPos + 1] = true;
                }
                else if (piece1 != null && checkChecking)
                {
                    possibleMoves[CurrentXPos + 1, CurrentYPos + 1] = true;
                }
            }

            //Front
            if(CurrentYPos != 7 && CurrentYPos != 1)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos, CurrentYPos + 1];

                if(piece1 == null)
                {
                    possibleMoves[CurrentXPos, CurrentYPos + 1] = true;
                }
            }

            if (CurrentYPos == 1)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos, CurrentYPos + 1];
                piece2 = BoardScript.Instance.ChessPieces[CurrentXPos, CurrentYPos + 2];

                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos, CurrentYPos + 1] = true;
                }
                if (piece2 == null)
                {
                    possibleMoves[CurrentXPos, CurrentYPos + 2] = true;
                }
            }

        }
        else
        {
            //Diagonals
            if (CurrentXPos != 0 && CurrentYPos != 0)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos - 1, CurrentYPos - 1];
                if (piece1 != null && piece1.player1)
                {
                    possibleMoves[CurrentXPos - 1, CurrentYPos - 1] = true;
                }
                else if (piece1 != null && checkChecking)
                {
                    possibleMoves[CurrentXPos - 1, CurrentYPos - 1] = true;
                }
            }

            if (CurrentXPos != 7 && CurrentYPos != 0)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos + 1, CurrentYPos - 1];
                if (piece1 != null && piece1.player1)
                {
                    possibleMoves[CurrentXPos + 1, CurrentYPos - 1] = true;
                }
                else if (piece1 != null && checkChecking)
                {
                    possibleMoves[CurrentXPos + 1, CurrentYPos - 1] = true;
                }
            }

            //Front
            if (CurrentYPos != 0 && CurrentYPos != 6)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos, CurrentYPos - 1];

                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos, CurrentYPos - 1] = true;
                }
            }

            if (CurrentYPos == 6)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos, CurrentYPos - 1];
                piece2 = BoardScript.Instance.ChessPieces[CurrentXPos, CurrentYPos - 2];

                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos, CurrentYPos - 1] = true;
                }
                if (piece2 == null)
                {
                    possibleMoves[CurrentXPos, CurrentYPos - 2] = true;
                }
            }
        }

        return possibleMoves;
    }
}
