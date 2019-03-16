using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use

		// jncor
		[Header("Sensors")]
		public Vector3 frontSensorPosition = new Vector3(0, 1.0f, 2.0f);
		public float sideSensorPosition = .5f;
		public float sensorLength = 10f;
		public float angle = 30f;
		public float[] frontSensorValues;
		public int tookHit = 0;
		public GameObject[] checkpoints;
		public int numberOfCheckpoints;

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
			frontSensorValues = new float[3];
        }

		// jncor
		public void SensorHandling(){
			RaycastHit hit;

			Vector3 sensorStartPos = transform.position;
			sensorStartPos += transform.forward * frontSensorPosition.z;
			sensorStartPos += transform.up * frontSensorPosition.y;

			// frontal
			if (Physics.Raycast (sensorStartPos, transform.forward, out hit, sensorLength)) {
				Debug.DrawLine (sensorStartPos, hit.point);
				//Debug.Log ("[0] Front "+ sensorStartPos + " " + hit.point + " dist: " + (sensorStartPos - hit.point).magnitude);
				frontSensorValues [0] = (sensorStartPos - hit.point).magnitude;
			} else {
				frontSensorValues [0] = 0;
			}


			// direita 
			sensorStartPos += transform.right * sideSensorPosition;
			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis(angle, transform.up) * transform.forward, out hit, sensorLength)) {
				Debug.DrawLine (sensorStartPos, hit.point);
				//Debug.Log ("[1] Left "+ sensorStartPos + " " + hit.point + " dist: " + (sensorStartPos - hit.point).magnitude);
				frontSensorValues [1] = (sensorStartPos - hit.point).magnitude;
			}else {
				frontSensorValues [1] = 0;
			}

			// esquerda
			sensorStartPos -= transform.right * 2 * sideSensorPosition;
			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis(-angle, transform.up) * transform.forward, out hit, sensorLength)) {
				Debug.DrawLine (sensorStartPos, hit.point);
				//Debug.Log ("[2] Right "+ sensorStartPos + " " + hit.point + " dist: " + (sensorStartPos - hit.point).magnitude);
				frontSensorValues [2] = (sensorStartPos - hit.point).magnitude;
			}else {
				frontSensorValues [2] = 0;
			}

			// jncor isto para ficar ainda melhor deviam ser raycasts para isto..
			int layer_mask = 1 << 2;
			float distanceToNextCheckpoint;
			//RaycastHit hit;
			float rayAngle = 30f;
			for (int i = 0; i*angle <= 360f; i++) {
				if (Physics.Raycast (m_Car.transform.position, Quaternion.AngleAxis(-rayAngle * i, transform.up) * transform.forward, out hit, Mathf.Infinity, layer_mask)) {
					Debug.DrawRay (m_Car.transform.position, Quaternion.AngleAxis((-rayAngle * i), transform.up) * transform.forward * hit.distance, Color.blue);
					distanceToNextCheckpoint = (m_Car.transform.position - hit.point).magnitude;
					//Debug.Log ("Raycast angle " + (-i*rayAngle) +" dist"+distanceToNextCheckpoint);
					break;
				}
			}
			//else{
				// to the center
				//distanceToNextCheckpoint = Vector3.Distance(m_Car.transform.position, checkpoints[numberOfCheckpoints % checkpoints.Length].transform.position);
				//Debug.Log("To center " + distanceToNextCheckpoint);
			//}
			// to the center
			//GameObject[] checkpoints = transform.parent.Find("CheckPoints").<GameObject>();
			//distanceToNextCheckpoint = Vector3.Distance(m_Car.transform.position, checkpoints[0].transform.position);
			//Debug.Log("Did Hit " + distanceToNextCheckpoint);
		}


        private void FixedUpdate()
        {
			

            // pass the input to the car!
			// jncor  os valores de h e de v têm de vir da rede
			// h e v variam entre -1 ... 1 
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
			// jncor para mover o carro apenas temos de usar esta função !
            m_Car.Move(h, v, v, handbrake);

			// jncor updating
			SensorHandling ();
        }
    }
}
