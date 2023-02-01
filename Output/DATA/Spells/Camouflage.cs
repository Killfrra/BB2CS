#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Camouflage : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Camouflage",
            BuffTextureName = "Teemo_Camouflage.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        Vector3 lastPosition;
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.CamouflageCheck(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            this.lastPosition = GetUnitPosition(owner);
        }
        public override void OnUpdateActions()
        {
            Vector3 curPosition;
            Vector3 lastPosition;
            float distance;
            bool isInvuln;
            curPosition = GetUnitPosition(owner);
            lastPosition = this.lastPosition;
            distance = DistanceBetweenPoints(curPosition, lastPosition);
            if(distance != 0)
            {
                this.lastPosition = curPosition;
                AddBuff((ObjAIBase)owner, owner, new Buffs.CamouflageCheck(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            isInvuln = GetInvulnerable(owner);
            if(isInvuln)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.CamouflageCheck(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            if(owner.IsDead)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.CamouflageCheck(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Recall)) == 0)
            {
            }
            else
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.CamouflageCheck(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinCaptureChannel)) == 0)
            {
            }
            else
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.CamouflageCheck(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.SummonerTeleport)) == 0)
            {
            }
            else
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.CamouflageCheck(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.CamouflageCheck)) == 0)
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.CamouflageStealth)) == 0)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.CamouflageStealth(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INVISIBILITY, 0.1f, true, false, false);
                }
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            if(spellVars.CastingBreaksStealth)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.CamouflageCheck(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            else if(!spellVars.CastingBreaksStealth)
            {
            }
            else
            {
                if(!spellVars.DoesntTriggerSpellCasts)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.CamouflageCheck(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
        public override void OnTakeDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(damageSource != default)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.CamouflageCheck(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(target is not ObjAIBase)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.CamouflageCheck(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnLaunchAttack()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.CamouflageCheck(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}