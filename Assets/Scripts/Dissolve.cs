using System.Collections;
using UnityEngine;

namespace ShaderGraphSample
{
    public class Dissolve : MonoBehaviour
    {
        // シェーダーを適用するマテリアル
        private Material material;

        // シェーダーのプロパティ名
        private const string PropertyName = "_ClipTime";

        // コルーチンの実行状態
        private bool isRunning = false;

        private void Start()
        {
            material = GetComponent<MeshRenderer>().material;
        }

        // 消滅開始
        public void DissolveStart()
        {
            // コルーチンが既に実行中かどうか確認
            if (!isRunning)
            {
                // コルーチンを開始
                StartCoroutine(DissolveCoroutine());
            }
        }

        // コルーチンで徐々に値を変化させる
        private IEnumerator DissolveCoroutine()
        {
            isRunning = true;

            float duration = 1.0f; // アニメーション継続時間
            float elapsedTime = 0.0f;

            while (elapsedTime < duration)
            {
                // 経過時間を更新
                elapsedTime += Time.deltaTime;

                // newValueを0から1に変化させる
                var newValue = Mathf.Lerp(0.0f, 1.0f, elapsedTime / duration);

                // マテリアルのプロパティ設定
                material?.SetFloat(PropertyName, newValue);

                // 次のフレームまで待つ
                yield return null;
            }

            // 最終的な値を設定
            material.SetFloat(PropertyName, 1.0f);

            // 3秒待機
            yield return new WaitForSeconds(3.0f);

            // 再表示
            material.SetFloat(PropertyName, 0.0f);

            // コルーチンの実行状態を解除
            isRunning = false;
        }
    }
}
