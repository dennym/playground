using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using System.Reflection;
// using System;

public class WorldController : MonoBehaviour {

  public GameObject grassPrefab;
  public GameObject waterPrefab;
  public GameObject concretePrefab;

  World world;

  void Start () {
    // Create green world with Grass tiles
    world = new World();

    for (int x = 0; x < world.Width; x++) {
      for (int y = 0; y < world.Height; y++) {
        Tile tile_data = world.GetTileAt(x, y);
        Vector3 pos = new Vector3(tile_data.X, 0, tile_data.Y);

        GameObject tile_go = Instantiate(grassPrefab, pos, Quaternion.identity);
        tile_go.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        tile_go.name = "Tile_" + x + "_" + y;
        tile_go.transform.SetParent(this.transform, true);

        tile_data.RegisterTileTypeChangedCallback( (tile) => { OnTileTypeChange(tile, tile_go); } );
      }
    }

    world.RandomizeTiles();
	}

  void Update () {
	}

  void OnTileTypeChange(Tile tile_data, GameObject tile_go) {
    Mesh initialMesh;
    Mesh swapMesh;
    Material initialMaterial;
    Material swapMaterial;
    GameObject theTarget;

    theTarget = tile_go;
    initialMesh = tile_go.GetComponent<MeshFilter>().sharedMesh;
    initialMaterial = tile_go.GetComponent<Renderer>().sharedMaterial;

    if (tile_data.Type == Tile.TileType.Grass) {
      swapMesh = grassPrefab.GetComponent<MeshFilter>().sharedMesh;
      swapMaterial = grassPrefab.GetComponent<Renderer>().sharedMaterial;

      theTarget.GetComponent<MeshFilter>().mesh = swapMesh;
      theTarget.GetComponent<Renderer>().material = swapMaterial;
    } else if (tile_data.Type == Tile.TileType.Concrete) {
      swapMesh = concretePrefab.GetComponent<MeshFilter>().sharedMesh;
      swapMaterial = concretePrefab.GetComponent<Renderer>().sharedMaterial;

      theTarget.GetComponent<MeshFilter>().mesh = swapMesh;
      theTarget.GetComponent<Renderer>().material = swapMaterial;
    } else {
      Debug.LogError("OnTileTypeChange - Unrecognized tile type");
    }
  }
}
