using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : ChessPiece
{
    public bool InCheck { get; set; }
    public bool hasMoved;

    /// <summary>
    /// Gets all opponent pieces on the board
    /// </summary>
    /// <param name="isPlayer1">Represents which players turn it is, True for White, False for Black.</param>
    /// <returns>A 2d array of where the opponent pieces are</returns>
    public ChessPiece[,] OpponentPieces(bool isPlayer1)
    {
        ChessPiece[,] opponents = new ChessPiece[8, 8];
        ChessPiece[,] players = BoardScript.Instance.ChessPieces;
        foreach (ChessPiece piece in players)
        {
            if (piece != null)
            {
                if (piece.player1 != isPlayer1)
                {
                    opponents[piece.CurrentXPos, piece.CurrentYPos] = piece;
                }
            }
        }

        return opponents;

    }

    /// <summary>
    /// Gets all opponent pieces on the board
    /// </summary>
    /// <returns>A 2d array of where the opponent pieces are</returns>
    public ChessPiece[,] OpponentPieces()
    {
        ChessPiece[,] opponents = new ChessPiece[8, 8];
        ChessPiece[,] players = BoardScript.Instance.ChessPieces;
        foreach (ChessPiece piece in players)
        {
            if (piece != null)
            {
                if (piece.colour != this.colour)
                {
                    opponents[piece.CurrentXPos, piece.CurrentYPos] = piece;
                }
            }
        }

        return opponents;
    }

    /// <summary>
    /// This checks is the King would be in check at position [x,y]
    /// </summary>
    /// <param name="opponents">A 2D array of all the opponent pieces</param>
    /// <param name="x">The desired coloumn of the board</param>
    /// <param name="y">The desired row of the board</param>
    /// <returns>True if the king would be in Check, false if not.</returns>
    public bool WouldBeCheck(ChessPiece[,] opponents, int x, int y)
    {
        foreach (ChessPiece opponent in opponents)
        {
            if (opponent != null)
            {
                opponent.checkChecking = true;
                bool[,] allowedMoves = opponent.CanMove();
                opponent.checkChecking = false;

                //Pawns can only take diagonally, thus check if the piece is a pawn
                if(opponent.GetType() == typeof(Pawn))
                {
                    //Can move in front of pawn
                    if(opponent.CurrentXPos == x)
                    {
                        allowedMoves[x, y] = false;
                    }
                    if (player1)
                    {
                        //Cannot move in front Diagonal from pawn
                        if(opponent.CurrentXPos - 1 == x && opponent.CurrentYPos - 1 == y)
                        {
                            allowedMoves[x, y] = true;
                        }
                        if(opponent.CurrentXPos + 1 == x && opponent.CurrentYPos - 1 == y)
                        {
                            allowedMoves[x, y] = true;
                        }
                    }
                    else
                    {
                        //Cannot move in front Diagonal from pawn
                        if (opponent.CurrentXPos - 1 == x && opponent.CurrentYPos + 1 == y)
                        {
                            allowedMoves[x, y] = true;
                        }
                        if (opponent.CurrentXPos + 1 == x && opponent.CurrentYPos + 1 == y)
                        {
                            allowedMoves[x, y] = true;
                        }
                    }
                }

                if (allowedMoves[x, y] == true)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool Castling(int side)
    {
        //Check if this King has not moved and is not in check
        if (hasMoved || InCheck)
        {
            return false;
        }

        ChessPiece piece1;
        ChessPiece[,] opponents = OpponentPieces(player1);
        //Short Castling
        if(side == 0)
        {

            //Check the square directly to the right of King
            piece1 = BoardScript.Instance.ChessPieces[CurrentXPos + 1, CurrentYPos];
            if (piece1 == null)
            {
                if (WouldBeCheck(opponents, CurrentXPos + 1, CurrentYPos))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            //Check 2 squares to the right of King
            piece1 = BoardScript.Instance.ChessPieces[CurrentXPos + 2, CurrentYPos];
            if (piece1 == null)
            {
                if (WouldBeCheck(opponents, CurrentXPos + 2, CurrentYPos))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            //Check 3 squares to the right of King is friendly Rook and it has not moved
            piece1 = BoardScript.Instance.ChessPieces[CurrentXPos + 3, CurrentYPos];
            if (piece1 != null && piece1.GetType() == typeof(Rook) && piece1.player1 == player1)
            {
                Rook rook = (Rook)piece1;
                if (rook.hasMoved)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
                
        }
        //Long Castling
        else if(side == 1)
        {
            //Check the square directly to the left of King
            piece1 = BoardScript.Instance.ChessPieces[CurrentXPos - 1, CurrentYPos];
            if (piece1 == null)
            {
                if (WouldBeCheck(opponents, CurrentXPos - 1, CurrentYPos))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            //Check 2 squares to the left of King
            piece1 = BoardScript.Instance.ChessPieces[CurrentXPos - 2, CurrentYPos];
            if (piece1 == null)
            {
                if (WouldBeCheck(opponents, CurrentXPos - 2, CurrentYPos))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            //Check 3 squares to the left of King
            piece1 = BoardScript.Instance.ChessPieces[CurrentXPos - 3, CurrentYPos];
            if (piece1 == null)
            {
                if (WouldBeCheck(opponents, CurrentXPos - 3, CurrentYPos))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            //Check 4 squares to the left of King is friendly Rook and it has not moved
            piece1 = BoardScript.Instance.ChessPieces[CurrentXPos - 4, CurrentYPos];
            if (piece1 != null && piece1.GetType() == typeof(Rook) && piece1.player1 == player1)
            {
                Rook rook = (Rook)piece1;
                if (rook.hasMoved)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// This gets all the possible moves for the King 
    /// (not including the moves which would put them in check as that causes a StackOverflow Error)
    /// </summary>
    /// <returns>A 2D array of boolean values representing each space on the board and if they are a valid move.</returns>
    public override bool[,] CanMove()
    {
        bool[,] possibleMoves = new bool[8, 8];
        ChessPiece piece1;

        if (player1)
        {

            //Forward Left
            if(CurrentYPos !=7 && CurrentXPos != 0)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos - 1, CurrentYPos + 1];

                if(piece1 == null)
                {
                    possibleMoves[CurrentXPos - 1, CurrentYPos + 1] = true;
                }
                else
                {
                    if (piece1.player1 != player1)
                    {
                        possibleMoves[CurrentXPos - 1, CurrentYPos + 1] = true;
                    }
                    else if (checkChecking)
                    {
                        possibleMoves[CurrentXPos - 1, CurrentYPos + 1] = true;
                    }
                }
            }

            //Forward
            if (CurrentYPos != 7)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos, CurrentYPos + 1];

                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos, CurrentYPos + 1] = true;
                }
                else
                {
                    if (piece1.player1 != player1)
                    {
                        possibleMoves[CurrentXPos, CurrentYPos + 1] = true;
                    }
                    else if (checkChecking)
                    {
                        possibleMoves[CurrentXPos, CurrentYPos + 1] = true;
                    }
                }
            }

            //Forward Right
            if (CurrentYPos != 7 && CurrentXPos != 7)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos + 1, CurrentYPos + 1];

                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos + 1, CurrentYPos + 1] = true;
                }

                else
                {
                    if (piece1.player1 != player1)
                    {
                        possibleMoves[CurrentXPos + 1, CurrentYPos + 1] = true;
                    }
                    else if (checkChecking)
                    {
                        possibleMoves[CurrentXPos + 1, CurrentYPos + 1] = true;
                    }
                }

            }

            //Right
            if (CurrentXPos != 7)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos + 1, CurrentYPos];

                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos + 1, CurrentYPos] = true;
                }
                else
                {
                    if (piece1.player1 != player1)
                    {
                        possibleMoves[CurrentXPos + 1, CurrentYPos] = true;
                    }
                    else if (checkChecking)
                    {
                        possibleMoves[CurrentXPos + 1, CurrentYPos] = true;
                    }
                }
            }

            //Back Right
            if (CurrentYPos != 0 && CurrentXPos != 7)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos + 1, CurrentYPos - 1];

                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos + 1, CurrentYPos - 1] = true;
                }
                else
                {
                    if (piece1.player1 != player1)
                    {
                        possibleMoves[CurrentXPos + 1, CurrentYPos - 1] = true;
                    }
                    else if (checkChecking)
                    {
                        possibleMoves[CurrentXPos + 1, CurrentYPos - 1] = true;
                    }
                }
            }

            //Back
            if (CurrentYPos != 0)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos, CurrentYPos - 1];

                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos, CurrentYPos - 1] = true;
                }

                else
                {
                    if (piece1.player1 != player1)
                    {
                        possibleMoves[CurrentXPos, CurrentYPos - 1] = true;
                    }
                    else if (checkChecking)
                    {
                        possibleMoves[CurrentXPos, CurrentYPos - 1] = true;
                    }
                }
            }

            //Back Left
            if (CurrentYPos != 0 && CurrentXPos != 0)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos - 1, CurrentYPos - 1];

                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos - 1, CurrentYPos - 1] = true;
                }

                else
                {
                    if (piece1.player1 != player1)
                    {
                        possibleMoves[CurrentXPos - 1, CurrentYPos - 1] = true;
                    }
                    else if (checkChecking)
                    {
                        possibleMoves[CurrentXPos - 1, CurrentYPos - 1] = true;
                    }
                }
            }

            //Left
            if (CurrentXPos != 0)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos - 1, CurrentYPos];

                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos - 1, CurrentYPos] = true;
                }

                else
                {
                    if (piece1.player1 != player1)
                    {
                        possibleMoves[CurrentXPos - 1, CurrentYPos] = true;
                    }
                    else if (checkChecking)
                    {
                        possibleMoves[CurrentXPos - 1, CurrentYPos] = true;
                    }
                }
            }



        }
        else
        {
            //Forward Left
            if (CurrentYPos != 0 && CurrentXPos != 7)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos + 1, CurrentYPos - 1];

                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos + 1, CurrentYPos - 1] = true;
                }
                else
                {
                    if (piece1.player1 != player1)
                    {
                        possibleMoves[CurrentXPos + 1, CurrentYPos - 1] = true;
                    }
                    else if (checkChecking)
                    {
                        possibleMoves[CurrentXPos + 1, CurrentYPos - 1] = true;
                    }
                }
            }

            //Forward
            if (CurrentYPos != 0)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos, CurrentYPos - 1];

                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos, CurrentYPos - 1] = true;
                }

                else
                {
                    if (piece1.player1 != player1)
                    {
                        possibleMoves[CurrentXPos, CurrentYPos - 1] = true;
                    }
                    else if (checkChecking)
                    {
                        possibleMoves[CurrentXPos, CurrentYPos - 1] = true;
                    }
                }
            }

            //Forward Right
            if (CurrentYPos != 0 && CurrentXPos != 0)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos - 1, CurrentYPos - 1];

                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos - 1, CurrentYPos - 1] = true;
                }

                else
                {
                    if (piece1.player1 != player1)
                    {
                        possibleMoves[CurrentXPos - 1, CurrentYPos - 1] = true;
                    }
                    else if (checkChecking)
                    {
                        possibleMoves[CurrentXPos - 1, CurrentYPos - 1] = true;
                    }
                }
            }

            //Right
            if (CurrentXPos != 0)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos - 1, CurrentYPos];

                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos - 1, CurrentYPos] = true;
                }

                else
                {
                    if (piece1.player1 != player1)
                    {
                        possibleMoves[CurrentXPos - 1, CurrentYPos] = true;
                    }
                    else if (checkChecking)
                    {
                        possibleMoves[CurrentXPos - 1, CurrentYPos] = true;
                    }
                }
            }

            //Back Right
            if (CurrentYPos != 7 && CurrentXPos != 0)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos - 1, CurrentYPos + 1];

                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos - 1, CurrentYPos + 1] = true;
                }
                else
                {
                    if (piece1.player1 != player1)
                    {
                        possibleMoves[CurrentXPos - 1, CurrentYPos + 1] = true;
                    }
                    else if (checkChecking)
                    {
                        possibleMoves[CurrentXPos - 1, CurrentYPos + 1] = true;
                    }
                }

            }

            //Back
            if (CurrentYPos != 7)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos, CurrentYPos + 1];

                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos, CurrentYPos + 1] = true;
                }
                else
                {
                    if (piece1.player1 != player1)
                    {
                        possibleMoves[CurrentXPos, CurrentYPos + 1] = true;
                    }
                    else if (checkChecking)
                    {
                        possibleMoves[CurrentXPos, CurrentYPos + 1] = true;
                    }
                }
            }

            //Back Left
            if (CurrentYPos != 7 && CurrentXPos != 7)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos + 1, CurrentYPos + 1];

                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos + 1, CurrentYPos + 1] = true;
                }

                else
                {
                    if (piece1.player1 != player1)
                    {
                        possibleMoves[CurrentXPos + 1, CurrentYPos + 1] = true;
                    }
                    else if (checkChecking)
                    {
                        possibleMoves[CurrentXPos + 1, CurrentYPos + 1] = true;
                    }
                }
            }

            //Left
            if (CurrentXPos != 7)
            {
                piece1 = BoardScript.Instance.ChessPieces[CurrentXPos + 1, CurrentYPos];

                if (piece1 == null)
                {
                    possibleMoves[CurrentXPos + 1, CurrentYPos] = true;
                }
                else
                {
                    if (piece1.player1 != player1)
                    {
                        possibleMoves[CurrentXPos + 1, CurrentYPos] = true;
                    }
                    else if (checkChecking)
                    {
                        possibleMoves[CurrentXPos + 1, CurrentYPos] = true;
                    }
                }
            }
        }

        return possibleMoves;
    }
}
