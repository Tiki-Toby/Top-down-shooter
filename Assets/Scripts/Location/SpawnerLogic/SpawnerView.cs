using UnityEngine;

namespace Location.SpawnerLogic
{
    public class SpawnerView : MonoBehaviour
    {
        [SerializeField] private SpawnerData spawnerData;

        public SpawnerData SpawnerData => spawnerData;
    }
}