#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class ShyvanaFireballDragon2 : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {80, 125, 170, 215, 260};
        public override void SelfExecute()
        {
            Vector3 point1;
            Vector3 point2;
            Vector3 point3;
            Vector3 point4;
            Vector3 point5;
            point1 = GetPointByUnitFacingOffset(owner, 300, 20);
            point2 = GetPointByUnitFacingOffset(owner, 300, -20);
            point3 = GetPointByUnitFacingOffset(owner, 325, 0);
            SpellCast((ObjAIBase)owner, default, point1, point1, 4, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
            SpellCast(attacker, default, point2, point2, 4, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
            SpellCast(attacker, default, point3, point3, 4, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
            point4 = GetPointByUnitFacingOffset(owner, 310, 10);
            point5 = GetPointByUnitFacingOffset(owner, 310, -10);
            SpellCast(attacker, default, point4, point5, 4, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
            SpellCast(attacker, default, point5, point5, 4, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID; // UNUSED
            float spellBaseDamage;
            teamID = GetTeamID(owner);
            spellBaseDamage = this.effect0[level];
            AddBuff(attacker, target, new Buffs.ShyvanaFireballParticle(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            ApplyDamage(attacker, target, spellBaseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 0, false, false, attacker);
            if(default is Champion)
            {
                AddBuff(attacker, target, new Buffs.ShyvanaFireballMissile(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
            else
            {
                AddBuff(attacker, target, new Buffs.ShyvanaFireballMissileMinion(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
        }
    }
}