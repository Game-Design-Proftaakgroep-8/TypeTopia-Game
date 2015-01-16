using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SumInfo
{
		public int minRange { get; private set; }
		public int maxRange { get; private set; }
		public int sumCommas { get; private set; }
		public List<int> commaOptions { get; private set; }
		
		public SumInfo(int minRange, int maxRange, int sumCommas) 
	{
			this.minRange = minRange;
			this.maxRange = maxRange;
			this.sumCommas = sumCommas;
			this.commaOptions = new List<int>();
		}
		
		public void AddCommaOption(int commaOption) 
	{
			this.commaOptions.Add(commaOption);
		}
}
