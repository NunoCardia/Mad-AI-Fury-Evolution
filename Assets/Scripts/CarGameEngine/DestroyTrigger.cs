using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrigger : MonoBehaviour {

	public Controller car;

	void OnTriggerEnter(){
		car.tookHit++;
		car.updateRaceStatus ();
		car.wrapUp();
	}

}
