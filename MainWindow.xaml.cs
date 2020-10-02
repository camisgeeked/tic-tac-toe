using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe1
{
    public partial class MainWindow : Window
    {

        #region Private Members
        //Holds the current results of cells in the active game.
        private MarkType[] mResults;
        
        /// <summary>
        /// True if it is player 1's turn (X) or player 2's turn (O)
        /// </summary>
        private bool mPlayer1Turn;

        /// <summary>
        /// True if the game has ended.
        /// </summary>
        private bool mGameEnded;

        #endregion


        #region Constructor

        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }
        #endregion

        /// <summary>
        /// starts a new game and clears all values back to start of game.
        /// </summary>
        private void NewGame()
        {
            // create new blank array of free cells
            mResults = new MarkType[9];

            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;

            // make sure Player1 is the current player.
            mPlayer1Turn = true;

            // clear off previous game
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            // make sure the game hasn't ended
            mGameEnded = false;


        }

        /// <summary>
        /// Handles a button click event
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The events of the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            // Start a new game on click after it's finished
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            var button = (Button)sender;

            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            // don't do anything if the cell already has a value in it
            if (mResults[index] != MarkType.Free)
                return;

            // set the cell value based on which players turn it is
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            button.Content = mPlayer1Turn ? "X" : "O";

            // change noughts to green
            if (!mPlayer1Turn)
                button.Foreground = Brushes.Red;

            // toggle player turns
            mPlayer1Turn ^= true;

            // checks for winner
            CheckForWiner();

        }
        /// <summary>
        /// Checks if there is a winner of a 3 line straight
        /// </summary>
        private void CheckForWiner()
        {
            #region Horizontal Wins

            // row 1 (0)
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                // game ends
                mGameEnded = true;

                // highlight winner cells
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }

            // row 2 (1)
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                // game ends
                mGameEnded = true;

                // highlight winner cells
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }

            // row 3 (2)
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                // game ends
                mGameEnded = true;

                // highlight winner cells
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Vertical Wins
            // column 1 (0)
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                // game ends
                mGameEnded = true;

                // highlight winner cells
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }

            // column 2 (1)
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                // game ends
                mGameEnded = true;

                // highlight winner cells
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }

            // column 3 (2)
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                // game ends
                mGameEnded = true;

                // highlight winner cells
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Diagonal Wins
            // check for diagonal wins
            // diagonal 1 (top left -> bottom right)
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                // game ends
                mGameEnded = true;

                // highlight winner cells
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }

            // diagonal 2 (top right -> bottom left)
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                // game ends
                mGameEnded = true;

                // highlight winner cells
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }
            #endregion

            #region No Winner
            // checks for no winner
            if (!mResults.Any(result => result == MarkType.Free))
                {
                // game ended
                mGameEnded = true;

                // turns all cells orange
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });
            }
            #endregion
        }
    }
}
