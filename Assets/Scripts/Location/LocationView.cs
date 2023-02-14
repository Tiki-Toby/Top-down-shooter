using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace Location
{
    public class LocationView : MonoBehaviour
    {
        [SerializeField] private InspectableDictionary<string, Transform> _characterSpawnPoints;
         
        public InspectableDictionary<string, Transform> CharacterSpawnPoints => _characterSpawnPoints;
    }
}