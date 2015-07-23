using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enumeration of Board Columns
/// (A = 0, B = 1, C = 2, D = 3, E = 4, F = 5, G = 6, H = 7)
/// </summary>
/// 
public enum Column {A,B,C,D,E,F,G,H};

/// <summary>
/// Enumeration of relative space locations
/// (FrontLeft = 0, Front = 1, FrontRight = 2, Left = 3, Right = 4, BackLeft = 5, Back = 6, BackRight =7)
/// </summary>
public enum SpaceDirection {FrontLeft, Front, FrontRight, Left, Right, BackLeft, Back, BackRight};

public class Board {

	
	public BoardSpace[,] spaces = new BoardSpace[8,8];

	public Board(BoardSpace[] spaceCollection){
		foreach (BoardSpace space in spaceCollection) {
			int[] indexArray = getIndex(space);
			spaces[indexArray[0],indexArray[1]] = space;
		}
	}
	
    /// <summary>
	/// Gets a BoardSpace game object by name.
	/// </summary>
	/// <returns>The space as BoardSpace.</returns>
	/// <param name="spaceName">Space name.</param>
	public BoardSpace getSpace(string spaceName){
		int spaceColumn = (int) Enum.Parse( typeof(Column), spaceName[0].ToString());
		int spaceRow = int.Parse(spaceName[1].ToString());

		return spaces [spaceColumn, spaceRow];
	}

	public int[] getIndex(BoardSpace space){
		return new int[]
        {
			(int) Enum.Parse (typeof(Column), space.spaceColumn.ToString ()),
			(int) (int.Parse (space.spaceRow.ToString ()) - 1)
		};
	}

    private BoardSpace verifySpace(int[] indexArray){
            if ((indexArray[0] < 0) || (indexArray[1] < 0) || (indexArray[0] > 7) || (indexArray[1] > 7)) {
                return null;
            }
            else
            {
                return spaces[indexArray[0], indexArray[1]];
            }
        }

    ///// <summary>
    ///// Returns true if the adjacent space in the specified direction of the current space for the specified team color.
    ///// </summary>
    ///// <param name="currentSpace"></param>
    ///// <param name="direction"></param>
    ///// <param name="PieceColor"></param>
    ///// <returns></returns>
    //public bool isSpaceAvailable(BoardSpace spaceToCheck, TeamColor pieceColor)
    //{
    //    int[] indexArray = getIndex(spaceToCheck);
    //    if (spaceToCheck.OccupyingPiece != null)
    //    {
    //        if (spaceToCheck.OccupyingPiece.PieceColor == pieceColor)
    //        {
    //            spaceToCheck.spaceState = SpaceState.Blocked;
    //            return false;
    //        }
    //        else
    //        {
    //            spaceToCheck.spaceState = SpaceState.Contested;
    //            return true;
    //        }
    //    }
    //    else
    //    {
    //        spaceToCheck.spaceState = SpaceState.Open;
    //    }
    //    return true;
    //}

