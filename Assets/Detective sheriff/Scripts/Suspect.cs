using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suspect : MonoBehaviour
{
    private bool _isGuilty;
    public bool isGuilty { get => _isGuilty; set => this._isGuilty = value; }

    public bool found = false;
}
