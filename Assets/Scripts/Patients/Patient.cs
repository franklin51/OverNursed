using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 跟不同種病人特性有關的
public class Patient : PatientBaseClass, PatientInterface
{
	float mass = 5;
	public float Mass {
		get { return mass; }
		set { mass = value; }
	}
}
