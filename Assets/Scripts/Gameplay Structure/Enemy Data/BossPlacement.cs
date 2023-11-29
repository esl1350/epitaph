using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Tilemaps;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class BossPlacement : EnemyPlacement
{
    public override void PlaceEnemies(int[,] occupiedMap, int buffer)
    { 
        mapWidth = occupiedMap.GetLength(0);
        mapHeight = occupiedMap.GetLength(1);
        origin = baseMap.origin;

        EnemyPlacementType enemy = registeredEnemyTypes[0];

        Vector3Int tileLoc = new Vector3Int(origin.x + (mapWidth/2), origin.y + (mapWidth / 2), 0);
        Vector3 spawnTilePos = baseMap.CellToWorld(tileLoc);

        Instantiate(enemy.enemy, new Vector3(spawnTilePos.x, spawnTilePos.y + 0.5f * enemy.height, 0), Quaternion.identity);
    }
}
