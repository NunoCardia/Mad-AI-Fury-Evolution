using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TournamentSelection : SelectionMethod{

	int tournamentSize;

	public TournamentSelection (int nTournament) {
		tournamentSize = nTournament;
	}

	public override List<Individual> selectIndividuals (List<Individual> oldpop, int num) {
		return tournamentSelection (oldpop, num);
	}

	public  List<Individual> tournamentSelection (List<Individual>oldpop, int num) {
		if (tournamentSize > oldpop.Count) { // checks if list lenghts are the same
			tournamentSize = oldpop.Count;
		}

		List<Individual> selectedInds = new List<Individual>(); 

		for (int i = 0; i<num; i++) { 					// tournament
			selectedInds.Add(selectIndividual(oldpop).Clone());
		}

		return selectedInds;

	}


	//função selecionar individuo
	public Individual selectIndividual (List<Individual> oldpop) {
		List<Individual> indList = new List<Individual> ();
		int index = (int)(Random.Range (0, tournamentSize)); 
		for (int i = 0; i < tournamentSize; i++) {    					
			while (indList.Contains (oldpop[index])) {
				index = (int)(Random.Range (0, tournamentSize));
			}
			indList.Add (oldpop [index]);
		}
		Individual best = indList [0];

		float bestFitness = best.Fitness;

		for (int j = 1; j < tournamentSize; j++) {
			if (indList [j].Fitness > bestFitness) {
				best = indList [j];
				bestFitness = indList [j].Fitness;
			}
		}
		return best;
	}
}

