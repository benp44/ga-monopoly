using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MG
{
    public class Simulation
    {
        public List<Player> AllPlayers { get; private set; }
        public int OffspringCount { get; set; }
        public int GamesPlayed { get; private set; }
        public double MutationRate { get; set; }

        public int Population
        {
            get
            {
                return AllPlayers.Count + _populationInGame;
            }
        }

        public int CumulativePopulation
        {
            get
            {
                return _cumulativePopulation;
            }
        }


        private volatile int _cumulativePopulation;
        private volatile int _populationInGame;

        public Simulation(int initialPopulation, double mutationRate)
        {
            OffspringCount = 8;
            GamesPlayed = 0;
            _cumulativePopulation = 0;
            _populationInGame = 0;
            MutationRate = mutationRate;

            AllPlayers = new List<Player>();

            for (int i = 0; i < initialPopulation; i++)
            {
                AllPlayers.SyncAdd(new Player(new GeneticCode()));
                _cumulativePopulation++;
            }
        }

        public void PlayAndBreed()
        {
            var players = new Queue<Player>();

            while (players.Count < 8)
            {
                var player = AllPlayers.SyncRemoveRandom();
                _populationInGame++;
                players.Enqueue(player);
            }

            var game1 = new Game(new List<Player> { players.Dequeue(), players.Dequeue(), players.Dequeue(), players.Dequeue() });
            var game2 = new Game(new List<Player> { players.Dequeue(), players.Dequeue(), players.Dequeue(), players.Dequeue() });

            var winner1 = game1.PlayToEnd();
            var winner2 = game2.PlayToEnd();

            GamesPlayed += 2;

            for (int i = 0; i < OffspringCount; i++)
            {
                var offspring = new Player(winner1.Genetics.Cross(winner2.Genetics, MutationRate));
                _populationInGame--;
                AllPlayers.SyncAdd(offspring);
                _cumulativePopulation++;
            }
        }
    }
}
