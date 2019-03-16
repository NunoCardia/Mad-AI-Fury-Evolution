using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticAlgorithm : MetaHeuristic {
	public float mutationProbability;
	public float crossoverProbability;
	public override void InitPopulation () {
		//You should implement the code to initialize the population here

		population = new List<Individual>();
		while (population.Count < populationSize) {
			GeneticIndividual new_ind = new GeneticIndividual (topology);
			new_ind.Initialize ();
			population.Add (new_ind);
		}
		//throw new System.NotImplementedException ();
	}

	//The Step function assumes that the fitness values of all the individuals in the population have been calculated.
	public override void Step() {
		updateReport ();
		//You should implement the code runs in each generation here
		//throw new System.NotImplementedException ();
		if(generation < numGenerations){

			List<Individual> new_population = selection.selectIndividuals (population,populationSize-elitism); //NOT SURE
			//crossover ?
			for (int i = 0; i < populationSize - elitism; i += 2) {
				Individual parent = new_population [i];
				parent.n_cuts = numberOfCuts;
				parent.Crossover (parent, crossoverProbability);
			}

			//Mutation ?
			for (int i = 0; i < populationSize - elitism; i++) {
				new_population [i].Mutate (mutationProbability);
			}

			//Elitism ?
			population.Sort ((Individual x,Individual y) => y.Fitness.CompareTo (x.Fitness));
			for (int i = 0; i < elitism; i++) {
				//Debug.Log(population[i].Fitness);
				new_population.Add (population [i]);
			}

			population = new_population;
			generation++;
		}
	}

}
