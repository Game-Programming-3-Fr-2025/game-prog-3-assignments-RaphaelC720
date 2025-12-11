using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class InfectedTilemap : MonoBehaviour
{
    public Tilemap tilemap;
    public Color normalColor = Color.white;
    public float timeToVanish = 5f;

    private readonly Dictionary<Vector3Int, Coroutine> active = new();
    private readonly Dictionary<Vector3Int, TileBase> original = new();

    void Awake()
    {
        if (!tilemap) 
            tilemap = GetComponent<Tilemap>();
        CacheOriginal();
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
        foreach (var keyValue in active)
            if (keyValue.Value != null) StopCoroutine(keyValue.Value);
        active.Clear();

        tilemap.ClearAllTiles();
        foreach (var keyValue in original)
        {
            tilemap.SetTile(keyValue.Key, keyValue.Value);
            tilemap.SetTileFlags(keyValue.Key, TileFlags.None); 
            tilemap.SetColor(keyValue.Key, normalColor);      
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
