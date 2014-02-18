using UnityEngine;
using System.Collections.Generic;

//Random commment
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

	public Transform spawnPoint;
	public bool createBlankTiles=false;

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

	public void saveCSV(string filename)
	{
		using(System.IO.StreamWriter tw=System.IO.File.CreateText(filename))
		{
			foreach(TileMap row in mapTiles)
			{
				for (int i=0;i<row.array.Length;++i)
				{
					tw.Write(row.array[i]);
					if (i!=row.array.Length-1)
					{
						tw.Write(",");
					}
				}
				tw.Write("\n");
			}
		}
	}

	void createTile(int id, Vector2 pos)
	{
		GameObject go=(GameObject)Instantiate(tilePrefab);
		go.name=id.ToString();
		SpriteRenderer sprite=go.renderer as SpriteRenderer;
		sprite.sprite=tiles[id];
		go.layer=LayerMask.NameToLayer("Ground");
		go.transform.position=pos;
		go.transform.parent=transform;
		go.AddComponent(typeof(BoxCollider2D));
	}

	public void toggleBlankTiles()
	{
		for(int i=0;i<transform.childCount;i++)
		{
			Transform current=transform.GetChild(i);
			if(current.name=="0")
			{
				current.gameObject.SetActive(!current.gameObject.activeInHierarchy);
			}
		}
	}

	void populateMap()
	{
		Vector2 startPosition=new Vector2(0.0f,0.0f);
		//this.transform.position=startPosition;
		float yOffset=pixelsToUnits;
		foreach(MultiDimensionalArray<int> t in mapTiles)
		{
			foreach(int i in t.array)
			{
				startPosition.x+=pixelsToUnits;

				if (createBlankTiles)
				{
					createTile(i,startPosition);
				}
				else{
					if (i!=0){
						createTile(i,startPosition);
					}
				}
			}
			startPosition.x=0.0f;
			startPosition.y-=yOffset;
		}

		//need to check this out
		Vector3 pos=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,0.0f));
		transform.position=new Vector3(pos.x*-1,pos.y/2);
	}
	

	// Use this for initialization
	void Start () {
		//loadCSV(Application.dataPath+"/MapCSV_LevelZero_MainGame.txt");
		//saveCSV(Application.dataPath+"/MapCSV_LevelZero_MainGame2.txt");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void generateBlankMap(int width, int height)
	{
		mapTiles=new TileMap[height];
		for(int r=0;r<mapTiles.Length;r++)
		{
			mapTiles[r]=new TileMap();
			mapTiles[r].array=new int[width];
			for(int t=0;t<mapTiles[r].array.Length;t++)
			{
				mapTiles[r].array[t]=0;
			}
		}

		populateMap();
	}
}
