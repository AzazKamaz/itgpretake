using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[DisallowMultipleComponent]
public class EndGameTriggerAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public string message;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new EndGameTriggerData {message = message});
    }
}