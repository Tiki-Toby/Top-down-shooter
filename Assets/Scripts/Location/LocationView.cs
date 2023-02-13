using System.Collections.Generic;
using UnityEngine;

namespace Location
{
    public class LocationView : MonoBehaviour
    {
        [SerializeField] private Dictionary<string, Transform> _characterSpawnPoints;
         
        public Dictionary<string, Transform> CharacterSpawnPoints => _characterSpawnPoints;
    }
}