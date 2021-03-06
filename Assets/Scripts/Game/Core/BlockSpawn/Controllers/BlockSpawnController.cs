﻿using System;
using Game.Core.Block;
using Game.Core.Level;
using UnityEngine;

namespace Game.Core.BlockSpawn
{
    public class BlockSpawnController : IBlockSpawnController
    {
        public event Action OnNextBlockChange;
        public event Action OnBlockSpawned;

        public IBlockModel NextBlock { get; private set; }

        private readonly IBlockModelStorage _blockStorage;
        private readonly BlockSpawnControllerConfig _config;
        private readonly IBlockModelFactory _blockFactory;
        private readonly ILevelModel _levelModel;
        private readonly ILevelPhysicsController _levelPhysics;

        public BlockSpawnController(IBlockModelStorage blockStorage,
                                    BlockSpawnControllerConfig config,
                                    IBlockModelFactory blockFactory,
                                    ILevelModel levelModel,
                                    ILevelPhysicsController levelPhysics)
        {
            _blockStorage = blockStorage;
            _config = config;
            _blockFactory = blockFactory;
            _levelModel = levelModel;
            _levelPhysics = levelPhysics;
        }

        public bool TrySpawnBlock()
        {
            if (NextBlock == null)
            {
                NextBlock = CreateRandomBlock();
            }

            if (_levelPhysics.CheckOverlappingLevelBlocks(NextBlock.Position, NextBlock.Rotation, NextBlock.Shape))
                return false;

            _blockStorage.AddBlock(NextBlock);
            NextBlock = CreateRandomBlock();
            OnNextBlockChange?.Invoke();
            OnBlockSpawned?.Invoke();
            
            return true;
        }

        private IBlockModel CreateRandomBlock()
        {
            var shape = _config.Shapes[UnityEngine.Random.Range(0, _config.Shapes.Count)];
            var levelSize = _levelModel.Size;
            var topCenter = new Vector3Int(levelSize.x / 2, levelSize.y - 1, levelSize.z / 2);

            return _blockFactory.CreateBlock(shape, topCenter, Quaternion.identity);
        }
    }
}
