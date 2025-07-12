using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    static string prefabsPath = "Prefabs/";
    private static Dictionary<string, ArrayList> pool = new Dictionary<string, ArrayList>();
    
    public static GameObject Get(GameObject prefabObject, Vector3 position, Quaternion rotation)
    {
        string key = prefabObject.name + "(Clone)";
        GameObject obj;
        if (pool.ContainsKey(key) && pool[key].Count > 0)
        {
            print("get " + prefabsPath + prefabObject.name);
            ArrayList list = pool[key];
            obj = list[0] as GameObject;
            list.RemoveAt(0);
            if (obj == null)
            {
                //print("get new " + prefabsPath + prefabObject.name);
                obj = Instantiate(prefabObject, position, rotation);
            }
            else
            {
                obj.transform.position = position;
                obj.transform.rotation = rotation;
            }
            obj.SetActive(true);
        }
        else
        {
            //print("new " + prefabsPath + prefabObject.name);
            obj = Instantiate(prefabObject, position, rotation);
        }
        return obj;
    }
    
    public static Object Get(string prefabName, Vector3 position, Quaternion rotation)
    {
        string key = prefabName + "(Clone)";
        Object obj;
        if (pool.ContainsKey(key) && pool[key].Count > 0)
        {
            //print("get " + prefabsPath + prefabName);
            ArrayList list = pool[key];
            obj = list[0] as Object;
            list.RemoveAt(0);
            if (obj == null)
            {
                //print("get new " + prefabsPath + prefabName);
                obj = Instantiate(Resources.Load(prefabsPath + prefabName), position, rotation);
            }
            else
            {
                (obj as GameObject).transform.position = position;
                (obj as GameObject).transform.rotation = rotation;
            }
            (obj as GameObject).SetActive(true);
        }
        else
        {
            //print("new " + prefabsPath + prefabName);
            obj = Instantiate(Resources.Load(prefabsPath + prefabName), position, rotation);
        }
        return obj;
    }

    public static Object Return(GameObject obj)
    {
        string key = obj.name;
        //print("return key " + obj.name);
        if (pool.ContainsKey(key))
        {
            if (pool[key] == null)
            {
                pool[key] = new ArrayList(); 
            }
            ArrayList list = pool[key];
            list.Add(obj);
        }
        else
        {
            pool[key] = new ArrayList { obj };
        }
        obj.SetActive(false);
        return obj;
    }
}
