#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AlZaharVoidling : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "AlZaharVoidling",
            BuffTextureName = "AlZahar_SummonVoidling.dds",
        };
        float bonusHealth;
        float bonusDamage;
        float timer;
        float lastTimeExecuted;
        public AlZaharVoidling(float bonusHealth = default, float bonusDamage = default)
        {
            this.bonusHealth = bonusHealth;
            this.bonusDamage = bonusDamage;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            Particle a; // UNUSED
            Vector3 targetPos; // UNITIALIZED
            //RequireVar(this.bonusDamage);
            //RequireVar(this.bonusHealth);
            IncPermanentFlatPhysicalDamageMod(owner, this.bonusDamage);
            IncPermanentFlatHPPoolMod(owner, this.bonusHealth);
            this.timer = 0;
            teamID = GetTeamID(owner);
            if(teamID == TeamId.TEAM_BLUE)
            {
                SpellEffectCreate(out a, out _, "VoidlingFlash.troy", default, TeamId.TEAM_BLUE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, targetPos, target, default, default, true);
            }
            else
            {
                SpellEffectCreate(out a, out _, "VoidlingFlash.troy", default, TeamId.TEAM_PURPLE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, targetPos, target, default, default, true);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            ApplyDamage((ObjAIBase)owner, owner, 8000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false);
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, 0.25f);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, true))
            {
                this.timer += 0.5f;
                if(this.timer >= 7)
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.AlZaharVoidlingPhase2)) == 0)
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.AlZaharVoidlingPhase2(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
                    }
                    if(this.timer >= 14)
                    {
                        if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.AlZaharVoidlingPhase3)) == 0)
                        {
                            AddBuff((ObjAIBase)owner, owner, new Buffs.AlZaharVoidlingPhase3(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
                        }
                    }
                }
            }
        }
    }
}