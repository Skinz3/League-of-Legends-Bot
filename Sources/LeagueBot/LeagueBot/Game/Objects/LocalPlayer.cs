using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueBot.Game.Modules;
using SharpDX;
using Newtonsoft.Json.Linq;

namespace LeagueBot.Game.Objects
{
    public class LocalPlayer
    {
        public static JObject UnitRadiusData;

        public static string GetSummonerName()
        {
            return Api.ActivePlayerData.GetSummonerName();
        }

        public static int GetLevel()
        {
            return Api.ActivePlayerData.GetLevel();
        }

        public static float GetCurrentGold()
        {
            return Api.ActivePlayerData.GetCurrentGold();
        }

        //public static void DrawAttackRange(Color Colour, float Thickness)
        //{
        //    if (IsVisible() && GetCurrentHealth() > 1.0f)
        //    {
        //        Overlay.Drawing.DrawFactory.DrawCircleRange(GetPosition(), GetBoundingRadius() + GetAttackRange(), Colour, Thickness);
        //    }
        //}

        //public static void DrawSpellRange(Spells.SpellBook.SpellSlot Slot, Color Colour, float Thickness)
        //{
        //    if (IsVisible() && GetCurrentHealth() > 1.0f)
        //    {
        //        Overlay.Drawing.DrawFactory.DrawCircleRange(GetPosition(), Spells.SpellBook.GetSpellRadius(Slot), Colour, Thickness);
        //    }
        //}

        private static List<string> RangeSlotList = new List<string>() { "Q", "W", "E", "R" };
        private static List<float> UsedRangeSlotsList = new List<float>();
        //public static void DrawAllSpellRange(Color RGB)
        //{
        //    foreach (string RangeSlot in RangeSlotList)
        //    {
        //        float SpellRange = Spells.SpellBook.SpellDB[RangeSlot].ToObject<JObject>()["Range"][0].ToObject<float>();

        //        if (UsedRangeSlotsList.Count != 0)
        //        {
        //            if (!UsedRangeSlotsList.Contains(SpellRange))
        //            {
        //                UsedRangeSlotsList.Add(SpellRange);
        //            }
        //        }
        //        else
        //        {
        //            UsedRangeSlotsList.Add(SpellRange);
        //        }
        //    }

        //    foreach (float Range in UsedRangeSlotsList)
        //    {
        //        Overlay.Drawing.DrawFactory.DrawCircleRange(GetPosition(), Range, RGB, 2.5f);
        //    }
        //}

        public static bool IsVisible()
        {
            return Memory.Read<bool>(Engine.GetLocalPlayer + OffsetManager.Object.Visibility);
        }

        public static Vector3 GetPosition()
        {
            float posX = Memory.Read<float>(Engine.GetLocalPlayer + OffsetManager.Object.Pos);
            float posY = Memory.Read<float>(Engine.GetLocalPlayer + OffsetManager.Object.Pos + 0x4);
            float posZ = Memory.Read<float>(Engine.GetLocalPlayer + OffsetManager.Object.Pos + 0x8);

            return new Vector3() { X = posX, Y = posY, Z = posZ };
        }

        public static string GetChampionName()
        {
            return Api.AllPlayerData.AllPlayers.Where(x => x.SummonerName == GetSummonerName()).First().ChampionName;
        }

        public static float GetAttackRange()
        {
            return Memory.Read<float>(Engine.GetLocalPlayer + OffsetManager.Object.AttackRange);
        }

        public static int GetBoundingRadius()
        {
            return int.Parse(UnitRadiusData[GetChampionName()]["Gameplay radius"].ToString());
        }

        public static float GetCurrentHealth()
        {
            return Api.ActivePlayerData.ChampionStats.GetCurrentHealth();
        }

        public static float GetMaxHealth()
        {
            return Api.ActivePlayerData.ChampionStats.GetMaxHealth();
        }

        public static float GetHealthRegenRate()
        {
            return Api.ActivePlayerData.ChampionStats.GetHealthRegenRate();
        }

        public string GetResourceType()
        {
            return Api.ActivePlayerData.ChampionStats.GetResourceType();
        }

        public static float GetCurrentMana()
        {
            return Api.ActivePlayerData.ChampionStats.GetResourceValue();
        }

        public static float GetCurrentManaMax()
        {
            return Api.ActivePlayerData.ChampionStats.GetResourceMax();
        }

        public static float GetAbilityPower()
        {
            return Api.ActivePlayerData.ChampionStats.GetAbilityPower();
        }

        public static float GetArmor()
        {
            return Api.ActivePlayerData.ChampionStats.GetArmor();
        }

        public static float GetArmorPenetrationFlat()
        {
            return Api.ActivePlayerData.ChampionStats.GetArmorPenetrationFlat();
        }

        public static float GetArmorPenetrationPercent()
        {
            return Api.ActivePlayerData.ChampionStats.GetArmorPenetrationPercent();
        }

        public static float GetAttackSpeed()
        {
            return Api.ActivePlayerData.ChampionStats.GetAttackSpeed();
        }

        public static float GetBonusArmorPenetrationPercent()
        {
            return Api.ActivePlayerData.ChampionStats.GetBonusArmorPenetrationPercent();
        }

        public static float GetBonusMagicPenetrationPercent()
        {
            return Api.ActivePlayerData.ChampionStats.GetBonusMagicPenetrationPercent();
        }

        public static float GetCooldownReduction()
        {
            return Api.ActivePlayerData.ChampionStats.GetCooldownReduction();
        }

        public static float GetCritChance()
        {
            return Api.ActivePlayerData.ChampionStats.GetCritChance();
        }

        public static float GetCritDamage()
        {
            return Api.ActivePlayerData.ChampionStats.GetCritDamage();
        }

        public static float GetLifeSteal()
        {
            return Api.ActivePlayerData.ChampionStats.GetLifeSteal();
        }

        public static float GetMagicLethality()
        {
            return Api.ActivePlayerData.ChampionStats.GetMagicLethality();
        }

        public static float GetMagicPenetrationFlat()
        {
            return Api.ActivePlayerData.ChampionStats.GetMagicPenetrationFlat();
        }

        public static float GetMagicPenetrationPercent()
        {
            return Api.ActivePlayerData.ChampionStats.GetMagicPenetrationPercent();
        }

        public static float GetMagicResist()
        {
            return Api.ActivePlayerData.ChampionStats.GetMagicResist();
        }

        public static float GetMoveSpeed()
        {
            return Api.ActivePlayerData.ChampionStats.GetMoveSpeed();
        }

        public static float GetPhysicalLethality()
        {
            return Api.ActivePlayerData.ChampionStats.GetPhysicalLethality();
        }

        public static float GetSpellVamp()
        {
            return Api.ActivePlayerData.ChampionStats.GetSpellVamp();
        }

        public static float GetTenacity()
        {
            return Api.ActivePlayerData.ChampionStats.GetTenacity();
        }
    }
}
