using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class IntroduceButton : Button, IInteractable
{
    private static int introducePlankSpawnCount;
    [SerializeField] private Transform[] plankSpawnPoints;
    [SerializeField] private Introduce[] introduces;
    
    [SerializeField] private GameObject introducePlankPrefab;

    private void Awake()
    {
        introducePlankSpawnCount = 0;
    }

    public void Interact()
    {
        if (isPushed) return;
        
        if (pushButtonCoroutine != null)
        {
            StopCoroutine(pushButtonCoroutine);
        }
        pushButtonCoroutine = StartCoroutine(PushButton());

        StartCoroutine(SpawnPlank());
    }

    private IEnumerator SpawnPlank()
    {
        if (introducePlankSpawnCount >= 10) yield break;
        
        introducePlankSpawnCount++;
        
        List<int> randomSpawnPointIndex = 
            Enumerable.Range(0, plankSpawnPoints.Length)
                .OrderBy(x => Random.value)
                .Take(introduces.Length)
                .ToList();
        
        for (int i = 0; i < introduces.Length; i++)
        {
            Vector3 position = plankSpawnPoints[randomSpawnPointIndex[i]].position;
            Quaternion rotation = Quaternion.Euler(90,0,Random.Range(1, 360));
            
            Transform plank = Instantiate(introducePlankPrefab, position, rotation).transform;
            if (plank.GetChild(0).TryGetComponent(out TextMeshPro introducePlankText))
            {
                introducePlankText.text = introduces[i].text;
                switch (introducePlankSpawnCount)
                {
                    case 7:
                        introducePlankText.text = "그만 꺼내라...";
                        yield break;
                    case 8:
                        introducePlankText.text = "계속 하겠다 이거지?";
                        yield break;
                    case 9:
                        introducePlankText.text = "마지막 경고다. 꺼내지 마라...";
                        yield break;
                    case 10:
                        introducePlankText.text = "!@#$%^&*|@#!@#|#!@)*&^%$#@#$%|#!@^&*@!#!@^%#^!@%&#*^#@!!@*#&*!@%$";
                        //  Something Event
                        yield break;
                }
            }
            else
            {
                throw new Exception("소개 팻말에 Text 컴포넌트가 없는뎁쇼...");
            }
            
            yield return new WaitForSeconds(0.3f);
        }
    }
}

[Serializable]
public struct Introduce
{
    [TextArea]
    public string text;
}