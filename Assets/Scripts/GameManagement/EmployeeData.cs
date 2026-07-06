using System;
using UnityEngine;

[System.Serializable]
public class EmployeeData
{
	public string employeeName;
	public int employeeNumber;
	public int currentDay;
	public int[] levelBuildChoices; //0 Incomplete 1 HPC 2 CMS
	public int coffeeLevel;
	public int coffeeDrank;
	public int coffeeThrown;
	public int efficiency;
	public bool[] endings;
	
	public EmployeeData(){
		employeeName = "";
		employeeNumber = 0;
		currentDay = 1;
		levelBuildChoices = new int[9];
		for(int i = 0; i < 9; i++)
		{
			levelBuildChoices[i] = 0;
		}
		coffeeLevel = 3;
		coffeeDrank = 0;
		coffeeThrown = 0;
		efficiency = 1000;
		endings = new bool[5];
		for(int i = 0; i < 5; i++)
		{
			endings[i] = false;
		}
	}
}
