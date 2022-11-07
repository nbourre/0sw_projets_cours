using Godot;
using System.Collections.Generic;
using System.Linq;

public class Map : Node
{
    /// Noeud enfant qui est un TileMap
    TileMap tileMap;
    TileSet tileSet;
    /// <summary>
    /// Dictionnaire contenant les différents types de tuiles
    /// Le nom de la tuile est la clé et la valeur est le ID de la tuile
    /// </summary>
    Dictionary<string, int> tileMapDictionary = new Dictionary<string, int>();
    
    int startTile;
    int endTile;
    int middleTile;

    bool debug = true;

    /// <summary>
    /// Objet pour le générateur de bruit
    /// </summary>
    OpenSimplexNoise noise;

    public override void _Ready()
    {
        tileMap = GetNode<TileMap>("TileMap");
        tileSet = tileMap.TileSet;

        GD.Randomize();
        noise = new OpenSimplexNoise();
        noise.Seed = (int)Mathf.Abs(GD.Randi());
        
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
        float xIncrement = 0.35f;

        int yStart = 10;
        int yLevel = yStart;
        int yEnd = yStart;

        int nbPlatforms = 15;
        int platformLength = 5;

        int xStart = 0;
        int xEnd = xStart + platformLength;
        int gapLength = 2;
        var noiseValue = yStart;
        var noiseValuePrevious = yStart;

        for (int i = 0; i < nbPlatforms; i++)
        {
            
            tileMap.SetCell(xStart, (int)noiseValue, startTile);
            yEnd = (int)noiseValue;

            GD.Print($"Platform : {i}, xStart: {xStart}, xEnd: {xEnd}, length: {platformLength}, noiseValue: {(int)noiseValue}");

          
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

            noiseValue = (int)(noise.GetNoise1d(xOffset) * 50) + yStart;
        }        
    }

    private float MapValue(float value, float fromLow, float fromHigh, float toLow, float toHigh) 
    {
        return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
    }
}
