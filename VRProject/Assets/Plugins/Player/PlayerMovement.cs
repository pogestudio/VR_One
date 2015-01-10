using UnityEngine;
using System.Collections;
//[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
	
		private Vector3 startVector;
		private Quaternion startRotationVector;
	
		public float restartHeight;
	
		public float regularSpeed;
		public float maxForce;
		public float maxSpeedChange;
		private float speedUserShouldHave;
		public float boostSpeed;
		public float boostTime;
		public float rotateSpeed = 3.0F;
		public float thresholdAccelerometerValue;
		
		private float endTimeForBoost;
		
		private Vector3 lastContactPoint;
		private Vector3 secondLastContactPoint;
		private Vector3 thirdLastContactPoint;
		
		private Vector3 lastFWTrajectory;
		
		
		private Transform PLAYERCAMTRANSFORM;
				
		private GUIText DEBUGGUITEXT;
		private GUIText FORCEINDICATOR;
		void Start ()
		{
				startVector = transform.position;
				startRotationVector = transform.rotation;
				DEBUGGUITEXT = GameObject.Find ("AccelerometerVector").GetComponent ("GUIText") as GUIText;
				FORCEINDICATOR = GameObject.Find ("ForceIndicator").GetComponent ("GUIText") as GUIText;
				PLAYERCAMTRANSFORM = GameObject.Find ("LeftCamera").transform;
				speedUserShouldHave = 0;
				endTimeForBoost = 0;
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
				
				bool shouldRestart = checkIfWeShouldRestartPlayer ();
				if (!shouldRestart) {
						CharacterController controller = GetComponent<CharacterController> ();
						Vector3 forward = getForwardDirectionWithSlope ();
						//if (controller.isGrounded)
						//Debug.Log ("STANDARD FW:" + transform.TransformDirection (Vector3.forward) + "\nNEW: " + forward);
						float speed = speedPlayerShouldHave ();
						bool forwardIsZero = forward == Vector3.zero;
						if (!controller.isGrounded || forwardIsZero) {
								Vector3 newSpeed = lastFWTrajectory;
								newSpeed.y -= 50.0f * Time.deltaTime;
								lastFWTrajectory = newSpeed;
								Debug.Log ("using last velocity: " + lastFWTrajectory);
								controller.Move (newSpeed * Time.deltaTime);
						} else {
								lastFWTrajectory = controller.velocity;
								lastFWTrajectory.Normalize ();
								lastFWTrajectory = lastFWTrajectory * speed;
								Debug.Log ("setting last velocity to: " + lastFWTrajectory);
								controller.SimpleMove (forward * speed);
						}
						
//						lastFWTrajectory = forward;
			
				}
		
		
				
				//getForwardDirectionWithSlope ();
//				controller.SimpleMove (newVelocity);
		
				//Debug.Log ("current speed:" + curSpeed);
				
		
				
				//Debug.Log ("current velocity: " + controller.velocity);
		
				//				if (nextDebugPrint < Time.time) {
				//						Vector3 dir = Input.acceleration;
				//						Debug.Log ("accelerometer: " + dir);		
				//						nextDebugPrint = Time.time + 1;
				//				}	
				//			
				
				
				/*
				
				complex move!
				CharacterController controller = GetComponent<CharacterController> ();
		
				Vector3 forward = transform.TransformDirection (Vector3.forward);
				
				Vector3 moveDirection;
				if (controller.isGrounded) {
						float speed = speedPlayerShouldHave ();
						moveDirection = forward;
						//moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
						//moveDirection = forward;
						//moveDirection = transform.TransformDirection (moveDirection);
						moveDirection *= speed;
						//if (Input.GetButton ("Jump"))
						//		moveDirection.y = jumpSpeed;
			
				} else {
						moveDirection = controller.velocity;
						float gravity = 9.81f;
						moveDirection.y -= gravity * Time.deltaTime; //gravity
				}
				controller.Move (moveDirection * Time.deltaTime);
				*/
		
		
		}
		
		private Vector3 getForwardDirectionWithSlope ()
		{
				RaycastHit hit;
				Ray downRay = new Ray (transform.position, -Vector3.up);
				Vector3 forwardVector = Vector3.zero;
				CharacterController controller = GetComponent<CharacterController> ();
		
				if (Physics.Raycast (downRay, out hit) && controller.isGrounded) {
						//			Debug.Log ("we has hit! point: " + hit.point);
						
						forwardVector = transform.TransformDirection (Vector3.forward); //default forward;
						Vector3 diffVector = getDiffVector (hit.point);
						Debug.DrawRay (transform.position, diffVector, Color.green);
			
						//diffVector.Normalize ();
						
						if (Mathf.Abs (diffVector.y) > 0.01) {
								Debug.Log ("diff in Y. Normal Forward: " + forwardVector + " \n new foward: " + (new Vector3 (forwardVector.x, diffVector.y, forwardVector.z)).normalized);
						}
					
						forwardVector.y = diffVector.y;
						forwardVector.Normalize ();
			
//						if (diffVector.y < -0.02) {
//								Debug.Log ("downward slope!!!!!!!!");
//						} else {
//								Debug.Log ("setting y: " + diffVector.y + " to a total of : " + forwardVector);
//						}
						lastContactPoint = hit.point;
				}
		
				return forwardVector;
		}
		
		private Vector3 getDiffVector (Vector3 newContactPoint)
		{
				//avg the last three diffVectors
				//Vector3 earliestDiff = secondLastContactPoint - thirdLastContactPoint;
				Vector3 secondEarliest = lastContactPoint - secondLastContactPoint;
				Vector3 latestDiff = newContactPoint - lastContactPoint;
				
				thirdLastContactPoint = secondLastContactPoint;
				secondLastContactPoint = lastContactPoint;
				lastContactPoint = newContactPoint;
		
				Vector3 averageVector = latestDiff + secondEarliest;//earliestDiff 
				averageVector.Normalize ();
				Debug.Log ("average vector:" + averageVector);
				return averageVector;
		
		}
		
		private bool checkIfWeShouldRestartPlayer ()
		{
				float currentHeight = transform.position.y;
				bool shouldRestart = currentHeight < restartHeight;
				if (shouldRestart) {
						putCharacterAtBeginning ();
						CharacterController controller = GetComponent<CharacterController> ();
						lastFWTrajectory = Vector3.zero;
						controller.SimpleMove (Vector3.zero);
				}
				
				return shouldRestart;
		
		}
		
		private float speedPlayerShouldHave ()
		{
				CharacterController controller = GetComponent<CharacterController> ();
				float currentPlayerSpeed = controller.velocity.magnitude;
				float speed = regularSpeed;
				//Debug.Log ("regularSpeed is:" + regularSpeed);
				if (Time.time < endTimeForBoost) {
						//Debug.Log ("We want to boooost with boostspeed" + boostSpeed);
						speed = boostSpeed;
				} else if (currentPlayerSpeed > regularSpeed + 1) {
						//if we have stopped boosting but is still traveling fast. slow down a little bit at a time
						speed = currentPlayerSpeed * 0.95f;
						//Debug.Log ("We want to slow down with speed" + speed);
				}
				//Debug.Log ("speed pSH is: " + speed);
				return speed;
		}
		
		public void boostPlayer ()
		{
				endTimeForBoost = Time.time + boostTime;
		}
		
		public void putCharacterAtBeginning ()
		{
				transform.position = startVector;
				transform.rotation = startRotationVector;
		}
		
		void OnCollisionStay (Collision collision)
		{
				foreach (ContactPoint contact in collision.contacts) {
						print (contact.thisCollider.name + " hit " + contact.otherCollider.name);
						Debug.DrawRay (contact.point, contact.normal, Color.white);
				}
		}
}
