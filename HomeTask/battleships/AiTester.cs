using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NLog;

namespace battleships
{
	public class AiTester
	{
		private readonly Settings settings;
	    private readonly GameVisualizer visualizer;
	    
        public Ai Ai { get; set; }
	    public Game Game { get; set; }

	    public event Action onGameCrashed;
	    public event Action onCreateGame;

		public AiTester(Settings settings, GameVisualizer visualizer)
		{
			this.settings = settings;
		    this.visualizer = visualizer;
		}

		public StatisticsWriter TestSingleFile(string exe)
		{
			var badShots = 0;
			var crashes = 0;
			var gamesPlayed = 0;
			var shots = new List<int>();
			for (var gameIndex = 0; gameIndex < settings.GamesCount; gameIndex++)
			{
                onCreateGame.Invoke();
				RunGameToEnd(Game, visualizer);
				gamesPlayed++;
				badShots += Game.BadShots;
				if (Game.AiCrashed)
				{
					crashes++;
					if (crashes > settings.CrashLimit) break;
                    onGameCrashed.Invoke();
				}
				else
					shots.Add(Game.TurnsCount);
				if (settings.Verbose)
				{
					Console.WriteLine(
						"Game #{3,4}: Turns {0,4}, BadShots {1}{2}",
						Game.TurnsCount, Game.BadShots, Game.AiCrashed ? ", Crashed" : "", gameIndex);
				}
			}
			Ai.Dispose();
			return new StatisticsWriter(settings, Ai, shots, crashes, badShots, gamesPlayed);
		}

		private void RunGameToEnd(Game game, GameVisualizer vis)
		{
			while (!game.IsOver())
			{
				game.MakeStep();
				if (settings.Interactive)
				{
					vis.Visualize(game);
					if (game.AiCrashed)
						Console.WriteLine(game.LastError.Message);
					Console.ReadKey();
				}
			}
		}
	}
}