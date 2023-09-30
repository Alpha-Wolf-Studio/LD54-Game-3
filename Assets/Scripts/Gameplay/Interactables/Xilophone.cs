using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xilophone : MonoBehaviour
{
    public List<Key> keys = new List<Key>();

    private void OnEnable()
    {
        
    }
}

[System.Serializable]
public class Key
{
    public Collider2D collider;
    public AudioClip sound;
}
