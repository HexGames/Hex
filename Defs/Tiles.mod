// 2025-09-03T00:12:44
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
			Tags Water
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
			Tags Land
			Tags Grass
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
			Tags Land
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
			Tags Land
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
			Tags Mountain
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
			Initiative 5
			Effect OnPlaceSelf:Gain:Population:2
			{
				Bonus Bonus:IfAdjacent-Water:2
				Bonus Bonus:If-Grass:2
			}
			Effect PerTurn:Gain:Population:1
			{
				Bonus Bonus:IfAdjacent-Gardens:1
				Bonus Malus:If-Desert:1
			}
			Effect OnLevelUp:Gain:Gold:1
			Stockpile Population
			SelfUpgrade Population:10
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
			Initiative 5
			Effect OnPlaceSelf:Gain:Population:10
			{
				Bonus Bonus:IfAdjacent-Water:5
				Bonus Bonus:If-Grass:5
			}
			Effect PerTurn:Gain:Population:1
			{
				Bonus Bonus:IfAdjacent-Gardens:1
				Bonus Malus:If-Desert:1
			}
			Effect OnLevelUp:Gain:Gold:2
			Stockpile Population
			SelfUpgrade Population:25
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
			Initiative 5
			Effect OnPlaceSelf:Gain:Population:25
			{
				Bonus Bonus:IfAdjacent-Water:10
				Bonus Bonus:If-Grass:10
			}
			Effect PerTurn:Gain:Population:1
			{
				Bonus Bonus:IfAdjacent-Gardens:1
				Bonus Malus:If-Desert:1
			}
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
			Weight 10
			Starting 3
			Tags Fishing_Boats
			Tags Food
			PlaceCondition On:Water
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
	Tile Fishing_II
	{
		Data 
		{
			Level 2
			Upgrade Fishing_III
			Weight 10
			Tags Fishing_Boats
			Tags Food
			PlaceCondition On:Water
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
	Tile Fishing_III
	{
		Data 
		{
			Level 3
			Weight 10
			Tags Fishing_Boats
			Tags Food
			PlaceCondition On:Water
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
	Tile Farm_I
	{
		Data 
		{
			Level 1
			Upgrade Farm_II
			Weight 10
			Starting 3
			Tags Building
			Tags Farm
			Tags Food
			PlaceCondition On:Land
			Initiative 4
			Effect PerTurn:Double-Houses
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
			Weight 10
			Tags Building
			Tags Farm
			Tags Food
			PlaceCondition On:Land
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
	Tile Farm_III
	{
		Data 
		{
			Level 3
			Weight 10
			Tags Building
			Tags Farm
			Tags Food
			PlaceCondition On:Land
			Initiative 4
			Effect PerTurn:Double-Houses
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
				Title Farm
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
			Weight 5
			Starting 3
			Tags Building
			Tags Mill
			Tags Food
			PlaceCondition On:Land
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
	Tile Mill_II
	{
		Data 
		{
			Level 3
			Weight 5
			Tags Building
			Tags Mill
			Tags Food
			PlaceCondition On:Land
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
			Level 3
			Weight 2
			Tags Building
			Tags Temple
			PlaceCondition On:Land
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
	Tile Mines_I
	{
		Data 
		{
			Level 1
			Upgrade Mines_II
			Weight 5
			Starting 3
			Tags Building
			Tags Mines
			Tags Gold
			PlaceCondition On:Land
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
	Tile Mines_II
	{
		Data 
		{
			Level 2
			Upgrade Mines_III
			Weight 5
			Tags Building
			Tags Mines
			Tags Gold
			PlaceCondition On:Land
			Initiative 5
			Effect PerTurn:Gain:Gold:2
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
	Tile Mines_III
	{
		Data 
		{
			Level 3
			Weight 5
			Tags Building
			Tags Mines
			Tags Gold
			PlaceCondition On:Land
			Initiative 5
			Effect PerTurn:Gain:Gold:2
			Effect PerTurn:PerAdjacent-Mountain:Gold:2
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
	Tile Forge_I
	{
		Data 
		{
			Level 2
			Upgrade Forge_II
			Weight 5
			Tags Building
			Tags Forge
			PlaceCondition On:Land
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
	Tile Forge_II
	{
		Data 
		{
			Level 3
			Weight 5
			Tags Building
			Tags Forge
			PlaceCondition On:Land
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
			Level 3
			Weight 2
			Tags Building
			Tags Palace
			PlaceCondition On:Land
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
	Tile Barraks_I
	{
		Data 
		{
			Level 1
			Upgrade Barraks_II
			Weight 5
			Starting 3
			Tags Millitary
			Tags Mustering_Grounds
			PlaceCondition On:Land
			Initiative 5
			Effect OnPlaceSelf:Gain:Soldiers:1
			Effect PerTurn:PerAdjacentLevel-Houses:Soldiers:1
			Effect OnLevelUp:Gain:Soldiers:2
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
	Tile Barraks_II
	{
		Data 
		{
			Level 2
			Upgrade Barraks_III
			Weight 5
			Tags Millitary
			Tags Mustering_Grounds
			PlaceCondition On:Land
			Initiative 5
			Effect OnPlaceSelf:Gain:Soldiers:5
			Effect PerTurn:PerAdjacentLevel-Houses:Soldiers:1
			Effect OnLevelUp:Gain:Soldiers:5
			Stockpile Soldiers
			SelfUpgrade Soldiers:25
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
	Tile Barraks_III
	{
		Data 
		{
			Level 3
			Weight 5
			Tags Millitary
			Tags Mustering_Grounds
			PlaceCondition On:Land
			Initiative 5
			Effect OnPlaceSelf:Gain:Soldiers:10
			Effect PerTurn:PerAdjacentLevel-Houses:Soldiers:1
			Stockpile Soldiers
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
	Tile Stable_I
	{
		Data 
		{
			Level 2
			Upgrade Stable_II
			Weight 5
			Tags Building
			Tags Millitary
			Tags Stable
			PlaceCondition On:Land
			{
				On:Land 
			}
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
	Tile Stable_II
	{
		Data 
		{
			Level 3
			Weight 5
			Tags Building
			Tags Millitary
			Tags Stable
			PlaceCondition On:Land
			{
				On:Land 
			}
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
			Level 3
			Weight 2
			Tags Building
			Tags Millitary
			Tags Castle
			PlaceCondition On:Land
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
	Tile Amphiteather_I
	{
		Data 
		{
			Level 1
			Upgrade Amphiteather_II
			Weight 5
			Starting 3
			Tags Building
			Tags Culture
			Tags Amphiteather
			PlaceCondition On:Land
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
	Tile Amphiteather_II
	{
		Data 
		{
			Level 2
			Upgrade Amphiteather_III
			Weight 5
			Tags Building
			Tags Culture
			Tags Amphiteather
			PlaceCondition On:Land
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
	Tile Amphiteather_III
	{
		Data 
		{
			Level 3
			Weight 5
			Tags Building
			Tags Culture
			Tags Amphiteather
			PlaceCondition On:Land
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
			Level 2
			Weight 2
			Tags Building
			Tags Culture
			Tags Mausoleum
			PlaceCondition On:Land
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
	Tile Workshop_I
	{
		Data 
		{
			Level 1
			Upgrade Workshop_II
			Weight 5
			Starting 3
			Tags Building
			Tags Trade
			Tags Workshop
			PlaceCondition On:Land
			Initiative 5
			Effect PerTurn:PerAdjacent-Forrest:Trade:1
			Effect PerTurn:PerAdjacent-Port:Gold:1
			Effect PerTurn:PerAdjacent-Market:Gold:1
			Effect PerTurn:PerAdjacentLevel-Houses:Gold:1
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
	Tile Workshop_II
	{
		Data 
		{
			Level 2
			Upgrade Workshop_III
			Weight 5
			Tags Building
			Tags Trade
			Tags Workshop
			PlaceCondition On:Land
			Initiative 5
			Effect PerTurn:PerAdjacent-Forrest:Trade:1
			Effect PerTurn:PerAdjacent-Port:Gold:2
			Effect PerTurn:PerAdjacent-Market:Gold:2
			Effect PerTurn:PerAdjacentLevel-Houses:Gold:1
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
	Tile Workshop_III
	{
		Data 
		{
			Level 3
			Weight 5
			Tags Building
			Tags Trade
			Tags Workshop
			PlaceCondition On:Land
			Initiative 5
			Effect PerTurn:PerAdjacent-Forrest:Trade:1
			Effect PerTurn:PerAdjacent-Port:Gold:3
			Effect PerTurn:PerAdjacent-Market:Gold:3
			Effect PerTurn:PerAdjacentLevel-Houses:Gold:1
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
	Tile Market_I
	{
		Data 
		{
			Level 2
			Upgrade Market_II
			Weight 5
			Tags Building
			Tags Trade
			Tags Market
			PlaceCondition On:Land
			PlaceCondition Margin
			Initiative 5
			Effect PerTurn:Gain:Trade:2
			Effect PerTurn:PerExisting-Workshop:Trade:1
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
	Tile Market_II
	{
		Data 
		{
			Level 3
			Weight 5
			Tags Building
			Tags Trade
			Tags Market
			PlaceCondition On:Land
			PlaceCondition Margin
			Initiative 5
			Effect PerTurn:Gain:Trade:3
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
			Level 2
			Weight 2
			Tags Building
			Tags Lighthouse
			PlaceCondition On:Water
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
	Tile Port_I
	{
		Data 
		{
			Level 2
			Upgrade Port_II
			Weight 5
			Tags Building
			Tags Trade
			Tags Port
			PlaceCondition On:Water
			Initiative 4
			Effect PerTurn:Gain:Trade:2
			Effect PerTurn:PerAdjacent-Houses:Trade:1
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
	Tile Port_II
	{
		Data 
		{
			Level 3
			Weight 5
			Tags Building
			Tags Trade
			Tags Port
			PlaceCondition On:Water
			Initiative 4
			Effect PerTurn:Gain:Trade:4
			Effect PerTurn:PerAdjacentLevel-Houses:Trade:1
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
	Tile Library_I
	{
		Data 
		{
			Level 1
			Upgrade Library_II
			Weight 2
			Starting 3
			Tags Building
			Tags Science
			Tags Library
			PlaceCondition On:Land
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
	Tile Library_II
	{
		Data 
		{
			Level 2
			Upgrade Library_III
			Weight 2
			Tags Building
			Tags Science
			Tags Library
			PlaceCondition On:Land
			Initiative 5
			Effect PerTurn:Gain:Science:2
			Effect PerTurn:PerAdjacent-Houses:Science:1
			Stockpile Science
			SelfUpgrade Science:25
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
	Tile Library_III
	{
		Data 
		{
			Level 3
			Weight 2
			Tags Building
			Tags Science
			Tags Library
			PlaceCondition On:Land
			Initiative 5
			Effect PerTurn:Gain:Science:3
			Effect PerTurn:PerAdjacent-Houses:Science:1
			Stockpile Science
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
			Level 3
			Weight 2
			Tags Building
			Tags Science
			Tags University
			PlaceCondition On:Land
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
			Level 1
			Weight 2
			Tags Gardens
			PlaceCondition On:Land
			Initiative 5
			Effect OnPlaceSelf:PerAdjacentLevel-House:Gold:2
			Effect OnPlaceSelf:PerAdjacentLevel-House:Culture:1
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
