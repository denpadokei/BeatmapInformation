using BeatmapInformation.AudioSpectrums;
using BeatmapInformation.Models;
using IPA.Config.Stores;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace BeatmapInformation.Configuration
{
    internal class PluginConfig
    {
        public static PluginConfig Instance { get; set; }
        public virtual bool Enable { get; set; } = true;
        public virtual bool LockPosition { get; set; } = false;
        public virtual bool ChangeScale { get; set; } = false;
        public virtual float ScreenScale { get; set; } = 0.02f;
        public virtual float ScreenRadius { get; set; } = 0f;
        public virtual bool SongTimerVisible { get; set; } = true;
        public virtual float SontTimeRingScale { get; set; } = 1.7f;
        public virtual float SongTimeTextFontSize { get; set; } = 7f;
        public virtual bool CoverVisible { get; set; } = true;
        public virtual float CoverPivotPos { get; set; } = 0.75f;
        public virtual float CoverAlpha { get; set; } = 0.75f;
        public virtual float CoverSize { get; set; } = 40f;
        public virtual float SongNameFontSize { get; set; } = 13;
        public virtual float SongSubNameFontSize { get; set; } = 5;
        public virtual float SongAuthorNameFontSize { get; set; } = 5;
        public virtual bool ScoreVisible { get; set; } = true;
        public virtual float ScoreFontSize { get; set; } = 12;
        public virtual bool ComboVisible { get; set; } = true;
        public virtual float ComboFontSize { get; set; } = 7;
        public virtual bool SeidoVisible { get; set; } = true;
        public virtual float SeidoFontSize { get; set; } = 10;
        public virtual bool RankVisible { get; set; } = true;
        public virtual float RankFontSize { get; set; } = 10;
        public virtual float TextSpaceHeight { get; set; } = 105f;
        public virtual bool DifficulityLabelVisible { get; set; } = true;
        public virtual float DifficulityLabelFontSize { get; set; } = 5f;
        public virtual float SubTextSpacing { get; set; } = -1;
        public virtual float ScoreTextSpacing { get; set; } = -5;
        public virtual float RankTextSpacing { get; set; } = -5;
        public virtual bool AudioSpectrumVisible { get; set; } = true;
        public virtual float AudioSpectrumAlpha { get; set; } = 0.8f;
        public virtual string BandType { get; set; } = AudioSpectrum.BandType.ThirtyOneBand.ToString();
        public virtual float ScreenPosX { get; set; } = 0f;
        public virtual float ScreenPosY { get; set; } = 0.7f;
        public virtual float ScreenPosZ { get; set; } = -1.1f;
        public virtual float ScreenRotX { get; set; } = 0;
        public virtual float ScreenRotY { get; set; } = 0;
        public virtual float ScreenRotZ { get; set; } = 0;
        public virtual string SongNameFormat { get; set; } = TextFormatter.SONG_NAME;
        public virtual string SongSubNameFormat { get; set; } = TextFormatter.SONG_SUB_NAME;
        public virtual string SongAuthorNameFormat { get; set; } = TextFormatter.SONG_AUTHOR_NAME;
        public virtual string DifficurityFormat { get; set; } = TextFormatter.DIFFICURITY;
        public virtual string ScoreFormat { get; set; } = TextFormatter.SCORE;
        public virtual string ComboFormat { get; set; } = $"{TextFormatter.COMBO} <size=50%>COMBO</size>";
        public virtual string SeidoFormat { get; set; } = $"{TextFormatter.SEIDO} %";
        public virtual string RankFormat { get; set; } = TextFormatter.RANK;

        public event Action<PluginConfig> OnReloaded;
        public event Action<PluginConfig> OnChenged;
        /// <summary>
        /// This is called whenever BSIPA reads the config from disk (including when file changes are detected).
        /// </summary>
        public virtual void OnReload() =>
            // Do stuff after config is read from disk.
            this.OnReloaded?.Invoke(this);

        /// <summary>
        /// Call this to force BSIPA to update the config file. This is also called by BSIPA if it detects the file was modified.
        /// </summary>
        public virtual void Changed()
        {
            // Do stuff when the config is changed.
        }

        /// <summary>
        /// Call this to have BSIPA copy the values from <paramref name="other"/> into this config.
        /// </summary>
        public virtual void CopyFrom(PluginConfig other)
        {
            // This instance's members populated from other
            this.Enable = other.Enable;
            this.LockPosition = other.LockPosition;
            this.ChangeScale = other.ChangeScale;
            this.ScreenScale = other.ScreenScale;
            this.ScreenRadius = other.ScreenRadius;
            this.CoverVisible = other.CoverVisible;
            this.CoverAlpha = other.CoverAlpha;
            this.CoverPivotPos = other.CoverPivotPos;
            this.CoverSize = other.CoverSize;
            this.SongTimerVisible = other.SongTimerVisible;
            this.SontTimeRingScale = other.SontTimeRingScale;
            this.SongTimeTextFontSize = other.SongTimeTextFontSize;
            this.TextSpaceHeight = other.TextSpaceHeight;
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
            this.AudioSpectrumVisible = other.AudioSpectrumVisible;
            this.AudioSpectrumAlpha = other.AudioSpectrumAlpha;
            this.ScreenPosX = other.ScreenPosX;
            this.ScreenPosY = other.ScreenPosY;
            this.ScreenPosZ = other.ScreenPosZ;
            this.ScreenRotX = other.ScreenRotX;
            this.ScreenRotY = other.ScreenRotY;
            this.ScreenRotZ = other.ScreenRotZ;
            this.SongNameFormat = other.SongNameFormat;
            this.SongSubNameFormat = other.SongSubNameFormat;
            this.SongAuthorNameFormat = other.SongAuthorNameFormat;
            this.DifficurityFormat = other.DifficurityFormat;
            this.ScoreFormat = other.ScoreFormat;
            this.ComboFormat = other.ComboFormat;
            this.SeidoFormat = other.SeidoFormat;
            this.RankFormat = other.RankFormat;
        }
    }
}