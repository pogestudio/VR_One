using UnityEngine;
using System.Collections;
//[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
	
		private Vector3 startVector;
	
		public float restartHeight;
	
		public float maxSpeed;
		public float maxForce;
		public float maxSpeedChange;
		private float speedUserShouldHave;
		public float rotateSpeed = 3.0F;
		public float thresholdAccelerometerValue;
		
		private Transform PLAYERCAMTRANSFORM;
				
		private GUIText DEBUGGUITEXT;
		private GUIText FORCEINDICATOR;
		void Start ()
		{
				startVector = transform.position;
				DEBUGGUITEXT = GameObject.Find ("AccelerometerVector").GetComponent ("GUIText") as GUIText;
				FORCEINDICATOR = GameObject.Find ("ForceIndicator").GetComponent ("GUIText") as GUIText;
				PLAYERCAMTRANSFORM = GameObject.Find ("LeftCamera").transform;
				speedUserShouldHave = 0;
		}
//		void Update ()
//		{
//				float percentage = currentAccelerationPercentage ();
//				bool shouldAddForce = Mathf.Abs (percentage) > thresholdAccelerometerValue;
//				Vector3 forceToAdd = Vector3.zero;
//				float currentVelocity = gameObject.rigidbody.velocity.magnitude;
//				if (shouldAddForce) {
//						forceToAdd = forceVectorFromAccelerationPercentage (percentage);	
//				}
//				//gameObject.rigidbody.AddForce (forceToAdd);
//				
//				//gameObject.rigidbody.velocity = gameObject.rigidbody.velocity + speedToAdd;
//				
//				FORCEINDICATOR.text = "AF: " + forceToAdd + " CS: " + gameObject.rigidbody.velocity.magnitude.ToString ("F1");
//				
////				Debug.Log ("ShouldAdd: " + shouldAddSpeed);
////				Debug.Log ("dir.z : " + dir.z);
////				Debug.Log ("threshold: " + thresholdAccelerometerValue);
////				Debug.Log ("forwardV: " + forwardVector);
////				Debug.Log ("percentage: " + percentage);
////				Debug.Log ("speed: " + gameObject.rigidbody.velocity);
////				Debug.Log ("pos: " + transform.position);
////		
//				
//				//			
//				checkIfWeShouldRestartPlayer ();
//	
//		}
//		
		private float currentAccelerationPercentage ()
		{
				//get current input acceleration
				Vector3 dir = Input.acceleration;
				DEBUGGUITEXT.text = "X: " + dir.x.ToString ("F1") + " Y:" + dir.y.ToString ("F1") + " Z:" + dir.z.ToString ("F1");
				FORCEINDICATOR.color = dir.z > 0 ? Color.green : Color.red;
				float howMuchDoThePlayerWantToAdd = Mathf.Abs (dir.y + 1) + Mathf.Abs (dir.z);
				float percentage = howMuchDoThePlayerWantToAdd / 2.0f;
				if (dir.z < 0) {
						percentage *= -1;
				}
				return percentage;
		}
		
//		private Vector3 forceVectorFromAccelerationPercentage (float percentage)
//		{
////				Transform cam = Camera.main.transform;
////				Vector3 straightInWorldCoordinates = cam.TransformDirection (transform.forward);
//				Vector3 cameraDirection = PLAYERCAMTRANSFORM.forward;
//				
//				
//				
//				//clear out force up or down
//				cameraDirection.y = 0;
//				cameraDirection.Normalize ();
//				Vector3 cameraDirectionFromPlayer = transform.position + cameraDirection;
//				transform.LookAt (cameraDirectionFromPlayer);
//				
//				Debug.Log ("Got camera:" + cameraDirection);
//				Vector3 forceAddingVector = cameraDirection * percentage * maxForce;
//				return forceAddingVector;
//				//take whatever forward direction we have
//				//normalize it
//				//remove the z and 
//				
//				//rigidbody.AddForce (cameraRelativeStraight * 10);
//				
//		}

		void calculateSpeedUserShouldHave ()
		{
//		
//				float percentageToChange = currentAccelerationPercentage ();
//				float currentSpeed = controller.velocity.magnitude;
//		
//				bool lessThanThreshold = Mathf.Abs (percentageToChange) < thresholdAccelerometerValue;
//				bool fastAndWantToMoveForward = currentSpeed > maxSpeed && percentageToChange > 0;
//				bool slowAndWantToMoveBackwards = currentSpeed <= 0 && percentageToChange < 0;
//				Vector3 newVelocity;
//				if (fastAndWantToMoveForward || lessThanThreshold) {
//						newVelocity = forward * currentSpeed;
//						FORCEINDICATOR.text = "Doing nothing, speed: " + currentSpeed.ToString ("F1");
//						FORCEINDICATOR.color = Color.black;
//				} else if (slowAndWantToMoveBackwards) {
//						newVelocity = forward * 0;
//						FORCEINDICATOR.text = "Standstill";
//						FORCEINDICATOR.color = Color.black;
//				} else {
//						//percentageToChange *= 2.0f;
//						float diff = maxSpeedChange * percentageToChange;
//						float newSpeed = currentSpeed + diff;
//						newVelocity = forward * newSpeed;
//						Debug.Log ("CurrS: " + currentSpeed);
//						Debug.Log ("Diff: " + diff + "perc: " + percentageToChange);
//						Debug.Log ("newS: " + newSpeed);
//						DEBUGGUITEXT.text = "D: " + diff.ToString ("F1") + " newS: " + newSpeed.ToString ("F1") + " newV: " + newVelocity.ToString ("F1");
//						if (percentageToChange > 0) {
//								FORCEINDICATOR.color = Color.green;
//								FORCEINDICATOR.text = "%-" + percentageToChange.ToString ("F1") + " Speed UP - " + newSpeed.ToString ("F1") + "m/s";
//						} else {
//								FORCEINDICATOR.color = Color.red;
//								FORCEINDICATOR.text = "%-" + percentageToChange.ToString ("F1") + " Speed DOWN - " + newSpeed.ToString ("F1") + "m/s";
//						}
//				}
//				Debug.Log ("Velocity: " + newVelocity);
//				Debug.Log ("FW * MS: " + forward * maxSpeed);
		}
	
		void Update ()
		{
				
//				FORCEINDICATOR.color = Color.magenta;
//				FORCEINDICATOR.text = "%-" + percentageToChange.ToString ("F1");
//				
				CharacterController controller = GetComponent<CharacterController> ();
		
				Vector3 forward = transform.TransformDirection (Vector3.forward);
		
//				controller.SimpleMove (newVelocity);
		
				//Debug.Log ("current speed:" + curSpeed);
				controller.SimpleMove (forward * maxSpeed);
				
				//Debug.Log ("current velocity: " + controller.velocity);
		
				//				if (nextDebugPrint < Time.time) {
				//						Vector3 dir = Input.acceleration;
				//						Debug.Log ("accelerometer: " + dir);		
				//						nextDebugPrint = Time.time + 1;
				//				}	
				//			
				checkIfWeShouldRestartPlayer ();
		
		
		}
		
		private void checkIfWeShouldRestartPlayer ()
		{
				float currentHeight = transform.position.y;
				if (currentHeight < restartHeight) {
						putCharacterAtBeginning ();
						CharacterController controller = GetComponent<CharacterController> ();
						controller.SimpleMove (Vector3.zero);
			
				}
		
		}
		public void putCharacterAtBeginning ()
		{
				transform.position = startVector;
		}
}
