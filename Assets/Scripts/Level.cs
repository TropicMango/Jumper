using UnityEngine;
using System.Collections;

public class Level {
    public int Width { get; set; }
    public int Height { get; set; }

    private bool[,] level;

    private int steps = 0;

    private static System.Random rng;

    public Level (int width, int height) {
        rng = new System.Random ();

        Width = width;
        Height = height;

        steps = 1000;

        level = new bool[Height, Width];
        int cx = Width / 2;
        int cy = Height / 2;

        Direction cdir = (Direction)rng.Next (0, 4);

        for (int i = 0; i < steps; i++) {
            level [cy, cx] = true;

            int random = rng.Next (0, 2);
            if (random == 1) {
                cdir = (Direction)rng.Next (0, 4);
            }

            switch (cdir) {
            case Direction.Up:
                if (cy < Height - 2)
                    cy += 1;
                break;
            case Direction.Left:
                if (cx > 1)
                    cx -= 1;
                break;
            case Direction.Down:
                if (cy > 1)
                    cy -= 1;                
                break;
            case Direction.Right:
                if (cx < Width - 2)
                    cx += 1;
                break;
            }
        }
	}

    public bool[,] GetLevel() {
        return level;
    }
	
}
