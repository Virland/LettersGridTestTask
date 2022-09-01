using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Grid
{
    [ExecuteAlways]
    [RequireComponent(typeof(GridLayout), typeof(RectTransform))]
    public class DecoratedGridLayout : UIBehaviour
    {
        [SerializeField] private RectTransformPool m_Pool;

        public int Width { get => m_Width; }
        public int Height { get => m_Height; }
        public ReadOnlyCollection<RectTransform> Cells
        {
            get { return m_ReadOnlyCells; }
        }

        private int m_Width;
        private int m_Height;
        private RectTransform m_RTForm;
        private GridLayoutGroup m_InternalGridLayout;
        private List<RectTransform> m_Cells;
        private ReadOnlyCollection<RectTransform> m_ReadOnlyCells;

        private void Awake()
        {
            m_Cells = new List<RectTransform>();
            m_ReadOnlyCells = m_Cells.AsReadOnly();
            m_RTForm = GetComponent<RectTransform>();
            m_InternalGridLayout = GetComponent<GridLayoutGroup>();

            m_InternalGridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        }

        public void SetSize(int width, int height)
        {
            if (width <= 0 || height <= 0) throw new ArgumentException($"width:{width} height:{height}");
            if (this.m_Width == width && this.m_Height == height) return;

            this.m_Width = width;
            this.m_Height = height;

            m_InternalGridLayout.constraintCount = width;

            UpdateCellsCount();
            RecalculateCellsDimension();
        }

        private void UpdateCellsCount()
        {
            int newCellCount = m_Width * m_Height;

            for (int i = 0; i < newCellCount; i++)
            {
                if (i >= m_Cells.Count)
                {
                    m_Cells.Add(m_Pool.Get());
                    m_Cells[i].SetParent(transform, false);
                }
            }

            for (int i = m_Cells.Count - 1; i >= newCellCount; i--)
            {
                Debug.Log(m_Cells.Count + " " + newCellCount + " " + i);
                m_Pool.Return(m_Cells[i]);
                m_Cells.RemoveAt(i);
            }
        }

        private void OnRectTransformDimensionsChange()
        {
            RecalculateCellsDimension();
        }

        private void RecalculateCellsDimension()
        {
            if (!m_RTForm || !m_InternalGridLayout) return;
            if (m_InternalGridLayout.constraint == GridLayoutGroup.Constraint.Flexible) return;

            int height = 0, width = 0, activeChildCount = 0;
            foreach (RectTransform child in m_RTForm)
                if (child.gameObject.activeInHierarchy)
                    activeChildCount++;

            if (activeChildCount == 0) return;

            if (m_InternalGridLayout.constraint == GridLayoutGroup.Constraint.FixedRowCount)
            {
                height = m_InternalGridLayout.constraintCount;
                width = activeChildCount / height;
            }
            else if (m_InternalGridLayout.constraint == GridLayoutGroup.Constraint.FixedColumnCount)
            {
                width = m_InternalGridLayout.constraintCount;
                height = activeChildCount / width;
            }

            if (width <= 0 || height <= 0) return;
            float size = Mathf.Min(
                m_RTForm.rect.width / width,
                m_RTForm.rect.height / height);
            m_InternalGridLayout.cellSize = new Vector2(size, size);

            LayoutRebuilder.ForceRebuildLayoutImmediate(m_RTForm);
        }
    }
}