using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour {

    const int StageTipSize = 30;

    int currentTipIndex;

    //ターゲットキャラクターの指定
    public Transform character;

    //ステージチッププレファブ配列
    public GameObject[] stageTips;

    //自動生成開始インデックス
    public int startTipIndex;

    //生成先読み個数
    public int preInstantiate;

    //生成済みステージチップほじりすと
    public List<GameObject> generatedStageList = new List<GameObject>();

	// Use this for initialization
	void Start () {
        currentTipIndex = startTipIndex - 1;
        UpdateStage(preInstantiate);
	}
	
	// Update is called once per frame
	void Update () {
        //kyラクターの位置から現在のステージチップのインデックスを計算
        int charaPositionIndex = (int)(character.position.z / StageTipSize);

        //次のステージチップに入ったら、ステージの更新を行う
        if(charaPositionIndex+preInstantiate > currentTipIndex)
        {
            UpdateStage(charaPositionIndex + preInstantiate);
        }
    }

    //指定のIndexまでのステージチップを生成して、管理下におく
    void UpdateStage(int toTipIndex)
    {
        if (toTipIndex <= currentTipIndex) return;

        //指定のステージチップまでを作成
        for(int i = currentTipIndex + 1; i <= toTipIndex; i++)
        {
            GameObject stageObject = GenerateStage(i);

            //生成したステージチップを管理リストに追加し
            generatedStageList.Add(stageObject);
        }

        //ステージ保持上限内になるまで古いステージを消去
        while (generatedStageList.Count > preInstantiate + 2) DestroyOldestStage();

        currentTipIndex = toTipIndex;

    }

    //指定のインデックスいちにStageオブジェクトをランダムに生成
    GameObject GenerateStage(int tipIndex)
    {
        int nextStageTip = Random.Range(0, stageTips.Length);

        GameObject stageObject= (GameObject)Instantiate(stageTips[nextStageTip], new Vector3(0, 0, tipIndex * StageTipSize), Quaternion.identity);

        return stageObject;
    }

    //一番古いステージを消去
    void DestroyOldestStage()
    {
        GameObject oldStage = generatedStageList[0];
        generatedStageList.RemoveAt(0);
        Destroy(oldStage);

    }
}

