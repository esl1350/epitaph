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
        mapHeight = 17;
        origin = baseMap.origin;

        EnemyPlacementType enemy = registeredEnemyTypes[0];

        Vector3Int tileLoc = new Vector3Int(origin.x + 2 + enemy.width / 2, origin.y + (mapHeight / 2), 0);
        Vector3 spawnTilePos = baseMap.CellToWorld(tileLoc);

        Instantiate(enemy.enemy, new Vector3(spawnTilePos.x + enemy.width, spawnTilePos.y, 0), Quaternion.identity);
    }
}
