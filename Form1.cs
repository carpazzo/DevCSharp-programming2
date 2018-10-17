using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace DartGame2._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MessageBox.Show("Dart game 2.0 \r\n Winner is whoever score 301 points first\r\n Players will enter their score manualy\r\n Computer players will add score automaticaly \r\n Good Luck, have Fun!!");
            gameonBox.Text = "Enter the number of players.\r\n Type  player name and Click Add player or Bots \r\n *Press Play*\r\n";
        }

        private const int MaximumScore = 301;
        private List<Player> _players = new List<Player>();
        private Random ComputerTrown = new Random();
        private bool isGameFinished = false;
        
        public class Player
        {
            public string PlayerName { get; set; }
            public PlayerType PlayerType { get; set; }
            public int PlayerScore { get; set; }
            public List<int> PlayerTurns = new List<int>();

            //This is the method that calculate the score that is in trurscore stored 
            public void CalculateScore()
            {
                this.PlayerScore = 0;
                foreach (var playerTurn in PlayerTurns)
                {
                    this.PlayerScore += playerTurn;
                }
            }
        }
     
        public enum PlayerType
        {
            Human = 1,
            Computer = 2
        }

        private void addPlayer_Click(object sender, EventArgs e)
        {

            //check if the number of players has been entered
            // show input box for each player
            //create human players
            try
            {
                int playerNumbers = Convert.ToInt32(numOfPlayers.Text);
                for (var i = 1; i <= playerNumbers; i++)
                {
                    Player newPlayer = new Player();
                    do
                    {
                        newPlayer.PlayerName = Prompt.ShowDialog("Enter player name", $"Player {i} Name");
                        if (String.IsNullOrWhiteSpace(newPlayer.PlayerName))
                        {
                            MessageBox.Show(@"Player name cannot be empty");
                        }
                    } while (String.IsNullOrWhiteSpace(newPlayer.PlayerName));
                    newPlayer.PlayerType = PlayerType.Human;
                    _players.Add(newPlayer);
                    gameonBox.Text += $@"Player[{i}]: {newPlayer.PlayerName}"+"\r\n";
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        
        }

        private void addComputer_Click(object sender, EventArgs e)
        {

            if (_players.Count == 0)
            {
                MessageBox.Show(@"The game cannot strat without players");
                return;
            }
            // create Computer Players
            try
            {
               
                for (var i = 1; i < Convert.ToInt32(numOfPlayers.Text); i++)
                {
                    Player newBot = new Player();
                    newBot.PlayerType = PlayerType.Computer;
                    newBot.PlayerName = $"Computer[{i}]";
                    _players.Add(newBot);
                    gameonBox.Text += $@"{newBot.PlayerName}";
                }
       
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
           
        }

        private void startGame_Click(object sender, EventArgs e)
        {
            // Check required fields
            if (_players.Count == 0)
            {
                MessageBox.Show(@"The game cannot strat without players");
                return;
            }
         
            gameonBox.Text = @"Game Started!";
            while (!isGameFinished)
            {
                foreach (var player in _players)
                {
                    //display whos playing turn is
                    playerNameDisplay.Text = player.PlayerName;
                    // 3 turns for each player
                    if (player.PlayerType == PlayerType.Human)
                    {
                        for (var i = 1; i <= 3 && !isGameFinished; i++)
                        {
                            //print turn to player see 
                            turnLabel.Text = $@"TURN {i}";

                            try
                            {
                                //ask for score for that turn, 3 times...
                                int turnScore = Convert.ToInt32(Prompt.ShowDialog("Enter Score", "Score"));
                                // check if the value is more than 20 or negative
                                if (turnScore > 20 || turnScore < 0)
                                {
                                    gameonBox.Text = "Don't cheat!\r\n 20 is the maximun score \r\n 0 is the minimun";
                                }
                                else
                                {
                                    //add value to the turnscore list  and calculate 
                                    player.PlayerTurns.Add(turnScore);
                                    player.CalculateScore();
                                    turnScore.ToString(gameonBox.Text);
                                    if (player.PlayerScore >= MaximumScore)
                                    {
                                        DisplayWinner(player);
                                    }
                                }
                            }
                            catch (Exception exception)
                            {
                                Console.WriteLine(exception);
                                throw; 
                            }


                        }
                    }
                    else if (player.PlayerType == PlayerType.Computer)
                    {

                        for (var i = 0; i < 3 && !isGameFinished; i++)
                        {
                            gameonBox.Text = ($"{player.PlayerName} Turn {i} Score: ");
                            int turnScore = ComputerTrown.Next(0, 21);
                            gameonBox.Text = Convert.ToString(turnScore);
                            player.PlayerTurns.Add(turnScore);
                            player.CalculateScore();
                            turnScore.ToString(gameonBox.Text);
                            if (player.PlayerScore >= MaximumScore)
                            {
                                DisplayWinner(player);

                            }
                        }
                    }
                }
            }
            
        }


        private void DisplayWinner(Player player)
        {
            // Show Winning Details
            isGameFinished = true;
            MessageBox.Show("We have a Winner!.");
            gameonBox.Text = ($"The Winner is {player.PlayerName},\r\n Final Score: {player.PlayerScore}\r\n");
            for (var j = 0; j < player.PlayerTurns.Count; j++)
            {
                gameonBox.Text += ($"Turn {j + 1} Score: {player.PlayerTurns[j]} \r\n");
            }
        }

        private void saveToFile_Click(object sender, EventArgs e)
        {
            //this is new how to save information in files out of the program
            using (StreamWriter streamWriter = new StreamWriter("savegame.txt", true))
            {
                streamWriter.WriteLine(DateTime.Today);
                streamWriter.WriteLine(gameonBox.Text);
                streamWriter.Close();
            }
            MessageBox.Show("This Game have been saved.\r\n You Can exit Now");
            saveToFile.Text = "Exit";
            if (saveToFile.Text == "Exit")
            {
                this.Close();
            }
        }
    }


    /* I had to use an outside source to get this prompt.
     * Original post on Stackoverflow
     * https://stackoverflow.com/questions/5427020/prompt-dialog-in-windows-forms
    */

    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 300,
                Height = 200,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterParent
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 100 };
            Button confirmation = new Button() { Text = "Ok", Left = 50, Width = 50, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }
}


    


