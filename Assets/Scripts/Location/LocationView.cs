using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Location
{
    public class LocationView : SerializedMonoBehaviour
    {
        [SerializeField] private Dictionary<string, Transform> _characterSpawnPoints;
         
        public Dictionary<string, Transform> CharacterSpawnPoints => _characterSpawnPoints;
    }
}