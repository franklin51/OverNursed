using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldMan : PatientBaseClass
{
    override protected bool Waiting4FirstMission() 
	{
		return false;
	}

	override protected bool ExecuteMission()
	{
		return true;
	}

	override protected bool Waiting() 
	{
		
		return false;
	}

	override protected void Inpatience() 
	{

	}

    public override void picked()
    {
        base.picked();

        MM.changeOwner(ID,lastPlayer);

    }

}
