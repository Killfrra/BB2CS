#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class BrandScorchAttack : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamOfOwner;
            Vector3 targetPos;
            Minion other3;
            float damageAmount;
            teamOfOwner = GetTeamID(attacker);
            targetPos = GetUnitPosition(target);
            other3 = SpawnMinion("SpellBook1", "SpellBook1", "idle.lua", targetPos, teamOfOwner ?? TeamId.TEAM_CASTER, false, true, false, true, true, true, 0, default, true, (Champion)attacker);
            AddBuff(attacker, other3, new Buffs.BrandScorchGround(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            damageAmount = GetTotalAttackDamage(owner);
            ApplyDamage(attacker, target, damageAmount, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
            RemoveOverrideAutoAttack(owner, false);
            SpellBuffRemove(owner, nameof(Buffs.BrandScorch), (ObjAIBase)owner);
        }
    }
}