using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public Tile UpperTile { get; private set; }
    public Tile LeftTile { get; private set; }
    public Tile LowerTile { get; private set; }
    public Tile RightTile { get; private set; }

    protected void SetNeighbourTile(Tile neighbour, int side)
    {
        switch (side)
        {
            case 0: UpperTile = neighbour; break;
            case 1: LeftTile = neighbour; break;
            case 2: LowerTile = neighbour; break;
            case 3: RightTile = neighbour; break;
        }
    }
}
