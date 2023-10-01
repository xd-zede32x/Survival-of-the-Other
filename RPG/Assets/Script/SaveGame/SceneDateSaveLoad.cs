using UnityEngine;
public class SceneDateSaveLoad : MonoBehaviour
{
    [SerializeField] private Transform _savingEnvironment;

    public void SaveScene()
    {
        BinarySevingSystem.SaveScene(_savingEnvironment);
        Debug.Log("Save");
    }

    public void LoadScene()
    {
        for (int index = 0; index < _savingEnvironment.childCount; index++)
        {
            Destroy(_savingEnvironment.GetChild(index).gameObject);
        }

        SceneData data = BinarySevingSystem.LoadScene();

        for (int index = 0; index < data.objectNames.Length; index++)
        {
            var prefabName = GetPrefabName(data, index);

            GameObject goToSpawn = Resources.Load<GameObject>($"ItemPrefabs/{prefabName}");
            Vector3 spawnPosition = new Vector3(data.objectPositions[index].x, data.objectPositions[index].y,
                data.objectPositions[index].z);

            GameObject sceneObject = Instantiate(goToSpawn, spawnPosition, Quaternion.identity);
            sceneObject.transform.SetParent(_savingEnvironment);
            sceneObject.GetComponent<Item>().amount = data.objectAmounts[index];
        }
        Debug.Log("Load");
    }

    private static string GetPrefabName(SceneData data, int i)
    {
        string prefabName = "";

        if (data.objectNames[i].IndexOf(" ") > 0)
        {
            int whitespaceIndex = data.objectNames[i].IndexOf(" ");
            int length = data.objectNames[i].Length;
            prefabName = data.objectNames[i].Remove(whitespaceIndex, data.objectNames[i].Length - whitespaceIndex);
        }

        else
        {
            prefabName = data.objectNames[i];
        }

        return prefabName;
    }
}