using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//mutação já não está feita????
//DUAS REPRESENTACOES - UMA COM NUMEROS REAIS E OUTRA COM INTEIROS

public class TestIndividual : Individual {



	public TestIndividual(int[] topology): base(topology)
	{
	}

	public override void Initialize () //  preenche com valores aleatorios
	{
		for (int i = 0; i < totalSize; i++) 
		{
			genotype [i] = Random.Range (-1.0f, 1.0f);
		}

	}
	/* ??????
	public void Init2(){
		for (int i = 0; i < totalSize; i++) 
		{
			genotype [i] = Mathf.FloorToInt(Random.Range (-1.0f, 1.0f));
		}
	}*/

	//Mutation ?
	public override void Mutate (float probability) 
	{
		for (int i = 0; i < totalSize; i++) {
			if (Random.Range (0.0f, 1.0f) < probability) {
				genotype [i] = Random.Range (-1.0f, 1.0f);
			}
		}
	}
	/* ??????
	public void Mutate2 (float probability){
		for (int i = 0; i < totalSize; i++) {
			if (Random.Range (0.0f, 1.0f) < probability) {
				genotype [i] = Mathf.FloorToInt (Random.Range (-1.0f, 1.0f));
			}
		}
	}*/
		



	public override void Crossover (Individual partner, float probability)
	{
		nCrossover (partner,probability);
		//HalfCrossover (partner, probability);
	}

	//nCrossover ?
	void nCrossover(Individual partner, float probability) {

		TestIndividual CrossedPartener = (TestIndividual)partner;

		//Debug.Log (n_cuts + " cuts");

		if (UnityEngine.Random.Range (0f, 1f) > probability) {
			return;
		}
		int crossoverPoint = Mathf.FloorToInt (totalSize / (n_cuts + 1));

		for (int i = crossoverPoint; i < totalSize; i += 2 * crossoverPoint) {
			for (int j = i; j < totalSize && j < i + crossoverPoint; j++) {
				float child = genotype [j];
				genotype [j] = CrossedPartener.genotype [j];
				CrossedPartener.genotype [j] = child;

			}
		}
	}
		
	//Half-Crossover ?
	void HalfCrossover(Individual partner, float probability) {
		
		TestIndividual CrossedPartener = (TestIndividual)partner;

		if (UnityEngine.Random.Range (0f, 1f) > probability) {
			return;
		}

		int crossoverPoint = Mathf.FloorToInt (totalSize / 2f);
		
		for (int i=0; i<crossoverPoint; i++) {
			float temp = genotype[i];
			genotype [i] = CrossedPartener.genotype [i];
			CrossedPartener.genotype [i] = temp;
		}

	}



	public override Individual Clone ()
	{
		TestIndividual new_ind = new TestIndividual(this.topology);
		genotype.CopyTo (new_ind.genotype, 0);
		new_ind.fitness = this.Fitness;
		new_ind.evaluated = false;
		return new_ind;
	}
}