    /// <summary>
    /// Returns the space directly next to currentSpace in the specified direction. Retunrns null if the space does not exist.
    /// If validate is true, the function will return the board space only if the space is a valid move for currentSpace's occupying piece and will return null otherwise.
    /// </summary>
    /// <param name="currentSpace"></param>
    /// <param name="direction"></param>
    /// <param name="pieceColor"></param>
    /// <param name="Validate"></param>
    /// <returns></returns>
    public BoardSpace getAdjacentSpace(BoardSpace currentSpace, SpaceDirection direction, TeamColor pieceColor, bool Validate)
    {
        if (currentSpace != null)
        {
            int[] indexArray = getIndex(currentSpace);
            ChessPiece activePiece = GameManager.currentInstance.activePiece;

            //Determine newSpaceRow and newSpaceColumn by checking arguments
            switch (direction)
            {				
                case (SpaceDirection.FrontLeft):
                    if (pieceColor == TeamColor.Black)
                    {
                        indexArray[0] += 1; //Column
                        indexArray[1] -= 1; //Row
                    }
                    else if (pieceColor == TeamColor.White)
                    {
                        indexArray[0] -= 1; //Column
                        indexArray[1] += 1; //Row
                    }
                    break;

                case (SpaceDirection.Front):
                    if (pieceColor == TeamColor.Black)
                    {
                        indexArray[1] -= 1; //Row
                    }
                    else if (pieceColor == TeamColor.White)
                    {
                        indexArray[1] += 1; //Row
                    }
                    break;

                case (SpaceDirection.FrontRight):
                    if (pieceColor == TeamColor.Black)
                    {
                        indexArray[0] -= 1; //Column
                        indexArray[1] -= 1; //Row
                    }
                    else if (pieceColor == TeamColor.White)
                    {
                        indexArray[0] += 1; //Column
                        indexArray[1] += 1; //Row
                    }
                    break;

                case (SpaceDirection.Left):
                    if (pieceColor == TeamColor.Black)
                    {
                        indexArray[0] += 1; //Column
                    }
                    else if (pieceColor == TeamColor.White)
                    {
                        indexArray[0] -= 1; //Column
                    }
                    break;

                case (SpaceDirection.Right):
                    if (pieceColor == TeamColor.Black)
                    {
                        indexArray[0] -= 1; //Column
                    }
                    else if (pieceColor == TeamColor.White)
                    {
                        indexArray[0] += 1; //Column
                    }
                    break;

                case (SpaceDirection.BackLeft):
                    if (pieceColor == TeamColor.Black)
                    {
                        indexArray[0] += 1; //Column
                        indexArray[1] += 1; //Row
                    }
                    else if (pieceColor == TeamColor.White)
                    {
                        indexArray[0] -= 1; //Column
                        indexArray[1] -= 1; //Row
                    }
                    break;

                case (SpaceDirection.Back):
                    if (pieceColor == TeamColor.Black)
                    {
                        indexArray[1] += 1; //Row
                    }
                    else if (pieceColor == TeamColor.White)
                    {
                        indexArray[1] -= 1; //Row
                    }
                    break;

                case (SpaceDirection.BackRight):
                    if (pieceColor == TeamColor.Black)
                    {
                        indexArray[0] -= 1; //Column
                        indexArray[1] += 1; //Row
                    }
                    else if (pieceColor == TeamColor.White)
                    {
                        indexArray[0] += 1; //Column
                        indexArray[1] -= 1; //Row
                    }
                    break;

                default:
                    break;
            }
            if ((indexArray[0] < 0) || (indexArray[1] < 0) || (indexArray[0] > 7) || (indexArray[1] > 7)) //space is outside of the board
            {
                return null;
            }
            BoardSpace newSpace = spaces[indexArray[0], indexArray[1]];
            if (Validate)
            {
                    return checkSpace(newSpace);
                
            }
            return newSpace;
                
        }
        return null;
    }

    /// <summary>
    /// Sets the spaceState for the passed in BoardSpace to reflect whether the space is open, blocked or contested.
    /// Returns the passed in BoardSpace if it is a valid move location for the activePiece. Returns null otherwise.
    /// </summary>
    /// <returns></returns>
    public BoardSpace checkSpace(BoardSpace spaceToCheck)
    {
        ChessPiece activePiece = GameManager.currentInstance.activePiece;
        TeamColor PieceColor = activePiece.PieceColor;
        if ((activePiece != null) && (spaceToCheck != null))  //ensure there is and activePiece and a valid spaceToCheck
        {
            if (spaceToCheck.OccupyingPiece != null)    //is the space occupied
            {
                //Debug.Log(spaceToCheck.OccupyingPiece);
                //Debug.Log(spaceToCheck.OccupyingPiece.PieceColor == PieceColor);
                if (spaceToCheck.OccupyingPiece.PieceColor == PieceColor) //is the OccupyingPiece the same color as the activePiece
                {
                    spaceToCheck.spaceState = SpaceState.Blocked;   //space is blocked
                    //Debug.Log(spaceToCheck.spaceState);
                    return null;                                    //can't move here; return null
                }
                else                                                       //is the OccupyingPiece a different color than the activePiece
                {
                    if (spaceToCheck.OccupyingPiece.GetType() == typeof(King))  //piece an enemy king?
                    {
                        spaceToCheck.spaceState = SpaceState.Blocked;       //space is blocked
                        return null;                                      //can't move here; return null
                    }
                    else 
                    {
                        spaceToCheck.spaceState = SpaceState.Contested;   //space is contested
                        return spaceToCheck;                              //can capture here; return the space
                    }
                }
            }
            else
            {
                spaceToCheck.spaceState = SpaceState.Open;
                return spaceToCheck;
            }
        }
        else
        {
            return null;
        }
    }
            //if (activePiece.GetType() == typeof (Knight)){
            //    if ((activePiece as Knight).IsLastSpace)
            //    {

