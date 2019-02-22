using System;
using Unity.Entities;
using UnityEngine;

namespace Components
{
    [Serializable]
    public struct MoveSpeed : IComponentData
    {
        public float Value; 
    }

    public class MoveSpeedComponent : ComponentDataWrapper<MoveSpeed>{}
}