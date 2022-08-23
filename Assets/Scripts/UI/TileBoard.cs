using Input;
using Extensions;
using UnityEngine;
using System.Collections.Generic;

namespace UI
{
    public class TileBoard : MonoBehaviour
    {
        private List<ITile> m_Tiles = new List<ITile>();
        private int m_ActualSize;

        [SerializeField] private DecoratedGridLayout m_Grid;
        [SerializeField] private RandomLetterTile m_Prefab;
        [SerializeField] private float shuffleAnimationLength = 2f;
        private float ànimationProgress = 1f;
        private InputPanel m_Input;

        public void Init(InputPanel inputSource)
        {
            m_Input = inputSource;
            m_Input.OnShuffleClick += Shuffle;
            m_Input.OnGenerateClick += SetSize;
        }

        private void Update()
        {
            if(ànimationProgress < 1f)
            {
                ànimationProgress += Time.deltaTime / shuffleAnimationLength;
                AnimateTiles(ànimationProgress);
            }
        }

        private void SetSize(int h, int w)
        {
            if (ànimationProgress < 1f) return;

            m_Grid.SetSize(w, h);

            m_ActualSize = w * h;
            int max = Mathf.Max(m_Tiles.Count, m_ActualSize);
            for (int i = 0; i < max; i++)
            {
                if (i >= m_Tiles.Count)
                {
                    var tile = CreateTile();
                    m_Tiles.Add(tile);
                    tile.SetTarget(m_Grid.Cells[i]);

                }
                m_Tiles[i].SetActive(i < m_ActualSize);
                m_Tiles[i].FitTarget(ànimationProgress);
            }
        }

        private void Shuffle()
        {
            if (ànimationProgress < 1f) return;

            m_Tiles.Shuffle(m_ActualSize);

            for (int i = 0; i < m_ActualSize; i++)
            {
                m_Tiles[i].SetTarget(m_Grid.Cells[i]);
            }
            ànimationProgress = 0f;
        }

        private void AnimateTiles(float progress)
        {
            for (int i = 0; i < m_ActualSize; i++)
            {
                m_Tiles[i].FitTarget(progress);
            }
        }

        private ITile CreateTile()
        {
            var tile = Instantiate(m_Prefab, transform);
            return tile;
        }

        private void OnDestroy() {
            m_Input.OnShuffleClick -= Shuffle;
            m_Input.OnGenerateClick -= SetSize;
        }
    }
}