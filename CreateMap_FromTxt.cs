using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;

public class CreateMap_FromTxt : MonoBehaviour
{
    //天候変化回数設定用
    private PlayerController playerController;
    private GameObject player;
    public int ChangeCnt;

    public GameObject mapChip;

    float MapChipSize = 0.64f;
    private int MapSize_X = 30;
    private int MapSize_Y = 17;
    private int stageNum;
    private GameObject gameDirector;
    private string[] mapData;

    private void LoadMap_FromTextFile(string filename)
    {
        string txtMapData = "";
        FileInfo fi = new FileInfo(Application.dataPath + "/StreamingAssets/" + "/MapData/" + filename);

        try
        {
            // 一行毎読み込み
            using (StreamReader sr = new StreamReader(fi.OpenRead(), Encoding.UTF8))
            {
                txtMapData = sr.ReadToEnd();
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.Message);
        }

        if (txtMapData.Length != 0)
        {
            // 空の要素を削除するためのオプション.
            System.StringSplitOptions option = System.StringSplitOptions.RemoveEmptyEntries;

            // 改行コードで１行ごとに切り出す..
            string[] lines = txtMapData.Split(new char[] { '\r', '\n' }, option);

            // 一行目はマップの大きさ.
            // "," で１区間ごとに切り出す.
            string[] sizewh = lines[0].Split(new char[] { ',' }, option);
            this.MapSize_X = int.Parse(sizewh[0]);
            this.MapSize_Y = int.Parse(sizewh[1]);

            string[] mapdata = new string[this.MapSize_Y * this.MapSize_X];

            for (int y = 0; y < this.MapSize_Y; ++y)
            {
                // "," で１区間ごとに切り出す.
                string[] data = lines[1 + y].Split(new char[] { ',' }, option);   //一行目はマップの大きさなので２行目から

                for (int x = 0; x < this.MapSize_X; ++x)
                {
                    mapdata[y * MapSize_X + x] = data[x];
                }
            }

            // ゲームで使う配列に丸ごとコピー
            this.mapData = mapdata;
        }
        else
        {
            //テキストが空だった
            Debug.LogWarning("txtMapData is null");
        }
    }
    private void SetWalls()
    {
        Vector2 vec = Vector2.zero;
        for (int y = 0; y < this.MapSize_Y; ++y)
        {
            for (int x = 0; x < this.MapSize_X; ++x)
            {
                //該当マスの位置を先に計算しておく
                vec.Set((x * MapChipSize) - 9.25f, ((this.MapSize_Y - 1 - y) * MapChipSize) - 5.0f);
                GameObject go;
                //マップデータのチップ番号を取得
                string index = this.mapData[y * this.MapSize_X + x];
                //チップ番号を int型 へ変換
                int chipNum = Convert.ToInt32(index);
                if (chipNum != 0)
                {
                    //チップを生成
                    go =
                        Instantiate(mapChip.transform.GetChild(chipNum).gameObject, vec, Quaternion.identity) as GameObject;
                    // 作成したオブジェクトを子として登録
                    go.transform.parent = transform;
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        this.gameDirector = GameObject.FindWithTag("GameManager");
        this.stageNum = gameDirector.GetComponent<GameManagement>().nowStageNum;
        string num = stageNum.ToString();
        LoadMap_FromTextFile("Map" + num + ".txt");	//テキストファイルからマップをロードする

        SetWalls();

        SetChangeCnt();
    }

    private void SetChangeCnt()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();

        switch (this.stageNum)
        {
            //1種類の天候でクリアできるステージ
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
                //this.ChangeCnt = 10;
                playerController.player.changeCnt = 6;
                break;

            //複数の天候が必要なステージ
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
                //this.ChangeCnt = 20;
                playerController.player.changeCnt = 30;
                break;
        }    }
}
