// 2025-06-04T11:59:40
Goals 
{
	Goal Start
	{
		Type ResourceCheck
		TurnMin 0
		UI 
		{
			Title Grow_Population
			Description Grow_to_20_Population.
			Goal 20_Population
			Reward Enable_Trading
		}
		Data 
		{
			Goal 
			{
				Have Population:20
			}
			Reward 
			{
				Unlock Trade
			}
		}
	}
	Goal StMark
	{
		Type Project
		TurnMin 20
		UI 
		{
			Title Build_Doge's_Chapel_
			Description Build_a_chapel_for_the_Doge.
		}
	}
}
