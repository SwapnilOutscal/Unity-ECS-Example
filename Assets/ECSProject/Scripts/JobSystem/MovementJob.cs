using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Components;
using Unity.Collections;
using Manager;

namespace JobScript
{
    [ComputeJobOptimization]
    public class MovementSystem : JobComponentSystem
    {
        struct MovementJob : IJobProcessComponentData<Position, Rotation , MoveSpeed>
        {
            public float deltaTime;
            public float maxRange;

            public void Execute(ref Position position, [ReadOnly] ref Rotation rotation, [ReadOnly] ref MoveSpeed speed)
            {
                float3 pos = position.Value;

                pos += deltaTime * speed.Value * math.forward(rotation.Value);

                //if(Vector3.Distance(pos,Vector3.zero) > maxRange)
                //{
                //    GameManager.instance.DestroyEntity(entity);
                //}

                position.Value = pos;
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            MovementJob movementJob = new MovementJob
            {
                deltaTime = Time.deltaTime,
                maxRange = GameManager.instance.maxRange
            };

            JobHandle moveHandle = movementJob.Schedule(this, inputDeps);

            return moveHandle;
        }

    }
}