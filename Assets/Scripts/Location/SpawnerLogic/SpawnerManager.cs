using System;
using System.Collections.Generic;
using Units.UnitLogic;
using UnityEngine;

namespace Location.SpawnerLogic
{
    public class SpawnerManager : IDisposable
    {
        private readonly UnitManager _unitManager;
        private readonly LinkedList<SpawnerController> _spawners;

        public SpawnerManager(UnitManager unitManager)
        {
            _unitManager = unitManager;
            _spawners = new LinkedList<SpawnerController>();
        }

        public void Init(GameObject spawnersParent)
        {
            Dispose();
            SpawnerView[] spawnerViews = spawnersParent.GetComponentsInChildren<SpawnerView>();
            foreach (var spawnerView in spawnerViews)
            {
                var spawner = new SpawnerController(_unitManager, spawnerView);
                _spawners.AddFirst(spawner);
            }
        }

        public void Update()
        {
            for (var spawnerNode = _spawners.First; spawnerNode != null; spawnerNode = spawnerNode.Next)
            {
                spawnerNode.Value.Update();
                if (!spawnerNode.Value.IsAlive)
                    _spawners.Remove(spawnerNode);
            }
        }
        
        public void Dispose()
        {
            _spawners.Clear();
        }
    }
}