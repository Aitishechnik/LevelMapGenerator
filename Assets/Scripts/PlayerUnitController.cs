using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerUnitController : MonoBehaviour
{
    [SerializeField]
    private Unit _unit;

    private Coroutine _routeCoroutineHandler;

    private void ChangeDirection(Tile tile)
    {
        if (_routeCoroutineHandler != null)
        {
            Debug.Log("Coroutine is stoped");
            StopCoroutine(_routeCoroutineHandler);
            _routeCoroutineHandler = null;
        }
    }

    private bool IsUnitMoving()
    {
        if (_unit.IsMoving)
            Debug.Log("Stopping");
        return !_unit.IsMoving;
    }
    
    private Queue<Tile> GetRouteWrapper(Tile tile)
    {
        List<Tile> currentNeighbours = new List<Tile>();
        PriorityQueue<float, Tile> queue = new PriorityQueue<float, Tile>();
        Dictionary<Tile, bool> visitedVertexes = new Dictionary<Tile, bool>();
        Dictionary<Tile, float> distance = new Dictionary<Tile, float>();
        queue.Enqueue(0, tile);
        visitedVertexes.Add(tile, true);
        distance.Add(tile, 0);

        while (queue.Count > 0)
        {
            Tile currentTile = queue.Dequeue(out float currentTilePriority);

            currentTile.GetFreeNeighbours(currentNeighbours);

            for (int i = 0; i < currentNeighbours.Count; i++)
            {
                if (!visitedVertexes.ContainsKey(currentNeighbours[i]))
                {
                    queue.Enqueue(currentTilePriority+currentTile.MoveCost, currentNeighbours[i]);
                    visitedVertexes.Add(currentNeighbours[i], true);
                    var currentTileDistance = distance[currentTile];
                    distance.Add(currentNeighbours[i], currentTileDistance + currentTile.MoveCost + HeuristicDistance(currentNeighbours[i], _unit.CurrentTile));
                    if (currentNeighbours[i] == _unit.CurrentTile)
                    {
                        queue.Clear();
                        break;
                    }
                }
            }
        }

        return GetRoute(distance);
    }

    private float HeuristicDistance(Tile tile1, Tile tile2)
    {
        return Vector3.Distance(tile1.transform.position, tile2.transform.position);
    }

    private void GoToTile(Tile tile)
    {      
        if (!tile.IsWalkable || tile == null)
            return;
        ChangeDirection(tile);
        _routeCoroutineHandler = StartCoroutine(RouteCoroutine(tile));
    }

    private IEnumerator RouteCoroutine(Tile tile)
    {
        yield return new WaitUntil(IsUnitMoving);

        var route = GetRouteWrapper(tile);

        while (route.Count > 0)
        {
            if (!_unit.IsMoving)
                _unit.MoveToTile(route.Dequeue());
           
            yield return null;
        }
        _routeCoroutineHandler = null;
    }

    private Queue<Tile> GetRoute(Dictionary<Tile, float> routeTiles)
    {
        var route = new Queue<Tile>();
        var neighbours = new List<Tile>();

        if (routeTiles.TryGetValue(_unit.CurrentTile, out float pathCost)/* && value == stepsToTarget*/)
        {
            Tile currentStep = _unit.CurrentTile;

            while(pathCost > 0)
            {   
                currentStep.GetFreeNeighbours(neighbours);
                float minCost = float.MaxValue;
                int minIndex = -1;

                for(int i = 0; i < neighbours.Count; i++)
                {
                    if(routeTiles.TryGetValue(neighbours[i], out float neighbourCost))
                    {
                        if(minCost < neighbourCost)
                        {
                            minCost = neighbourCost;
                            minIndex = i;
                        }
                    }
                }
                route.Enqueue(neighbours[minIndex]);
                pathCost = minCost;
            }
        }

        return route;
    }

    private void Start()
    {
        Tile.OnTileClick += GoToTile;
    }
}
