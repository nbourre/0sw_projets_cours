using Godot;
using System.Collections.Generic;
using System.Linq;

public class Map : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    TileMap tileMap;
    TileSet tileSet;
    Dictionary<string, int> tileMapDictionary = new Dictionary<string, int>();

    int startTile;
    int endTile;
    int middleTile;

    bool debug = true;

    OpenSimplexNoise noise;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        tileMap = GetNode<TileMap>("TileMap");
        tileSet = tileMap.TileSet;

        GD.Randomize();
        noise = new OpenSimplexNoise();
        
        loadRessources();
        GenerateMap();
    }

    public void loadRessources() {
        var ids = tileSet.GetTilesIds();

        foreach (var id in ids)  {
            int? idx = id as int?;
            if (idx.HasValue)
            {
                tileMapDictionary.Add(tileSet.TileGetName(idx.Value), idx.Value);
            }
        }

        startTile = tileMapDictionary["ridge_left"];
        endTile = tileMapDictionary["ridge_right"];
        middleTile = tileMapDictionary["middle"];
    }

    public void GenerateMap()
    {
        tileMap.Clear();

        

        float xOffset = 0f;
        float xIncrement = 1;

        int yStart = 8;
        int yLevel = yStart;
        int yEnd = yStart;

        int nbPlatforms = 15;
        int platformLength = 5;

        int xStart = 0;
        int xEnd = xStart + platformLength;
        int gapLength = 2;

        for (int j = 0; j < nbPlatforms; j++)
        {
            var noiseValue = noise.GetNoise1d(xOffset) * 90 + yStart;
            tileMap.SetCell(xStart, (int)noiseValue, startTile);
            yEnd = (int)noiseValue;

            GD.Print($"Platform : {j}, xStart: {xStart}, xEnd: {xEnd}, length: {platformLength}, noiseValue: {(int)noiseValue}");

          
            for (int x = xStart + 1; x < xEnd; x++)
            {
                tileMap.SetCell(x, (int)noiseValue, middleTile);                
            }

            tileMap.SetCell(xEnd, yEnd, endTile);
            
            platformLength = (int)GD.RandRange(5, 10);
            gapLength = (int)GD.RandRange(2, 5);

            yStart = yEnd;
            xStart = xEnd + gapLength;
            xEnd += platformLength;
            xOffset += xIncrement;

            
        }        
    }

    private float MapValue(float value, float fromLow, float fromHigh, float toLow, float toHigh) 
{
        return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
    }
}
