using Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Grid
{
    public class TileBoard : MonoBehaviour
    {
        public Action<bool> OnCanResizeChanged = delegate {};
        public Action<bool> OnCanShuffleChanged = delegate {};

        private bool m_CanResize;
        public bool CanResize { get => m_CanResize; private set { m_CanResize = value; OnCanResizeChanged(m_CanResize); } }

        private bool m_CanShuffle;
        public bool CanShuffle { get => m_CanShuffle; private set { m_CanShuffle = value; OnCanShuffleChanged(m_CanShuffle); } }

        private List<ITile> m_Tiles = new List<ITile>();
        private int m_ActualSize;

        [SerializeField] private DecoratedGridLayout m_Grid;
        [SerializeField] private float shuffleAnimationLength = 2f;
        [SerializeField] private GameObjectPool<ITile> m_Pool;
        private float m_AnimationProgress = 1f;

        public void Init()
        {
            m_Pool.Init();
            UpdateState();
        }

        private void UpdateState()
        {
            CanShuffle = m_ActualSize > 1 && m_AnimationProgress >= 1f;
            CanResize = m_AnimationProgress >= 1f;
        }

        public void SetSize(int h, int w)
        {
            if (!CanResize) throw new InvalidOperationException("Board can`t be resized in this state");
            if (w < 0 || h < 0) throw new ArgumentException("Negative arguments not allowed");

            m_Grid.SetSize(w, h);

            m_ActualSize = w * h;

            for (int i = 0; i < m_ActualSize; i++)
            {
                if (i >= m_Tiles.Count) m_Tiles.Add(m_Pool.Get());
                m_Tiles[i].SetTarget(m_Grid.Cells[i]);
                m_Tiles[i].FitTarget(m_AnimationProgress);
            }

            for (int i = m_Tiles.Count - 1; i >= m_ActualSize; i--)
            {
                m_Pool.Return(m_Tiles[i]);
                m_Tiles.RemoveAt(i);
            }

            UpdateState();
        }

        public void Shuffle()
        {
            if (!CanShuffle) throw new InvalidOperationException("Board can`t be shuffled in this state");
            if (CanShuffle) PlayShuffle();
        }

        private async Task PlayShuffle()
        {
            UpdateState();

            m_Tiles.Shuffle(m_ActualSize);

            for (int i = 0; i < m_ActualSize; i++)
            {
                m_Tiles[i].SetTarget(m_Grid.Cells[i]);
            }

            m_AnimationProgress = 0f;

            while (m_AnimationProgress < 1f)
            {
                m_AnimationProgress += Time.deltaTime / shuffleAnimationLength;
                AnimateTiles(m_AnimationProgress);
                await Task.Yield();
            }

            UpdateState();
        }

        private void AnimateTiles(float progress)
        {
            for (int i = 0; i < m_ActualSize; i++)
            {
                m_Tiles[i].FitTarget(progress);
            }
        }
    }
}