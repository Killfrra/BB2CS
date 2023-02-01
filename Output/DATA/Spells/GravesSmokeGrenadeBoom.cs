#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GravesSmokeGrenadeBoom : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Nearsight_glb.troy", },
            BuffName = "GravesSmokeGrenade",
            BuffTextureName = "GravesSmokeGrenade.dds",
        };
        float sightReduction;
        public GravesSmokeGrenadeBoom(float sightReduction = default)
        {
            this.sightReduction = sightReduction;
        }
        public override void OnActivate()
        {
            //RequireVar(this.sightReduction);
            IncPermanentFlatBubbleRadiusMod(owner, this.sightReduction);
        }
        public override void OnDeactivate(bool expired)
        {
            float sightReduction;
            sightReduction = this.sightReduction * -1;
            IncPermanentFlatBubbleRadiusMod(owner, sightReduction);
        }
        public override void OnUpdateStats()
        {
            ApplyNearSight(attacker, owner, 0.25f);
            if(GetBuffCountFromCaster(owner, attacker, nameof(Buffs.GravesSmokeGrenadeBoomSlow)) > 0)
            {
            }
            else
            {
            }
        }
        public override void OnBeingHit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            float distance;
            TeamId teamID;
            Region pineapple; // UNUSED
            if(attacker is Champion)
            {
                distance = DistanceBetweenObjects("Attacker", "Owner");
                if(distance < 800)
                {
                    teamID = GetTeamID(owner);
                    pineapple = AddUnitPerceptionBubble(teamID, 75, attacker, 1, default, default, false);
                }
            }
        }
    }
}
namespace Spells
{
    public class GravesSmokeGrenadeBoom : BBSpellScript
    {
        float[] effect0 = {4.5f, 4.5f, 4.5f, 4.5f, 4.5f};
        int[] effect1 = {5, 5, 5, 5, 5};
        int[] effect2 = {60, 110, 160, 210, 260};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            Vector3 targetPos;
            TeamId teamID;
            float buffDuration;
            Particle particle; // UNUSED
            float aD;
            float bonusDamage;
            float dmg; // UNITIALIZED
            float totalDamage;
            float remainder;
            float ticks;
            float tickDamage; // UNUSED
            Minion other3;
            string name;
            string checkName;
            targetPos = GetUnitPosition(target);
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            teamID = GetTeamID(owner);
            buffDuration = this.effect0[level];
            SpellEffectCreate(out particle, out _, "Graves_SmokeGrenade_Boom.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, default, default, targetPos, true, false, false, false, false);
            aD = GetFlatPhysicalDamageMod(owner);
            bonusDamage = aD * 0.6f;
            totalDamage = bonusDamage + dmg;
            remainder = buffDuration % 0.5f;
            ticks = buffDuration - remainder;
            tickDamage = totalDamage / ticks;
            other3 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", targetPos, teamID, false, true, false, true, true, true, 50, false, true, (Champion)owner);
            AddBuff(attacker, other3, new Buffs.GravesSmokeGrenade(), 1, 1, this.effect1[level], BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, false))
            {
                BreakSpellShields(unit);
                ApplyDamage(attacker, unit, this.effect2[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 0, false, false, attacker);
                if(unit is Champion)
                {
                    name = GetUnitSkinName(unit);
                    checkName = "Nocturne";
                    if(checkName == name)
                    {
                        AddBuff(attacker, unit, new Buffs.GravesSmokeGrenadeSecretPassive(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, true);
                    }
                }
            }
        }
    }
}