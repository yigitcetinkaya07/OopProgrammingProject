using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType { None, Pushback, Rockets, Smash }

public class PowerUp:MonoBehaviour
{
    [field: SerializeField]
    public PowerUpType powerUpType { get; private set; }
}
