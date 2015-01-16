using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SumInfo
{
	public string sumType { get; private set; }
	public int sumLevel { get; private set; }
	public int minRange { get; private set; }
	public int maxRange { get; private set; }
	public int sumCommas { get; private set; }
	public List<int> commaOptions { get; private set; }
	
	public SumInfo(string sumType, int sumLevel, int minRange, int maxRange, int sumCommas) 
	{
		this.sumType = sumType;
		this.sumLevel = sumLevel;
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
