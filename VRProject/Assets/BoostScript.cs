using UnityEngine;
using System.Collections;

public class BoostScript : MonoBehaviour
{

		GameObject playerGO;

		// Use this for initialization
		void Start ()
		{
				playerGO = GameObject.FindGameObjectWithTag ("Player");
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
		
		void OnTriggerEnter (Collider other)
		{
				
				Debug.Log ("We have collision");
				GameObject collidingObject = other.gameObject;
				if (collidingObject == playerGO) {
						PlayerMovement pm = playerGO.GetComponentInChildren<PlayerMovement> ();
						pm.boostPlayer ();
						Debug.Log ("want to boost player");
				}
		}
}
