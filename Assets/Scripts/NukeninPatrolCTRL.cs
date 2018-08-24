using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukeninPatrolCTRL : MonoBehaviour {
	
	public Transform Pos1, Pos2;
	public float speed = 2f;
	public float waitTime = 3f;
	Vector3 nextPos;

	void Start () {
		nextPos = Pos1.position;
		StartCoroutine(Move());	
	}

	IEnumerator Move() {
		while (true) {
			if (transform.position == Pos1.position) {
				nextPos = Pos2.position;
				yield return new WaitForSeconds(waitTime);
			}
			if (transform.position == Pos2.position) {
				nextPos = Pos1.position;
				yield return new WaitForSeconds(waitTime);
			}
			transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
			yield return null; 
		}
	}

	void OnDrawGizmos() {
		Gizmos.DrawLine(Pos1.position, Pos2.position);
	}
}
 
