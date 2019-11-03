using System;
using System.Linq;
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
        int seed = 1;
        //UnityEngine.Random.InitState(seed);
        tiles = new OCT_Tile[sizeX, sizeY];
        for (int x = 0; x < sizeX; x++) {
            for (int y = 0; y < sizeY; y++) {
                var tile = tiles[x, y] = Instantiate(tilePrefab);
                tile.x = x;
                tile.y = y;
                tile.transform.position = new Vector3(x, y);
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

        if (Input.GetKeyDown(KeyCode.P)) {
            StartCoroutine(MakePath(tiles[0, 0]));
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            RandomizeAll();
        }
        if (Input.GetKeyDown(KeyCode.X)) {
            AddRandomSprockets(50);
        }

    }

    private void AddRandomSprockets(int count) {
        for (int i = 0; i < count; i++) {
            tiles[UnityEngine.Random.Range(0, sizeX), UnityEngine.Random.Range(0, sizeY)].flags |= (Flags)(1 << UnityEngine.Random.Range(0, 8));
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
                //DrawOctagon(tile.transform.position);
                foreach (var dir in flags.Dir()) {
                    Gizmos.DrawRay(tile.transform.position, dir.normalized * .4f);
                }
            }
        Gizmos.color = Color.yellow;
        DrawOctagon(tiles[x, y].transform.position);

        Gizmos.color = Color.green*.4f;
        MarkNeighbors();

        Gizmos.color = Color.cyan * .5f;
        foreach (var item in searchedTiles) {
            Gizmos.DrawSphere(item.transform.position - Vector3.back, .13f);
        }

        //Debug
        Gizmos.color = Color.red;
        foreach (var item in checkedTiles) {
            //Gizmos.DrawCube(item.transform.position + Vector3.back, Vector3.one * .4f);
        }
        //End Debug
    }
    void DrawOctagon(Vector2 pos) {
        var f = (13f / 16f) * Mathf.PI * 2;
        var start = new Vector2(Mathf.Sin(f), Mathf.Cos(f));
        Vector2 end;
        for (int i = 0; i < 8; i++) {
            f = (float)(i * 2 - 1) / 16;
            f *= Mathf.PI * 2;
            end = new Vector2(Mathf.Sin(f), Mathf.Cos(f));
            Gizmos.DrawLine(start * .5f + pos, end * .5f + pos);
            start = end;
        }
    }

    void MarkNeighbors() {
        foreach (var item in GetNeighbors(tiles[x,y])) {
            Gizmos.DrawSphere(item.transform.position, .1f);
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

    List<OCT_Tile> GetAdjacent(OCT_Tile tile) {
        List<OCT_Tile> adjacent = new List<OCT_Tile>();
        foreach (Flags val in Enum.GetValues(typeof(Flags))) {
            
            int x = tile.x + (int)val.Dir()[0].x;
            int y = tile.y + (int)val.Dir()[0].y;
            if (
                x >= 0 && x < sizeX &&
                y >= 0 && y < sizeY
                )
                adjacent.Add(tiles[x, y]);
            
        }
        return adjacent;
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

    [ContextMenu("Make Connection")]
    void TempMakeConnection() {
        MakeConnection(temp_Tile, temp_Directions);
    }
    public OCT_Tile temp_Tile;
    public Flags temp_Directions;
    void MakeConnection(OCT_Tile tile, Flags direction, out OCT_Tile otherTile) {
        tile.flags |= direction;
        int x = tile.x + (int)direction.Dir()[0].x;
        int y = tile.y + (int)direction.Dir()[0].y;
        otherTile = tiles[x,y];
        tiles[x, y].flags |= direction.GetOpposites();
    }
    void MakeConnection(OCT_Tile tile, Flags direction) {
        OCT_Tile nullTile;
        MakeConnection(tile, direction, out nullTile);
    }

    void MakeConnection(OCT_Tile tile, OCT_Tile tile2) {
        OCT_Tile nullTile;
        if (Vector3.Distance(tile.transform.position, tile2.transform.position) > 1.9f) {
            Debug.DrawLine(tile.transform.position, tile2.transform.position);
            throw new Exception("Can't connect Tiles not adjacent");
        }

        var direction = (tile2.transform.position - tile.transform.position).FromDirection();
        MakeConnection(tile, direction, out nullTile);
    }

    OCT_Tile GetTileInDirection(OCT_Tile tile, Flags direction) {
        int x = tile.x + (int)direction.Dir()[0].x;
        int y = tile.y + (int)direction.Dir()[0].y;
        if (x >= 0 && x < sizeX && y >= 0 && y < sizeY)
            return tiles[x, y];
        else
            return null;
    }
    
    
    // TODO: Clean up this code (And optimalize)
    // TODO: make normal void function
    List<OCT_Tile> checkedTiles = new List<OCT_Tile>();
    IEnumerator MakePath(OCT_Tile fromTile) {
        List<OCT_Tile> tilesToCheck = new List<OCT_Tile>();
        checkedTiles.Clear();
        int iterator = 0;
        int curI = 0;
        foreach (var item in tiles) {
            tilesToCheck.Add(item);
            if (fromTile == item)
                iterator = curI;
            curI++;
        }

        var tile = tilesToCheck[iterator];
        int failSafe = 0;
        while (tilesToCheck.Count > 0 && failSafe < 1000) {
            yield return null;

            var f = (Flags)(1 << UnityEngine.Random.Range(0, 8));
            var otherTile = GetTileInDirection(tile, f);

            bool b = false;

            for (int i = 0; i < 7; i++) {
                f = f.RotateCW();
                otherTile = GetTileInDirection(tile, f);
                if (otherTile && tilesToCheck.Contains(otherTile)) {
                    MakeConnection(tile, otherTile);
                    tilesToCheck.Remove(otherTile);
                    checkedTiles.Add(otherTile);

                    tile = otherTile;

                    b = true;
                    break;
                }
            }
            if (b)
                continue;

            yield return null;
            var newTile = tilesToCheck[UnityEngine.Random.Range(0, tilesToCheck.Count - 1)];

            // TODO: This is messy....
            int failsafe = 0;
            while (!GetAdjacent(newTile).Contains(tile) && failsafe < 1000) {
                //UnityEngine.Random.InitState(randSeed);
                newTile = tilesToCheck[UnityEngine.Random.Range(0, tilesToCheck.Count - 1)];
                foreach (var item in GetAdjacent(newTile)) {
                    if (checkedTiles.Contains(item)) {
                        tile = item;
                    }
                }
                Debug.DrawLine(newTile.transform.position, tile.transform.position);
                //randSeed++;
                failsafe++;
            }
            MakeConnection(tile, newTile);

            tilesToCheck.Remove(newTile);
            checkedTiles.Add(newTile);

            tile = newTile;




            failSafe++;
                yield return null;
        }
        Debug.Log("failsafe: " + failSafe);
    }

    void RandomizeAll() {
        foreach (var item in tiles) {
            RandomizeTile(item);
        }
    }

    void RandomizeTile(OCT_Tile tile) {
        for (int i = 0; i < UnityEngine.Random.Range(0, 8); i++) {

            tile.flags = tile.flags.RotateCW();
        }
    }
}
