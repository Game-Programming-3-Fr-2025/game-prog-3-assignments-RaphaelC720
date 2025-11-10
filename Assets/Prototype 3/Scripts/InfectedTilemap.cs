using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class InfectedTilemap : MonoBehaviour
{
    public Tilemap tilemap;
    public Color healthyColor = Color.white;
    public float timeToVanish = 5f;

    private readonly Dictionary<Vector3Int, Coroutine> active = new();
    private readonly Dictionary<Vector3Int, TileBase> original = new();

    private void Reset()
    {
        tilemap = GetComponent<Tilemap>();
    }
    void Awake()
    {
        if (!tilemap) tilemap = GetComponent<Tilemap>();
        CacheOriginal();
    }
    private void Update()
    {

    }

    public void InfectAtWorldPos(Vector3 worldPos)
    {
        if (!tilemap) return;
        Vector3Int Cell = tilemap.WorldToCell(worldPos);
        if(!tilemap.HasTile(Cell)) return;
        if (active.ContainsKey(Cell)) return;

        Coroutine c = StartCoroutine(InfectAndVanish(Cell));
        active[Cell] = c;
    }
    public void RestoreAllToStart()
    {
        // stop all pending infections
        foreach (var kv in active)
            if (kv.Value != null) StopCoroutine(kv.Value);
        active.Clear();

        // wipe map then place original tiles
        tilemap.ClearAllTiles();
        foreach (var kv in original)
        {
            tilemap.SetTile(kv.Key, kv.Value);
            tilemap.SetTileFlags(kv.Key, TileFlags.None); // allow tint
            tilemap.SetColor(kv.Key, healthyColor);       // reset tint
        }
        tilemap.RefreshAllTiles();
    }
    private IEnumerator InfectAndVanish(Vector3Int Cell)
    {
        tilemap.SetTileFlags(Cell, TileFlags.None);
        tilemap.SetColor(Cell, Color.red);

        float time = 0f;
        while (time < timeToVanish)
        {
            time += Time.deltaTime;
            yield return null;
        }

        tilemap.SetTile(Cell, null);
        active.Remove(Cell);
    }
    private void CacheOriginal()
    {
        original.Clear();
        BoundsInt b = tilemap.cellBounds;
        foreach (var pos in b.allPositionsWithin)
        {
            var t = tilemap.GetTile(pos);
            if (t != null) original[pos] = t;
        }
    }
}
