using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : ChessPiece
{
    /// <summary>
    /// This checks all possible moves for the bishop.
    /// Checking all diagonal spaces on the board from 
    /// where the selected piece is. Stops if it finds 
    /// another piece.
    /// </summary>
    /// <returns>A 2D array of boolean values representing each space on the board and if they are a valid move.</returns>
    public override bool[,] CanMove()
    {
        bool[,] possibleMoves = new bool[8, 8];
        ChessPiece piece1;

        if (player1)
        {
            //Front Right
            if (CurrentYPos != 7 && CurrentXPos != 7) 
            {
                int j = CurrentXPos;
                for (int i = CurrentYPos + 1; i < 8 && i >= 0; i++)
                {
                    j += 1;

                    piece1 = BoardScript.Instance.ChessPieces[j, i];

                    if (piece1 == null)
                    {
                        possibleMoves[j, i] = true;
                    }

                    if (piece1 != null && !piece1.player1)
                    {
                        possibleMoves[j, i] = true;
                        break;
                    }
                    if (piece1 != null && piece1.player1)
                    {
                        if (checkChecking)
                        {
                            possibleMoves[j, i] = true;
                        }
                        break;
                    }

                    if (j == 7)
                    {
                        break;
                    }
                }
            }

            //Front Left
            if (CurrentYPos != 7 && CurrentXPos != 0)
            {
                int j = CurrentXPos;
                for (int i = CurrentYPos + 1; i < 8 && i >= 0; i++)
                {
                    j -= 1;

                    piece1 = BoardScript.Instance.ChessPieces[j, i];

                    if (piece1 == null)
                    {
                        possibleMoves[j, i] = true;
                    }

                    if (piece1 != null && !piece1.player1)
                    {
                        possibleMoves[j, i] = true;
                        break;
                    }
                    if (piece1 != null && piece1.player1)
                    {
                        if (checkChecking)
                        {
                            possibleMoves[j, i] = true;
                        }
                        break;
                    }

                    if (j == 0)
                    {
                        break;
                    }
                }
            }

            //Behind Right
            if (CurrentYPos != 0 && CurrentXPos != 7)
            {
                int j = CurrentXPos;
                for (int i = CurrentYPos - 1; i < 8 && i >= 0; i--)
                {
                    j += 1;

                    piece1 = BoardScript.Instance.ChessPieces[j, i];

                    if (piece1 == null)
                    {
                        possibleMoves[j, i] = true;
                    }

                    if (piece1 != null && !piece1.player1)
                    {
                        possibleMoves[j, i] = true;
                        break;
                    }
                    if (piece1 != null && piece1.player1)
                    {
                        if (checkChecking)
                        {
                            possibleMoves[j, i] = true;
                        }
                        break;
                    }

                    if (j == 7)
                    {
                        break;
                    }
                }
            }

            //Behind Left
            if (CurrentYPos != 0 && CurrentXPos != 0)
            {
                int j = CurrentXPos;
                for (int i = CurrentYPos - 1; i < 8 && i >= 0; i--)
                {
                    j -= 1;

                    piece1 = BoardScript.Instance.ChessPieces[j, i];

                    if (piece1 == null)
                    {
                        possibleMoves[j, i] = true;
                    }

                    if (piece1 != null && !piece1.player1)
                    {
                        possibleMoves[j, i] = true;
                        break;
                    }
                    if (piece1 != null && piece1.player1)
                    {
                        if (checkChecking)
                        {
                            possibleMoves[j, i] = true;
                        }
                        break;
                    }

                    if (j == 0)
                    {
                        break;
                    }
                }
            }

        }

        else
        {
            //Back Left
            if (CurrentYPos != 7 && CurrentXPos != 7)
            {
                int j = CurrentXPos;
                for (int i = CurrentYPos + 1; i < 8 && i >= 0; i++)
                {
                    j += 1;

                    piece1 = BoardScript.Instance.ChessPieces[j, i];

                    if (piece1 == null)
                    {
                        possibleMoves[j, i] = true;
                    }

                    if (piece1 != null && piece1.player1)
                    {
                        possibleMoves[j, i] = true;
                        break;
                    }
                    if (piece1 != null && !piece1.player1)
                    {
                        if (checkChecking)
                        {
                            possibleMoves[j, i] = true;
                        }
                        break;
                    }

                    if (j == 7)
                    {
                        break;
                    }
                }
            }

            //Back Right
            if (CurrentYPos != 7 && CurrentXPos != 0)
            {
                int j = CurrentXPos;
                for (int i = CurrentYPos + 1; i < 8 && i >= 0; i++)
                {
                    j -= 1;

                    piece1 = BoardScript.Instance.ChessPieces[j, i];

                    if (piece1 == null)
                    {
                        possibleMoves[j, i] = true;
                    }

                    if (piece1 != null && piece1.player1)
                    {
                        possibleMoves[j, i] = true;
                        break;
                    }
                    if (piece1 != null && !piece1.player1)
                    {
                        if (checkChecking)
                        {
                            possibleMoves[j, i] = true;
                        }
                        break;
                    }

                    if (j == 0)
                    {
                        break;
                    }
                }
            }

            //Front Left
            if (CurrentYPos != 0 && CurrentXPos != 7)
            {
                int j = CurrentXPos;
                for (int i = CurrentYPos - 1; i < 8 && i >= 0; i--)
                {
                    j += 1;

                    piece1 = BoardScript.Instance.ChessPieces[j, i];

                    if (piece1 == null)
                    {
                        possibleMoves[j, i] = true;
                    }

                    if (piece1 != null && piece1.player1)
                    {
                        possibleMoves[j, i] = true;
                        break;
                    }
                    if (piece1 != null && !piece1.player1)
                    {
                        if (checkChecking)
                        {
                            possibleMoves[j, i] = true;
                        }
                        break;
                    }

                    if (j == 7)
                    {
                        break;
                    }
                }
            }

            //Front Right
            if (CurrentYPos != 0 && CurrentXPos != 0)
            {
                int j = CurrentXPos;
                for (int i = CurrentYPos - 1; i < 8 && i >= 0; i--)
                {
                    j -= 1;

                    piece1 = BoardScript.Instance.ChessPieces[j, i];

                    if (piece1 == null)
                    {
                        possibleMoves[j, i] = true;
                    }

                    if (piece1 != null && piece1.player1)
                    {
                        possibleMoves[j, i] = true;
                        break;
                    }
                    if (piece1 != null && !piece1.player1)
                    {
                        if (checkChecking)
                        {
                            possibleMoves[j, i] = true;
                        }
                        break;
                    }

                    if (j == 0)
                    {
                        break;
                    }
                }
            }

        }

        return possibleMoves;
    }
}
