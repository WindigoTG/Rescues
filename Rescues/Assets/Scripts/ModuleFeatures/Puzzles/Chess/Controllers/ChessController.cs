using UnityEngine;

namespace Rescues
{
    public class ChessController: IPuzzleController
    {
        #region IPuzzleController
        public void Initialize(Puzzle puzzle)
        {
            puzzle.Activated += Activate;
            puzzle.Closed += Close;
            puzzle.Finished += Finish;
            puzzle.CheckCompleted += CheckComplete;
            puzzle.ResetValuesToDefault += ResetValues;
            
            puzzle.ForceClose();
        }

        public void Activate(Puzzle puzzle)
        {
            if (puzzle.IsFinished) return;
            
            var puzzlePosition = Camera.main.transform.position;
            puzzlePosition.z = 0;
            puzzle.transform.position = puzzlePosition;
            puzzle.gameObject.SetActive(true);
        }

        public void Close(Puzzle puzzle)
        {
            puzzle.Close();
        }

        public void CheckComplete(Puzzle puzzle)
        {
            var specificPuzzle = puzzle as ChessPuzzle;
            // if (specificPuzzle != null && specificPuzzle.Connectors.All(s => s.IsCorrectWire))
            //     Finish(specificPuzzle);
        }

        public void Finish(Puzzle puzzle)
        {
            puzzle.IsFinished = true;
            Close(puzzle);
        }

        public void ResetValues(Puzzle puzzle)
        {
            var specificPuzzle = puzzle as ChessPuzzle;
            if (specificPuzzle != null)
            {
                specificPuzzle.ChessBoard.SetNullableBoard();
                specificPuzzle.ChessBoard.SetPuzzledFigures();
            }
        }
        
        #endregion
    }
}