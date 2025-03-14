using System;

public class Player
{
	public string name { get; set; }

	public float distance { get; set; }

	public float targetDistance { get; set; }

	public override string ToString()
	{
		return this.distance + "__" + this.name;
	}
}
