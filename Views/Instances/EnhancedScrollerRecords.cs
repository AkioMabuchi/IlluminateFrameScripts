using System;
using System.Collections;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using Enums;
using Structs;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Views.Instances
{
    [RequireComponent(typeof(EnhancedScroller))]
    public class EnhancedScrollerRecords : MonoBehaviour, IEnhancedScrollerDelegate
    {
        [SerializeField] private EnhancedScroller enhancedScroller;

        [SerializeField] private CellViewRecord prefabCellViewRecord;
        
        [SerializeField] private float cellViewRecordHeight;
        [SerializeField] private float loadingCycle;

        private readonly List<CellViewRecordParamsGroup> _recordParamsGroups = new();
        
        private void Reset()
        {
            enhancedScroller = GetComponent<EnhancedScroller>();
        }

        private void Awake()
        {
            enhancedScroller.Delegate = this;
        }

        public void ClearRecords()
        {
            _recordParamsGroups.Clear();
        }
        public void SetRecords(IEnumerable<CellViewRecordParamsGroup> recordParamsGroups)
        {
            _recordParamsGroups.Clear();
            
            foreach (var recordParamsGroup in recordParamsGroups)
            {
                _recordParamsGroups.Add(recordParamsGroup);
            }
        }

        public void Render()
        {
            enhancedScroller.ReloadData();
        }
        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return _recordParamsGroups.Count;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return cellViewRecordHeight;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            var cellView = scroller.GetCellView(prefabCellViewRecord);
            switch (cellView)
            {
                case CellViewRecord cellViewRecord:
                {
                    cellViewRecord.SetParams(_recordParamsGroups[dataIndex]);
                    break;
                }
            }

            return cellView;
        }
    }
}
