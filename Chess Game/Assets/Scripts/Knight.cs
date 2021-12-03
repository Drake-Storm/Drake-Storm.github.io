using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : ChessPiece
{
    /// <summary>
    /// Checks all valid spaces for the Knight,
    /// this means 2 spaces horizontally/vertically
    /// and 1 space vertically/horizontally from the selected piece;
    /// </summary>
    /// <returns>A 2D array of boolean values representing each space on the board and if they are a valid move.</returns>
    public override bool[,] CanMove()
    {
        bool[,] possibleMoves = new bool[8, 8];
        ChessPiece piece1;

        if (player1)
        {

            //2 in front, 1 right
            if(CurrentXPos <= 6 && CurrentYPos <= 5)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos + 1, CurrentYPos + 2];
                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos + 1, CurrentYPos + 2] = true;
                }

                if (piece1 != null && !piece1.player1)
                {
                    possibleMoves[CurrentXPos + 1, CurrentYPos + 2] = true;
                }
                else if (piece1 != null && checkChecking)
                {
                    possibleMoves[CurrentXPos + 1, CurrentYPos + 2] = true;
                }

            }

            //1 in front, 2 right
            if (CurrentXPos <= 5 && CurrentYPos <= 6)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos + 2, CurrentYPos + 1];
                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos + 2, CurrentYPos + 1] = true;
                }

                if (piece1 != null && !piece1.player1)
                {
                    possibleMoves[CurrentXPos + 2, CurrentYPos + 1] = true;
                }
                else if (piece1 != null && checkChecking)
                {
                    possibleMoves[CurrentXPos + 2, CurrentYPos + 1] = true;
                }

            }

            //1 behind, 2 right
            if (CurrentXPos <= 5 && CurrentYPos >= 1)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos + 2, CurrentYPos - 1];
                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos + 2, CurrentYPos - 1] = true;
                }

                if (piece1 != null && !piece1.player1)
                {
                    possibleMoves[CurrentXPos + 2, CurrentYPos - 1] = true;
                }
                else if (piece1 != null && checkChecking)
                {
                    possibleMoves[CurrentXPos + 2, CurrentYPos - 1] = true;
                }

            }

            //2 behind, 1 right
            if (CurrentXPos <= 6 && CurrentYPos >= 2)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos + 1, CurrentYPos -2];
                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos + 1, CurrentYPos - 2] = true;
                }

                if (piece1 != null && !piece1.player1)
                {
                    possibleMoves[CurrentXPos + 1, CurrentYPos - 2] = true;
                }
                else if (piece1 != null && checkChecking)
                {
                    possibleMoves[CurrentXPos + 1, CurrentYPos - 2] = true;
                }

            }

            //2 behind, 1 left
            if (CurrentXPos >= 1 && CurrentYPos >= 2)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos - 1, CurrentYPos - 2];
                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos - 1, CurrentYPos - 2] = true;
                }

                if (piece1 != null && !piece1.player1)
                {
                    possibleMoves[CurrentXPos - 1, CurrentYPos - 2] = true;
                }
                else if (piece1 != null && checkChecking)
                {
                    possibleMoves[CurrentXPos - 1, CurrentYPos - 2] = true;
                }

            }

            //1 behind, 2 left
            if (CurrentXPos >= 2 && CurrentYPos >= 1)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos - 2, CurrentYPos - 1];
                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos - 2, CurrentYPos - 1] = true;
                }

                if (piece1 != null && !piece1.player1)
                {
                    possibleMoves[CurrentXPos - 2, CurrentYPos - 1] = true;
                }
                else if (piece1 != null && checkChecking)
                {
                    possibleMoves[CurrentXPos - 2, CurrentYPos - 1] = true;
                }

            }

            //1 in front, 2 left
            if (CurrentXPos >= 2 && CurrentYPos <= 6)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos - 2, CurrentYPos + 1];
                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos - 2, CurrentYPos + 1] = true;
                }

                if (piece1 != null && !piece1.player1)
                {
                    possibleMoves[CurrentXPos - 2, CurrentYPos + 1] = true;
                }
                else if (piece1 != null && checkChecking)
                {
                    possibleMoves[CurrentXPos - 2, CurrentYPos + 1] = true;
                }

            }

            //2 in front, 1 left
            if (CurrentXPos >= 1 && CurrentYPos <= 5)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos - 1, CurrentYPos + 2];
                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos - 1, CurrentYPos + 2] = true;
                }

                if (piece1 != null && !piece1.player1)
                {
                    possibleMoves[CurrentXPos - 1, CurrentYPos + 2] = true;
                }
                else if (piece1 != null && checkChecking)
                {
                    possibleMoves[CurrentXPos - 1, CurrentYPos + 2] = true;
                }

            }
        }
        else
        {

            //2 in behind, 1 left
            if (CurrentXPos <= 6 && CurrentYPos <= 5)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos + 1, CurrentYPos + 2];
                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos + 1, CurrentYPos + 2] = true;
                }

                if (piece1 != null && piece1.player1)
                {
                    possibleMoves[CurrentXPos + 1, CurrentYPos + 2] = true;
                }
                else if (piece1 != null && checkChecking)
                {
                    possibleMoves[CurrentXPos + 1, CurrentYPos + 2] = true;
                }

            }

            //1 in behind, 2 left
            if (CurrentXPos <= 5 && CurrentYPos <= 6)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos + 2, CurrentYPos + 1];
                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos + 2, CurrentYPos + 1] = true;
                }

                if (piece1 != null && piece1.player1)
                {
                    possibleMoves[CurrentXPos + 2, CurrentYPos + 1] = true;
                }
                else if (piece1 != null && checkChecking)
                {
                    possibleMoves[CurrentXPos + 2, CurrentYPos + 1] = true;
                }

            }

            //1 in front, 2 left
            if (CurrentXPos <= 5 && CurrentYPos >= 1)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos + 2, CurrentYPos - 1];
                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos + 2, CurrentYPos - 1] = true;
                }

                if (piece1 != null && piece1.player1)
                {
                    possibleMoves[CurrentXPos + 2, CurrentYPos - 1] = true;
                }
                else if (piece1 != null && checkChecking)
                {
                    possibleMoves[CurrentXPos + 2, CurrentYPos - 1] = true;
                }

            }

            //2 in front, 1 left
            if (CurrentXPos <= 6 && CurrentYPos >= 2)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos + 1, CurrentYPos - 2];
                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos + 1, CurrentYPos - 2] = true;
                }

                if (piece1 != null && piece1.player1)
                {
                    possibleMoves[CurrentXPos + 1, CurrentYPos - 2] = true;
                }
                else if (piece1 != null && checkChecking)
                {
                    possibleMoves[CurrentXPos + 1, CurrentYPos - 2] = true;
                }

            }

            //2 in front, 1 right
            if (CurrentXPos >= 1 && CurrentYPos >= 2)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos - 1, CurrentYPos - 2];
                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos - 1, CurrentYPos - 2] = true;
                }

                if (piece1 != null && piece1.player1)
                {
                    possibleMoves[CurrentXPos - 1, CurrentYPos - 2] = true;
                }
                else if (piece1 != null && checkChecking)
                {
                    possibleMoves[CurrentXPos - 1, CurrentYPos - 2] = true;
                }

            }

            //1 in front, 2 right
            if (CurrentXPos >= 2 && CurrentYPos >= 1)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos - 2, CurrentYPos - 1];
                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos - 2, CurrentYPos - 1] = true;
                }

                if (piece1 != null && piece1.player1)
                {
                    possibleMoves[CurrentXPos - 2, CurrentYPos - 1] = true;
                }
                else if (piece1 != null && checkChecking)
                {
                    possibleMoves[CurrentXPos - 2, CurrentYPos - 1] = true;
                }

            }

            //1 in behind, 2 right
            if (CurrentXPos >= 2 && CurrentYPos <= 6)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos - 2, CurrentYPos + 1];
                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos - 2, CurrentYPos + 1] = true;
                }

                if (piece1 != null && piece1.player1)
                {
                    possibleMoves[CurrentXPos - 2, CurrentYPos + 1] = true;
                }
                else if (piece1 != null && checkChecking)
                {
                    possibleMoves[CurrentXPos - 2, CurrentYPos + 1] = true;
                }

            }

            //2 in behind, 1 right
            if (CurrentXPos >= 1 && CurrentYPos <= 5)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos - 1, CurrentYPos + 2];
                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos - 1, CurrentYPos + 2] = true;
                }

                if (piece1 != null && piece1.player1)
                {
                    possibleMoves[CurrentXPos - 1, CurrentYPos + 2] = true;
                }
                else if (piece1 != null && checkChecking)
                {
                    possibleMoves[CurrentXPos - 1, CurrentYPos + 2] = true;
                }

            }
        }

        return possibleMoves;
    }
}
