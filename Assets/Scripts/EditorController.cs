using UnityEngine;
using System.Collections;

public class EditorController : MonoBehaviour {

	public Level currentEditedLevel;
	public string levelName;

	// Use this for initialization
	void Start () {
		currentEditedLevel.generateBlankMap(32,32);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{

	}
}
