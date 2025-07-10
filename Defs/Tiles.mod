// 2025-07-10T16:20:25
Tiles 
{
	Tile Grass
	{
		Data 
		{
			Level 0
			Rarity 9
			Tags Grass
			Tags Empty
		}
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
				Description Some_grass.
			}
		}
	}
	Tile House
	{
		Data 
		{
			Level 1
			Rarity 1
			Tags House
			Tags Building
			PlaceCondition Empty
			PlaceCondition Nature
			Effect OnPlaceSelf:Gain:Population:3
			Effect PerTurn:TransformX1-Food:Gold:1
		}
		Map 
		{
			Prefab House
		}
		UI 
		{
			Title House
			ToolTip 
			{
				Title House
				Description It's_a_house.
			}
		}
	}
	Tile Forest
	{
		Data 
		{
			Level 1
			Rarity 1
			Tags Forest
			Tags Nature
			PlaceCondition Empty
			Effect PerTurn:PerAdjacent-House:Wood:1
		}
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
				Description It's_a_forest.
			}
		}
	}
	Tile Castle
	{
		Data 
		{
			Level 2
			Rarity 3
			Tags Castle
			PlaceCondition Empty
			PlaceCondition Nature
			DraftCondition House
			DraftCondition Farm
			Effect PerTurn:PerAdjacent-House:Gold:1
			Effect OnDestroySelf:Gain:Fame:3
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
				Description It's_a_castle.
			}
		}
	}
	Tile Farm
	{
		Data 
		{
			Level 1
			Rarity 1
			Tags Farm
			Tags Nature
			PlaceCondition Empty
			Effect PerTurn:IfAdjacent-House:Food:1
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
				Description It's_a_farm.
			}
		}
	}
	Tile Overgrowth
	{
		Data 
		{
			Level 2
			Rarity 1
			Tags Overgrowth
			PlaceCondition Building
			Effect OnPlaceSelf:Gain:Fame:1
		}
		Map 
		{
			Prefab Overgrowth
		}
		UI 
		{
			Title Overgrowth
			ToolTip 
			{
				Title Overgrowth
				Description Abandon_it.
			}
		}
	}
	Tile Ruin
	{
		Data 
		{
			Level 3
			Rarity 1
			Tags Ruin
			PlaceCondition Castle
			Effect OnPlaceSelf:Gain:Fame:3
		}
		Map 
		{
			Prefab Ruin
		}
		UI 
		{
			Title Ruin
			ToolTip 
			{
				Title Ruin
				Description Ruin_it.
			}
		}
	}
	Tile Clear
	{
		Data 
		{
			Level 2
			Rarity 2
			Tags Action
			PlaceCondition Overgrowth
			PlaceCondition Ruin
			Effect OnPlaceSelf:Cost:Gold:1
		}
		Map 
		{
			Prefab Clear
		}
		UI 
		{
			Title Clear
			ToolTip 
			{
				Title Clear
				Description Clear_the_tile.
			}
		}
	}
	Tile Monument
	{
		Data 
		{
			Level 3
			Rarity 2
			Tags Monument
			Tags Building
			PlaceCondition Empty
			PlaceCondition Nature
			Effect OnPlaceSelf:TransformAll-Fame:Gold:1
		}
		Map 
		{
			Prefab Monument
		}
		UI 
		{
			Title Monument
			ToolTip 
			{
				Title Monument
				Description It's_a_monument.
			}
		}
	}
	Tile NobleHouse
	{
		Data 
		{
			Level 1
			Rarity 3
			Tags House
			Tags Building
			PlaceCondition House
			Effect PerTurn:PerAdjacent-House:Gold:1
			Effect OnPlaceOther:PerAdjacent-Farm:Gold:5
		}
		Map 
		{
			Prefab NobleHouse
		}
		UI 
		{
			Title Noble_House
			ToolTip 
			{
				Title Noble_House
				Description It's_a_noble_house.
			}
		}
	}
}
