using UnityEngine;
using System.Collections.Generic;

public class MultiDimensionalArray<T>:IEnumerable<T>
{
	public T[] array;

	public IEnumerator<T> GetEnumerator()
	{
		foreach(T t in array)
		{
			if(t==null)
				break;

			yield return t;
		}
	}

	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
	{
		// Lets call the generic version here
		return this.GetEnumerator();
	}
}
[System.Serializable]
public class TileMap:MultiDimensionalArray<int>
{
}

public class Level : MonoBehaviour {

	public List<Sprite> tiles;

	public TileMap[] mapTiles;
	public int pixelsToUnits=32;

	public GameObject tilePrefab;

	public void loadCSV(string filename)
	{
		if (System.IO.File.Exists(filename)){
			string[] levelText=System.IO.File.ReadAllLines(filename);
			mapTiles=new TileMap[levelText.Length];
			for(int row=0;row<levelText.Length;++row)
			{
				Debug.Log(row);
				string[] tileIDs=levelText[row].Split(',');
				mapTiles[row]=new TileMap();
				mapTiles[row].array=new int[tileIDs.Length];
				for (int columns=0;columns<tileIDs.Length;++columns)
				{
					mapTiles[row].array[columns]=int.Parse(tileIDs[columns]);
				}
			}

			populateMap();
		}
		else
		{
			Debug.Log("File dosen't exist");
		}
	}

	public void populateMap()
	{

		Vector2 startPosition=new Vector2(0.0f,0.0f);
		//this.transform.position=startPosition;
		float yOffset=pixelsToUnits;
		foreach(MultiDimensionalArray<int> t in mapTiles)
		{
			foreach(int i in t.array)
			{
				startPosition.x+=pixelsToUnits;
				if (i!=0){

					GameObject go=(GameObject)Instantiate(tilePrefab);
					SpriteRenderer sprite=go.renderer as SpriteRenderer;
					sprite.sprite=tiles[i];
					go.transform.position=startPosition;
					go.transform.parent=transform;
					go.AddComponent(typeof(BoxCollider2D));
				}
			}
			startPosition.x=0.0f;
			startPosition.y-=yOffset;
		}

		Vector3 pos=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,0.0f));
		transform.position=new Vector3(pos.x*-1,pos.y/2);
	}
	// Use this for initialization
	void Start () {
		loadCSV(Application.dataPath+"/MapCSV_LevelZero_MainGame.txt");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
