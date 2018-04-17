using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InitiateMap : MonoBehaviour {

    public Grid grid;
    public Tilemap wallmap;
    public TileBase tile;

    /// <summary>
    /// 0 - floor (none)
    /// 1 - wall (undestroyable)
    /// 
    /// 
    /// </summary>

    
    

	void Start () {
        
        wallmap.CompressBounds();
        int x, y;
        x = wallmap.size.x;
        y = wallmap.size.y;
        int xoff = wallmap.origin.x;
        int yoff = wallmap.origin.y;
        int[,] Map = new int[x, y];
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                if (wallmap.HasTile(new Vector3Int(i+xoff, j+yoff, 0)))
                {
                    Map[i, j] = 1;
                }
            }
        }
        //FOR DEBUGING
        //DebugArray(Map);
    }
    
    ///FOR DEBUGING
    void DebugArray(int[,] Map)
    {
        string arrayDebug = "";
        for (int j = Map.GetLength(1) - 1; j >= 0; j--)
        {
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                arrayDebug += Map[i, j].ToString();
            }
            arrayDebug += "\n";
        }
        Debug.Log(arrayDebug);
    }
    /////////////////////////
}
