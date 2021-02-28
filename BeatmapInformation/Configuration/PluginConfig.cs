using System;
using System.Runtime.CompilerServices;
using IPA.Config.Stores;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace BeatmapInformation.Configuration
{
    internal class PluginConfig
    {
        public static PluginConfig Instance { get; set; }
        public virtual bool Enable { get; set; } = true;
        public virtual bool CoverVisible { get; set; } = true;
        public virtual float CoverPivotPos { get; set; } = 0.75f;
        public virtual float CoverSize { get; set; } = 40f;
        public virtual int SongNameFontSize { get; set; } = 13;
        public virtual int SongSubNameFontSize { get; set; } = 5;
        public virtual int SongAuthorNameFontSize { get; set; } = 5;
        public virtual bool ScoreVisible { get; set; } = true;
        public virtual int ScoreFontSize { get; set; } = 12;
        public virtual bool ComboVisible { get; set; } = true;
        public virtual int ComboFontSize { get; set; } = 7;
        public virtual bool SeidoVisible { get; set; } = true;
        public virtual int SeidoFontSize { get; set; } = 10;
        public virtual bool RankVisible { get; set; } = true;
        public virtual int RankFontSize { get; set; } = 10;
        public virtual bool DifficulityLabelVisible { get; set; } = true;
        public virtual int DifficulityLabelFontSize { get; set; } = 5;
        public virtual float SubTextSpacing { get; set; } = -1;
        public virtual float ScoreTextSpacing { get; set; } = -5;
        public virtual float RankTextSpacing { get; set; } = -5;
        //public virtual float ScreenScale { get; set; } = 0f;
        public virtual float ScreenPosX { get; set; } = 0f;
        public virtual float ScreenPosY { get; set; } = 0.7f;
        public virtual float ScreenPosZ { get; set; } = -1.1f;
        public virtual float ScreenRotX { get; set; } = 0;
        public virtual float ScreenRotY { get; set; } = 0;
        public virtual float ScreenRotZ { get; set; } = 0;
        public virtual float ScreenRotW { get; set; } = 0;

        public event Action<PluginConfig> OnReloaded;
        public event Action<PluginConfig> OnChenged;
        /// <summary>
        /// This is called whenever BSIPA reads the config from disk (including when file changes are detected).
        /// </summary>
        public virtual void OnReload()
        {
            // Do stuff after config is read from disk.
            this.OnReloaded?.Invoke(this);
        }

        /// <summary>
        /// Call this to force BSIPA to update the config file. This is also called by BSIPA if it detects the file was modified.
        /// </summary>
        public virtual void Changed()
        {
            // Do stuff when the config is changed.
            this.OnChenged?.Invoke(this);
        }

        /// <summary>
        /// Call this to have BSIPA copy the values from <paramref name="other"/> into this config.
        /// </summary>
        public virtual void CopyFrom(PluginConfig other)
        {
            // This instance's members populated from other
            this.Enable = other.Enable;
            this.CoverVisible = other.CoverVisible;
            this.CoverPivotPos = other.CoverPivotPos;
            this.CoverSize = other.CoverSize;
            this.SongNameFontSize = other.SongNameFontSize;
            this.SongSubNameFontSize = other.SongSubNameFontSize;
            this.SongAuthorNameFontSize = other.SongAuthorNameFontSize;
            this.ScoreVisible = other.ScoreVisible;
            this.ScoreFontSize = other.ScoreFontSize;
            this.ComboVisible = other.ComboVisible;
            this.ComboFontSize = other.ComboFontSize;
            this.SeidoVisible = other.SeidoVisible;
            this.SeidoFontSize = other.SeidoFontSize;
            this.RankVisible = other.RankVisible;
            this.RankFontSize = other.RankFontSize;
            this.DifficulityLabelVisible = other.DifficulityLabelVisible;
            this.DifficulityLabelFontSize = other.DifficulityLabelFontSize;
            this.SubTextSpacing = other.SubTextSpacing;
            this.ScoreTextSpacing = other.ScoreTextSpacing;
            this.RankTextSpacing = other.RankTextSpacing;
            //this.ScreenScale = other.ScreenScale;
            this.ScreenPosX = other.ScreenPosX;
            this.ScreenPosY = other.ScreenPosY;
            this.ScreenPosZ = other.ScreenPosZ;
            this.ScreenRotX = other.ScreenRotX;
            this.ScreenRotY = other.ScreenRotY;
            this.ScreenRotZ = other.ScreenRotZ;
            this.ScreenRotW = other.ScreenRotW;
        }
    }
}