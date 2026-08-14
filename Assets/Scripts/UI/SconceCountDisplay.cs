using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class SconceCountDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text CountText;

        private void Update()
        {
            CountText.text = $"SCONCES : {GameManager.Instance.LitSconceCount} / {GameManager.Instance.TotalSconceCount}";
        }
    }
}