﻿using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Core.Block
{
    [CreateAssetMenu(menuName = "ScriptableObject/Installers/Game/Core/Block", fileName = "CoreBlockInstaller")]
    public class CoreBlockInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private List<BlockShapeData> _blockShapes;
        [SerializeField] private List<BlockShapeTextureData> _shapeTextures;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<BlockShapeDataProvider>().AsSingle().WithArguments(_blockShapes);
            Container.BindInterfacesTo<BlockShapeTextureProvider>().AsSingle().WithArguments(_shapeTextures);
            Container.BindInterfacesTo<BlockModelFactory>().AsSingle();
            Container.BindInterfacesTo<BlockModelStorage>().AsSingle();
            Container.BindInterfacesTo<BlockViewBuilder>().AsSingle();
            Container.BindInterfacesTo<BlockDrawingController>().AsSingle();
            Container.BindInterfacesTo<BlockShapeUtil>().AsSingle();
            Container.BindInterfacesTo<BlockViewRotationAnimator>().AsSingle();
            Container.BindInterfacesTo<BlockViewMovementAnimator>().AsSingle();
        }
    }
}
