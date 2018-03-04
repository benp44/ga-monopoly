using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MG
{
    public partial class GameForm : Form
    {
        private Game _currentGame;

        public GameForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            NewGame(sender, e);
        }

        private void NewGame(object sender, EventArgs e)
        {
            var players = new List<Player>() { new Player(new GeneticCode()), new Player(new GeneticCode()), new Player(new GeneticCode()), new Player(new GeneticCode()) };

            _currentGame = new Game(players);

            RefreshInfo();
        }

        private void PlayPlayerTurn(object sender, EventArgs e)
        {
            _currentGame.PlayPlayerTurn(_currentGame.NextPlayer);
            RefreshInfo();
        }

        private void PlayAllPlayersTurn(object sender, EventArgs e)
        {
            _currentGame.PlayTurn();
            RefreshInfo();
        }

        private void PlayTenTurns(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                _currentGame.PlayTurn();
            }

            RefreshInfo();
        }

        private void PlayUntilEnd(object sender, EventArgs e)
        {
            var gameOver = false;

            while(gameOver == false)
            {
                gameOver = _currentGame.PlayTurn();
            }

            RefreshInfo();
        }

        private void RefreshInfo()
        {
            listViewPlayers.Items.Clear();

            var playerId = 0;
            foreach (var player in _currentGame.Players)
            {
                var item = new ListViewItem(new string[] { (++playerId).ToString(), player.Location.Name, player.Money.ToString(), player.Status.ToString(), GetLastTurnString(player), GetAssetsString(player) });
                item.SubItems[1].BackColor = player.Location.DisplayColor;
                item.UseItemStyleForSubItems = false;

                if (player == _currentGame.NextPlayer)
                {
                    item.SubItems[0].Font = new Font(item.SubItems[0].Font, FontStyle.Bold);
                }

                listViewPlayers.Items.Add(item);
            }

            toolStripStatusLabel.Text = "Player turns played: " + _currentGame.PlayerTurnsPlayed + ", Unsold Properties: " + _currentGame.Bank.Assets.Count() + ". Free Parking: " + _currentGame.FreeParking.Money + ".";
        }

        private string GetLastTurnString(Player player)
        {
            var builder = new StringBuilder();

            player.LastTurnEvents.ForEach(x => builder.Append(x + "; "));

            return builder.ToString();
        }

        private string GetAssetsString(Player player)
        {
            var result = new StringBuilder ();

            foreach (var asset in player.Assets)
            {
                if (asset.Mortgaged)
                {
                    result.Append("*");
                }

                result.Append(asset.Name);

                if (asset is Property)
                {
                    result.Append("[");
                    var prop = (Property)asset;
                    if (prop.BuildingCount == 5)
                    {
                        result.Append("H");
                    }
                    else
                    {
                        for (int i = 0; i < prop.BuildingCount; i++)
                        {
                            result.Append("h");
                        }
                    }

                    result.Append("]");
                }

                result.Append("; ");
            }

            return result.ToString();
        }

        private void play10GamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                NewGame(sender, e);
                PlayUntilEnd(sender, e);
            }
        }
    }
}
