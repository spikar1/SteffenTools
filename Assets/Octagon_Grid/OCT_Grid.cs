using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OCT_Grid : MonoBehaviour
{
    public OCT_Tile[,] tiles;
    public int sizeX, sizeY;
    public OCT_Tile tilePrefab;

    public int x, y;

    private void Start() {
        tiles = new OCT_Tile[sizeX, sizeY];
        for (int x = 0; x < sizeX; x++) {
            for (int y = 0; y < sizeY; y++) {
                var tile = tiles[x, y] = Instantiate(tilePrefab);
                tile.x = x;
                tile.y = y;
                tile.transform.position = new Vector3(x*2, y*2);
                tile.transform.parent = transform;
            }
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            MoveCursor(0,1);
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            MoveCursor(-1,0);
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            MoveCursor(0,-1);
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            MoveCursor(1,0);

        if (Input.GetKeyDown(KeyCode.Q)) {
            tiles[x,y].RotateCCW();
        }
        if (Input.GetKeyDown(KeyCode.E)) {

            tiles[x, y].RotateCW();
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            searchedTiles.Clear();
            Flood(tiles[x,y]);
        }
    }

    private void MoveCursor(int v1, int v2) {
        x += v1;
        y += v2;

        x = (int)Mathf.Repeat(x, sizeX);
        y = (int)Mathf.Repeat(y, sizeY);
    }

    private void OnDrawGizmos() {
        if (tiles == null)
            return;
        Gizmos.color = Color.white;
            foreach(var tile in tiles) {
                var flags = tile.flags;
                DrawOctagon(tile.transform.position);
                foreach (var dir in flags.Dir()) {
                    Gizmos.DrawRay(tile.transform.position, dir);
                }
            }
        Gizmos.color = Color.yellow;
        DrawOctagon(tiles[x, y].transform.position);

        Gizmos.color = Color.green;
        MarkNeighbors();

        Gizmos.color = Color.cyan;
        foreach (var item in searchedTiles) {
            Gizmos.DrawSphere(item.transform.position - Vector3.back, .2f);
        }
    }
    void DrawOctagon(Vector2 pos) {
        var f = (13f / 16f) * Mathf.PI * 2;
        var start = new Vector2(Mathf.Sin(f), Mathf.Cos(f));
        Vector2 end;
        for (int i = 0; i < 8; i++) {
            f = (float)(i * 2 - 1) / 16;
            f *= Mathf.PI * 2;
            end = new Vector2(Mathf.Sin(f), Mathf.Cos(f));
            Gizmos.DrawLine(start.normalized + pos, end.normalized + pos);
            start = end;
        }
    }

    void MarkNeighbors() {
        foreach (var item in GetNeighbors(tiles[x,y])) {
            Gizmos.DrawSphere(item.transform.position, .3f);
        }
    }

    List<OCT_Tile> GetNeighbors(OCT_Tile tile) {
        List<OCT_Tile> neighbors = new List<OCT_Tile>();
        foreach (Flags val in Enum.GetValues(typeof(Flags))) {
            if (tile.flags.HasFlag(val)) {
                int x = tile.x + (int)val.Dir()[0].x;
                int y = tile.y + (int)val.Dir()[0].y;
                if (
                    x >= 0 && x < sizeX &&
                    y >= 0 && y < sizeY &&
                    tiles[x, y].flags.HasFlag(val.GetOpposites())
                    )
                    neighbors.Add(tiles[x, y]);
            }
        }
        return neighbors;
    }

    List<OCT_Tile> searchedTiles = new List<OCT_Tile>();
    void Flood(OCT_Tile tile) {
        searchedTiles.Add(tile);
        foreach (var item in GetNeighbors(tile)) {
            
            if (!searchedTiles.Contains(item)) {
                Flood(item);
            }
        }
    }
}
