using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : ChessPiece
{
    /// <summary>
    /// This gets every possible move for the Queen, 
    /// adding the logic for finding valid moves for the Rook
    /// and Bishop together.
    /// </summary>
    /// <returns>A 2D array of boolean values representing each space on the board and if they are a valid move.</returns>
    public override bool[,] CanMove()
    {
        bool[,] possibleMoves = new bool[8, 8];
        ChessPiece piece1;

        if (player1)
        {
            //Valid Horizontals and Verticals

            //In front 
            if (CurrentYPos != 7)
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
            if (CurrentYPos != 0)
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
            if (CurrentXPos != 7)
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


            //Valid Diagonals
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
            //Valid Horizontals and Verticals

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

            //Valid Diagonals

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
