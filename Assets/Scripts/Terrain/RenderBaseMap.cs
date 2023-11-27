using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class RenderBaseMap : MonoBehaviour
{
    public List<Tile> baseTiles = new List<Tile>();

    public Tile topLeftCornerTile;
    public Tile topCenterTile;
    public Tile topRightCornerTile;
    public Tile centerLeftTile;
    public Tile centerRightTile;
    public Tile bottomLeftCornerTile;
    public Tile bottomCenterTile;
    public Tile bottomRightCornerTile;
    public Tile topLeftCornerOutTile; // tiles for when the given spot is surrounded by obstacle
    public Tile topRightCornerOutTile; // tiles for when the given spot is surrounded by obstacle

    private int prevIdx;
    private int[,] terrainMap;
    private Tilemap tilemap;
    private Vector3Int origin;
    public void Render(int[,] map, Vector3Int o) {
        terrainMap = map;

        tilemap = gameObject.GetComponent<Tilemap>();
        origin = o;

        prevIdx = 0;

        for (int i = 0; i<tilemap.size.x; i++) {
            for (int j = 0; j<tilemap.size.y; j++) {
                int[,] mapSlice = GetSlice(i,j);
                
                if (IsOnlyZeroes(mapSlice)) {
                    PlaceBaseTile(i, j);
                } else {
                    tilemap.SetTile(new Vector3Int(origin.x + i, origin.y + j, 0), GetBoundaryTile(mapSlice));
                }
            
            }
        }

    }
    

    private Tile GetBoundaryTile(int[,] slice) {
        //the demons are back -- coords of the matrix are in the order x, y
        // (not oriented typically of a 2d matrix, but consistent w all other tilemap logic)
        if (slice[1, 1] == 1) {
            return bottomCenterTile;
        }
        if (slice[1, 0] == 1) {
            return topCenterTile;
        }
        if (slice[1, 2] == 1) {
            if (slice[2, 1] == 1) {
                return topLeftCornerOutTile;
            } else if (slice[0, 1] == 1) {
                return topRightCornerOutTile;
            }
            return bottomCenterTile;
        }
        if (slice[0, 1] == 1) {
            return centerRightTile;
        }
        if (slice [2, 1] == 1) {
            return centerLeftTile;
        }
        if (slice[0,0] == 1) {
            return topRightCornerTile;
        }
        if (slice [2, 0] == 1) {
            return topLeftCornerTile;
        }
        if (slice[0, 2] == 1) {
            return bottomRightCornerTile;
        }
        return bottomLeftCornerTile;
    }

    private void PlaceBaseTile(int x, int y) {
        int newIdx = Random.Range(0, baseTiles.Count);
        if (newIdx == prevIdx) {
            newIdx = (newIdx + 1) % baseTiles.Count;
        }
        tilemap.SetTile(new Vector3Int(origin.x + x, origin.y + y, 0), baseTiles[newIdx]);
        prevIdx = newIdx;

    }

    private bool IsOnlyZeroes(int[,] arr) {
        for (int i = 0; i < arr.GetLength(0); i++) {
            for (int j = 0; j < arr.GetLength(1); j++) {
                if (arr[i, j] != 0) {
                    return false;
                }
            }
        }
        return true;
    }

    private int[,] GetSlice(int x, int y) {
        int[,] slice = new int[3, 3];

        for (int i = -1; i < 2; i++) {
            for (int j = -1; j < 2; j++) {
                if (x + i < 0 || x + i >= tilemap.size.x || y + j < 0 || y + j >= tilemap.size.y) {
                    slice[1+i, 1+j] = 0;
                } else {
                    slice[1+i, 1+j] = terrainMap[x+i, y+j];
                }
            }
        }

        return slice;
    }

}
