#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CrestofUnyieldingStone : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Crest Of Unyielding Stone",
            BuffTextureName = "PlantKing_AnimateEntangler.dds",
            NonDispellable = true,
        };
        Particle buffParticle;
        float bonusDamage;
        float bonusResist;
        public override void OnActivate()
        {
            float gameTimeSec;
            float bonusResist;
            float bonusDamage;
            float resistFloored;
            float resistCapped;
            float damageFloored;
            float damageCapped;
            float tTDmg;
            SpellEffectCreate(out this.buffParticle, out _, "Global_Invulnerability.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
            gameTimeSec = GetGameTime();
            bonusResist = 0.0666f * gameTimeSec;
            bonusDamage = 0.000333f * gameTimeSec;
            resistFloored = Math.Max(40, bonusResist);
            resistCapped = Math.Min(80, resistFloored);
            damageFloored = Math.Max(0.2f, bonusDamage);
            damageCapped = Math.Min(0.4f, damageFloored);
            this.bonusDamage = damageCapped;
            this.bonusResist = resistCapped;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectTurrets))
            {
                IncPermanentPercentPhysicalDamageMod(unit, this.bonusDamage);
                IncPermanentFlatArmorMod(unit, this.bonusResist);
                IncPermanentFlatSpellBlockMod(unit, this.bonusResist);
            }
            tTDmg = 100 * this.bonusDamage;
            SetBuffToolTipVar(1, tTDmg);
            SetBuffToolTipVar(2, this.bonusResist);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.buffParticle);
            this.bonusDamage *= -1;
            this.bonusResist *= -1;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectTurrets))
            {
                IncPermanentPercentPhysicalDamageMod(unit, this.bonusDamage);
                IncPermanentFlatArmorMod(unit, this.bonusResist);
                IncPermanentFlatSpellBlockMod(unit, this.bonusResist);
            }
        }
        public override void OnDeath()
        {
            if(attacker is Champion)
            {
                if(!attacker.IsDead)
                {
                    AddBuff(attacker, attacker, new Buffs.CrestofUnyieldingStone(), 1, 1, 120, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0);
                }
            }
        }
    }
}