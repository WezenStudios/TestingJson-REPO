using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class ObjectManager : MonoBehaviour
{
    /// <summary>
    /// ESSE CÓDIGO FOI DESENVOLVIDO PELOS DISCENTES:
    /// NICHOLAS FRANÇA - 201914470011
    /// MATHEUS QUETTO - 201914470009
    /// </summary>

    public GameObject objectPrefab; // Prefab do objeto em 3D
    private string fileName = "objects.json"; // Nome do arquivo JSON

    private List<Object3D> objects = new List<Object3D>(); // Lista de objetos em 3D

    private void Start()
    {
        // Adiciona o prefab do cubo
        objectPrefab = Resources.Load<GameObject>("CubePrefab");

        // Chama os métodos para gravar e carregar objetos
        SaveObjects();
        LoadObjects();
    }

    private void SaveObjects()
    {
        // Cria alguns objetos em 3D para salvar
        for (int i = 0; i < 5; i++)
        {
            Object3D obj = new Object3D();
            obj.name = "Object " + i;
            obj.posX = Random.Range(-5f, 5f);
            obj.posY = Random.Range(-5f, 5f);
            obj.posZ = Random.Range(-5f, 5f);
            obj.rotX = Random.Range(0f, 360f);
            obj.rotY = Random.Range(0f, 360f);
            obj.rotZ = Random.Range(0f, 360f);
            objects.Add(obj);
        }

        // Converte a lista de objetos em JSON
        string json = JsonUtility.ToJson(new Object3DList { objects = objects }, true);

        // Grava o JSON em um arquivo
        string path = Path.Combine(Application.dataPath, fileName);
        File.WriteAllText(path, json);

        Debug.Log("Objetos salvos em: " + path);
    }

    private void LoadObjects()
    {
        // Lê o JSON do arquivo
        string path = Path.Combine(Application.dataPath, fileName);
        string json = File.ReadAllText(path);

        // Converte o JSON de volta para a lista de objetos
        Object3DList loadedList = JsonUtility.FromJson<Object3DList>(json);
        objects = loadedList.objects;

        // Instancia os objetos na cena
        foreach (Object3D obj in objects)
        {
            // Instancia o objeto prefab
            GameObject newObject = Instantiate(objectPrefab, new Vector3(obj.posX, obj.posY, obj.posZ), Quaternion.Euler(obj.rotX, obj.rotY, obj.rotZ));
            newObject.name = obj.name;
        }
    }
}

[System.Serializable]
public class Object3DList
{
    public List<Object3D> objects;
}
