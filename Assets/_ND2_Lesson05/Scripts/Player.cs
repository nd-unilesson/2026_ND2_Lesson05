using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤー
/// --------------------------------------------
/// ・操作方法
///     - WASD      : 上下左右に移動
///     - マウス    : 向きを変える（マウスの方向を見る）
///     - 左クリック: ショット
///     
/// ・敵との接触で「死ぬ」
/// </summary>
public class Player : MonoBehaviour
{
    // === フィールド === //
    [Header("* * * 移動設定 * * *")]
    [SerializeField] private float _moveSpeed = 5.0f;       // 移動速度

    [Header("* * * 振りむき設定 * * *")]
    [SerializeField] private Transform _lookTarget;         // 向くターゲット

    [Header("* * * ショット設定 * * *")]
    [SerializeField] private GameObject _bulletPrefab;      // 弾丸のプレハブ
    [SerializeField] private Transform _shotPoint;          // 弾丸の出現位置
    [SerializeField] private float _shotPower;              // 弾丸の射出力

    private Vector3 _moveVector = new Vector3(0, 0, 0);     // 移動ベクトル



    void Start()
    {
        
    }


    void Update()
    {
        Movement();     // 移動の実行
        LookAt();       // 振りむきの実行
        Shot();         // 攻撃の実行
    }


    /// <summary>
    /// 動くメソッド
    /// </summary>
    public void Movement()
    {
        // 移動ベクトルをリセット
        _moveVector = new Vector3(0, 0, 0);

        if(Keyboard.current.wKey.IsPressed())
        {
            _moveVector = new Vector3(0, 0, 1);
        }

        if(Keyboard.current.aKey.IsPressed())
        {
            _moveVector = new Vector3(-1, 0, 0);
        }

        if(Keyboard.current.sKey.IsPressed())
        {
            _moveVector = new Vector3(0, 0, -1);
        }

        if(Keyboard.current.dKey.IsPressed())
        {
            _moveVector = new Vector3(1, 0, 0);
        }

        // 移動
        transform.Translate(_moveVector * _moveSpeed * Time.deltaTime, Space.World);
        
        // 移動ベクトルの可視化
        Debug.DrawRay(transform.position, _moveVector, Color.green, 0, false);
    }


    /// <summary>
    /// 方向を向くメソッド
    /// </summary>
    public void LookAt()
    {
        // 振りむく
        // LookAt( ターゲット, 軸 );
        transform.LookAt( _lookTarget, Vector3.up );

        // 向きベクトルの可視化
        Debug.DrawRay(transform.position, transform.forward, Color.blue, 0, false);
    }


    /// <summary>
    /// 攻撃メソッド
    /// </summary>
    public void Shot()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // 弾丸の複製
            GameObject bullet = Instantiate(_bulletPrefab, _shotPoint.position, _shotPoint.rotation);

            // 射出
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();  // 弾丸の< Rigidbody >を取得
            bulletRb.AddForce(_shotPoint.forward * _shotPower, ForceMode.Impulse);

            // 数秒で消える
            Destroy(bullet, 5.0f);
        }
    }
}
