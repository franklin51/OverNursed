using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PatientType
{
	float Speed { get; set; }
}

public class GeneralPatient : PatientType {
	float speed = 5;
	public float Speed {
		get { return speed; }
		set { speed = value; }
	}
}
