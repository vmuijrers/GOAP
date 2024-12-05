using UnityEngine;
using System.Collections;
using UnityEditor;

[System.Flags]
public enum Status
{
		Slowed = (1 << 0),
		Poison = (1 << 1),
		Happy  = (1 << 2)

}
public class Flags : MonoBehaviour
{
		[BitMask(typeof(Status))]
		public Status
				myStatus;

		private int[][] myArray = new int[2] [];
		// Use this for initialization
		void Start ()
		{
				
				myArray [0] = new int[2];
				myArray [1] = new int[3];
				myArray [0] [0] = 1;
				myArray [0] [1] = 2;
				myArray [1] [0] = 3;
				myArray [1] [1] = 4;
				myArray [1] [2] = 5;
				for (int i =0; i < myArray.Length; i++) {
						for (int j =0; j < myArray[i].Length; j++) {
								print ("i,j: " + myArray [i] [j]);
						}	
				}
				myStatus = Status.Poison | Status.Happy;

				myStatus |= Status.Slowed;
				Debug.Log (myStatus.ToString ());
				//System.Console.WriteLine ("0x{0:x8}", myStatus);
		}
	
		// Update is called once per frame
		void Update ()
		{
				//print (myStatus.ToString ());
				if (Input.GetKeyDown (KeyCode.A)) {
						myStatus = AddStatus (myStatus, Status.Happy);

				}
				if (Input.GetKeyDown (KeyCode.S)) {
						myStatus = FlipFlag (myStatus, Status.Poison);

				}
				if (Input.GetKeyDown (KeyCode.D)) {
						myStatus = RemoveStatus (myStatus, Status.Happy);

				}
		}

		Status FlipFlag (Status myStatus, Status s)
		{
				return myStatus ^= s;
		}
		
		Status AddStatus (Status myStatus, Status s)
		{
				return myStatus |= s;
		}
		Status RemoveStatus (Status myStatus, Status s)
		{
				
				return myStatus &= ~s;
		}
}
