using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mono.Options;

public class BatchmodeConfig {

	public static bool batchmode = false;

	private static bool processed = false;
	private static readonly object syncLock = new object();

	public static void HandleArgs(EvolvingCarControl game, MetaHeuristic engine){
		
		
		lock (syncLock) 
		{			
			if (!processed) {
				// get the list of arguments 
				string[] args = Environment.GetCommandLineArgs ();

				bool show_help = false;

				OptionSet parser = new OptionSet () {
					"Usage: ",
					"",
					{"batchmode", "run in batchmode",
						v => batchmode = v != null
					},
					{"generations=", "the number of generations to execute.",
						(int v) => engine.numGenerations = v
					},
					{"log=", "the logger output filename to use.",
						v => engine.logFilename = v
					},
					{"seed=", "the seed.",
						(int v) => game.seed = v
					},
					{ "h|help",  "show this message and exit", 
						v => show_help = v != null 
					},
				};

				try{
					parser.Parse(args);
					processed = true;
				}
				catch (OptionException e) {
					Console.Write ("3dcar: ");
					Console.WriteLine (e.Message);
					Console.WriteLine ("Try ` --help' for more information.");
					Application.Quit ();
					return;
				}

				if (show_help) {
					parser.WriteOptionDescriptions (Console.Out);
					Application.Quit();
					return;
				}

			}
		}

	}
}
