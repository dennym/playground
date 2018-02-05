using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Tile  {
  public enum TileType { Grass, Concrete, Water };

  TileType type = TileType.Grass;

  Action<Tile> cbtileTypeChanged;

  public TileType Type {
    get { return type; }
    set {
      TileType oldType = type;
      type = value;
      if (cbtileTypeChanged != null && oldType != type) {
        cbtileTypeChanged(this);
      }
    }
  }

  LooseObject looseObject;
  InstalledObject installedObject;

  World world;
  int x;
  public int X {
    get { return x; }
  }
  int y;
  public int Y {
    get { return y; }
  }

  public Tile( World world, int x, int y) {
    this.world = world;
    this.x = x;
    this.y = y;
  }

  public void RegisterTileTypeChangedCallback(Action<Tile> callback) {
    cbtileTypeChanged += callback;
  }

  public void UnregisterTileTypeChangedCallback(Action<Tile> callback) {
    cbtileTypeChanged -= callback;
  }
}