                //    }
            //}


    /// <summary>
    /// Returns true if the passed in space is checked by a piece with a color other than the one passed in.
    /// </summary>
    /// <param name="space"></param>
    /// <returns></returns>
    public bool isSpaceChecked(BoardSpace space, TeamColor teamColor)
    {
        // Logical OR of possible checked conditions
        bool Checked = ((checkedFromDirection(space, SpaceDirection.FrontLeft, teamColor) != null) || (checkedFromDirection(space, SpaceDirection.Front, teamColor) != null) || (checkedFromDirection(space, SpaceDirection.FrontRight, teamColor) != null)
            || (checkedFromDirection(space, SpaceDirection.Left, teamColor) != null) || (checkedFromDirection(space, SpaceDirection.Right, teamColor) != null) || (checkedFromDirection(space, SpaceDirection.BackLeft, teamColor) != null)
            || (checkedFromDirection(space, SpaceDirection.Back, teamColor) != null) || (checkedFromDirection(space, SpaceDirection.BackRight, teamColor) != null) || checkedByKnights(space, teamColor) != null);
        Debug.Log(space);
        Debug.Log(checkedFromDirection(space, SpaceDirection.FrontLeft, teamColor));
        Debug.Log(checkedFromDirection(space, SpaceDirection.Front, teamColor));
        Debug.Log(checkedFromDirection(space, SpaceDirection.FrontRight, teamColor));
        Debug.Log(checkedFromDirection(space, SpaceDirection.Left, teamColor));
        Debug.Log(checkedFromDirection(space, SpaceDirection.Right, teamColor));
        Debug.Log(checkedFromDirection(space, SpaceDirection.BackLeft, teamColor));
        Debug.Log(checkedFromDirection(space, SpaceDirection.Back, teamColor));
        Debug.Log(checkedFromDirection(space, SpaceDirection.BackRight, teamColor));
        Debug.Log(checkedFromDirection(space, SpaceDirection.Front, teamColor));
        Debug.Log(checkedByKnights(space, teamColor) != null);

       

        return Checked;
    }

    /// <summary>
    /// Function that returns all pieces currently checking the potential space of a king of the specified teamColor. (Returns null if there are none.)  
    /// </summary>
    /// <param name="space"></param>
    /// <param name="teamColor"></param>
    /// <returns></returns>
    public ChessPiece[] getCheckingPieces (BoardSpace space, TeamColor teamColor) 
    {
        List<ChessPiece> checkingPieces = new List<ChessPiece>();
        checkingPieces.Add(checkedFromDirection(space, SpaceDirection.FrontLeft, teamColor));
        checkingPieces.Add(checkedFromDirection(space, SpaceDirection.Front, teamColor));
        checkingPieces.Add(checkedFromDirection(space, SpaceDirection.FrontRight, teamColor));
        checkingPieces.Add(checkedFromDirection(space, SpaceDirection.Left, teamColor));
        checkingPieces.Add(checkedFromDirection(space, SpaceDirection.Right, teamColor));
        checkingPieces.Add(checkedFromDirection(space, SpaceDirection.BackLeft, teamColor));
        checkingPieces.Add(checkedFromDirection(space, SpaceDirection.Back, teamColor));
        checkingPieces.Add(checkedFromDirection(space, SpaceDirection.BackRight, teamColor));
        foreach (Knight checkingKnight in checkedByKnights(space, teamColor))
        {
            checkingPieces.Add(checkingKnight);
        }
        return checkingPieces.ToArray();
    }

