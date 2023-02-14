using UnityEngine;
using System;

[Serializable]
public class WallPositions
{
    public Vector3 WallPosition;
    public Quaternion WallRotation;
}
[CreateAssetMenu(menuName = "New Scénario")]
public class ScenarioData : ScriptableObject
{
    public WallPositions[] WallPositions;

}
