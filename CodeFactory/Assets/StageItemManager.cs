using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StageItemManager : MonoBehaviour
{
    public enum StageItemType
    {
        None = -1,
        NoPaintRocket = 0,
        PaintRocket = 1,
        NoPaintRubberDuck = 2,
        PaintRubberDuck = 3,
        MiddleShield = 4,
        BigShield = 5,
        BasketBall = 6,
        InputWood = 7,
        OutputWood = 8,
        Block_A = 9,
    }
    
    [SerializeField] private GameObject[] stageItems;
    [SerializeField] private Transform[] respawnItemPosition;
    
    [SerializeField] private Transform rocketRoot;
    [SerializeField] private Transform duckRoot;
    [SerializeField] private Transform shieldRoot;
    [SerializeField] private Transform basketBallRoot;
    [SerializeField] private Transform woodBlockRoot;

    private readonly Dictionary<StageItemType, Action> _itemSpawnFunctions = new Dictionary<StageItemType, Action>();
    private readonly Dictionary<StageItemType, Transform> _itemRoot = new Dictionary<StageItemType, Transform>();

    private void Awake()
    {
        // StageItemType에 따라 함수를 생성하여 딕셔너리에 추가
        foreach (StageItemType itemType in Enum.GetValues(typeof(StageItemType)))
        {
            int index = (int)itemType;

            // 함수를 생성하여 딕셔너리에 추가
            _itemSpawnFunctions.Add(itemType, () => SpawnItem(index));
        }
    }

    public void OnSelectItemEvent(StageItemType stageItemType)
    {
        // 딕셔너리에서 해당하는 StageItemType의 함수를 실행
        if (_itemSpawnFunctions.TryGetValue(stageItemType, out Action spawnFunction))
        {
            spawnFunction.Invoke();
        }
    }

    private void SpawnItem(int itemIndex)
    {
        if (itemIndex >= 0 && itemIndex < stageItems.Length && itemIndex < respawnItemPosition.Length)
        {
            // 아이템 생성과 동시에 해당 아이템 애니메이션 재생
            StageItem item = Instantiate(stageItems[itemIndex], respawnItemPosition[itemIndex].position, Quaternion.identity).GetComponent<StageItem>();
            Transform root = GetItemRoot(itemIndex);

            if (root == null) Debug.LogError("생성된 아이템의 부모 위치를 찾을 수 없습니다!");
            
            item.transform.SetParent(root, false); // 생성된 아이템 부모 설정
            item.ControllerItemAnimation(true);
            Debug.Log(item.gameObject.name + " 생성 완료");
        }
        else
        {
            Debug.LogError("생성할 아이템과 위치 인덱스를 찾을 수 없습니다!");
        }
    }

    private Transform GetItemRoot(int itemIndex)
    {
        switch (itemIndex)
        {
            case <= 1:
                return rocketRoot;
            case <= 3:
                return duckRoot;
            case <= 5:
                return shieldRoot;
            case 6:
                return basketBallRoot;
            case <= 9:
                return woodBlockRoot;
            
            default: return null;
        }
    }
    
}