    /// <summary>
    /// Returns all knights checking the space (relative to the teamColor); null if there are none
    /// </summary>
    /// <param name="space"></param>
    /// <param name="teamColor"></param>
    /// <returns></returns>
    private Knight[] checkedByKnights(BoardSpace space, TeamColor teamColor) 
    {
        List<Knight> checkingKnights = new List<Knight>();
        BoardSpace tempSpace, sideSpace;
        //CHECK FRONT LEFT / FRONT RIGHT
        tempSpace = GameManager.currentInstance.Board.getAdjacentSpace(space, SpaceDirection.Front, teamColor, false);
        tempSpace = GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Front, teamColor, false);
        sideSpace = GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Left, teamColor, false);
        if ((sideSpace != null) && (sideSpace.OccupyingPiece != null))
        { 
            if ((sideSpace.OccupyingPiece.PieceColor != teamColor) && (sideSpace.OccupyingPiece.GetType() == typeof(Knight)))
            {
                //Debug.Log("Space Checked by Knight");
                checkingKnights.Add((Knight) sideSpace.OccupyingPiece);
            }
            sideSpace = GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Right, teamColor, false);
            if ((sideSpace != null) && (sideSpace.OccupyingPiece != null) && (sideSpace.OccupyingPiece.PieceColor != teamColor) 
                && (sideSpace.OccupyingPiece.GetType() == typeof(Knight)))
            {
                //Debug.Log("Space Checked by Knight");
                checkingKnights.Add((Knight)sideSpace.OccupyingPiece);
            }
        }

        //CHECK LEFT FRONT / LEFT BACK
        tempSpace = GameManager.currentInstance.Board.getAdjacentSpace(space, SpaceDirection.Left, teamColor, false);
        tempSpace = GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Left, teamColor, false);
        sideSpace = GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Front, teamColor, false);
        if ((sideSpace != null) && (sideSpace.OccupyingPiece != null))  
        {
            if ((sideSpace.OccupyingPiece.PieceColor != teamColor) && (sideSpace.OccupyingPiece.GetType() == typeof(Knight)))
            {
                //Debug.Log("Space Checked by Knight");
                checkingKnights.Add((Knight)sideSpace.OccupyingPiece);
            }
            sideSpace = GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Back, teamColor, false);
            if ((sideSpace != null) && (sideSpace.OccupyingPiece != null) && (sideSpace.OccupyingPiece.PieceColor != teamColor)
                && (sideSpace.OccupyingPiece.GetType() == typeof(Knight)))
            {
                //Debug.Log("Space Checked by Knight");
                checkingKnights.Add((Knight)sideSpace.OccupyingPiece);
            }
        }

        //CHECK RIGHT FRONT / RIGHT BACK
        tempSpace = GameManager.currentInstance.Board.getAdjacentSpace(space, SpaceDirection.Right, teamColor, false);
        tempSpace = GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Right, teamColor, false);
        sideSpace = GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Front, teamColor, false);
        if ((sideSpace != null) && (sideSpace.OccupyingPiece != null))
        {
            if ((sideSpace.OccupyingPiece.PieceColor != teamColor) && (sideSpace.OccupyingPiece.GetType() == typeof(Knight)))
            {
                //Debug.Log("Space Checked by Knight");
                checkingKnights.Add((Knight)sideSpace.OccupyingPiece);
            }
            sideSpace = GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Back, teamColor, false);
            if ((sideSpace != null) && (sideSpace.OccupyingPiece != null) && (sideSpace.OccupyingPiece.PieceColor != teamColor)
                && (sideSpace.OccupyingPiece.GetType() == typeof(Knight)))
            {
                //Debug.Log("Space Checked by Knight");
                checkingKnights.Add((Knight)sideSpace.OccupyingPiece);
            }
        }

        //CHECK BACK LEFT / BACK RIGHT
        tempSpace = GameManager.currentInstance.Board.getAdjacentSpace(space, SpaceDirection.Back, teamColor, false);
        tempSpace = GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Back, teamColor, false);
        sideSpace = GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Left, teamColor, false);
        if ((sideSpace != null) && (sideSpace.OccupyingPiece != null))
        {
            if ((sideSpace.OccupyingPiece.PieceColor != teamColor) && (sideSpace.OccupyingPiece.GetType() == typeof(Knight)))
            {
                //Debug.Log("Space Checked by Knight");
                checkingKnights.Add((Knight)sideSpace.OccupyingPiece);
            }
            sideSpace = GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Right, teamColor, false);
            if ((sideSpace != null) && (sideSpace.OccupyingPiece != null) && (sideSpace.OccupyingPiece.PieceColor != teamColor)
                && (sideSpace.OccupyingPiece.GetType() == typeof(Knight)))
            {
                //Debug.Log("Space Checked by Knight");
                checkingKnights.Add((Knight)sideSpace.OccupyingPiece);
            }
        }
        if (checkingKnights != null)
        {
            return checkingKnights.ToArray();
        }
        return null;
    }

    /// <summary>
    /// Takes a space and direction and returns the nearest checking piece from the specified direction (returns null if there is no checking piece.)
    /// </summary>
    /// <param name="space"></param>
    /// <param name="direction"></param>
    private ChessPiece checkedFromDirection(BoardSpace space, SpaceDirection direction, TeamColor teamColor)    
    {
        BoardSpace tempSpace;
        if ((direction == SpaceDirection.Front) || (direction == SpaceDirection.Back) || (direction == SpaceDirection.Left) || (direction == SpaceDirection.Right))
        {
            tempSpace = GameManager.currentInstance.Board.getAdjacentSpace(space, direction, teamColor, false);
            while ((tempSpace != null) && ((tempSpace.OccupyingPiece == null) || ((tempSpace.OccupyingPiece.PieceColor == teamColor) && (tempSpace.OccupyingPiece.GetType() == typeof(King)))))
            {
                tempSpace = GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, direction, teamColor, false); //search for piece on forward column
            }

            if (tempSpace != null)  //piece found; not the end of Board
            {
                if ((tempSpace.OccupyingPiece != null) && (tempSpace.OccupyingPiece.PieceColor != teamColor))  //piece is an enemy piece
                {
                    switch ((tempSpace.OccupyingPiece.GetType().ToString()))  //is the piece one that can capture along the column?
                    {
                        case ("Rook"):
                            return tempSpace.OccupyingPiece;
                        case ("Queen"):
                            return tempSpace.OccupyingPiece;
                        case ("King"):
                            if (tempSpace == GameManager.currentInstance.Board.getAdjacentSpace(space, direction, teamColor, false))
                            {
                                return tempSpace.OccupyingPiece;
                            }
                            break;
                        default:
                            Debug.Log("Returning null...");
                            return null;
                    }
                }
            }
        }
        else if ((direction == SpaceDirection.FrontLeft) || (direction == SpaceDirection.FrontRight) || (direction == SpaceDirection.BackLeft) || (direction == SpaceDirection.BackRight)) 
        {
            tempSpace = GameManager.currentInstance.Board.getAdjacentSpace(space, direction, teamColor, false);
            while ((tempSpace != null) &&(tempSpace.OccupyingPiece == null))
            {
                tempSpace = GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, direction, teamColor, false); //search for piece on forward column
            }
            if (tempSpace != null)  //piece found; not the end of Board
            {
                if ((tempSpace.OccupyingPiece != null) && (tempSpace.OccupyingPiece.PieceColor != teamColor))  //piece is an enemy piece
                {

                    switch ((tempSpace.OccupyingPiece.GetType().ToString()))  //is the piece one that can capture along the diagonal?
                    {
                        case ("Bishop"):
                            return tempSpace.OccupyingPiece;
                        case ("Queen"):
                            return tempSpace.OccupyingPiece;
                        case ("King"):
                            if (tempSpace == GameManager.currentInstance.Board.getAdjacentSpace(space, direction, teamColor, false))
                            {
                                return tempSpace.OccupyingPiece;
                            }
                            break;
                        case ("Pawn"):
                            if (tempSpace == GameManager.currentInstance.Board.getAdjacentSpace(space, direction, teamColor, false))
                            {
                                return tempSpace.OccupyingPiece;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        return null;
    
    }
}