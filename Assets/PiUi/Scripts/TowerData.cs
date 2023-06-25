namespace PlaceTowers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    [CreateAssetMenu]
    public class TowerData : ScriptableObject
    {
        public List<tower> TowerList;
    }

    [Serializable]
    public class tower
    {
        [field: SerializeField]
        public string Type { get; private set; }
        [field: SerializeField]
        public int ID { get; private set; }
        [field: SerializeField]
        public GameObject Prefab { get; private set; }
    }
}