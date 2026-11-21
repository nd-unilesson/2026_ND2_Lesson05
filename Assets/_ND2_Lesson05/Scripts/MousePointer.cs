using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// マウスポインター
/// -------------------------------------------------
/// |・マウスの座標に合わせて動く                   |
/// -------------------------------------------------
/// </summary>
public class MousePointer : MonoBehaviour
{
    void Start()
    {
        
    }


    void Update()
    {
        Movement();     // 移動の実行
    }


    /// <summary>
    /// 動くメソッド
    /// </summary>
    public void Movement()
    {
        // マウスの座標を取得
        Vector2 mousePosition = Mouse.current.position.value;

        // スクリーン座標系を作る
        Vector3 screenPosition = mousePosition;
        screenPosition.z = 10;

        // マウスの座標をワールド座標に変換する
        // ワールド座標系 = Camera.ScreenToWorldPoint( スクリーン座標系 )
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        // オブジェクトの座標をマウスと合わせる
        transform.position = worldPosition;
    }
}
