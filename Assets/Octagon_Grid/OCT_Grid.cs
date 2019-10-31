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
                tile.transform.position = new Vector3(x*2, y*2);
                tile.transform.parent = transform;
            }
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.W))
            MoveCursor(0,1);
        if (Input.GetKeyDown(KeyCode.A))
            MoveCursor(-1,0);
        if (Input.GetKeyDown(KeyCode.S))
            MoveCursor(0,-1);
        if (Input.GetKeyDown(KeyCode.D))
            MoveCursor(1,0);

        if (Input.GetKeyDown(KeyCode.Q)) {
            tiles[x,y].RotateCCW();
        }
        if (Input.GetKeyDown(KeyCode.E)) {

            tiles[x, y].RotateCW();
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
                    Gizmos.DrawRay(tile.transform.position, dir.normalized);
                }
            }
        Gizmos.color = Color.yellow;
        DrawOctagon(tiles[x, y].transform.position);
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

    void FindAll(int x, int y) {

    }
}
