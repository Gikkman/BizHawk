﻿using System;
using System.ComponentModel;

using Newtonsoft.Json;

using BizHawk.Common;
using BizHawk.Emulation.Common;
using BizHawk.Emulation.Cores.Nintendo.GBHawk;

namespace BizHawk.Emulation.Cores.Nintendo.GBHawkLink4x
{
	public partial class GBHawkLink4x : IEmulator, IStatable, ISettable<GBHawkLink4x.GBLink4xSettings, GBHawkLink4x.GBLink4xSyncSettings>
	{
		public GBLink4xSettings GetSettings()
		{
			return Link4xSettings.Clone();
		}

		public GBLink4xSyncSettings GetSyncSettings()
		{
			return Link4xSyncSettings.Clone();
		}

		public bool PutSettings(GBLink4xSettings o)
		{
			Link4xSettings = o;
			return false;
		}

		public bool PutSyncSettings(GBLink4xSyncSettings o)
		{
			bool ret = GBLink4xSyncSettings.NeedsReboot(Link4xSyncSettings, o);
			Link4xSyncSettings = o;
			return ret;
		}

		private GBLink4xSettings Link4xSettings = new GBLink4xSettings();
		public GBLink4xSyncSettings Link4xSyncSettings = new GBLink4xSyncSettings();

		public class GBLink4xSettings
		{
			[DisplayName("Color Mode")]
			[Description("Pick Between Green scale and Grey scale colors")]
			[DefaultValue(GBHawk.GBHawk.GBSettings.PaletteType.BW)]
			public GBHawk.GBHawk.GBSettings.PaletteType Palette_A { get; set; }

			[DisplayName("Color Mode")]
			[Description("Pick Between Green scale and Grey scale colors")]
			[DefaultValue(GBHawk.GBHawk.GBSettings.PaletteType.BW)]
			public GBHawk.GBHawk.GBSettings.PaletteType Palette_B { get; set; }

			[DisplayName("Color Mode")]
			[Description("Pick Between Green scale and Grey scale colors")]
			[DefaultValue(GBHawk.GBHawk.GBSettings.PaletteType.BW)]
			public GBHawk.GBHawk.GBSettings.PaletteType Palette_C { get; set; }

			[DisplayName("Color Mode")]
			[Description("Pick Between Green scale and Grey scale colors")]
			[DefaultValue(GBHawk.GBHawk.GBSettings.PaletteType.BW)]
			public GBHawk.GBHawk.GBSettings.PaletteType Palette_D { get; set; }

			public enum AudioSrc
			{
				A,
				B,
				C,
				D,
				None
			}

			[DisplayName("Audio Selection")]
			[Description("Choose Audio Source. Both will produce Stereo sound.")]
			[DefaultValue(AudioSrc.A)]
			public AudioSrc AudioSet { get; set; }

			public GBLink4xSettings Clone()
			{
				return (GBLink4xSettings)MemberwiseClone();
			}
		}

		public class GBLink4xSyncSettings
		{
			[DisplayName("Console Mode A")]
			[Description("Pick which console to run, 'Auto' chooses from ROM extension, 'GB' and 'GBC' chooses the respective system")]
			[DefaultValue(GBHawk.GBHawk.GBSyncSettings.ConsoleModeType.Auto)]
			public GBHawk.GBHawk.GBSyncSettings.ConsoleModeType ConsoleMode_A { get; set; }

			[DisplayName("Console Mode B")]
			[Description("Pick which console to run, 'Auto' chooses from ROM extension, 'GB' and 'GBC' chooses the respective system")]
			[DefaultValue(GBHawk.GBHawk.GBSyncSettings.ConsoleModeType.Auto)]
			public GBHawk.GBHawk.GBSyncSettings.ConsoleModeType ConsoleMode_B { get; set; }

			[DisplayName("Console Mode C")]
			[Description("Pick which console to run, 'Auto' chooses from ROM extension, 'GB' and 'GBC' chooses the respective system")]
			[DefaultValue(GBHawk.GBHawk.GBSyncSettings.ConsoleModeType.Auto)]
			public GBHawk.GBHawk.GBSyncSettings.ConsoleModeType ConsoleMode_C { get; set; }

			[DisplayName("Console Mode D")]
			[Description("Pick which console to run, 'Auto' chooses from ROM extension, 'GB' and 'GBC' chooses the respective system")]
			[DefaultValue(GBHawk.GBHawk.GBSyncSettings.ConsoleModeType.Auto)]
			public GBHawk.GBHawk.GBSyncSettings.ConsoleModeType ConsoleMode_D { get; set; }

