
using UnityEngine;
using System.Collections;

public class EditorController : MonoBehaviour {

	public Level currentEditedLevel;
	public string levelName;
	public GameObject selectedTile;

	// Use this for initialization
	void Start () {
		currentEditedLevel.generateBlankMap(32,32);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)){
		RaycastHit2D hit=Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero);
			if (hit.collider!=null)
			{
			}
		}

	}

	void OnGUI()
	{

	}
}
