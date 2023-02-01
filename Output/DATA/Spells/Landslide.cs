#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Landslide : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        Particle partname; // UNUSED
        int[] effect0 = {60, 100, 140, 180, 220};
        public override void SelfExecute()
        {
            int nextBuffVars_Level;
            float armorAmount;
            float baseDamage;
            float armorDamage;
            int malphiteSkinID;
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_Level = level;
            armorAmount = GetArmor(owner);
            baseDamage = this.effect0[level];
            armorAmount *= 0.5f;
            armorDamage = armorAmount + baseDamage;
            malphiteSkinID = GetSkinID(owner);
            if(malphiteSkinID == 2)
            {
                SpellEffectCreate(out this.partname, out _, "landslide_blue_nova.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position, target, default, default, true, default, default, false, false);
            }
            else
            {
                SpellEffectCreate(out this.partname, out _, "landslide_nova.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position, target, default, default, true, default, default, false, false);
            }
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                ApplyDamage(attacker, unit, armorDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                AddBuff(attacker, unit, new Buffs.LandslideDebuff(nextBuffVars_Level), 1, 1, 4, BuffAddType.STACKS_AND_OVERLAPS, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
        }
    }
}