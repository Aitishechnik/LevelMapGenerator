using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Unit : MonoBehaviour, IDamagable
{
    public Tile CurrentTile { get; private set; }

    private UnitsPool _pool;

    [SerializeField]
    private MeshRenderer _meshRenderer;

    [SerializeField]
    private MeshFilter _meshFilter;

    public UnitData ThisUnitData { get; private set; }

    public StatsData ThisUnitStats { get; private set; }

    public Tile CurrentTarget { get; private set; }
    public bool IsMoving { get; private set; }

    public void SetPool(UnitsPool pool)
    {
        _pool = pool;
    }

    public void ReturnToPool()
    {
        _pool.Return(this);
    }

    public IEnumerator MoveSmoothly(Tile targetTile, float moveSpeed)
    {
        IsMoving = true;
        targetTile.AttachUnit(this);
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = targetTile.transform.position + new Vector3(0, ThisUnitData.OffsetY, 0);

        float elapsedTime = 0f;
        
        while (elapsedTime < moveSpeed)
        {
            float t = elapsedTime / moveSpeed;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        AttachToTile(targetTile);
        IsMoving = false;
    }

    public void AttachToTile(Tile tile) 
    {
        if (CurrentTile != null)
            CurrentTile.DetachUnit();

        CurrentTile = tile;
        CurrentTile.AttachUnit(this);
        transform.position = tile.transform.position + Vector3.up;
    }

    public void SetData(UnitData unitData, bool isControllable)
    {
        ThisUnitData = unitData;
        _meshFilter.mesh = ThisUnitData.Mesh;
        _meshRenderer.material = ThisUnitData.Material;
        tag = isControllable ? Tags.PLAYER : Tags.ENEMY;
    }

    public void SetStats(StatsData stats)
    {
        ThisUnitStats = stats;
        ThisUnitStats.SetHP(ThisUnitStats.HP > 0 ? ThisUnitStats.HP : 1);
    }

    public void MoveToTile(Tile tile, bool isTeleport = false)
    {
        if (IsMoving)
        {
            throw new Exception("Unit is moving");
        }

        if (tile.IsOccupied)
        {
            throw new Exception("Tile is occupied");
        }

        if(tile == null)
        {
            throw new Exception("Tile == null");
        }

        if(isTeleport)
            AttachToTile(tile);
        else
        {
            if (CurrentTile.IsNeighbour(tile))
            {
                StartCoroutine(MoveSmoothly(tile, ThisUnitStats.MovingSpeed*tile.MoveCost));
            }
            else
            {
                throw new Exception("Is not a neighbour.");
            }
        }           
    }

    private Coroutine _routeCoroutineHandler;

    private void ChangeDirection(Tile tile)
    {
        if (_routeCoroutineHandler != null)
        {
            Debug.Log("Coroutine is stoped");
            StopCoroutine(_routeCoroutineHandler);
            CurrentTarget = tile;
            _routeCoroutineHandler = null;
        }
    }

    private bool IsUnitMoving()
    {
        if (IsMoving)
            Debug.Log("Stopping");
        return !this.IsMoving;
    }

    private Queue<Tile> GetRouteWrapper(Tile tile)
    {
        List<Tile> currentNeighbours = new List<Tile>();
        PriorityQueue<float, Tile> queue = new PriorityQueue<float, Tile>();
        List<Tile> visitedVertexes = new List<Tile>();
        Dictionary<Tile, float> distance = new Dictionary<Tile, float>();
        queue.Enqueue(0, tile);
        visitedVertexes.Add(tile);
        distance.Add(tile, 0);

        while (queue.Count > 0)
        {
            Tile currentTile = queue.Dequeue(out float currentTilePriority);

            currentTile.GetFreeNeighbours(currentNeighbours);
            for (int i = 0; i < currentNeighbours.Count; i++)
            {
                if (!visitedVertexes.Contains(currentNeighbours[i]))
                {
                    queue.Enqueue(currentTilePriority + currentTile.MoveCost, currentNeighbours[i]);
                    visitedVertexes.Add(currentNeighbours[i]);
                    var currentTileDistance = distance[currentTile];
                    distance.Add(currentNeighbours[i], currentTileDistance + currentNeighbours[i].MoveCost/* + HeuristicDistance(currentNeighbours[i], this.CurrentTile)*/);
                    if (currentNeighbours[i] == this.CurrentTile)
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

    public void GoToTile(Tile tile)
    {
        if (tile == null || !tile.IsWalkable || tile == CurrentTarget)
            return;
        ChangeDirection(tile);
        _routeCoroutineHandler = StartCoroutine(RouteCoroutine(tile));
    }

    private IEnumerator RouteCoroutine(Tile tile)
    {
        yield return new WaitUntil(IsUnitMoving);

        CurrentTarget = tile;
        var route = GetRouteWrapper(tile);

        while (route.Count > 0)
        {
            if (!this.IsMoving)
                this.MoveToTile(route.Dequeue());

            yield return null;
        }
        _routeCoroutineHandler = null;
        CurrentTarget = null;
    }

    private Queue<Tile> GetRoute(Dictionary<Tile, float> routeTiles)
    {
        var route = new Queue<Tile>();
        var neighbours = new List<Tile>();

        if (routeTiles.TryGetValue(this.CurrentTile, out float pathCost))
        {
            Tile currentStep = this.CurrentTile;

            while (pathCost > 0)
            {
                currentStep.GetFreeNeighbours(neighbours);
                float minCost = float.MaxValue;
                int minIndex = -1;

                for (int i = 0; i < neighbours.Count; i++)
                {
                    if (routeTiles.TryGetValue(neighbours[i], out float neighbourCost))
                    {
                        if (neighbourCost < minCost)
                        {
                            minCost = neighbourCost;
                            minIndex = i;
                        }
                    }
                }
                route.Enqueue(neighbours[minIndex]);
                pathCost = minCost;
                currentStep = neighbours[minIndex];
                currentStep.DebugText.text = pathCost.ToString("F1");
            }
        }

        return route;
    }

    public void ReceiveDamage(Damage damage)
    {
        ThisUnitStats.ReceiveDamage(damage);
        Debug.Log($"{gameObject.tag} HP: {ThisUnitStats.HP}");
    }
}
