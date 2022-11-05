using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface PatientInterface
{
	float Mass { get; set; } // 質量影響玩家拿起後移動的速度，或是直接算增減速度
}
