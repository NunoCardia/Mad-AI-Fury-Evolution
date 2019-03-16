using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticIndividual : Individual {


	public GeneticIndividual(int[] topology) : base(topology) {
	}

	public override void Initialize () 
	{
		for (int i = 0; i < totalSize; i++) 
		{
			genotype [i] = Random.Range (-1.0f, 1.0f);
		}
	}
		
	public override void Crossover (Individual partner, float probability)
	{
		nCrossover (partner,probability);
	}

	public override void Mutate (float probability)
	{
		for (int i = 0; i < totalSize; i++) {
			if (Random.Range (0.0f, 1.0f) < probability) {
				genotype [i] = Random.Range (-1.0f, 1.0f);
			}
		}
	}

	public override Individual Clone ()
	{
		GeneticIndividual new_ind = new GeneticIndividual(this.topology);

		genotype.CopyTo (new_ind.genotype, 0);
		new_ind.fitness = this.Fitness;
		new_ind.evaluated = false;

		return new_ind;
	}

	void nCrossover(Individual partner, float probability) {

		float temp_genotype;
		GeneticIndividual partner2 = (GeneticIndividual)partner;
		if (UnityEngine.Random.Range (0f, 1f) > probability) {
			return;
		}
		List<int> to_cut = new List<int>();
		for (int i = 0; i < n_cuts; i++) {
			int auxiliar = Random.Range (1, genotype.Length - 1);
			while(to_cut.Contains(auxiliar)){
				auxiliar=Random.Range(1,genotype.Length);
			}
			to_cut.Add(auxiliar);
		}
		to_cut.Sort ();
		for (int i = 0; i < to_cut.Count; i++) {
			int limit = (i == to_cut.Count - 1) ? n_cuts - 1 : to_cut [i + 1];
			for (int j = to_cut [i]; j < limit; j++) {
				temp_genotype = genotype [j];
				genotype [j] = partner2.genotype [j];
				partner2.genotype [j] = temp_genotype;
				i++;
			}
		}
		
		/*if (UnityEngine.Random.Range (0f, 1f) > probability) {
			return;
		}
		List<int> points = new List<int>();
		int aux;
		while(points.Count != n_cuts)
		{
			aux = UnityEngine.Random.Range(0, totalSize);

			if (!points.Contains(aux))
			{
				points.Add(aux);
			}
		}
		points.Sort ();

		for (int j = 0; j < points.Count; j++)
		{
			int limit = (j == points.Count-1) ? n_cuts-1 : points[j+1];
			for (int i = points[j]; i < limit; i++)
			{
				float tmp = genotype[i];
				genotype[i] = CrossedPartener.genotype[i];
				CrossedPartener.genotype[i] =tmp;
				j++;
			}
		}*/
	}

	//Half-Crossover SINGLE POINT!!!
	void HalfCrossover(Individual partner, float probability) {

		GeneticIndividual CrossedPartener = (GeneticIndividual)partner;

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

}
