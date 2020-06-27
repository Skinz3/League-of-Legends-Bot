using LeagueBot.DesignPattern;
using LeagueBot.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBot.Game.Settings
{
    class LeagueManager
    {
        const string CONFIG_PATH = @"League of Legends\Config\game.cfg";

        const string KEYCONFIG_PATH = @"League of Legends\Config\input.ini";

        const string PERSISTED_CONFIG_PATH = @"League of Legends\Config\PersistedSettings.json";

        //[StartupInvoke("League settings", StartupInvokePriority.FourthPass, false)]
        public static void ApplySettings()
        {
            CFGFile config = new CFGFile(Path.Combine(Configuration.Instance.ClientPath, CONFIG_PATH));

            config.Set("General", "WindowMode", "1");
            config.Set("General", "Width", "1024");
            config.Set("General", "Height", "768");
            config.Set("General", "Colors", "32");
            config.Set("General", "RelativeTeamColors", "1");
            config.Set("General", "UserSetResolution", "1");
            config.Set("General", "Antialiasing", "0");
            config.Set("General", "AutoAcquireTarget", "1");


            config.Set("Performance", "GraphicsSlider", "6");
            config.Set("Performance", "ShadowsEnabled", "1");
            config.Set("Performance", "CharacterInking", "1");
            config.Set("Performance", "EnableHUDAnimations", "0");
            config.Set("Performance", "EnableParticleOptimizations", "0");
            config.Set("Performance", "BudgetOverdrawAverage", "10");
            config.Set("Performance", "BudgetSkinnedVertexCount", "200000");
            config.Set("Performance", "BudgetSkinnedDrawCallCount", "100");
            config.Set("Performance", "BudgetTextureUsage", "150000");
            config.Set("Performance", "BudgetVertexCount", "500000");
            config.Set("Performance", "BudgetTriangleCount", "300000");
            config.Set("Performance", "BudgetDrawCallCount", "1000");
            config.Set("Performance", "EnableGrassSwaying", "1");
            config.Set("Performance", "EnableFXAA", "1");
            config.Set("Performance", "FrameCapType", "8");
            config.Set("Performance", "ShadowQuality", "4");
            config.Set("Performance", "EffectsQuality", "4");
            config.Set("Performance", "EnvironmentQuality", "4");
            config.Set("Performance", "CharacterQuality", "4");
            config.Set("Performance", "AutoPerformanceSettings", "0");


            config.Set("HUD", "ShowTimestamps", "0");
            config.Set("HUD", "ShowNeutralCamps", "1");
            config.Set("HUD", "ObjectTooltips", "1");
            config.Set("HUD", "MinimapMoveSelf", "1");
            config.Set("HUD", "DrawHealthBars", "1");
            config.Set("HUD", "ChatScale", "100");
            config.Set("HUD", "AutoDisplayTarget", "1");
            config.Set("HUD", "FlashScreenWhenStunned", "1");
            config.Set("HUD", "FlashScreenWhenDamaged", "1");
            config.Set("HUD", "ShowPlayerPerks", "0");
            config.Set("HUD", "ShowPlayerStats", "1");
            config.Set("HUD", "ShowAllChannelChatSpectator", "0");
            config.Set("HUD", "GlobalScaleReplay", "1.0000");
            config.Set("HUD", "ReplayScrollSmoothingEnabled", "0");
            config.Set("HUD", "ReplayMiddleMouseScrollSpeed", "0.5000");
            config.Set("HUD", "ItemShopPrevY", "0");
            config.Set("HUD", "ItemShopPrevX", "15");
            config.Set("HUD", "ItemShopResizeHeight", "76");
            config.Set("HUD", "ItemShopResizeWidth", "260");
            config.Set("HUD", "ItemShopPrevResizeHeight", "1080");
            config.Set("HUD", "ItemShopPrevResizeWidth", "1920");
            config.Set("HUD", "ShowAlliedChat", "1");
            config.Set("HUD", "ShowAllChannelChat", "1");
            config.Set("HUD", "GlobalScale", "1.0000");
            config.Set("HUD", "MinimapScale", "1.0000");
            config.Set("HUD", "DeathRecapScale", "1.0000");
            config.Set("HUD", "PracticeToolScale", "1.0000");
            config.Set("HUD", "ItemShopItemDisplayMode", "0");
            config.Set("HUD", "ItemShopStartPane", "1");
            config.Set("HUD", "DisableMouseCaptureDebugger", "0");
            config.Set("HUD", "ShowSpellCosts", "0");
            config.Set("HUD", "NameTagDisplay", "1");
            config.Set("HUD", "ShowChampionIndicator", "0");
            config.Set("HUD", "ShowSummonerNames", "1");
            config.Set("HUD", "CameraLockMode", "0");
            config.Set("HUD", "MiddleClickDragScrollEnabled", "0");
            config.Set("HUD", "ScrollSmoothingEnabled", "0");
            config.Set("HUD", "KeyboardScrollSpeed", "0.0000");
            config.Set("HUD", "MiddleMouseScrollSpeed", "0.5000");
            config.Set("HUD", "MapScrollSpeed", "0.5000");
            config.Set("HUD", "ShowAttackRadius", "1");
            config.Set("HUD", "NumericCooldownFormat", "1");
            config.Set("HUD", "EternalsMilestoneDisplayMode", "0");
            config.Set("HUD", "HideEnemySummonerEmotes", "0");
            config.Set("HUD", "DisableHudSpellClick", "0");
            config.Set("HUD", "SmartCastWithIndicator_CastWhenNewSpellSelected", "0");
            config.Set("HUD", "SmartCastOnKeyRelease", "0");
            config.Set("HUD", "EnableLineMissileVis", "1");
            config.Set("HUD", "ShowSummonerNamesInScoreboard", "0");
            config.Set("HUD", "MirroredScoreboard", "0");
            config.Set("HUD", "ShowTeamFramesOnLeft", "0");
            config.Set("HUD", "FlipMiniMap", "0");


            config.Set("Chat", "Transparency", "0.0000");
            config.Set("Chat", "ChatY", "0");
            config.Set("Chat", "ChatX", "0");
            config.Set("Chat", "EnableChatFilter", "1");

            config.Set("Accessibility", "ColorContrast", "0.5000");
            config.Set("Accessibility", "ColorBrightness", "0.5000");
            config.Set("Accessibility", "ColorGamma", "0.5000");
            config.Set("Accessibility", "ColorLevel", "0.5000");

            config.Save();

            //Logger.Write("League of legends settings applied.", MessageState.INFO2);

            CFGFile file = new CFGFile(Path.Combine(Configuration.Instance.ClientPath, KEYCONFIG_PATH));


            file.Set("GameEvents", "evtSelectSelf", "[F1]");
            file.Set("GameEvents", "evtSelectAlly4", "[F5]");
            file.Set("GameEvents", "evtSelectAlly3", "[F4]");
            file.Set("GameEvents", "evtSelectAlly2", "[F3]");
            file.Set("GameEvents", "evtSelectAlly1", "[F2]");


            file.Set("GameEvents", "evtCastSpell1", "[1]");
            file.Set("GameEvents", "evtCastSpell2", "[2]");
            file.Set("GameEvents", "evtCastSpell3", "[3]");
            file.Set("GameEvents", "evtCastSpell4", "[4]");


            file.Set("GameEvents", "evtUseItem1", "[q]");
            file.Set("GameEvents", "evtUseItem2", "[w]");
            file.Set("GameEvents", "evtUseItem3", "[e]");
            //file.Set("General", "AutoAcquireTarget", "1");

            file.Save();

            string target = Path.Combine(Configuration.Instance.ClientPath, PERSISTED_CONFIG_PATH);

            File.Copy("PersistedSettings.json", target, true);
        }
    }
}
