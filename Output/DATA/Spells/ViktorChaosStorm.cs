#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class ViktorChaosStorm : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        Fade blah; // UNUSED
        int[] effect0 = {0, 400, 800, 600, 800};
        int[] effect1 = {0, 25, 50};
        int[] effect2 = {150, 250, 350};
        public override void SelfExecute()
        {
            Vector3 targetPos;
            Pet other1;
            targetPos = GetCastSpellTargetPos();
            other1 = SpawnPet("Tibbers", "TempMovableChar", nameof(Buffs.InfernalGuardian), "StormIdle.lua", 7, targetPos, this.effect0[level], this.effect1[level]);
            foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, targetPos, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
            {
                AddBuff((ObjAIBase)owner, unit, new Buffs.ViktorChaosStormGuide(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            SetTargetable(other1, false);
            SetInvulnerable(other1, true);
            StopMove(other1);
            this.blah = PushCharacterFade(other1, 0, 0);
            AddBuff((ObjAIBase)owner, other1, new Buffs.ViktorChaosStormAOE(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff(attacker, attacker, new Buffs.ViktorChaosStormTimer(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff(attacker, other1, new Buffs.ViktorExpirationTimer(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            SetSpell((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.ViktorChaosStormGuide));
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            Vector3 spellTargetPos; // UNUSED
            float baseDamage;
            float aPPreMod;
            float aPPostMod;
            float finalDamage;
            spellTargetPos = GetCastSpellTargetPos();
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            baseDamage = this.effect2[level];
            aPPreMod = GetFlatMagicDamageMod(owner);
            aPPostMod = 0.55f * aPPreMod;
            finalDamage = baseDamage + aPPostMod;
            BreakSpellShields(target);
            ApplyDamage(attacker, target, finalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
            ApplySilence(owner, target, 0.5f);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 2000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectUntargetable, nameof(Buffs.ViktorChaosStormAOE), true))
            {
                Particle hi; // UNUSED
                SpellEffectCreate(out hi, out _, "Viktor_ChaosStorm_hit.troy", default, TeamId.TEAM_NEUTRAL, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "Head", default, target, "Spine", default, true, false, false, false, false);
            }
        }
    }
}