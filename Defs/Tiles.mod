// 2025-08-04T15:50:51
Tiles 
{
	Tile Grass
	{
		Map 
		{
			Prefab Grass
		}
		UI 
		{
			Title Grass
			ToolTip 
			{
				Title Grass
				Description It's_a_tile.
			}
		}
		Data 
		{
			Terrain Flat
			Terrain Hills
			Climate Temperate
			Tags Grass
		}
	}
	Tile Water
	{
		Map 
		{
			Prefab Water
		}
		UI 
		{
			Title Water
			ToolTip 
			{
				Title Water
				Description It's_a_tile.
			}
		}
		Data 
		{
			Terrain Water
			Tags Water
		}
	}
	Tile Desert
	{
		Map 
		{
			Prefab Desert
		}
		UI 
		{
			Title Desert
			ToolTip 
			{
				Title Desert
				Description It's_a_tile.
			}
		}
		Data 
		{
			Terrain Flat
			Terrain Hills
			Climate Arid
			Tags Desert
			PlaceCondition NotAdjacent:Water
		}
	}
	Tile Forest
	{
		Map 
		{
			Prefab Forest
		}
		UI 
		{
			Title Forest
			ToolTip 
			{
				Title Forest
				Description It's_a_tile.
			}
		}
		Data 
		{
			Terrain Flat
			Terrain Hills
			Tags Forrest
		}
	}
	Tile Mountain
	{
		Map 
		{
			Prefab Mountain
		}
		UI 
		{
			Title Mountain
			ToolTip 
			{
				Title Mountain
				Description It's_a_tile.
			}
		}
		Data 
		{
			Terrain Mountain
			Tags Mountain
		}
	}
	Tile Houses
	{
		Data 
		{
			Level 1
			Weight 10
			Starting 6
			Terrain Flat
			Terrain Hills
			Climate __
			Tags Building
			Tags Houses
			Initiative 5
			Effect OnPlaceSelf:Gain:Population:2
			{
				Bonus Bonus:IfAdjacentTerrain-Water:50
				Bonus Bonus:IfTerrain-Flat:50
			}
			Effect PerTurn:Gain:Population:1
			{
				Bonus Bonus:IfAdjacent-Gardens:100
				Bonus Bonus:IfClimate-Arid:-100
			}
			Stockpile Population
			SelfUpgrade Population:4
		}
		Map 
		{
			Prefab House
		}
		UI 
		{
			Title Houses
			ToolTip 
			{
				Title Houses
				Description It's_a_tile.
			}
		}
	}
	Tile FishingBoats
	{
		Data 
		{
			Level 1
			Weight 10
			Starting 3
			Terrain Water
			Tags FishingBoats
			Initiative 4
			Effect PerTurn:Double-Houses
		}
		Map 
		{
			Prefab FishingBoats
		}
		UI 
		{
			Title FishingBoats
			ToolTip 
			{
				Title FishingBoats
				Description It's_a_tile.
			}
		}
	}
	Tile Farm
	{
		Data 
		{
			Level 2
			Weight 10
			Starting 3
			Terrain Flat
			Climate Temperate
			Tags Building
			Tags Farm
			PlaceCondition IfClimate:Temperate
			Initiative 4
			Effect PerTurn:Double-Houses
		}
		Map 
		{
			Prefab Farm
		}
		UI 
		{
			Title Farm
			ToolTip 
			{
				Title Farm
				Description It's_a_tile.
			}
		}
	}
	Tile Mill
	{
		Data 
		{
			Level 3
			Weight 5
			Starting 2
			Terrain Flat
			Terrain Hills
			Tags Building
			Tags Mill
			Initiative 3
			Effect PerTurn:Double-Farms
		}
		Map 
		{
			Prefab Mill
		}
		UI 
		{
			Title Mill
			ToolTip 
			{
				Title Mill
				Description It's_a_tile.
			}
		}
	}
	Tile Temple
	{
		Data 
		{
			Level 7
			Weight 2
			Starting 1
			Terrain Flat
			Terrain Hills
			Tags Building
			Tags Temple
			Initiative 6
			Effect PerTurn:Reactivate-Houses
			Effect PerTurn:Reactivate-Culture
			Effect PerTurn:PerAdjacent-Farm:Gold:2
			Effect PerTurn:PerAdjacent-Workshop:Gold:2
		}
		Map 
		{
			Prefab Tower
		}
		UI 
		{
			Title Temple
			ToolTip 
			{
				Title Temple
				Description It's_a_tile.
			}
		}
	}
	Tile Mines
	{
		Data 
		{
			Level 2
			Weight 5
			Starting 2
			Terrain Hills
			Tags Building
			Tags Mines
			Initiative 5
			Effect PerTurn:Gain:Gold:1
			Effect PerTurn:PerAdjacent-Mountain:Gold:1
		}
		Map 
		{
			Prefab Mine
		}
		UI 
		{
			Title Mines
			ToolTip 
			{
				Title Mines
				Description It's_a_tile.
			}
		}
	}
	Tile Forge
	{
		Data 
		{
			Level 3
			Weight 5
			Starting 1
			Terrain Flat
			Terrain Hills
			Tags Building
			Tags Forge
			Initiative 4
			Effect PerTurn:Double-Mines
			Effect PerTurn:Double-Millitary
			Effect PerTurn:Double-Workshop
		}
		Map 
		{
			Prefab Forge
		}
		UI 
		{
			Title Forge
			ToolTip 
			{
				Title Forge
				Description It's_a_tile.
			}
		}
	}
	Tile Palace
	{
		Data 
		{
			Level 9
			Weight 2
			Starting 1
			Terrain Flat
			Terrain Hills
			Tags Building
			Tags Palace
			Initiative 7
			Effect PerTurn:Reactivate-Castle
			Effect PerTurn:Reactivate-Temple
			Effect PerTurn:IfAdjacent-Gardens:Culture:1
		}
		Map 
		{
			Prefab NobleHouse
		}
		UI 
		{
			Title Palace
			ToolTip 
			{
				Title Palace
				Description It's_a_tile.
			}
		}
	}
	Tile MusteringGrounds
	{
		Data 
		{
			Level 3
			Weight 5
			Starting 2
			Terrain Flat
			Terrain Hills
			Tags Millitary
			Tags Mustering_Grounds
			Initiative 5
			Effect OnPlaceSelf:Gain:Soldiers:1
			Effect PerTurn:PerAdjacent-Houses:Soldiers:1
			Stockpile Soldiers
			SelfUpgrade Soldiers:10
		}
		Map 
		{
			Prefab Archery
		}
		UI 
		{
			Title MusteringGrounds
			ToolTip 
			{
				Title MusteringGrounds
				Description It's_a_tile.
			}
		}
	}
	Tile Stable
	{
		Data 
		{
			Level 5
			Weight 5
			Starting 1
			Terrain Flat
			Tags Building
			Tags Millitary
			Tags Stable
			Initiative 4
			Effect PerTurn:PerAdjacent-Castle:Soldiers:3
			Effect PerTurn:Double-MusteringGrounds
			{
				Condition Condition:IfAdjacent:Farm
			}
		}
		Map 
		{
			Prefab Sheep
		}
		UI 
		{
			Title Stable
			ToolTip 
			{
				Title Stable
				Description It's_a_tile.
			}
		}
	}
	Tile Castle
	{
		Data 
		{
			Level 7
			Weight 2
			Starting 1
			Terrain Flat
			Terrain Hills
			Tags Building
			Tags Millitary
			Tags Castle
			Initiative 6
			Effect PerTurn:Reactivate-Millitary
			Effect OnPlaceSelf:If-Hills:SelfLevel:1
		}
		Map 
		{
			Prefab Castle
		}
		UI 
		{
			Title Castle
			ToolTip 
			{
				Title Castle
				Description It's_a_tile.
			}
		}
	}
	Tile Amphiteather
	{
		Data 
		{
			Level 3
			Weight 5
			Starting 1
			Terrain Flat
			Terrain Hills
			Tags Building
			Tags Culture
			Tags Amphiteather
			Initiative 5
			Effect PerTurn:PerAdjacent-Houses:Culture:1
		}
		Map 
		{
			Prefab Amphiteather
		}
		UI 
		{
			Title Amphiteather
			ToolTip 
			{
				Title Amphiteather
				Description It's_a_tile.
			}
		}
	}
	Tile Mausoleum
	{
		Data 
		{
			Level 6
			Weight 2
			Starting 1
			Terrain Flat
			Terrain Hills
			Tags Building
			Tags Culture
			Tags Mausoleum
			Initiative 4
			Effect PerTurn:Double-Amphiteather
			Effect OnPlaceSelf:Cost:Population:1
			Effect OnPlaceSelf:Cost:Gold:1
		}
		Map 
		{
			Prefab Mausoleum
		}
		UI 
		{
			Title Mausoleum
			ToolTip 
			{
				Title Mausoleum
				Description It's_a_tile.
			}
		}
	}
	Tile Workshop
	{
		Data 
		{
			Level 3
			Weight 5
			Starting 2
			Terrain Flat
			Terrain Hills
			Tags Building
			Tags Trade
			Tags Workshop
			Initiative 5
			Effect PerTurn:PerAdjacent-Forrest:Trade:1
			Effect PerTurn:PerAdjacent-Port:Gold:3
			Effect PerTurn:PerAdjacent-Market:Gold:3
			Effect PerTurn:PerAdjacent-Houses:Gold:1
		}
		Map 
		{
			Prefab Workshop
		}
		UI 
		{
			Title Workshop
			ToolTip 
			{
				Title Workshop
				Description It's_a_tile.
			}
		}
	}
	Tile Market
	{
		Data 
		{
			Level 4
			Weight 5
			Starting 1
			Terrain Flat
			Terrain Hills
			Tags Building
			Tags Trade
			Tags Market
			PlaceCondition Margin
			Initiative 5
			Effect PerTurn:Gain:Trade:2
			Effect PerTurn:PerExisting-Workshop:Trade:2
		}
		Map 
		{
			Prefab Market
		}
		UI 
		{
			Title Market
			ToolTip 
			{
				Title Market
				Description It's_a_tile.
			}
		}
	}
	Tile Lighthouse
	{
		Data 
		{
			Level 4
			Weight 2
			Starting 1
			Terrain Water
			Tags Building
			Tags Lighthouse
			PlaceCondition Adjacent:Hills
			Initiative 3
			Effect PerTurn:Double-Port
			Effect PerTurn:Double-FishingBoats
		}
		Map 
		{
			Prefab Lighthouse
		}
		UI 
		{
			Title Lighthouse
			ToolTip 
			{
				Title Lighthouse
				Description It's_a_tile.
			}
		}
	}
	Tile Port
	{
		Data 
		{
			Level 4
			Weight 5
			Starting 1
			Terrain Water
			Tags Building
			Tags Trade
			Tags Port
			PlaceCondition Adjacent:Flat
			Initiative 4
			Effect PerTurn:Gain:Trade:3
			Effect PerTurn:PerAdjacent-5xPopulation:Trade:1
			Effect PerTurn:Double-FishingBoats
		}
		Map 
		{
			Prefab Port
		}
		UI 
		{
			Title Port
			ToolTip 
			{
				Title Port
				Description It's_a_tile.
			}
		}
	}
	Tile Library
	{
		Data 
		{
			Level 5
			Weight 2
			Starting 1
			Terrain Flat
			Terrain Hills
			Tags Building
			Tags Science
			Tags Library
			Initiative 5
			Effect PerTurn:Gain:Science:1
			Effect PerTurn:PerAdjacent-Houses:Science:1
			Stockpile Science
			SelfUpgrade Science:10
		}
		Map 
		{
			Prefab Library
		}
		UI 
		{
			Title Library
			ToolTip 
			{
				Title Library
				Description It's_a_tile.
			}
		}
	}
	Tile University
	{
		Data 
		{
			Level 8
			Weight 2
			Starting 1
			Terrain Flat
			Terrain Hills
			Tags Building
			Tags Science
			Tags University
			Initiative 6
			Effect PerTurn:Reactivate-Library
			Effect PerTurn:PerAdjacent-Houses:Science:2
			Effect PerTurn:IfAdjacent-Gardens:Culture:1
		}
		Map 
		{
			Prefab University
		}
		UI 
		{
			Title University
			ToolTip 
			{
				Title University
				Description It's_a_tile.
			}
		}
	}
	Tile Gardens
	{
		Data 
		{
			Level 7
			Weight 2
			Starting 1
			Terrain Flat
			Terrain Hills
			Tags Gardens
			Initiative 5
			Effect OnPlaceSelf:Gain:Culture:1
		}
		Map 
		{
			Prefab Garden
		}
		UI 
		{
			Title Gardens
			ToolTip 
			{
				Title Gardens
				Description It's_a_tile.
			}
		}
	}
}
