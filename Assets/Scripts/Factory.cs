using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    private readonly Dictionary<string, GameObject> _prefabs = new Dictionary<string, GameObject>();
    
    [Serializable]
    public struct NamedPrefab {
        public string name;
        public GameObject prefab;
    }

    [SerializeField]
    public NamedPrefab[] prefabs;

    private void Awake()
    {
        foreach (var i in prefabs)
        {
            _prefabs[i.name] = i.prefab;
        }
    }

    public GameObject Instantiate(string prefabName)
    {
        var prefab = _prefabs[prefabName];
        return GameObject.Instantiate(prefab);
    }
}
