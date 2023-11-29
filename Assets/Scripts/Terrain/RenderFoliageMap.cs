using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RenderFoliageMap : MonoBehaviour
{
    public List<Tile> leftTiles = new List<Tile>();
    public List<Tile> rightTiles = new List<Tile>();
    public List<Tile> topTiles = new List<Tile>();
    public List<Tile> botTiles = new List<Tile>();
    public List<Tile> cornerTiles = new List<Tile>();
    private Tilemap tilemap;
    private Vector3Int origin;
    private Vector3Int dims;
    public void RenderScreenOnly(Vector3Int d, Vector3Int o) {
        Render(new Vector3Int(d.x, 17, d.z), o);
    }
    public void Render(Vector3Int d, Vector3Int o)
    {
        tilemap = gameObject.GetComponent<Tilemap>();
        origin = o;
        dims = d;

        SetCorners();

        for (int i = 1; i <= dims.y; i += 4) {
            tilemap.SetTile(new Vector3Int(origin.x, origin.y + i, 0), leftTiles[Random.Range(0, 2)]);
            tilemap.SetTile(new Vector3Int(origin.x + dims.x -1, origin.y + i, 0), rightTiles[Random.Range(0, 2)]);
        }

        for (int i = 1; i < 13; i += 4) {
            tilemap.SetTile(new Vector3Int(origin.x + i, origin.y, 0), botTiles[Random.Range(0, 2)]);
            tilemap.SetTile(new Vector3Int(origin.x + dims.x - i, origin.y, 0), botTiles[Random.Range(0, 2)]);
            tilemap.SetTile(new Vector3Int(origin.x + i, origin.y + dims.y -1, 0), topTiles[Random.Range(0, 2)]);
            tilemap.SetTile(new Vector3Int(origin.x + dims.x - i, origin.y + dims.y - 1, 0), topTiles[Random.Range(0, 2)]);
        }


    }

    // Update is called once per frame
    void SetCorners()
    {
        tilemap.SetTile(new Vector3Int(origin.x, origin.y+dims.y, 0), cornerTiles[0]);
        tilemap.SetTile(new Vector3Int(origin.x+dims.x, origin.y+dims.y, 0), cornerTiles[1]);
        tilemap.SetTile(new Vector3Int(origin.x, origin.y, 0), cornerTiles[2]);
        tilemap.SetTile(new Vector3Int(origin.x + dims.x, origin.y, 0), cornerTiles[3]);

    }
}
