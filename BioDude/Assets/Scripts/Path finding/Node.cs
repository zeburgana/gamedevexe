using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Node : MonoBehaviour {

    private Vector2Int coordinates;
    private int H;
    private int G;
    private int F;


    //[SerializeField]
    //Grid grid;
    //public Tilemap wallmap;

    private void Start()
    {
    }

    //private void Update()
    //{
    //    Vector3Int cords = grid.WorldToCell(transform.position);
    //    cords.z = 0;
    //    Debug.Log(cords);
    //    Debug.Log(wallmap.HasTile(cords));
    //}

}
