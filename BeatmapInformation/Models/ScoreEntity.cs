using Zenject;

namespace BeatmapInformation.Models
{
    public class ScoreEntity
    {
        public int Score { get; private set; }
        public int Combo { get; private set; }
        public float Seido { get; private set; }
        public string Rank { get; private set; }
        public void Clear()
        {
            this.Score = 0;
            this.Combo = 0;
            this.Seido = 0;
            this.Rank = "SS";
        }

        public void Set(int score, int combo, float seido, RankModel.Rank rank)
        {
            this.Score = score;
            this.Combo = combo;
            this.Seido = seido;
            this.Rank = RankModel.GetRankName(rank);
        }
        public class Pool : MemoryPool<ScoreEntity>
        {
            protected override void Reinitialize(ScoreEntity item) => item.Clear();
        }
    }
}
