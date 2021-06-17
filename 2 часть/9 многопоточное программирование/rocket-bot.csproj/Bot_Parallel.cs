using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rocket_bot
{
    public partial class Bot
    {
        public Rocket GetNextMove(Rocket rocket)
        {
            var bestMove = Tuple.Create(Turn.None, 0.0);
            Parallel.For(0, threadsCount, (i) =>
            {
                var currentBestMove = SearchBestMove(rocket, new Random(random.Next()), iterationsCount / threadsCount);
                lock (bestMove)
                {
                    if (currentBestMove.Item2 > bestMove.Item2)
                        bestMove = currentBestMove;
                }
            });
            var newRocket = rocket.Move(bestMove.Item1, level);
            return newRocket;
        }
    }
}