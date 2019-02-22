using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;
using Components;
using JobScript;
using UnityEngine.UI;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public Text countText;
        public int incrementCount, moveSpeed, xLimits, zLimits, maxRange;
        public GameObject vehiclePrefab;
        EntityManager entityManager;
        private int vehicleCount = 0;
        NativeArray<Entity> entities;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        // Use this for initialization
        void Start()
        {
            entityManager = World.Active.GetOrCreateManager<EntityManager>();
            AddEntity();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                AddEntity();
        }

        void AddEntity()
        {
            entities = new NativeArray<Entity>(incrementCount, Allocator.Temp);
            entityManager.Instantiate(vehiclePrefab, entities);

            for (int i = 0; i < incrementCount; i++)
            {
                float x = UnityEngine.Random.Range(-xLimits, xLimits);
                float z = UnityEngine.Random.Range(-zLimits, zLimits);
                entityManager.SetComponentData(entities[i], new Position { Value = new float3(x, 0, z) });
                entityManager.SetComponentData(entities[i], new MoveSpeed { Value = moveSpeed });
                float rotY = UnityEngine.Random.Range(-1f, 1f);
                float rotX = UnityEngine.Random.Range(-1, 1);
                entityManager.SetComponentData(entities[i], new Rotation { Value = new quaternion(rotX, rotY, 0, 0) });
            }
            
            entities.Dispose();
            vehicleCount += incrementCount;
            countText.text = "Count:" + vehicleCount;
        }

        //public void DestroyEntity(Entity entity)
        //{
        //    entityManager.DestroyEntity(entity);
        //    entities.Dispose();
        //}
    }
}