// 2025-09-13T01:31:37
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
			Effect OnPlace:Population:25:10*IfAdjacent*Water:10*If*Grass
			Effect PerTurn:Population:1:1*IfAdjacent*Gardens:-1*If*Desert
			Effect OnLevelUp:Gold:1
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
			Effect OnPlace:Population:25:10*IfAdjacent*Water:10*If*Grass
			Effect PerTurn:Population:1:1*IfAdjacent*Gardens:-1*If*Desert
			Effect OnLevelUp:Gold:2
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
			Effect OnPlace:Population:25:10*IfAdjacent*Water:10*If*Grass
			Effect PerTurn:Population:1:1*IfAdjacent*Gardens:-1*If*Desert
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
			Effect Always:MultiplyAdjacent:Houses*2
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
			Effect Always:MultiplyAdjacent:Houses*3
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
			Effect Always:MultiplyAdjacent:Houses*4
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
			Effect Always:MultiplyAdjacent:Houses*2
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
			Effect Always:MultiplyAdjacent:Houses*3
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
			Effect Always:MultiplyAdjacent:Houses*4
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
			Effect Always:MultiplyAdjacent:Farms*2
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
			Effect Always:MultiplyAdjacent:Farms*3
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
			Effect PerTurn:Gold:2*PerAdjacent*Farm:2*PerAdjacent*Workshop
			Effect Always:ReactivateAdjacent:Houses:Culture
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
			Effect PerTurn:Gold:1:1*PerAdjacent*Mountain
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
			Effect PerTurn:Gold:2:1*PerAdjacent*Mountain
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
			Effect PerTurn:Gold:2:2*PerAdjacent*Mountain
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
			Effect Always:MultiplyAdjacent:Mines*2:Millitary*2:Workshop*2
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
			Effect Always:MultiplyAdjacent:Mines*3:Millitary*3:Workshop*3
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
			Effect PerTurn:Culture:1*IfAdjacent*Gardens
			Effect Always:ReactivateAdjacent:Castle:Temple
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
			Effect OnPlace:Soldiers:1
			Effect PerTurn:Soldiers:1*PerAdjacentLevel*Houses
			Effect OnLevelUp:Soldiers:3
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
			Effect OnPlace:Soldiers:5
			Effect PerTurn:Soldiers:1*PerAdjacentLevel*Houses
			Effect OnLevelUp:Soldiers:6
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
			Effect OnPlace:Soldiers:10
			Effect PerTurn:Soldiers:1*PerAdjacentLevel*Houses
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
			Initiative 4
			Effect PerTurn:Soldiers:2*PerAdjacent*Castle
			Effect Always:Double*MusteringGrounds*IfAdjacent*Farm
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
			Initiative 4
			Effect PerTurn:Soldiers:4*PerAdjacent*Castle
			Effect Always:Double*MusteringGrounds*IfAdjacent*Farm
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
			Effect OnPlace:SelfLevel:1:If*Hills
			Effect Always:ReactivateAdjacent:Millitary
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
			Effect PerTurn:Culture:1*PerAdjacent*Houses
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
			Effect PerTurn:Culture:2*PerAdjacent*Houses
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
			Effect PerTurn:Culture:3*PerAdjacent*Houses
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
			Effect OnPlace:Population:1
			Effect Always:MultiplyAdjacent-2:Amphiteather
			Effect Cost:Gold:1
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
			Effect PerTurn:Gold:1*PerAdjacent*Port:1*PerAdjacent*Market:1*PerAdjacentLevel*Houses
			Effect PerTurn:Trade:1*PerAdjacent*Forrest:
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
			Effect PerTurn:Gold:2*PerAdjacent*Port:2*PerAdjacent*Market:1*PerAdjacentLevel*Houses
			Effect PerTurn:Trade:1*PerAdjacent*Forrest:
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
			Effect PerTurn:Gold:3*PerAdjacent*Port:3*PerAdjacent*Market:1*PerAdjacentLevel*Houses
			Effect PerTurn:Trade:1*PerAdjacent*Forrest:
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
			Effect PerTurn:Trade:2:1*PerExisting*Workshop
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
			Effect PerTurn:Trade:3:2*PerExisting*Workshop
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
			Effect Always:MultiplyAdjacent-2:Port:FishingBoats
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
			Effect PerTurn:Trade:2:1*PerAdjacent*Houses
			Effect Always:MultiplyAdjacent-2:FishingBoats
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
			Effect PerTurn:Trade:4:1*PerAdjacentLevel*Houses
			Effect Always:MultiplyAdjacent-3:FishingBoats
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
			Effect PerTurn:Science:1:1*PerAdjacent*Houses
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
			Effect PerTurn:Science:2:1*PerAdjacent-Houses
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
			Effect PerTurn:Science:3:1*PerAdjacent-Houses
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
			Effect Always:ReactivateAdjacent:Library
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
			Effect OnPlace:Gold:2*PerAdjacentLevel*House
			Effect OnPlace:Culture:1*PerAdjacentLevel*House
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
