using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rescues
{
    public class ChessController: IPuzzleController
    {
        #region Fields

        private List<string> Sequence;
        private bool firstActive=true;
        #endregion
        
        #region IPuzzleController
        public void Initialize(Puzzle puzzle)
        {
            Sequence = new List<string>();
            puzzle.Activated += Activate;
            puzzle.Closed += Close;
            puzzle.Finished += Finish;
            puzzle.CheckCompleted += CheckComplete;
            puzzle.ResetValuesToDefault += ResetValues;
            puzzle.ForceClose();
        }

        public void Activate(Puzzle puzzle)
        {            
            var puzzlePosition = Camera.main.transform.position;
            puzzlePosition.z = 0;
            puzzle.transform.position = puzzlePosition;
            puzzle.gameObject.SetActive(true);
        }

        public void Close(Puzzle puzzle)
        {
            
        }

        public void CheckComplete(Puzzle puzzle)
        {
            var specificPuzzle = puzzle as ChessPuzzle;
            if (firstActive)
            {
                Sequence.AddRange(specificPuzzle.ChessBoard._chessPuzzleData.Sequence.Split(' '));
                firstActive=false;
            }
            var playersSequence = specificPuzzle._playersSequence;//.
            if (specificPuzzle != null
                && CheckSequence(playersSequence))
                puzzle.Finish();
            if (playersSequence.Count > Sequence.Count)
                ResetValues(specificPuzzle);
        }

        private bool CheckSequence(List<string> playersSequence)
        {
            var isOk = true;
            for (int j = 0; j < Sequence.Count; j++)
            {
                Debug.Log("isOk: "+isOk);
                if (Sequence[j]!=playersSequence[j])
                {
                    isOk = false;
                    break;
                }
            }
            return isOk;
        }
        
        public void Finish(Puzzle puzzle)
        {
        }

        public void ResetValues(Puzzle puzzle)
        {
            var specificPuzzle = puzzle as ChessPuzzle;
            if (specificPuzzle != null)
            {
                specificPuzzle.CleanData();
                specificPuzzle.Initiate();
                specificPuzzle.ChessBoard.SetNullableBoard();
                var board = specificPuzzle.ChessBoard.GetBoard();
                for (int i = 0; i <= 7; i++)
                {
                    for (int j = 0; j <= 7; j++)
                    {
                        board[i, j].SetCellOccupied(false);
                    }
                }
                specificPuzzle.ChessBoard.SetPuzzledFigures();
            }
        }
        
        #endregion
    }
}