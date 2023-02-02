﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class ScoutsBounty : BBSpellScript
    {
        int[] effect0 = {100, 150, 200};
        int[] effect1 = {-30, -45, -60};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_BonusGold;
            float nextBuffVars_ArmorReduction;
            nextBuffVars_BonusGold = this.effect0[level];
            nextBuffVars_ArmorReduction = this.effect1[level];
            AddBuff(attacker, target, new Buffs.ScoutsBounty(nextBuffVars_ArmorReduction, nextBuffVars_BonusGold), 1, 1, 30, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0);
        }
    }
}
namespace Buffs
{
    public class ScoutsBounty : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "ArchersMark_tar.troy", },
            BuffName = "Scout's Bounty",
            BuffTextureName = "Bowmaster_ArchersMark.dds",
        };
        float armorReduction;
        float bonusGold;
        Region bubbleID;
        public ScoutsBounty(float armorReduction = default, float bonusGold = default)
        {
            this.armorReduction = armorReduction;
            this.bonusGold = bonusGold;
        }
        public override void OnActivate()
        {
            TeamId casterID;
            //RequireVar(this.armorReduction);
            //RequireVar(this.bonusGold);
            casterID = GetTeamID(attacker);
            this.bubbleID = AddUnitPerceptionBubble(casterID, 1200, owner, 60, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(this.bubbleID);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, this.armorReduction);
            if(owner.IsDead)
            {
                IncGold(attacker, this.bonusGold);
                SpellBuffRemove(owner, nameof(Buffs.ScoutsBounty), attacker);
            }
        }
    }
}