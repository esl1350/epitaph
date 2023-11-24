using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoLAutomaton : MonoBehaviour
{
    [Range(0, 100)] [SerializeField] private int initChance;
    [Range(0, 8)] [SerializeField] private int birthLimit;
    [Range(1, 8)] [SerializeField] private int deathLimit;
    [Range(1, 10)] [SerializeField] private int numRuns;
    private int[,] terrain;
    private int width;
    private int height;
    private int bufferSize;

    public int[,] Simulate(int w, int h, int buffer) {
        bufferSize = buffer;
        width = w;
        height = h;
        if (terrain == null) {
            terrain = new int[width, height];
            InitPos();
        }

        for (int i = 0; i < numRuns; i++) {
            terrain = GenTilePos(terrain);
        }

        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if (j < bufferSize) {
                    terrain[i, j] = 0;
                }
                if (j + bufferSize > height) {
                    terrain[i, j] = 0;
                }
            }
        }
        return terrain;
    }

    private int[,] GenTilePos(int[,] prevMap) {
        int[,] currMap = new int[width, height];
        int neighbors;
        BoundsInt bounds = new BoundsInt(-1, -1, 0, 3, 3, 1);

        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                neighbors = 0;
                foreach (var b in bounds.allPositionsWithin) {
                    if (b.x == 0 && b.y == 0) {
                        continue;
                    }
                    if (i+b.x >= 0 && i+b.x < width && j+b.y >= 0 & j+b.y < height) {
                        neighbors += prevMap[i + b.x, j + b.y];
                    }
                }

                if (prevMap[i,j] == 1) {
                    if (neighbors < deathLimit) {
                        currMap[i, j] = 0;
                    } else {
                        currMap[i, j] = 1;
                    }
                }

                if (prevMap[i, j] == 0) {
                    if (neighbors > birthLimit) {
                        currMap[i, j] = 1;
                    } else {
                        currMap[i, j] = 0;
                    }

                }
            }
        }
        return currMap;
    }

    private void InitPos() {
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                terrain[i, j] = Random.Range(1, 101) < initChance ? 1 : 0;
            }
        }
    }



}
