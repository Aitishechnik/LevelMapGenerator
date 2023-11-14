using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerUnitController : MonoBehaviour
{
    [SerializeField]
    private Unit _unit;

    private Coroutine _routeCoroutineHandler;

    private void ChangeDirection()
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
        Debug.Log("Stopping");
        return !_unit.IsMoving;
    }
    
    private Queue<Tile> GetRouteWrapper(Tile tile)
    {
        List<Tile> currentNeighbours = new List<Tile>();
        Queue<Tile> queue = new Queue<Tile>();
        Dictionary<Tile, bool> visitedVertexes = new Dictionary<Tile, bool>();
        Dictionary<Tile, int> distance = new Dictionary<Tile, int>();
        queue.Enqueue(tile);
        visitedVertexes.Add(tile, true);
        distance.Add(tile, 0);

        while (queue.Count > 0)
        {
            Tile currentTile = queue.Dequeue();

            currentTile.GetFreeNeighbours(currentNeighbours);

            for (int i = 0; i < currentNeighbours.Count; i++)
            {
                if (!visitedVertexes.ContainsKey(currentNeighbours[i]))
                {
                    queue.Enqueue(currentNeighbours[i]);
                    visitedVertexes.Add(currentNeighbours[i], true);
                    int currentTileDistance = distance[currentTile];
                    distance.Add(currentNeighbours[i], currentTileDistance + 1);
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
    private void GoToTile(Tile tile)
    {
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

    private Queue<Tile> GetRoute(Dictionary<Tile, int> routeTiles)
    {
        var route = new Queue<Tile>();
        int stepsToTarget = 0;

        foreach(var _value in routeTiles.Values)
        {
            if (_value > stepsToTarget)
                stepsToTarget = _value;
        }

        if (routeTiles.TryGetValue(_unit.CurrentTile, out int value) && value == stepsToTarget)
        {
            Tile currentStep = _unit.CurrentTile;

            while(stepsToTarget > 0)
            {
                stepsToTarget--;
                
                foreach(var i in routeTiles)
                {
                    if (i.Value == stepsToTarget && currentStep.IsNeighbour(i.Key))
                    {
                        currentStep = i.Key;
                        route.Enqueue(currentStep);
                    }                        
                }
            }
        }

        return route;
    }

    private void Start()
    {
        Tile.OnClick += ChangeDirection;
        Tile.OnTileClick += GoToTile;
    }
}
