using UnityEngine;
using uPools;

public sealed class ObjectPoolFacade<T> : ObjectPoolBase<T> where T : MonoBehaviour
{
    private readonly T _prefab;
    private readonly Transform _parent;
    private readonly bool _activateOnRent;

    public ObjectPoolFacade(T prefab, int capacity, Transform parent = null, bool activateOnRent = true)
    {
        _prefab = prefab ?? throw new System.ArgumentNullException(nameof(prefab));
        _parent = parent ?? new GameObject($"Pool<{typeof(T).Name}>").transform;
        _activateOnRent = activateOnRent;
        
        prefab.gameObject.SetActive(false);
        Prewarm(capacity);
    }

    protected override T CreateInstance()
    {
        var instance = Object.Instantiate(_prefab, _parent);
        instance.gameObject.SetActive(_activateOnRent);
        return instance;
    }

    protected override void OnRent(T instance)
    {
        base.OnRent(instance);
        instance.gameObject.SetActive(true);
    }

    protected override void OnReturn(T instance)
    {
        base.OnReturn(instance);
        instance.gameObject.SetActive(false);
    }

    protected override void OnDestroy(T instance)
    {
        base.OnDestroy(instance);
        Object.Destroy(instance.gameObject);
    }
}