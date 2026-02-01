// 2026-02-02T00:49:37
Tiles 
{
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
			Tags Unused
			Terrain Water
		}
	}
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
			Tags Unused
			Terrain Grass
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
			Tags Unused
			Terrain Desert
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
			Tags Forrest
			Terrain Grass
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
			Tags Unused
			Terrain Mountain
		}
	}
	Tile Houses_I
	{
		Data 
		{
			Level 1
			Upgrade Houses_II
			Weight 10
			Starting 6
			Tags Building
			Tags Houses
			Tags Population
			PlaceCondition IfTerrain:Grass:Desert
			PlaceCondition IfTag:Unused
			Effect OnPlace:Production:Population:2
			Effect OnPlace:Production:Population:2:IfTerrain:Grass
			Effect OnPlace:Production:Population:2:IfAdjacent:Water
			Effect PerTurn:Production:Population:1
			Effect PerTurn:Production:Population:1:IfTerrain:Grass
			Effect PerTurn:Production:Population:1:IfAdjacent:Water
			Stockpile Population
			SelfUpgrade Population:25
		}
		Map 
		{
			Prefab House
		}
		UI 
		{
			Title Village
			ToolTip 
			{
				Title Village
				Description It's_a_tile.
			}
		}
	}
	Tile Houses_II
	{
		Data 
		{
			Level 2
			Upgrade Houses_III
			Weight 2
			Tags Building
			Tags Houses
			Tags Population
			PlaceCondition IfTerrain:Grass:Desert
			PlaceCondition IfTag:Unused
			Effect OnPlace:Production:Population:25
			Effect OnPlace:Production:Population:10:IfTerrain:Grass
			Effect OnPlace:Production:Population:10:IfAdjacent:Water
			Effect PerTurn:Production:Population:1
			Effect PerTurn:Production:Population:1:IfTerrain:Grass
			Effect PerTurn:Production:Population:1:IfAdjacent:Water
			Stockpile Population
			SelfUpgrade Population:100
		}
		Map 
		{
			Prefab House
		}
		UI 
		{
			Title Town
			ToolTip 
			{
				Title Town
				Description It's_a_tile.
			}
		}
	}
	Tile Houses_III
	{
		Data 
		{
			Level 3
			Weight 1
			Tags Building
			Tags Houses
			Tags Population
			PlaceCondition IfTerrain:Grass:Desert
			PlaceCondition IfTag:Unused
			Effect OnPlace:Production:Population:100
			Effect OnPlace:Production:Population:25:IfTerrain:Grass
			Effect OnPlace:Production:Population:25:IfAdjacent:Water
			Effect PerTurn:Production:Population:1
			Effect PerTurn:Production:Population:1:IfTerrain:Grass
			Effect PerTurn:Production:Population:1:IfAdjacent:Water
			Stockpile Population
		}
		Map 
		{
			Prefab House
		}
		UI 
		{
			Title City
			ToolTip 
			{
				Title City
				Description It's_a_tile.
			}
		}
	}
	Tile Fishing_I
	{
		Data 
		{
			Level 1
			Upgrade Fishing_II
			Weight 4
			Starting 3
			Tags FishingBoats
			Tags Food
			PlaceCondition IfTerrain:Water
			PlaceCondition IfTag:Unused
			Effect Always:MultiplyAdjacent:Houses:2
			UpgradeByToken Yes
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
	Tile Fishing_II
	{
		Data 
		{
			Level 2
			Upgrade Fishing_III
			Weight 2
			Tags FishingBoats
			Tags Food
			PlaceCondition IfTerrain:Water
			PlaceCondition IfTag:Unused
			Effect Always:MultiplyAdjacent:Houses:3
			UpgradeByToken Yes
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
	Tile Fishing_III
	{
		Data 
		{
			Level 3
			Weight 1
			Tags FishingBoats
			Tags Food
			PlaceCondition IfTerrain:Water
			PlaceCondition IfTag:Unused
			Effect Always:MultiplyAdjacent:Houses:4
			UpgradeByToken Yes
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
	Tile Farm_I
	{
		Data 
		{
			Level 1
			Upgrade Farm_II
			Weight 4
			Starting 3
			Tags Building
			Tags Farm
			Tags Food
			PlaceCondition IfTerrain:Grass
			PlaceCondition IfTag:Unused
			Effect Always:MultiplyAdjacent:Houses:2
			UpgradeByToken Yes
		}
		Map 
		{
			Prefab Farm
		}
		UI 
		{
			Title Small_Farm
			ToolTip 
			{
				Title Small_Farm
				Description It's_a_tile.
			}
		}
	}
	Tile Farm_II
	{
		Data 
		{
			Level 2
			Upgrade Farm_III
			Weight 2
			Tags Building
			Tags Farm
			Tags Food
			PlaceCondition IfTerrain:Grass
			PlaceCondition IfTag:Unused
			Effect Always:MultiplyAdjacent:Houses:3
			UpgradeByToken Yes
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
	Tile Farm_III
	{
		Data 
		{
			Level 3
			Weight 1
			Tags Building
			Tags Farm
			Tags Food
			PlaceCondition IfTerrain:Grass
			PlaceCondition IfTag:Unused
			Effect Always:MultiplyAdjacent:Houses:3
			UpgradeByToken Yes
		}
		Map 
		{
			Prefab Farm
		}
		UI 
		{
			Title Large_Farm
			ToolTip 
			{
				Title Large_Farm
				Description It's_a_tile.
			}
		}
	}
	Tile Mill_I
	{
		Data 
		{
			Level 2
			Upgrade Mill_II
			Weight 3
			Starting 3
			Tags Building
			Tags Mill
			Tags Food
			PlaceCondition IfTerrain:Grass:Desert
			PlaceCondition IfTag:Unused
			Effect Always:MultiplyAdjacent:Farm:2
			UpgradeByToken Yes
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
	Tile Mill_II
	{
		Data 
		{
			Level 2
			Weight 1
			Tags Building
			Tags Mill
			Tags Food
			PlaceCondition IfTerrain:Grass:Desert
			PlaceCondition IfTag:Unused
			Effect Always:MultiplyAdjacent:Farm:3
			UpgradeByToken Yes
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
}
