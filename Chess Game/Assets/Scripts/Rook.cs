using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : ChessPiece
{
    public bool hasMoved;

    /// <summary>
    /// This gets every possible move for the Rook, 
    /// using for loops to check all spaces in front, behind, left, and right.
    /// Stops if it finds another piece.
    /// </summary>
    /// <returns>A 2D array of boolean values representing each space on the board and if they are a valid move.</returns>
    public override bool[,] CanMove()
    {
        bool[,] possibleMoves = new bool[8, 8];
        ChessPiece piece1;

        if (player1)
        {
            //In front 
            if(CurrentYPos != 7)
            {
                for (int i = CurrentYPos + 1; i < 8 && i >= 0; i++)
                {
                    piece1 = BoardScript.Instance.ChessPieces[CurrentXPos, i];

                    if (piece1 == null)
                    {
                        possibleMoves[CurrentXPos, i] = true;
                    }

                    if (piece1 != null && !piece1.player1)
                    {
                        possibleMoves[CurrentXPos, i] = true;
                        break;
                    }
                    if (piece1 != null && piece1.player1)
                    {
                        if (checkChecking)
                        {
                            possibleMoves[CurrentXPos, i] = true;
                        }
                        break;
                    } 
                }
            }

            //Behind
            if(CurrentYPos != 0)
            {
                for (int i = CurrentYPos - 1; i < 8 && i >= 0; i--)
                {

                    piece1 = BoardScript.Instance.ChessPieces[CurrentXPos, i];
                    if (piece1 == null)
                    {
                        possibleMoves[CurrentXPos, i] = true;
                    }
                    if (piece1 != null && !piece1.player1)
                    {
                        possibleMoves[CurrentXPos, i] = true;
                        break;
                    }
                    if (piece1 != null && piece1.player1)
                    {
                        if (checkChecking)
                        {
                            possibleMoves[CurrentXPos, i] = true;
                        }
                        break; 
                    }
                }
            }

            //Right
            if(CurrentXPos != 7)
            {
                for (int i = CurrentXPos + 1; i < 8 && i >= 0; i++)
                {
                    piece1 = BoardScript.Instance.ChessPieces[i, CurrentYPos];

                    if (piece1 == null)
                    {
                        possibleMoves[i, CurrentYPos] = true;
                    }
                    if (piece1 != null && !piece1.player1)
                    {
                        possibleMoves[i, CurrentYPos] = true;
                        break;
                    }
                    if (piece1 != null && piece1.player1)
                    {
                        if (checkChecking)
                        {
                            possibleMoves[i, CurrentYPos] = true;
                        }
                        break;
                    }
                }
                
            }

            //Left
            if (CurrentXPos != 0)
            {
                for (int i = CurrentXPos - 1; i < 8 && i >= 0; i--)
                {
                    piece1 = BoardScript.Instance.ChessPieces[i, CurrentYPos];
                    if (piece1 == null)
                    {
                        possibleMoves[i, CurrentYPos] = true;
                    }
                    if (piece1 != null && !piece1.player1)
                    {
                        possibleMoves[i, CurrentYPos] = true;
                        break;
                    }
                    if (piece1 != null && piece1.player1)
                    {
                        if (checkChecking)
                        {
                            possibleMoves[i, CurrentYPos] = true;
                        }
                        break;
                    }
                }
            }

        }
        else
        {
            //Behind
            if (CurrentYPos != 0)
            {
                for (int i = CurrentYPos + 1; i < 8 && i >= 0; i++)
                {
                    piece1 = BoardScript.Instance.ChessPieces[CurrentXPos, i];
                    if (piece1 == null)
                    {
                        possibleMoves[CurrentXPos, i] = true;
                    }
                    if (piece1 != null && piece1.player1)
                    {
                        possibleMoves[CurrentXPos, i] = true;
                        break;
                    }
                    if (piece1 != null && !piece1.player1)
                    {
                        if (checkChecking)
                        {
                            possibleMoves[CurrentXPos, i] = true;
                        }
                        break;
                    }
                }
            }

            //In front
            if (CurrentYPos != 0)
            {
                for (int i = CurrentYPos - 1; i < 8 && i >= 0; i--)
                {
                    piece1 = BoardScript.Instance.ChessPieces[CurrentXPos, i];
                    if (piece1 == null)
                    {
                        possibleMoves[CurrentXPos, i] = true;
                    }
                    if (piece1 != null && piece1.player1)
                    {
                        possibleMoves[CurrentXPos, i] = true;
                        break;
                    }
                    if (piece1 != null && !piece1.player1)
                    {
                        if (checkChecking)
                        {
                            possibleMoves[CurrentXPos, i] = true;
                        }
                        break;
                    } 
                }
            }

            //Left
            if (CurrentXPos != 7)
            {
                for (int i = CurrentXPos + 1; i < 8 && i >= 0; i++)
                {
                    piece1 = BoardScript.Instance.ChessPieces[i, CurrentYPos];
                    if (piece1 == null)
                    {
                        possibleMoves[i, CurrentYPos] = true;
                    }
                    if (piece1 != null && piece1.player1)
                    {
                        possibleMoves[i, CurrentYPos] = true;
                        break;
                    }
                    if (piece1 != null && !piece1.player1)
                    {
                        if (checkChecking)
                        {
                            possibleMoves[i, CurrentYPos] = true;
                        }
                        break;
                    } 
                }
            }

            //Right
            if (CurrentXPos != 0)
            {
                for (int i = CurrentXPos - 1; i < 8 && i >= 0; i--)
                {
                    piece1 = BoardScript.Instance.ChessPieces[i, CurrentYPos];
                    if (piece1 == null)
                    {
                        possibleMoves[i, CurrentYPos] = true;
                    }
                    if (piece1 != null && piece1.player1)
                    {
                        possibleMoves[i, CurrentYPos] = true;
                        break;
                    }
                    if (piece1 != null && !piece1.player1)
                    {
                        if (checkChecking)
                        {
                            possibleMoves[i, CurrentYPos] = true;
                        }
                        break;
                    }
                }
            }
        }
        return possibleMoves;
    }
}