			[DisplayName("CGB in GBA")]
			[Description("Emulate GBA hardware running a CGB game, instead of CGB hardware.  Relevant only for titles that detect the presense of a GBA, such as Shantae.")]
			[DefaultValue(false)]
			public bool GBACGB { get; set; }

			[DisplayName("RTC Initial Time A")]
			[Description("Set the initial RTC time in terms of elapsed seconds.")]
			[DefaultValue(0)]
			public int RTCInitialTime_A
			{
				get { return _RTCInitialTime_A; }
				set { _RTCInitialTime_A = Math.Max(0, Math.Min(1024 * 24 * 60 * 60, value)); }
			}

			[DisplayName("RTC Initial Time B")]
			[Description("Set the initial RTC time in terms of elapsed seconds.")]
			[DefaultValue(0)]
			public int RTCInitialTime_B
			{
				get { return _RTCInitialTime_B; }
				set { _RTCInitialTime_B = Math.Max(0, Math.Min(1024 * 24 * 60 * 60, value)); }
			}

			[DisplayName("RTC Initial Time C")]
			[Description("Set the initial RTC time in terms of elapsed seconds.")]
			[DefaultValue(0)]
			public int RTCInitialTime_C
			{
				get { return _RTCInitialTime_C; }
				set { _RTCInitialTime_C = Math.Max(0, Math.Min(1024 * 24 * 60 * 60, value)); }
			}

			[DisplayName("RTC Initial Time D")]
			[Description("Set the initial RTC time in terms of elapsed seconds.")]
			[DefaultValue(0)]
			public int RTCInitialTime_D
			{
				get { return _RTCInitialTime_D; }
				set { _RTCInitialTime_D = Math.Max(0, Math.Min(1024 * 24 * 60 * 60, value)); }
			}

			[DisplayName("Timer Div Initial Time A")]
			[Description("Don't change from 0 unless it's hardware accurate. GBA GBC mode is known to be 8.")]
			[DefaultValue(8)]
			public int DivInitialTime_A
			{
				get { return _DivInitialTime_A; }
				set { _DivInitialTime_A = Math.Min((ushort)65535, (ushort)value); }
			}

			[DisplayName("Timer Div Initial Time B")]
			[Description("Don't change from 0 unless it's hardware accurate. GBA GBC mode is known to be 8.")]
			[DefaultValue(8)]
			public int DivInitialTime_B
			{
				get { return _DivInitialTime_B; }
				set { _DivInitialTime_B = Math.Min((ushort)65535, (ushort)value); }
			}

			[DisplayName("Timer Div Initial Time C")]
			[Description("Don't change from 0 unless it's hardware accurate. GBA GBC mode is known to be 8.")]
			[DefaultValue(8)]
			public int DivInitialTime_C
			{
				get { return _DivInitialTime_C; }
				set { _DivInitialTime_C = Math.Min((ushort)65535, (ushort)value); }
			}

			[DisplayName("Timer Div Initial Time D")]
			[Description("Don't change from 0 unless it's hardware accurate. GBA GBC mode is known to be 8.")]
			[DefaultValue(8)]
			public int DivInitialTime_D
			{
				get { return _DivInitialTime_D; }
				set { _DivInitialTime_D = Math.Min((ushort)65535, (ushort)value); }
			}

			[DisplayName("Use Existing SaveRAM")]
			[Description("When true, existing SaveRAM will be loaded at boot up")]
			[DefaultValue(false)]
			public bool Use_SRAM { get; set; }

			[JsonIgnore]
			private int _RTCInitialTime_A;
			private int _RTCInitialTime_B;
			private int _RTCInitialTime_C;
			private int _RTCInitialTime_D;
			[JsonIgnore]
			public ushort _DivInitialTime_A = 8;
			public ushort _DivInitialTime_B = 8;
			public ushort _DivInitialTime_C = 8;
			public ushort _DivInitialTime_D = 8;

			public GBLink4xSyncSettings Clone()
			{
				return (GBLink4xSyncSettings)MemberwiseClone();
			}

			public static bool NeedsReboot(GBLink4xSyncSettings x, GBLink4xSyncSettings y)
			{
				return !DeepEquality.DeepEquals(x, y);
			}
		}
	}
}
