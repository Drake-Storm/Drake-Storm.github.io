using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChessAI : MonoBehaviour
{
    System.Random random;
    ChessPiece[,] chessPieces = BoardScript.Instance.ChessPieces;
    public string colour;
    bool playerTurn = BoardScript.Instance.player1Turn;
    King theKing;
    ChessPiece pieceWithBestMove;
    public Vector2 theBestMove;

    /// <summary>
    /// This will select the piece with the best move evaluation.
    /// currently if there is non with the best move evaluation then it returns a random piece.
    /// </summary>
    /// <returns>A chess piece with the best move evaluation.</returns>
    public ChessPiece SelectChessPiece()
    {

        chessPieces = BoardScript.Instance.ChessPieces;
        theBestMove = new Vector2(-1, -1);
        random = new System.Random();
        pieceWithBestMove = null;



        foreach (ChessPiece chessPiece in chessPieces)
        {
            if (chessPiece != null && chessPiece.GetType() == typeof(King))
            {
                if (chessPiece.player1 != playerTurn)
                {
                    theKing = (King)chessPiece;
                    break;
                }
            }
        }

        if (BoardScript.Instance.IsCheckmate() || BoardScript.Instance.IsStalemate())
        {
            return null;
        }

        ChessPiece[,] friendlyPieces;

        if (colour == "Black")
        {
            friendlyPieces = theKing.OpponentPieces(playerTurn);
        }
        else
        {
            friendlyPieces = theKing.OpponentPieces(!playerTurn);
        }

        int count = 0;
        int pieceMoveEval = -100000;
        ChessPiece tempPiece = null;
        Dictionary<ChessPiece, Vector2> tiedPieces = new Dictionary<ChessPiece, Vector2>();

        if (BoardScript.Instance.IsCheckmate() || BoardScript.Instance.IsStalemate())
        {
            return null;
        }

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if(friendlyPieces[i,j] != null)
                {
                    count++;
                    tempPiece = friendlyPieces[i, j];

                    Vector3 bestMoveForPiece = AiMove(friendlyPieces[i, j]);

                    if(pieceMoveEval == bestMoveForPiece.z)
                    {
                        tiedPieces.Add(friendlyPieces[i, j], new Vector2(bestMoveForPiece.x, bestMoveForPiece.y));
                    }

                    if(pieceMoveEval < bestMoveForPiece.z && bestMoveForPiece.x != -1)
                    {
                        pieceMoveEval = (int)bestMoveForPiece.z;
                        pieceWithBestMove = friendlyPieces[i, j];
                        theBestMove = new Vector2(bestMoveForPiece.x, bestMoveForPiece.y);
                        tiedPieces.Clear();
                        tiedPieces.Add(friendlyPieces[i, j], theBestMove);
                    }

                }
            }
        }

        if (tempPiece != null && count == 1)
        {
            return tempPiece;
        }

        if(pieceMoveEval == -100000)
        {
            if (theKing.InCheck)
            {
                if (BoardScript.Instance.IsCheckmate())
                {
                    return null;
                }
                if(pieceWithBestMove != null)
                {
                    return pieceWithBestMove;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                List<ChessPiece> activePieces = BoardScript.Instance.activePieces;

                ChessPiece piece = activePieces[random.Next(activePieces.Count)];

                return piece;
            }

            
        }
        else
        {
            if (BoardScript.Instance.IsCheckmate() || BoardScript.Instance.IsStalemate())
            {
                return null;
            }
            if (tiedPieces.Count > 1)
            {
                int index = random.Next(tiedPieces.Count - 1);
                theBestMove = tiedPieces.Values.ElementAt(index);
                pieceWithBestMove = tiedPieces.Keys.ElementAt(index);
                return pieceWithBestMove;
            }
            else
            {
                return pieceWithBestMove;
            }
        }
    }

    /// <summary>
    /// Gets each piece value for position/piece evaluation.
    /// Piece values are from: https://www.freecodecamp.org/news/simple-chess-ai-step-by-step-1d55a9266977/
    /// </summary>
    /// <param name="chessPiece">The piece for each possible move, used to get the best move</param>
    /// <returns>An int representing the value of the piece or move</returns>
    public int GetPieceVal(ChessPiece chessPiece)
    {
        if (chessPiece == null)
        {
            return 0;
        }

        //If the chess piece to be evaluated is on the AI side
        if(chessPiece.player1 != playerTurn)
        {

            if (chessPiece.GetType() == typeof(Pawn))
            {
                return 10;
            }
            else if (chessPiece.GetType() == typeof(Knight))
            {
                return 30;
            }
            else if (chessPiece.GetType() == typeof(Bishop))
            {
                return 30;
            }
            else if (chessPiece.GetType() == typeof(Rook))
            {
                return 50;
            }
            else if (chessPiece.GetType() == typeof(Queen))
            {
                return 90;
            }
            else if (chessPiece.GetType() == typeof(King))
            {
                return 900;
            }
            else
            {
                return 0;
            }
        }
        else
        {

            if (chessPiece.GetType() == typeof(Pawn))
            {
                return -10;
            }
            else if (chessPiece.GetType() == typeof(Knight))
            {
                return -30;
            }
            else if (chessPiece.GetType() == typeof(Bishop))
            {
                return -30;
            }
            else if (chessPiece.GetType() == typeof(Rook))
            {
                return -50;
            }
            else if (chessPiece.GetType() == typeof(Queen))
            {
                return -90;
            }
            else if (chessPiece.GetType() == typeof(King))
            {
                return -900;
            }
            else
            {
                return 0;
            }
        }

    }

    /// <summary>
    /// This will go through the entire board and evaluate each tile, adding them together to get a total board evaluation
    /// </summary>
    /// <returns>An int representing the boards score</returns>
    public int EvaluateTheBoard(ChessPiece[,] thePieces)
    {
        int total = 0;

        foreach(ChessPiece thePiece in thePieces)
        {
            total += GetPieceVal(thePiece);
        }

        return total;
    }

    /// <summary>
    /// Evaluates each move the piece is able to make
    /// </summary>
    /// <param name="piece">The chess piece to have its moves evaluated</param>
    /// <returns>A 2d array representing each spot on the board and an evaluation for it based on the available moves of the piece</returns>
    public int[,] PieceMovesEval(ChessPiece piece)
    {

        bool[,] availableMoves = GetAvailableMoves(piece);
        int[,] moves = new int[8, 8];

        if(piece == null)
        {
            return moves;
        }



        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (availableMoves[i, j])
                {
                    ChessPiece[,] allThePieces = (ChessPiece[,])chessPieces.Clone();
                    int evaluation = MinimaxEval(2, allThePieces, true, -100000, 100000, piece);

                    moves[i, j] = evaluation;
                }
            }
        }

        return moves;
    }

    /// <summary>
    /// Gets all the available moves for desired piece. Makes sure to take out any moves that would result in the piece's team to be in check.
    /// </summary>
    /// <param name="piece">The desired piece</param>
    /// <returns>A 2d array of boolean values representing the board, all true values are the valid moves the piece can make</returns>
    public bool[,] GetAvailableMoves(ChessPiece piece)
    {
        bool[,] availableMoves = piece.CanMove();

        //Make sure AI can't move into check
        if (piece.GetType() == typeof(King))
        {
            chessPieces[piece.CurrentXPos, piece.CurrentYPos] = null;
            King king = (King)piece;
            ChessPiece[,] opponents = king.OpponentPieces();
            bool[,] wouldBeCheck = new bool[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (availableMoves[i, j])
                    {
                        wouldBeCheck[i, j] = king.WouldBeCheck(opponents, i, j);
                        if (wouldBeCheck[i, j])
                        {
                            availableMoves[i, j] = false;
                        }
                    }
                }
            }

            //Check the King can castle
            if (!king.hasMoved)
            {
                availableMoves[piece.CurrentXPos + 2, piece.CurrentYPos] = king.Castling(0);
                availableMoves[piece.CurrentXPos - 2, piece.CurrentYPos] = king.Castling(1);
            }
        }

        //If the current king is in check, make sure the only allowed moves are able to get them out of check
        if (theKing.InCheck)
        {
            availableMoves = BoardScript.Instance.CanGetOutOfCheck(piece);
            bool checkmate = true;
            foreach (bool move in availableMoves)
            {
                if (move == true)
                {
                    checkmate = false;
                    break;
                }
                
            }

            if (checkmate)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (availableMoves[i, j])
                        {
                            checkmate = false;
                        }
                    }
                }
            }

            if (checkmate)
            {
                bool isItCheckmate = BoardScript.Instance.IsCheckmate();
                if (!isItCheckmate)
                {
                    availableMoves = BoardScript.Instance.CanGetOutOfCheck(piece);
                }
                
            }
        }

        if (piece.GetType() == typeof(King))
        {
            chessPieces[piece.CurrentXPos, piece.CurrentYPos] = piece;
        }

        ChessPiece[,] theOpponents;

        theOpponents = theKing.OpponentPieces();

        //Make sure no moves allow for the AI to put itself in check
        //Make sure AI can't move into check
        if (piece.GetType() == typeof(King))
        {
            chessPieces[piece.CurrentXPos, piece.CurrentYPos] = null;
            King king = (King)piece;
            ChessPiece[,] opponents = king.OpponentPieces();
            bool[,] wouldBeCheck = new bool[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if(availableMoves[i, j])
                    {
                        wouldBeCheck[i, j] = king.WouldBeCheck(opponents, i, j);
                        if (wouldBeCheck[i, j])
                        {
                            availableMoves[i, j] = false;
                        }
                    }
                }
            }

            chessPieces[piece.CurrentXPos, piece.CurrentYPos] = piece;

            //Check the King can castle
            if (!king.hasMoved)
            {
                availableMoves[piece.CurrentXPos + 2, piece.CurrentYPos] = king.Castling(0);
                availableMoves[piece.CurrentXPos - 2, piece.CurrentYPos] = king.Castling(1);
            }
        }

        //Make sure not to allow piece to move where it already is 
        if (availableMoves[piece.CurrentXPos, piece.CurrentYPos])
        {
            availableMoves[piece.CurrentXPos, piece.CurrentYPos] = false;
        }

        //Check if the piece is blocking check, if so then do not allow it to move unless it will take out the offender
        if(piece.GetType() != typeof(King))
        {
            chessPieces[piece.CurrentXPos, piece.CurrentYPos] = null;
            if (theKing.WouldBeCheck(theOpponents, theKing.CurrentXPos, theKing.CurrentYPos))
            {
                ChessPiece offender = BoardScript.Instance.OffendingPiece();
                if(offender != null)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (offender.CurrentXPos != i || offender.CurrentYPos != j)
                            {
                                availableMoves[i, j] = false;
                            }
                        }
                    }
                }
                
            }
            chessPieces[piece.CurrentXPos, piece.CurrentYPos] = piece;
        }
        

        return availableMoves;
    }

    /// <summary>
    /// This is the minimax algorithm using Alpha-Beta pruning, searching through all the pieces moves and figuring out the best one accordingly.
    /// Both minimax and Alpha-Beta pruning descriptions can be found here: https://www.freecodecamp.org/news/simple-chess-ai-step-by-step-1d55a9266977/
    /// </summary>
    /// <param name="depth">How far the recursion goes.</param>
    /// <param name="pieces">A clone of the chesspieces, allowing to move pieces without affecting the real board</param>
    /// <param name="isAI">Determines whether we are looking for the max move eval or min move eval</param>
    /// <param name="alpha">used in Alpha-Beta pruning, allows to find the best move faster</param>
    /// <param name="beta">used in Alpha-Beta pruning, allows to find the best move faster</param>
    /// <param name="thePiece">an optional ChessPiece, if null then it will look through all pieces on the same side</param>
    /// <returns></returns>
    private int MinimaxEval(int depth, ChessPiece[,] pieces, bool isAI, int alpha, int beta, ChessPiece? thePiece)
    {
        if (depth == 0)
        {
            return EvaluateTheBoard(pieces);
        }
        if (BoardScript.Instance.IsCheckmate() || BoardScript.Instance.IsStalemate())
        {
            return EvaluateTheBoard(pieces);
        }

        bool[,] validMoves = new bool[8,8];

        if (thePiece != null)
        {
            validMoves = GetAvailableMoves(thePiece);
        }


        ChessPiece[,] opponents;
        ChessPiece[,] friendlys;

        if (colour == "Black")
        {
            friendlys = theKing.OpponentPieces(playerTurn);
            opponents = theKing.OpponentPieces(!playerTurn);
        }
        else
        {
            friendlys = theKing.OpponentPieces(!playerTurn);
            opponents = theKing.OpponentPieces(playerTurn);
        }



        if (isAI)
        {
            int moveEval = -9999;

            if(thePiece != null)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (validMoves[i, j])
                        {
                            if(pieces[i,j] != null)
                            {
                                ChessPiece takenPiece = pieces[i, j];

                                pieces[i, j] = null;
                                pieces[thePiece.CurrentXPos, thePiece.CurrentYPos] = null;
                                pieces[i, j] = thePiece;

                                moveEval = Mathf.Max(moveEval, MinimaxEval(depth - 1, pieces, !isAI, alpha, beta, null));

                                pieces[i, j] = null;
                                pieces[thePiece.CurrentXPos, thePiece.CurrentYPos] = thePiece;
                                pieces[i, j] = takenPiece;
                            }
                            else
                            {
                                pieces[thePiece.CurrentXPos, thePiece.CurrentYPos] = null;
                                pieces[i, j] = thePiece;

                                moveEval = Mathf.Max(moveEval, MinimaxEval(depth - 1, pieces, !isAI, alpha, beta, null));

                                pieces[i, j] = null;
                                pieces[thePiece.CurrentXPos, thePiece.CurrentYPos] = thePiece;
                            }

                            alpha = Mathf.Max(alpha, moveEval);

                            if(beta <= alpha)
                            {
                                return moveEval;
                            }

                        }
                    }
                }
            }
            else
            {
                

                foreach (ChessPiece friendly in friendlys)
                {
                    if (friendly != null)
                    {
                        validMoves = GetAvailableMoves(friendly);
                        for (int i = 0; i < 8; i++)
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                if (validMoves[i, j])
                                {
                                    if (pieces[i, j] != null)
                                    {
                                        ChessPiece takenPiece = pieces[i, j];

                                        pieces[i, j] = null;
                                        pieces[friendly.CurrentXPos, friendly.CurrentYPos] = null;
                                        pieces[i, j] = friendly;

                                        moveEval = Mathf.Max(moveEval, MinimaxEval(depth - 1, pieces, !isAI, alpha, beta, null));

                                        pieces[i, j] = null;
                                        pieces[friendly.CurrentXPos, friendly.CurrentYPos] = friendly;
                                        pieces[i, j] = takenPiece;
                                    }
                                    else
                                    {
                                        pieces[friendly.CurrentXPos, friendly.CurrentYPos] = null;
                                        pieces[i, j] = friendly;

                                        moveEval = Mathf.Max(moveEval, MinimaxEval(depth - 1, pieces, !isAI, alpha, beta, null));

                                        pieces[i, j] = null;
                                        pieces[friendly.CurrentXPos, friendly.CurrentYPos] = friendly;
                                    }


                                    alpha = Mathf.Max(alpha, moveEval);

                                    if (beta <= alpha)
                                    {
                                        return moveEval;
                                    }

                                }
                            }
                        }

                    }
                }
            }

            
            return moveEval;
        }
        else
        {
            int moveEval = 9999;
            

            foreach(ChessPiece opponent in opponents)
            {
                if (opponent != null)
                {
                    validMoves = GetAvailableMoves(opponent);

                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (validMoves[i, j])
                            {
                                if (pieces[i, j] != null)
                                {
                                    ChessPiece takenPiece = pieces[i, j];

                                    pieces[i, j] = null;
                                    pieces[opponent.CurrentXPos, opponent.CurrentYPos] = null;
                                    pieces[i, j] = opponent;

                                    moveEval = Mathf.Min(moveEval, MinimaxEval(depth - 1, pieces, !isAI, alpha, beta, null));

                                    pieces[i, j] = null;
                                    pieces[opponent.CurrentXPos, opponent.CurrentYPos] = opponent;
                                    pieces[i, j] = takenPiece;
                                }
                                else
                                {
                                    pieces[opponent.CurrentXPos, opponent.CurrentYPos] = null;
                                    pieces[i, j] = opponent;

                                    moveEval = Mathf.Min(moveEval, MinimaxEval(depth - 1, pieces, !isAI, alpha, beta, null));

                                    pieces[i, j] = null;
                                    pieces[opponent.CurrentXPos, opponent.CurrentYPos] = opponent;
                                }

                                beta = Mathf.Min(beta, moveEval);

                                if (beta <= alpha)
                                {
                                    return moveEval;
                                }

                            }
                        }
                    }
                }
            }

            return moveEval;
        }
    }


    /// <summary>
    /// This will determine the best move for the selected piece.
    /// </summary>
    /// <param name="piece">The selected piece</param>
    /// <returns>A Vector3 representing the best move for the selected piece aloong with the evaluation</returns>
    public Vector3 AiMove(ChessPiece piece)
    {
        random = new System.Random();
        bool[,] movesWithinPieceRange = piece.CanMove();
        bool[,] availableMoves = GetAvailableMoves(piece);
        
        int[,] theMovesEvaluations = PieceMovesEval(piece);

        int theBestEval = -9999;
        Vector3 theBestMove = new Vector3(-1, -1, -1);

        ChessPiece[,] opponents;

        opponents = theKing.OpponentPieces();

        for (int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                if(availableMoves[i, j])
                {

                    if (theBestEval < theMovesEvaluations[i, j])
                    {

                        if (movesWithinPieceRange[i, j])
                        {
                            if(piece.GetType() != typeof(King))
                            {
                                ChessPiece takenPiece = opponents[i,j];
                                if(chessPieces[i,j] != null)
                                {
                                    opponents[i, j] = null;
                                }
                                chessPieces[piece.CurrentXPos, piece.CurrentYPos] = null;
                                chessPieces[i, j] = piece;
                                if (!theKing.WouldBeCheck(opponents, theKing.CurrentXPos, theKing.CurrentYPos))
                                {
                                    theBestEval = theMovesEvaluations[i, j];
                                    theBestMove = new Vector3(i, j, theBestEval);
                                }
                                chessPieces[piece.CurrentXPos, piece.CurrentYPos] = piece;
                                chessPieces[i, j] = takenPiece;  
                            }
                            else
                            {
                                if (!theKing.WouldBeCheck(opponents, i, j))
                                {
                                    theBestEval = theMovesEvaluations[i, j];
                                    theBestMove = new Vector3(i, j, theBestEval);
                                }
                            }
                        }


                    }
                }
            }
        }

        return theBestMove;

    }

}
