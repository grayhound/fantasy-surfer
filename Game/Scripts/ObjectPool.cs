using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    static GameObject parent;

    static private Dictionary<string, List<GameObject>> pooled_objects = new Dictionary<string, List<GameObject>>();

    void Awake()
    {
        parent = gameObject;
    }

    public static void Clear()
    {
        pooled_objects.Clear();
    }

    public static void CreatePool(string alias, GameObject game_object, int amount = 0)
    {
        if (!pooled_objects.ContainsKey(alias)) {
            pooled_objects.Add(alias, new List<GameObject>());
            if (amount > 0) {
                for (int i = 0; i < amount; i++) {
                    CreateAndAddToPool(alias, game_object);
                }
            }
        }
    }

    private static void CreateAndAddToPool(string alias, GameObject game_object)
    {
        GameObject object_to_add = (GameObject)Instantiate(game_object);
        AddToPool(alias, object_to_add);
    }

    private static void AddToPool(string alias, GameObject game_object)
    {
        game_object.transform.parent = parent.transform;
        game_object.transform.localPosition = new Vector3(0, 0, 0);
        game_object.SetActive(false);
        pooled_objects[alias].Add(game_object);
    }

    public static GameObject Spawn(string alias)
    {
        GameObject result = null;
        if (pooled_objects.ContainsKey(alias)) {
            var list = pooled_objects[alias];
            if (list.Count > 0) {
                result = (GameObject)list[0];
                list.RemoveAt(0);
                result.SetActive(true);
                result.transform.parent = null;                
            }
        }
        return result;
    }

    public static void Recycle(string alias, GameObject game_object)
    {
        if (game_object == null) {
            return;
        }
        if (!pooled_objects.ContainsKey(alias)) {
            pooled_objects.Add(alias, new List<GameObject>());
        }
        AddToPool(alias, game_object);
    }
}