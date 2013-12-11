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

	public int LevelWidth=32;
	public int LevelHeight=32;

	public TileMap[] mapTiles;
	public int pixelsToUnits=32;

	public GameObject tilePrefab;

	// Use this for initialization
	void Start () {
		Vector2 startPosition=new Vector2((-Screen.width/2f)/pixelsToUnits,(Screen.height/2f)/pixelsToUnits);
		float yOffset=0.0f;
		foreach(MultiDimensionalArray<int> t in mapTiles)
		{
			foreach(int i in t.array)
			{
				GameObject go=(GameObject)Instantiate(tilePrefab);
				SpriteRenderer sprite=go.renderer as SpriteRenderer;
				sprite.sprite=tiles[i];
				startPosition.x+=go.transform.lossyScale.x;
				yOffset=go.transform.localScale.y;
				go.transform.position=startPosition;

				if (i>0)
				{
					go.AddComponent(typeof(BoxCollider2D));
				}
			}
			startPosition.x=(-Screen.width/2f)/pixelsToUnits;
			startPosition.y-=yOffset;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
