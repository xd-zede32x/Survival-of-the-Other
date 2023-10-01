using UnityEngine;
using System.Collections.Generic;
public class SlotArder : MonoBehaviour
{
    [SerializeField] private GameObject _topPrefab;
    [SerializeField] private GameObject _panstPrefab;
    [SerializeField] private GameObject _shoesPrefab;
    [SerializeField] private GameObject _chestPlatePrefab;
    [SerializeField] private GameObject _armorMaskPrefab;
    [SerializeField] private SkinnedMeshRenderer _playerSkin;
    [SerializeField] private List<GameObject> _equipedSClothes;

    [SerializeField] private bool _putAllClothes;
    

    private void Start()
    {
        _equipedSClothes = new List<GameObject>();

        if (_putAllClothes)
        {
            AddClothes(_topPrefab);
            AddClothes(_panstPrefab);
            AddClothes(_shoesPrefab);
            AddClothes(_chestPlatePrefab);
            AddClothes(_armorMaskPrefab);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            AddClothes(_topPrefab);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            AddClothes(_panstPrefab);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            AddClothes(_shoesPrefab);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            AddClothes(_chestPlatePrefab);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            AddClothes(_armorMaskPrefab);
        }
    }

    public void AddClothes(GameObject clothPrefab)
    {
        GameObject clothObj = Instantiate(clothPrefab, _playerSkin.transform.parent);
        SkinnedMeshRenderer[] renderers = clothObj.GetComponentsInChildren<SkinnedMeshRenderer>();
        
        foreach (SkinnedMeshRenderer renderer in renderers)
        {
            renderer.bones = _playerSkin.bones;
            renderer.rootBone = _playerSkin.rootBone;
        }

        _equipedSClothes.Add(clothObj);
    }

    public void RemoveCloth(GameObject searchedClothObject)
    {
        foreach (GameObject clothObj in _equipedSClothes)
        {
            if (clothObj.name.Contains(searchedClothObject.name))
            {
                _equipedSClothes.Remove(clothObj);
                Destroy(clothObj);
                return;
            }
        }
    }
}