using UnityEngine;
using System;

[Serializable]
public class WallPositions
{
    public Vector3 WallPosition;
    public Quaternion WallRotation;
}
[Serializable]
public class StarPositions
{
    public Vector3 StarPosition;
    public Quaternion StarRotation;
}
[CreateAssetMenu(menuName = "New Scénario")]
public class ScenarioData : ScriptableObject
{
    public WallPositions[] WallPositions;
    public StarPositions[] StarPositions;

}
