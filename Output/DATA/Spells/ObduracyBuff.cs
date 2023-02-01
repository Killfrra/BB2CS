#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ObduracyBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MalphiteDamageBuff",
            BuffTextureName = "Malphite_BrutalStrikes.dds",
        };
        float percMod;
        float damageIncrease;
        float armorIncrease;
        Particle sandroot;
        Particle sandRHand;
        Particle sandLHand;
        public ObduracyBuff(float percMod = default)
        {
            this.percMod = percMod;
        }
        public override void OnActivate()
        {
            float damageVar;
            float armorVar;
            int malphiteSkinID;
            //RequireVar(this.percMod);
            damageVar = GetTotalAttackDamage(owner);
            this.damageIncrease = damageVar * this.percMod;
            IncPermanentFlatPhysicalDamageMod(owner, this.damageIncrease);
            armorVar = GetArmor(owner);
            this.armorIncrease = armorVar * this.percMod;
            IncPermanentFlatArmorMod(owner, this.armorIncrease);
            malphiteSkinID = GetSkinID(owner);
            SpellEffectCreate(out this.sandroot, out _, "Malphite_Enrage_glow.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, target, default, default, false, false, false, false, false);
            OverrideAutoAttack(0, SpellSlotType.ExtraSlots, owner, 1, true);
            if(malphiteSkinID == 3)
            {
                SpellEffectCreate(out this.sandRHand, out _, "Malphite_Enrage_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_finger_b", default, target, default, default, false, false, false, false, false);
                SpellEffectCreate(out this.sandLHand, out _, "Malphite_Enrage_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_finger_b", default, target, default, default, false, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out this.sandRHand, out _, "Malphite_Enrage_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_thumb_b", default, target, default, default, false, false, false, false, false);
                SpellEffectCreate(out this.sandLHand, out _, "Malphite_Enrage_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_finger_b", default, target, default, default, false, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            this.damageIncrease *= -1;
            IncPermanentFlatPhysicalDamageMod(owner, this.damageIncrease);
            this.armorIncrease *= -1;
            IncPermanentFlatArmorMod(owner, this.armorIncrease);
            RemoveOverrideAutoAttack(owner, true);
            SpellEffectRemove(this.sandLHand);
            SpellEffectRemove(this.sandRHand);
            SpellEffectRemove(this.sandroot);
        }
    }
}