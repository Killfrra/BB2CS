﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class KarmaTranscendence : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "KarmaPassive",
            BuffTextureName = "FallenAngel_Empathize.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            SetBuffToolTipVar(1, 30);
        }
        public override void OnUpdateStats()
        {
            float percentHealth;
            float percentMissing;
            int charLevel;
            float aPGain;
            percentHealth = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
            percentMissing = 1 - percentHealth;
            charLevel = GetLevel(owner);
            if(charLevel < 3)
            {
                aPGain = percentMissing * 30;
            }
            else if(charLevel < 6)
            {
                aPGain = percentMissing * 50;
            }
            else if(charLevel < 9)
            {
                aPGain = percentMissing * 70;
            }
            else if(charLevel < 12)
            {
                aPGain = percentMissing * 90;
            }
            else if(charLevel < 15)
            {
                aPGain = percentMissing * 110;
            }
            else
            {
                aPGain = percentMissing * 130;
            }
            IncFlatMagicDamageMod(owner, aPGain);
        }
        public override void OnLevelUp()
        {
            int level;
            level = GetLevel(owner);
            if(level == 3)
            {
                SetBuffToolTipVar(1, 50);
            }
            else if(level == 6)
            {
                SetBuffToolTipVar(1, 70);
            }
            else if(level == 9)
            {
                SetBuffToolTipVar(1, 90);
            }
            else if(level == 12)
            {
                SetBuffToolTipVar(1, 110);
            }
            else if(level == 15)
            {
                SetBuffToolTipVar(1, 130);
            }
        }
    }
}