using UnityEngine;
using System.Collections;

public class Map {
    public int Width { get; set; }
    public int Height { get; set; }

    private bool[,] cellMap;

    // Parameters
    private const float aliveChance = 0.45f;
    private const int deathLimit = 3;
    private const int birthLimit = 4;
    private const int numberOfSteps = 6;

    public Map (int width, int height) {
        Width = width;
        Height = height;

        cellMap = new bool[Width, Height];
        cellMap = InitMap (cellMap);
//        PrintMap ();
        for (int j = 0; j < 6; j++) {
            for (int i = 0; i < numberOfSteps; i++) {
                cellMap = SimulationStep (cellMap);
            }
        }
        PrintMap ();
    }

    private void PrintMap() {
        for (int x = 0; x < Width; x++) {
            string debug = "";
            for (int y = 0; y < Height; y++) {
                debug += cellMap [x, y].ToString () + " ";
            }
            Debug.Log (debug);
        }
    }

    private bool[,] InitMap(bool[,] map) {
        for (int x = 0; x < Width; x++) {
            for (int y = 0; y < Height; y++) {
                float rand = Random.Range (0.0f, 1.0f);
                if (rand < aliveChance) {
                    map [x, y] = true;
                }
            }
        }
        return map;
    }

    public bool[,] SimulationStep(bool[,] oldMap) {
        bool[,] newMap = new bool[Width, Height];
        for (int x = 0; x < Width; x++) {
            for (int y = 0; y < Height; y++) {
                int neighbors = CountAliveNeighbors (oldMap, x, y);
                if (oldMap [x, y]) {
                    if (neighbors < deathLimit) {
                        newMap [x, y] = true;
                    } else {
                        newMap [x, y] = false;
                    }
                } else {
                    if (neighbors > birthLimit) {
                        newMap [x, y] = false;
                    } else {
                        newMap [x, y] = true;
                    }
                }
            }
        }
        return newMap;
    }

    public int CountAliveNeighbors(bool[,] map, int x, int y) {
        int count = 0;
        for (int i = -1; i <= 1; i++) {
            for (int j = -1; j <= 1; j++) {
                int neighbor_x = x + i;
                int neighbor_y = y + j;
                if (i == 0 && j == 0) {
                    continue;
                } else if(neighbor_x < 0 || neighbor_y < 0 || 
                    neighbor_x >= map.GetLength(0) || neighbor_y >= map.GetLength(1)) {
                    continue;
                } else if(map[neighbor_x, neighbor_y]) {
                    count += 1;
                }
            }
        }
        return count;
    }

    public bool[,] GetCellMap() {
        return cellMap;
    }
}

