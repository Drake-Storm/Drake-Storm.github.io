using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoardScript : MonoBehaviour
{
    public static BoardScript Instance { get; set; }
    public bool[,] AllowedMoves { get; set; }

    public ChessPiece[,] ChessPieces {
        get;
        set; 
    }
    private ChessPiece selectedPiece;
    public bool player1Turn = true;
    public Camera whiteCam;
    public Camera blackCam;

    public RawImage wpawn1;
    public RawImage wpawn2;
    public RawImage wpawn3;
    public RawImage wpawn4;
    public RawImage wpawn5;
    public RawImage wpawn6;
    public RawImage wpawn7;
    public RawImage wpawn8;
    public RawImage wrook1;
    public RawImage wrook2;
    public RawImage wknight1;
    public RawImage wknight2;
    public RawImage wbishop1;
    public RawImage wbishop2;
    public RawImage wqueen;
    public RawImage wking;
    public RawImage bpawn1;
    public RawImage bpawn2;
    public RawImage bpawn3;
    public RawImage bpawn4;
    public RawImage bpawn5;
    public RawImage bpawn6;
    public RawImage bpawn7;
    public RawImage bpawn8;
    public RawImage brook1;
    public RawImage brook2;
    public RawImage bknight1;
    public RawImage bknight2;
    public RawImage bbishop1;
    public RawImage bbishop2;
    public RawImage bqueen;
    public RawImage bking;
    public RawImage takenwpawn1;
    public RawImage takenwpawn2;
    public RawImage takenwpawn3;
    public RawImage takenwpawn4;
    public RawImage takenwpawn5;
    public RawImage takenwpawn6;
    public RawImage takenwpawn7;
    public RawImage takenwpawn8;
    public RawImage takenwrook1;
    public RawImage takenwrook2;
    public RawImage takenwknight1;
    public RawImage takenwknight2;
    public RawImage takenwbishop1;
    public RawImage takenwbishop2;
    public RawImage takenwqueen;
    public RawImage takenwking;
    public RawImage takenbpawn1;
    public RawImage takenbpawn2;
    public RawImage takenbpawn3;
    public RawImage takenbpawn4;
    public RawImage takenbpawn5;
    public RawImage takenbpawn6;
    public RawImage takenbpawn7;
    public RawImage takenbpawn8;
    public RawImage takenbrook1;
    public RawImage takenbrook2;
    public RawImage takenbknight1;
    public RawImage takenbknight2;
    public RawImage takenbbishop1;
    public RawImage takenbbishop2;
    public RawImage takenbqueen;
    public RawImage takenbking;

    private bool endTheGame = false;
    private const float tileSize = 1.0f;
    private const float tileOffset = 0.5f;
    private int selectedTileX = -1;
    private int selectedTileY = -1;
    public List<GameObject> chessPiecePrefabs;
    private List<GameObject> activeChessPieces;
    public List<ChessPiece> activePieces;
    private King theKing;
    public TMPro.TMP_Dropdown promotion;
    public GameObject promotionScreen;
    public GameObject endingScreen;
    public Text winnerText;
    public string homeScene;
    public string setupScene;
    public string leaderboardScene;
    private ChessPiece pieceToPromote;
    private string theSetOpponent;
    private string theSetColour;
    public bool playerWins;
    private bool aiTurn;
    private ChessAI AI;
    private bool runUpdate = true;

    // Start is called before the first frame update
    void Start()
    {
        endingScreen.SetActive(false);
        whiteCam.enabled = true;
        whiteCam.tag = "MainCamera";
        blackCam.enabled = false;
        blackCam.tag = "Second Camera";
        Instance = this;
        SpawnAllPieces();
        theSetOpponent = PlayerPrefs.GetString("opponent");
        theSetColour = PlayerPrefs.GetString("colour");
        if (theSetOpponent == "Computer")
        {
            GameObject go = new GameObject();
            AI = go.AddComponent<ChessAI>();
            if(theSetColour == "White")
            {
                aiTurn = !player1Turn;
                AI.colour = "Black";
            }
            else
            {
                aiTurn = player1Turn;
                AI.colour = "White";
                whiteCam.enabled = false;
                whiteCam.tag = "Second Camera";
                blackCam.enabled = true;
                blackCam.tag = "MainCamera";
            }
        }
        takenbbishop1.enabled = false;
        takenbbishop2.enabled = false;
        takenbking.enabled = false;
        takenbknight1.enabled = false;
        takenbknight2.enabled = false;
        takenbpawn1.enabled = false;
        takenbpawn2.enabled = false;
        takenbpawn3.enabled = false;
        takenbpawn4.enabled = false;
        takenbpawn5.enabled = false;
        takenbpawn6.enabled = false;
        takenbpawn7.enabled = false;
        takenbpawn8.enabled = false;
        takenbqueen.enabled = false;
        takenbrook1.enabled = false;
        takenbrook2.enabled = false;
        takenwbishop1.enabled = false;
        takenwbishop2.enabled = false;
        takenwking.enabled = false;
        takenwknight1.enabled = false;
        takenwknight2.enabled = false;
        takenwpawn1.enabled = false;
        takenwpawn2.enabled = false;
        takenwpawn3.enabled = false;
        takenwpawn4.enabled = false;
        takenwpawn5.enabled = false;
        takenwpawn6.enabled = false;
        takenwpawn7.enabled = false;
        takenwpawn8.enabled = false;
        takenwqueen.enabled = false;
        takenwrook1.enabled = false;
        takenwrook2.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateSelected();

        if (!endTheGame)
        {
            
            if (IsCheckmate())
            {
                endTheGame = true;
                EndGame(!player1Turn);
            }
            if (IsStalemate())
            {
                endTheGame = true;
                EndGame(!player1Turn);
            }

            if (AI == null)
            {
                if (player1Turn)
                {
                    whiteCam.enabled = true;
                    whiteCam.tag = "MainCamera";
                    blackCam.enabled = false;
                    blackCam.tag = "Second Camera";
                }
                else
                {
                    whiteCam.enabled = false;
                    whiteCam.tag = "Second Camera";
                    blackCam.enabled = true;
                    blackCam.tag = "MainCamera";
                }
            }
            if (promotionScreen.activeInHierarchy == false)
            {
                if (aiTurn)
                {
                    Vector2 aiMove = new Vector2(-1, -1);

                    do
                    {
                        theKing = GetKing();
                        if (IsCheckmate())
                        {
                            EndGame(!player1Turn);
                            endTheGame = true;
                            break;
                        }
                        if (IsStalemate())
                        {
                            EndGame(!player1Turn);
                            endTheGame = true;
                            break;
                        }
                        selectedPiece = AI.SelectChessPiece();
                        if (selectedPiece != null)
                        {
                            if (selectedPiece.player1 == player1Turn)
                            {
                                SelectPiece(selectedPiece.CurrentXPos, selectedPiece.CurrentYPos);
                                aiMove = AI.theBestMove;

                            }
                            else
                            {
                                aiMove.x = -1;
                                aiMove.y = -1;
                            }
                        }
                        else
                        {
                            if (IsCheckmate())
                            {
                                EndGame(!player1Turn);
                                endTheGame = true;
                                break;
                            }
                            if (IsStalemate())
                            {
                                EndGame(!player1Turn);
                                endTheGame = true;
                                break;
                            }
                            if (WouldBeCheckmate())
                            {
                                EndGame(!player1Turn);
                                endTheGame = true;
                                break;
                            }
                        }

                    } while (aiMove.x < 0 && aiMove.y < 0);

                    MovePiece((int)Math.Round(aiMove.x), (int)Math.Round(aiMove.y));
                }

                else
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (selectedTileX >= 0 && selectedTileY >= 0)
                        {
                            if (selectedPiece == null)
                            {
                                SelectPiece(selectedTileX, selectedTileY);
                            }
                            else
                            {
                                MovePiece(selectedTileX, selectedTileY);
                            }
                        }
                    }
                }
            }
            

        }

    }

    /// <summary>
    /// This is used for another level of checkmate checking to make sure the AI recognizes when the game ends
    /// </summary>
    /// <returns>bool representing if it is checkmate or not</returns>
    private bool WouldBeCheckmate()
    {
        King king = new King();
        foreach (ChessPiece chessPiece in ChessPieces)
        {
            if (chessPiece != null)
            {
                if (chessPiece.GetType() == typeof(King))
                {
                    if (chessPiece.player1 != player1Turn)
                    {
                        king = (King)chessPiece;
                        break;
                    }
                }
            }
        }
        //get all pieces on the same team
        ChessPiece[,] friendlyPieces = king.OpponentPieces(!player1Turn);

        //Set a bool[8,8] of all false values
        bool[,] movesToGetOutOfCheck = new bool[8, 8];
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                movesToGetOutOfCheck[i, j] = false;
            }
        }
        //Check all pieces to see if they have a move that can get the king out of check
        foreach (ChessPiece piece in friendlyPieces)
        {
            if (piece != null)
            {
                bool[,] possibleMoves = CanGetOutOfCheck(piece);
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (possibleMoves[i, j])
                        {
                            movesToGetOutOfCheck[i, j] = true;
                        }
                    }
                }
            }
        }
        //Check all values to see if any are true
        foreach (bool check in movesToGetOutOfCheck)
        {
            if (check)
            {
                //This means at least one piece can get the king out of check, thus not checkmate
                return false;
            }
        }

        return true;
    }


    /// <summary>
    /// Gets the current players king, will be used for game state checking
    /// </summary>
    /// <returns>The current players king piece</returns>
    private King GetKing()
    {
        foreach (ChessPiece chessPiece in ChessPieces)
        {
            if (chessPiece != null)
            {
                if (chessPiece.GetType() == typeof(King))
                {
                    if(chessPiece.player1 == player1Turn)
                    {
                        return (King)chessPiece;
                    }
                }
            }
        }
        return null;
    }

    /// <summary>
    /// When the user clicks the resign button then end the game
    /// </summary>
    public void Resign()
    {
        EndGame(!player1Turn);
    }

    /// <summary>
    /// When the game ends display the winner, update the database, and allow the user to play again, check leaderboards, or return to the main menu
    /// </summary>
    /// <param name="winner">boolean value representing who won, true = white, false = black</param>
    private void EndGame(bool winner)
    {
        if (IsStalemate())
        {
            winnerText.text = "Stalemate!";
        }
        else
        {
            if (winner)
            {
                if(theSetColour == "White")
                {
                    String accountName = PlayerPrefs.GetString("username");
                    winnerText.text = accountName + " Won!";
                    playerWins = true;
                }
                else
                {
                    if(AI != null)
                    {
                        winnerText.text = "Computer Won!";
                        playerWins = false;
                    }
                    else
                    {
                        winnerText.text = "Player2 Won!";
                        playerWins = false;
                    }
                }
            }
            else
            {
                if (theSetColour != "White")
                {
                    String accountName = PlayerPrefs.GetString("username");
                    winnerText.text = accountName + " Won!";
                    playerWins = true;
                }
                else
                {
                    if (AI != null)
                    {
                        winnerText.text = "Computer Wins!";
                        playerWins = false;
                    }
                    else
                    {
                        winnerText.text = "Player2 Wins!";
                        playerWins = false;
                    }
                }
            }

        }
        StartCoroutine(GetPlayer());
        endingScreen.SetActive(true);
        
    }

    /// <summary>
    /// This gets the player id from the database
    /// </summary>
    /// <returns>The playerID</returns>
    IEnumerator GetPlayer()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", PlayerPrefs.GetString("username"));

        UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost:1080/sqlconnect/getPlayer.php", form);
        yield return webRequest.SendWebRequest();

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(webRequest.result);
            Debug.Log("Error: " + webRequest.error.ToString());

        }
        else
        {
            string theResult = webRequest.downloadHandler.text;
            if (theResult == "3: username does not exist")
            {
                Debug.Log("Username does not exist");
            }
            else if (theResult == "2: username check failed" || theResult == "1: connection failed")
            {
                Debug.Log("Error: " + theResult);
            }
            else
            {
                StartUpdateDatabase(theResult);
            }
        }
    }

    /// <summary>
    /// This will start the UpdateDatabase coroutine
    /// </summary>
    /// <param name="id">the id of the player from the players table</param>
    private void StartUpdateDatabase(string id)
    {
        if (runUpdate)
        {
            runUpdate = false;
            StartCoroutine(UpdateDatabase(id));
        }
    }

    /// <summary>
    /// This will update the database to add the win/loss
    /// </summary>
    /// <param name="id">The playerID of the user</param>
    /// <returns></returns>
    IEnumerator UpdateDatabase(string id)
    {

        WWWForm form = new WWWForm();
        form.AddField("id", id);
        if(AI != null)
        {
            form.AddField("versus", "Computer");
            if (IsStalemate())
            {
                form.AddField("outcome", "Stalemate");
            }
            else
            {
                if (playerWins)
                {
                    form.AddField("outcome", "Win");
                }
                else
                {
                    form.AddField("outcome", "Loss");
                }
            }
            
        }
        else
        {
            form.AddField("versus", "Player");
            if (IsStalemate())
            {
                form.AddField("outcome", "Stalemate");
            }
            else
            {
                if (playerWins)
                {
                    form.AddField("outcome", "Win");
                }
                else
                {
                    form.AddField("outcome", "Loss");
                }
            }
                
        }
        UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost:1080/sqlconnect/updatePlayer.php", form);
        yield return webRequest.SendWebRequest();

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(webRequest.result);
            Debug.Log("Error: " + webRequest.error.ToString());

        }
        else
        {
            Debug.Log("Update Successful!");
        }
    }

    /// <summary>
    /// Brings the user back to the Main Menu screen when they click the Home button
    /// </summary>
    public void HomeButtonPressed()
    {
        SceneManager.LoadScene(homeScene);
    }

    /// <summary>
    /// function called when the user clicks the Play button
    /// </summary>
    public void PlayBtnPressed()
    {
        SceneManager.LoadScene(setupScene);
    }

    /// <summary>
    /// function called when the user clicks the leaderboard button
    /// </summary>
    public void LeaderboardBtnPressed()
    {
        SceneManager.LoadScene(leaderboardScene);
    }

    /// <summary>
    /// This Checks if the current king is in check
    /// </summary>
    /// <returns>True if they are in check, False if not.</returns>
    private bool IsInCheck()
    {
        ChessPiece[,] opponents = theKing.OpponentPieces(player1Turn);

        return theKing.WouldBeCheck(opponents, theKing.CurrentXPos, theKing.CurrentYPos);
    }

    /// <summary>
    /// This get the piece that is putting the King in check
    /// </summary>
    /// <returns>The chesspiece that is putting the King in check</returns>
    public ChessPiece OffendingPiece()
    {
        ChessPiece[,] opponents = theKing.OpponentPieces(player1Turn);

        foreach (ChessPiece opponent in opponents)
        {
            if (opponent != null)
            {
                bool[,] allowedMoves = opponent.CanMove();

                //Pawns can only take diagonally, thus check if the piece is a pawn
                if (opponent.GetType() == typeof(Pawn))
                {
                    //Can move in front of pawn
                    if (opponent.CurrentXPos == theKing.CurrentXPos)
                    {
                        allowedMoves[theKing.CurrentXPos, theKing.CurrentYPos] = false;
                    }
                    if (!opponent.player1)
                    {
                        //Cannot move in front Diagonal from pawn
                        if (opponent.CurrentXPos - 1 == theKing.CurrentXPos && opponent.CurrentYPos - 1 == theKing.CurrentYPos)
                        {
                            allowedMoves[theKing.CurrentXPos, theKing.CurrentYPos] = true;
                        }
                        if (opponent.CurrentXPos + 1 == theKing.CurrentXPos && opponent.CurrentYPos - 1 == theKing.CurrentYPos)
                        {
                            allowedMoves[theKing.CurrentXPos, theKing.CurrentYPos] = true;
                        }
                    }
                    else
                    {
                        //Cannot move in front Diagonal from pawn
                        if (opponent.CurrentXPos - 1 == theKing.CurrentXPos && opponent.CurrentYPos + 1 == theKing.CurrentYPos)
                        {
                            allowedMoves[theKing.CurrentXPos, theKing.CurrentYPos] = true;
                        }
                        if (opponent.CurrentXPos + 1 == theKing.CurrentXPos && opponent.CurrentYPos + 1 == theKing.CurrentYPos)
                        {
                            allowedMoves[theKing.CurrentXPos, theKing.CurrentYPos] = true;
                        }
                    }
                }

                if (allowedMoves[theKing.CurrentXPos, theKing.CurrentYPos] == true)
                {
                    return opponent;
                }
            }
        }
        return null;
    }

    /// <summary>
    /// This looks through a pieces available moves and checks if any can get the player out of check
    /// </summary>
    /// <param name="chessPiece">One of the players chess pieces</param>
    /// <returns>A 2d Array representing all spaces on the board, each space has a value of true or false, true if that move gets the player out of check</returns>
    public bool[,] CanGetOutOfCheck(ChessPiece chessPiece)
    {
        bool[,] theAllowedMoves = new bool[8,8];
        //Get the moves valid for that specific piece
        theAllowedMoves = chessPiece.CanMove();

        if (chessPiece.GetType() == typeof(King))
        {
            ChessPieces[chessPiece.CurrentXPos, chessPiece.CurrentYPos] = null;
            King king = (King)chessPiece;
            ChessPiece[,] opponents = king.OpponentPieces(player1Turn);
            bool[,] wouldBeCheck = new bool[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (theAllowedMoves[i, j])
                    {
                        wouldBeCheck[i, j] = king.WouldBeCheck(opponents, i, j);
                        if (wouldBeCheck[i, j])
                        {
                            theAllowedMoves[i, j] = false;
                        }

                    }
                }
            }

            ChessPieces[chessPiece.CurrentXPos, chessPiece.CurrentYPos] = chessPiece;
        }

        ChessPiece offender = OffendingPiece();

        if (chessPiece.GetType() != typeof(King))
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (theAllowedMoves[i, j])
                    {
                        if (ChessPieces[i, j] == null)
                        {

                            //Check if this move will block the check
                            if (chessPiece.player1)
                            {
                                SpawnChessPiece(5, i, j);
                            }
                            else
                            {
                                SpawnChessPiece(11, i, j);
                            }
                            ChessPiece aChessPiece = ChessPieces[i, j];
                            bool checkCheck = IsInCheck();
                            activeChessPieces.Remove(aChessPiece.gameObject);
                            activePieces.Remove(aChessPiece);
                            Destroy(aChessPiece.gameObject);
                            ChessPieces[i, j] = null;
                            if (checkCheck)
                            {
                                if(offender != null)
                                {
                                    //If it does not block, check if it will capture the offending piece
                                    if (offender.CurrentXPos != i || offender.CurrentYPos != j)
                                    {
                                        //If it won't capture or block then this move is invalid
                                        theAllowedMoves[i, j] = false;
                                    }
                                }
                                
                            }
                        }
                        else
                        {
                            //check if it will capture the offending piece
                            if(offender != null)
                            {
                                if (offender.CurrentXPos != i || offender.CurrentYPos != j)
                                {
                                    //If it won't capture or block then this move is invalid
                                    theAllowedMoves[i, j] = false;
                                }
                            }
                        }
                    }
                }
            }
        }


        return theAllowedMoves;
    }

    /// <summary>
    /// This checks if the player has any valid moves to get out of check, if not then that is checkmate
    /// </summary>
    /// <returns>A bool value representing whether or not the player is in checkmate.</returns>
    public bool IsCheckmate()
    {

        if (theKing == null)
        {
            theKing = GetKing();
        }


        //get all pieces on the same team
        ChessPiece[,] friendlyPieces = theKing.OpponentPieces(!player1Turn);

        //Set a bool[8,8] of all false values
        bool[,] movesToGetOutOfCheck = new bool[8, 8];
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                movesToGetOutOfCheck[i, j] = false;
            }
        }
        //Check all pieces to see if they have a move that can get the king out of check
        foreach (ChessPiece piece in friendlyPieces)
        {
            if (piece != null)
            {
                bool[,] possibleMoves = CanGetOutOfCheck(piece);
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (possibleMoves[i, j])
                        {
                            movesToGetOutOfCheck[i, j] = true;
                        }
                    }
                }
            }
        }
        //Check all values to see if any are true
        foreach (bool check in movesToGetOutOfCheck)
        {
            if (check)
            {
                //This means at least one piece can get the king out of check, thus not checkmate
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// This checks the players pieces for a stalemate.
    /// Conditions for stalemate:
    ///     - The current player has no available moves but is not in check
    ///     - Both teams only have their Kings left
    ///     - One team only has their King while the other team only has their King and a Bishop OR Knight
    ///     - Both teams only have their Kings and one Bishop on diagonals of the same colour.
    /// </summary>
    /// <returns>A bool which is true if one of the players is in stalemate, false if not.</returns>
    public bool IsStalemate()
    {
        List<ChessPiece> friendly = new List<ChessPiece>();
        List<ChessPiece> opponents = new List<ChessPiece>();
        bool[,] allMoves = new bool[8, 8];
        bool cannotMove = true;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                allMoves[i, j] = false;
            }
        }

        if (theKing.InCheck)
        {
            return false;
        }

        foreach (ChessPiece piece in activePieces)
        {
            if (piece.player1 == player1Turn)
            {
                friendly.Add(piece);
            }
            else
            {
                opponents.Add(piece);
            }
        }

        //Check if the player has any possible moves
        if(theKing.InCheck == false) //make sure king is not in check
        {
            foreach(ChessPiece piece in friendly)
            {
                bool[,] pieceMoves = piece.CanMove();

                //If the piece is the king then make sure not to include moves which can put them in check
                if (piece.GetType() == typeof(King))
                {
                    ChessPiece[,] theOpponents = theKing.OpponentPieces(player1Turn);
                    bool[,] wouldBeCheck = new bool[8, 8];
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            wouldBeCheck[i, j] = theKing.WouldBeCheck(theOpponents, i, j);
                            if (wouldBeCheck[i, j])
                            {
                                pieceMoves[i, j] = false;
                            }
                        }
                    }
                }

                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (!allMoves[i, j])
                        {
                            if (pieceMoves[i, j])
                            {
                                allMoves[i, j] = true;
                            }
                        }
                    }
                }
            }
            foreach(bool move in allMoves)
            {
                if (move)
                {
                    cannotMove = false; //If the player has at least one possible move then it is not stalemate
                }
            }

        }

        //Check if both teams only have their kings
        if (friendly.Count == 1 && opponents.Count == 1)
        {
            if (friendly[0].GetType() == typeof(King))
            {
                if(opponents[0].GetType() == typeof(King))
                {
                    return true;
                }
            }
        }


        if(friendly.Count <= 2 && opponents.Count <= 2)
        {
            //If the player only has their King left
            if (friendly.Count == 1 && friendly[0].GetType() == typeof(King))
            {
                //Check if the opponent has more than one piece
                if (opponents.Count != 1)
                {
                    //If the opponent has only King and Bishop OR Knight
                    if (opponents[0].GetType() == typeof(King))
                    {
                        if (opponents[1].GetType() == typeof(Bishop) || opponents[1].GetType() == typeof(Knight))
                        {
                            return true;
                        }
                    }
                    if (opponents[0].GetType() == typeof(Bishop) || opponents[0].GetType() == typeof(Knight))
                    {
                        if (opponents[1].GetType() == typeof(King))
                        {
                            return true;
                        }
                    }
                }
                
            }

            //Check if both teams only have their Kings and a Bishop on diagonals of the same colour

            //Getting colours of each tile in a chessboard
            string[,] aChessBoard = new string[8, 8];
            bool white = true;
            for(int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (white)
                    {
                        aChessBoard[i, j] = "White";
                    }
                    else
                    {
                        aChessBoard[i, j] = "Black";
                    }
                    white = !white;
                }
            }
            string opponentColour = "";
            string friendColour = "";

            if(friendly.Count != 1 && opponents.Count != 1)
            {
                //If first firendly piece is a king
                if (friendly[0].GetType() == typeof(King))
                {

                    //Make sure second friendly is a bishop
                    if (friendly[1].GetType() == typeof(Bishop))
                    {
                        friendColour = aChessBoard[friendly[1].CurrentXPos, friendly[1].CurrentYPos]; //get the colour the bishop is on

                        //make sure opponent also only has king and bishop
                        if (opponents.Count != 1)
                        {
                            if (opponents[0].GetType() == typeof(King))
                            {
                                if (opponents[1].GetType() == typeof(Bishop))
                                {
                                    opponentColour = aChessBoard[opponents[1].CurrentXPos, opponents[1].CurrentYPos]; //get the colour the bishop is on
                                }

                            }
                            else if (opponents[0].GetType() == typeof(Bishop))
                            {
                                if (opponents[1].GetType() == typeof(King))
                                {
                                    opponentColour = aChessBoard[opponents[0].CurrentXPos, opponents[0].CurrentYPos]; //get the colour the bishop is on
                                }
                            }
                        }

                    }
                }
                if (friendly[0].GetType() == typeof(Bishop))
                {
                    friendColour = aChessBoard[friendly[0].CurrentXPos, friendly[0].CurrentYPos]; //get the colour the bishop is on

                    //make sure the second piece is a King
                    if (friendly[1].GetType() == typeof(King))
                    {
                        if (opponents.Count != 1)
                        {
                            //make sure opponent also only has king and bishop
                            if (opponents[0].GetType() == typeof(King))
                            {
                                if (opponents[1].GetType() == typeof(Bishop))
                                {
                                    opponentColour = aChessBoard[opponents[1].CurrentXPos, opponents[1].CurrentYPos]; //get the colour the bishop is on
                                }

                            }
                            else if (opponents[0].GetType() == typeof(Bishop))
                            {
                                if (opponents[1].GetType() == typeof(King))
                                {
                                    opponentColour = aChessBoard[opponents[0].CurrentXPos, opponents[0].CurrentYPos]; //get the colour the bishop is on
                                }
                            }
                        }
                    }
                }
            }

            if(friendColour != "" && opponentColour != "")
            {
                if (friendColour == opponentColour)
                {
                    return true;
                }
            }
        }

        //If non of the conditions for stalemate are met then return whether or not the current player has any possible moves.
        return cannotMove;
    }

    /// <summary>
    /// This will Destroy the piece and update the UI to show the pieces left and taken.
    /// </summary>
    /// <param name="piece">The Chess piece that was taken</param>
    public void DestroyPiece(ChessPiece piece)
    {
        activeChessPieces.Remove(piece.gameObject);
        activePieces.Remove(piece);
        if (piece.GetType() == typeof(Pawn))
        {
            if (player1Turn)
            {
                ChessPiece[,] friendlyPieces = theKing.OpponentPieces(!piece.player1);
                int pawnCounter = 0;
                for(int i = 0; i < 8; i++)
                {
                    for( int j = 0; j < 8; j++)
                    {
                        if(friendlyPieces[i,j] != null && friendlyPieces[i,j].GetType() == typeof(Pawn))
                        {
                            pawnCounter++; 
                        }
                    }
                }
                if(pawnCounter == 8)
                {
                    takenbpawn1.enabled = true;
                    bpawn1.enabled = false;
                } 
                else if (pawnCounter == 7)
                {
                    takenbpawn2.enabled = true;
                    bpawn2.enabled = false;
                }
                else if (pawnCounter == 6)
                {
                    takenbpawn3.enabled = true;
                    bpawn3.enabled = false;
                }
                else if (pawnCounter == 5)
                {
                    takenbpawn4.enabled = true;
                    bpawn4.enabled = false;
                }
                else if (pawnCounter == 4)
                {
                    takenbpawn5.enabled = true;
                    bpawn5.enabled = false;
                }
                else if (pawnCounter == 3)
                {
                    takenbpawn6.enabled = true;
                    bpawn6.enabled = false;
                }
                else if (pawnCounter == 2)
                {
                    takenbpawn7.enabled = true;
                    bpawn7.enabled = false;
                }
                else if (pawnCounter == 1)
                {
                    takenbpawn8.enabled = true;
                    bpawn8.enabled = false;
                }
            }
            else
            {
                ChessPiece[,] friendlyPieces = theKing.OpponentPieces(!piece.player1);
                int pawnCounter = 0;
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (friendlyPieces[i, j] != null && friendlyPieces[i, j].GetType() == typeof(Pawn))
                        {
                            pawnCounter++;
                        }
                    }
                }
                if (pawnCounter == 8)
                {
                    takenwpawn1.enabled = true;
                    wpawn1.enabled = false;
                }
                else if (pawnCounter == 7)
                {
                    takenwpawn2.enabled = true;
                    wpawn2.enabled = false;
                }
                else if (pawnCounter == 6)
                {
                    takenwpawn3.enabled = true;
                    wpawn3.enabled = false;
                }
                else if (pawnCounter == 5)
                {
                    takenwpawn4.enabled = true;
                    wpawn4.enabled = false;
                }
                else if (pawnCounter == 4)
                {
                    takenwpawn5.enabled = true;
                    wpawn5.enabled = false;
                }
                else if (pawnCounter == 3)
                {
                    takenwpawn6.enabled = true;
                    wpawn6.enabled = false;
                }
                else if (pawnCounter == 2)
                {
                    takenwpawn7.enabled = true;
                    wpawn7.enabled = false;
                }
                else if (pawnCounter == 1)
                {
                    takenwpawn8.enabled = true;
                    wpawn8.enabled = false;
                }
            }
        }
        if (piece.GetType() == typeof(Rook))
        {
            if (player1Turn)
            {
                ChessPiece[,] friendlyPieces = theKing.OpponentPieces(!piece.player1);
                int rookCounter = 0;
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (friendlyPieces[i, j] != null && friendlyPieces[i, j].GetType() == typeof(Rook))
                        {
                            rookCounter++;
                        }
                    }
                }
                if (rookCounter == 2)
                {
                    takenbrook1.enabled = true;
                    brook1.enabled = false;
                }
                else if (rookCounter == 1)
                {
                    takenbrook2.enabled = true;
                    brook2.enabled = false;
                }
            }
            else
            {
                ChessPiece[,] friendlyPieces = theKing.OpponentPieces(!piece.player1);
                int rookCounter = 0;
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (friendlyPieces[i, j] != null && friendlyPieces[i, j].GetType() == typeof(Rook))
                        {
                            rookCounter++;
                        }
                    }
                }
                if (rookCounter == 2)
                {
                    takenwrook1.enabled = true;
                    wrook1.enabled = false;
                }
                else if (rookCounter == 1)
                {
                    takenwrook2.enabled = true;
                    wrook2.enabled = false;
                }
            }
        }
        if (piece.GetType() == typeof(Knight))
        {
            if (player1Turn)
            {
                ChessPiece[,] friendlyPieces = theKing.OpponentPieces(!piece.player1);
                int knightCounter = 0;
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (friendlyPieces[i, j] != null && friendlyPieces[i, j].GetType() == typeof(Knight))
                        {
                            knightCounter++;
                        }
                    }
                }
                if (knightCounter == 2)
                {
                    takenbknight1.enabled = true;
                    bknight1.enabled = false;
                }
                else if (knightCounter == 1)
                {
                    takenbknight2.enabled = true;
                    bknight2.enabled = false;
                }
            }
            else
            {
                ChessPiece[,] friendlyPieces = theKing.OpponentPieces(!piece.player1);
                int knightCounter = 0;
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (friendlyPieces[i, j] != null && friendlyPieces[i, j].GetType() == typeof(Knight))
                        {
                            knightCounter++;
                        }
                    }
                }
                if (knightCounter == 2)
                {
                    takenwknight1.enabled = true;
                    wknight1.enabled = false;
                }
                else if (knightCounter == 1)
                {
                    takenwknight2.enabled = true;
                    wknight2.enabled = false;
                }
            }
        }
        if (piece.GetType() == typeof(Bishop))
        {
            if (player1Turn)
            {
                ChessPiece[,] friendlyPieces = theKing.OpponentPieces(!piece.player1);
                int bishopCounter = 0;
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (friendlyPieces[i, j] != null && friendlyPieces[i, j].GetType() == typeof(Bishop))
                        {
                            bishopCounter++;
                        }
                    }
                }
                if (bishopCounter == 2)
                {
                    takenbbishop1.enabled = true;
                    bbishop1.enabled = false;
                }
                else if (bishopCounter == 1)
                {
                    takenbbishop2.enabled = true;
                    bbishop2.enabled = false;
                }
            }
            else
            {
                ChessPiece[,] friendlyPieces = theKing.OpponentPieces(!piece.player1);
                int bishopCounter = 0;
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (friendlyPieces[i, j] != null && friendlyPieces[i, j].GetType() == typeof(Bishop))
                        {
                            bishopCounter++;
                        }
                    }
                }
                if (bishopCounter == 2)
                {
                    takenwbishop1.enabled = true;
                    wbishop1.enabled = false;
                }
                else if (bishopCounter == 1)
                {
                    takenwbishop2.enabled = true;
                    wbishop2.enabled = false;
                }
            }
        }
        if (piece.GetType() == typeof(Queen))
        {
            if (player1Turn)
            {
                takenbqueen.enabled = true;
                bqueen.enabled = false;
            }
            else
            {
                takenwqueen.enabled = true;
                wqueen.enabled = false;
            }
        }

        Destroy(piece.gameObject);
    }

    /// <summary>
    /// This will move the selected piece
    /// </summary>
    /// <param name="x">the column of the desired move</param>
    /// <param name="y">the row of the desired move</param>
    private void MovePiece(int x, int y)
    {
        
        if (AllowedMoves[x, y] == true)
        {

            ChessPiece aChessPiece = ChessPieces[x, y];

            if (aChessPiece != null && aChessPiece.player1 != player1Turn)
            {

                //Piece Captured!!!
                DestroyPiece(aChessPiece);

                if (aChessPiece.GetType() == typeof(King))
                {
                    return;
                }
            }

            //If the selectedPiece is a king then check if it is castling
            if (selectedPiece.GetType() == typeof(King))
            {
                if (x == selectedPiece.CurrentXPos + 2)
                {
                    //Short castling
                    ChessPieces[selectedPiece.CurrentXPos, selectedPiece.CurrentYPos] = null;
                    selectedPiece.transform.position = GetTileCenter(x, y);
                    selectedPiece.SetPos(x, y);
                    ChessPieces[x, y] = selectedPiece;
                    selectedPiece = ChessPieces[x + 1, y];
                    MovePiece(x - 1, y);
                    player1Turn = !player1Turn;
                    if (AI != null)
                    {
                        aiTurn = !aiTurn;
                    }
                }

                else if (x == selectedPiece.CurrentXPos - 2)
                {
                    //Long castling
                    ChessPieces[selectedPiece.CurrentXPos, selectedPiece.CurrentYPos] = null;
                    selectedPiece.transform.position = GetTileCenter(x, y);
                    selectedPiece.SetPos(x, y);
                    ChessPieces[x, y] = selectedPiece;
                    selectedPiece = ChessPieces[x - 2, y];
                    MovePiece(x + 1, y);
                    player1Turn = !player1Turn; 
                    if (AI != null)
                    {
                        aiTurn = !aiTurn;
                    }
                }
                
            }

            if(selectedPiece != null)
            {
                ChessPieces[selectedPiece.CurrentXPos, selectedPiece.CurrentYPos] = null;
                selectedPiece.transform.position = GetTileCenter(x, y);
                selectedPiece.SetPos(x, y);
                ChessPieces[x, y] = selectedPiece;

                //If the piece is a rook or a king then change the hasMoved property
                if (selectedPiece.GetType() == typeof(Rook))
                {
                    Rook piece = (Rook)selectedPiece;
                    piece.hasMoved = true;

                    ChessPieces[x, y] = piece;
                }

                if (selectedPiece.GetType() == typeof(King))
                {
                    King piece = (King)selectedPiece;
                    piece.hasMoved = true;

                    ChessPieces[x, y] = piece;
                }

                if (selectedPiece.GetType() == typeof(Pawn))
                {
                    if (player1Turn)
                    {
                        if (selectedPiece.CurrentYPos == 7)
                        {
                            pieceToPromote = selectedPiece;
                            if (aiTurn)
                            {
                                Promotion(1);
                            }
                            else
                            {
                                promotionScreen.SetActive(true);
                            }
                        }
                    }
                    else
                    {
                        if (selectedPiece.CurrentYPos == 0)
                        {
                            pieceToPromote = selectedPiece; 
                            if (aiTurn)
                            {
                                Promotion(7);
                            }
                            else
                            {
                                promotionScreen.SetActive(true);
                            }
                        }
                    }
                }
            }


            player1Turn = !player1Turn;


            //Get the current players king and check if they are in check.
            theKing = GetKing();
            theKing.InCheck = IsInCheck();

            //Display that the king is in check
            if (theKing.InCheck)
            {

                bool checkmate = IsCheckmate();

                if (checkmate)
                {
                    EndGame(!theKing.player1);
                }
                
            }
            bool stalemate = IsStalemate();
            if (stalemate)
            {
                EndGame(!theKing.player1);
            }
            if(AI != null)
            {
                aiTurn = !aiTurn;
            }
        }

        TileHighlighter.Instance.HideHighights();
        selectedPiece = null;
    }

    /// <summary>
    /// This checks if the player is selecting a piece, if so then that piece gets selected
    /// </summary>
    /// <param name="x">the column of the chessboard</param>
    /// <param name="y">the row of the chessboard</param>
    private void SelectPiece(int x, int y )
    {
        if (ChessPieces[x,y] == null)
        {
            return;
        }
        if (ChessPieces[x, y].player1 != player1Turn)
        {
            return;
        }


        AllowedMoves = ChessPieces[x, y].CanMove();
        selectedPiece = ChessPieces[x, y];

        //If the selected piece is a king then make sure it cannot move into check
        if (selectedPiece.GetType() == typeof(King))
        {
            ChessPieces[selectedPiece.CurrentXPos, selectedPiece.CurrentYPos] = null;
            King king = (King)selectedPiece;
            ChessPiece[,] opponents = king.OpponentPieces(player1Turn);
            bool[,] wouldBeCheck = new bool[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    wouldBeCheck[i, j] = king.WouldBeCheck(opponents, i, j);
                    if (wouldBeCheck[i, j])
                    {
                        AllowedMoves[i, j] = false;
                    }
                }
            }

            //Check the King can castle
            if (!king.hasMoved)
            {
                AllowedMoves[x + 2, y] = king.Castling(0);
                AllowedMoves[x - 2, y] = king.Castling(1);
            }
        }

        //If the piece is blocking a check make sure it cannot move unless it would take the offender
        if (selectedPiece.GetType() != typeof(King))
        {
            theKing = GetKing();
            ChessPiece[,] theOpponents = theKing.OpponentPieces(player1Turn);
            ChessPieces[selectedPiece.CurrentXPos, selectedPiece.CurrentYPos] = null;
            if (theKing.WouldBeCheck(theOpponents, theKing.CurrentXPos, theKing.CurrentYPos))
            {
                ChessPiece offender = BoardScript.Instance.OffendingPiece();
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (offender.CurrentXPos != i || offender.CurrentYPos != j)
                        {
                            AllowedMoves[i, j] = false;
                        }
                    }
                }
            }
            ChessPieces[selectedPiece.CurrentXPos, selectedPiece.CurrentYPos] = selectedPiece;
        }

        //If the current king is in check, make sure the only allowed moves are able to get them out of check
        if (theKing.InCheck)
        {
            AllowedMoves = CanGetOutOfCheck(selectedPiece);
            bool checkmate = true;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (AllowedMoves[i, j])
                    {
                        checkmate = false;
                    }
                }
            }
            if (checkmate)
            {
                checkmate = IsCheckmate();
                if (checkmate)
                {
                    EndGame(!theKing.player1);
                }
                
            }
        }

        if (selectedPiece.GetType() == typeof(King))
        {
            ChessPieces[selectedPiece.CurrentXPos, selectedPiece.CurrentYPos] = selectedPiece;
        }

        

        TileHighlighter.Instance.HighlightPossible(AllowedMoves);
    }


    /// <summary>
    /// This will destroy the pawn and spawn a piece of the players choosing
    /// in the same position.
    /// </summary>
    /// <param name="index">The index of the piece the player chooses to spawn</param>
    public void Promotion(int index)
    {
        int x = pieceToPromote.CurrentXPos;
        int y = pieceToPromote.CurrentYPos;

        activeChessPieces.Remove(pieceToPromote.gameObject);
        activePieces.Remove(pieceToPromote);
        Destroy(pieceToPromote.gameObject);

        SpawnChessPiece(index, x, y);
    }

    /// <summary>
    /// This gets the input from the pawn promotion popup and calls a promotion 
    /// function to promote the pawn to another piece.
    /// </summary>
    /// <param name="sender">The dropdown from the pawn promotion popup</param>
    public void PromotePawn(TMPro.TMP_Dropdown sender)
    {
        string piecePromote = sender.options[sender.value].text;
        sender.SetValueWithoutNotify(0);


        if (piecePromote == "Rook")
        {
            if (player1Turn)
            {
                Promotion(8);
            }
            else
            {
                Promotion(2);
            }
        }
        else if (piecePromote == "Knight")
        {
            if (player1Turn)
            {
                Promotion(10);
            }
            else
            {
                Promotion(4);
            }
        }
        else if (piecePromote == "Bishop")
        {
            if (player1Turn)
            {
                Promotion(9);
            }
            else
            {
                Promotion(3);
            }
        }
        else if (piecePromote == "Queen")
        {
            if (player1Turn)
            {
                Promotion(7);
            }
            else
            {
                Promotion(1);
            }
        }
        else
        {
            return;
        }
        theKing.InCheck = IsInCheck();
        if (theKing.InCheck)
        {
            bool checkmate = IsCheckmate();
            if (checkmate)
            {
                EndGame(!theKing.player1);
            }
        }

        promotionScreen.SetActive(false);
    }

    /// <summary>
    /// This function will spawn the desired chess piece where it should go
    /// </summary>
    /// <param name="index">An int referring to the index of the chessPiecePrefabs list that corresponds to the desired chess piece</param>
    /// <param name="x">The column on the chess board that the piece should go</param>
    /// <param name="y">The row on the chess board that the piece should go</param>
    private void SpawnChessPiece( int index, int x, int y)
    {
        Vector3 position = GetTileCenter(x, y);
        GameObject gameObject = Instantiate(chessPiecePrefabs[index], position, Quaternion.identity) as GameObject;
        gameObject.transform.SetParent(transform);

        //Knight rotations are different than the rest thus, check if the piece is a knight
        if(index != 4 && index != 10)
        {
            gameObject.transform.SetPositionAndRotation(new Vector3(position.x, position.y + 0.25f, position.z), Quaternion.identity);
            gameObject.transform.Rotate(-90.0f, 90.0f, 0.0f);
        }
        else if (index == 4)
        {
            gameObject.transform.Rotate(0.0f, -90.0f, 90.0f);
        }
        else
        {
            gameObject.transform.Rotate(0.0f, 90.0f, 90.0f);
        }

        ChessPieces[x, y] = gameObject.GetComponent<ChessPiece>();
        ChessPieces[x, y].SetPos(x, y);
        activeChessPieces.Add(gameObject);
        activePieces.Add(gameObject.GetComponent<ChessPiece>());
        if(index < 6)
        {
            ChessPieces[x, y].colour = "White";
        }
        else
        {
            ChessPieces[x, y].colour = "Black";
        }
    }

    /// <summary>
    /// This function gets called once at the start of each game, it spawns all pieces in their starting positions
    /// </summary>
    private void SpawnAllPieces()
    {
        activeChessPieces = new List<GameObject>();
        activePieces = new List<ChessPiece>();
        ChessPieces = new ChessPiece[8, 8];


        //Spawn Kings
        SpawnChessPiece(0, 4, 0);
        SpawnChessPiece(6, 4, 7);

        //Spawn Queens
        SpawnChessPiece(1, 3, 0);
        SpawnChessPiece(7, 3, 7);

        //Spawn Bishops
        SpawnChessPiece(3, 2, 0);
        SpawnChessPiece(3, 5, 0);
        SpawnChessPiece(9, 5, 7);
        SpawnChessPiece(9, 2, 7);

        //Spawn Knights
        SpawnChessPiece(4, 1, 0);
        SpawnChessPiece(4, 6, 0);
        SpawnChessPiece(10, 6, 7);
        SpawnChessPiece(10, 1, 7);

        //Spawn Rooks
        SpawnChessPiece(2, 0, 0);
        SpawnChessPiece(2, 7, 0);
        SpawnChessPiece(8, 7, 7);
        SpawnChessPiece(8, 0, 7);

        //Spawn Pawns
        //White Pawns
        for (int i = 0; i < 8; i++)
        {
            SpawnChessPiece(5, i, 1);
        }

        //Black Pawns
        for (int i = 0; i < 8; i++)
        {
            SpawnChessPiece(11, i, 6);
        }

        theKing = GetKing();
    }

    /// <summary>
    /// This function gets the Vector3 position for the desired tile of the chess board
    /// </summary>
    /// <param name="x">The desired coloumn of the chessboard</param>
    /// <param name="y">The desired row of the chessboard</param>
    /// <returns></returns>
    private Vector3 GetTileCenter(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.z += (tileSize * y) + tileOffset; //y refers to the row on the chessboard, however y in unity is height thus origin.z
        origin.x += (tileSize * x) + tileOffset;
        return origin;
    }

    /// <summary>
    /// This function gets the position of the mouse on the chessboard
    /// </summary>
    private void UpdateSelected()
    {
        if (!Camera.main)
        {
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 20.0f, LayerMask.GetMask("ChessPlane")))
        {
            selectedTileX = (int)hit.point.x;
            selectedTileY = (int)hit.point.z;
        }
        else
        {
            selectedTileX = -1;
            selectedTileY = -1;
        }

    }
}
