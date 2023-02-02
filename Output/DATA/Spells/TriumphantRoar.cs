#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class TriumphantRoar : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {60, 90, 120, 150, 180};
        public override void SelfExecute()
        {
            float abilityPower;
            float baseHeal;
            float finalHeal;
            Particle par; // UNUSED
            abilityPower = GetFlatMagicDamageMod(owner);
            abilityPower *= 0.2f;
            baseHeal = this.effect0[level];
            finalHeal = baseHeal + abilityPower;
            IncHealth(owner, finalHeal, owner);
            SpellEffectCreate(out par, out _, "Meditate_eff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, default, default, false, false);
            finalHeal /= 2;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 575, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, default, true))
            {
                float temp1;
                temp1 = GetHealthPercent(target, PrimaryAbilityResourceType.MANA);
                if(temp1 < 1)
                {
                    ApplyAssistMarker(attacker, target, 10);
                }
                IncHealth(unit, finalHeal, owner);
                SpellEffectCreate(out par, out _, "Meditate_eff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, target, default, default, true, default, default, false, false);
            }
            AddBuff((ObjAIBase)owner, owner, new Buffs.AlistarTrample(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, false, false, false);
        }
    }
}
namespace Buffs
{
    public class TriumphantRoar : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Triumphant Roar",
            BuffTextureName = "Minotaur_TriumphantRoar.dds",
        };
        float movementSpeed;
        public TriumphantRoar(float movementSpeed = default)
        {
            this.movementSpeed = movementSpeed;
        }
        public override void OnActivate()
        {
            //RequireVar(this.movementSpeed);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, this.movementSpeed);
        }
    }
}