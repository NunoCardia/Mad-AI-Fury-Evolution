using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour {

	public Controller car;
	public GameObject currCheckpoint;
	public GameObject nextCheckpoint;

	void OnTriggerEnter(){
		if (Vector3.Dot (car.m_Car.transform.forward, currCheckpoint.transform.forward) <= 0) {
			currCheckpoint.SetActive (false);
			car.updateCheckPoints ();
			nextCheckpoint.SetActive (true);
		}
	}
}
