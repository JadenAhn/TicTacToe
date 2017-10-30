/* Board.cs
* Assignment 2
* Revision History
*   Jaden Ahn, 2017.10.02: Created
*   Jaden Ahn, 2017.10.03: Improved image quality
*                          Added comments
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JAhnAssignment2
{
    /// <summary>
    /// A class used for Tic Tac To game
    /// </summary>
    public partial class Board : Form
    {
        /// <summary>
        /// Constructor to initialize component
        /// </summary>
        public Board()
        {
            InitializeComponent();
        }

        int playerIndex;
        int gameCount;
        const int BOARD_SIZE = 9;
        string[] playerMark = { "X", "O" };
        string[] board = new string[BOARD_SIZE];
        string resultMessage = "";


        /// <summary>
        /// This method initializes the form
        /// Creats board array and reset all picture box image to null
        /// </summary>
        void init()
        {
            playerIndex = 0;
            gameCount = 0;
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                board[i] = i.ToString();
            }
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                PictureBox pb = this.Controls.Find("MyImg" + i, false).FirstOrDefault() as PictureBox;
                pb.Image = null;
            }
        }

        /// <summary>
        /// This method checks and returns true when the game is over
        /// There are 2 cases for game over which is a current player wins or ties
        /// </summary>
        /// <param name="playerMark">Mark of the current player. O or X</param>
        /// <returns>True if the game is over</returns>
        bool CheckGameResult(string playerMark)
        {
            //Check all the winning condition
            if (board[0] == playerMark && board[1] == playerMark && board[2] == playerMark ||
                board[3] == playerMark && board[4] == playerMark && board[5] == playerMark ||
                board[6] == playerMark && board[7] == playerMark && board[8] == playerMark ||
                board[0] == playerMark && board[3] == playerMark && board[6] == playerMark ||
                board[1] == playerMark && board[4] == playerMark && board[7] == playerMark ||
                board[2] == playerMark && board[5] == playerMark && board[8] == playerMark ||
                board[0] == playerMark && board[4] == playerMark && board[8] == playerMark ||
                board[2] == playerMark && board[4] == playerMark && board[6] == playerMark
                )
            {
                resultMessage = $"{playerMark} WINS!!";
                return true;
            }
            //Check if the game is a tie
            else if (gameCount == BOARD_SIZE)
            {
                resultMessage = "TIE...";
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Event handler for picture boxes, MyImg0 ~ MyImg8
        /// </summary>
        /// <param name="sender">Picture box object that calls this method</param>
        /// <param name="e"></param>
        private void Img_Click(object sender, EventArgs e)
        {

            PictureBox pb = (PictureBox)sender;

            //Get the cell position from PictureBox name
            string pbName = (sender as PictureBox).Name.Substring(5);
            int cellPosition = int.Parse(pbName);

            //Checks if current clicked position is empty or not
            if (board[cellPosition] != playerMark[0] && board[cellPosition] != playerMark[1])
            {
                gameCount++;
                if (playerIndex == 0)
                {
                    pb.Image = TicTacToe.Properties.Resources.X;
                }
                else
                {
                    pb.Image = TicTacToe.Properties.Resources.O;
                }
                board[cellPosition] = playerMark[playerIndex];

                //If the game is over, win or tie, show messagebox with YesNo  buttons
                if (CheckGameResult(playerMark[playerIndex]))
                {
                    DialogResult dialogResult = MessageBox.Show(resultMessage + " TRY AGAIN?", "GAME OVER", MessageBoxButtons.YesNo);

                    switch (dialogResult)
                    {
                        case DialogResult.Yes:
                            init();
                            break;
                        case DialogResult.No:
                            this.Close();
                            break;
                        default:
                            break;
                    }
                }
                //Players take turns
                else
                {
                    playerIndex = 1 - playerIndex;
                }
            }
        }
        /// <summary>
        /// This method calls init() method when the form loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            init();
        }
    }
}
