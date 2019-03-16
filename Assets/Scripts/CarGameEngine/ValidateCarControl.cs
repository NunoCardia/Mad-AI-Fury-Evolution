using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class ValidateCarControl : MonoBehaviour {

	// instances
	public static ValidateCarControl instance = null;
	public Text infoText;
	public bool simulating = false;
	public string savePath;
	public GameObject simulationPrefab;
	private GameObject bestSimulation;
	private NeuralNetwork bestController;


	void Awake(){
		// deal with the singleton part
		if (instance == null) {
			instance = this;
		}
		else if (instance != this) {
			Destroy (gameObject);    
		}
		DontDestroyOnLoad(gameObject);
		loadBest ();
		simulating = false;

	}

	void loadBest() {
		if(File.Exists(savePath))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(savePath, FileMode.Open);
			this.bestController = (NeuralNetwork) bf.Deserialize(file);
			file.Close();
		}
	}

	private SimulationInfo createSimulation(int sim_i, Rect location)
	{
		GameObject sim = Instantiate (simulationPrefab, transform.position + new Vector3 (0, 0, (sim_i * 1000)), transform.rotation);
		Controller player_script = sim.GetComponentInChildren<Controller> ();
		player_script.GetComponentInChildren<Camera> ().rect = location;


		// handle destroy walls
		DestroyTrigger[] triggers = sim.transform.Find ("DeathWalls").gameObject.GetComponentsInChildren<DestroyTrigger> ();
		foreach (DestroyTrigger t in triggers) {
			t.car = player_script;
		}
		return new SimulationInfo (sim, sim.GetComponentInChildren<Controller> (),0);
	}

	void Update () {
		infoText.text = "Best Individual Found";
		// show best.. in loop
		if (!simulating) {
			SimulationInfo info = createSimulation (0, new Rect (0.0f, 0.0f, 1f, 1f));
			info.playerc.neuralController = bestController;
			info.playerc.running = true;
			bestSimulation = info.sim;
			Time.timeScale = 6;
			simulating = true;

		} else if (simulating) {

			if (!bestSimulation.GetComponentInChildren<Controller> ().running && bestSimulation.GetComponentInChildren<Controller> ().gameOver) {
				simulating = false;
				Destroy (bestSimulation);
			}
		}
	}
	}




