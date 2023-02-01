#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BantamTrap : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Noxious Trap",
            BuffTextureName = "Bowmaster_ArchersMark.dds",
            SpellFXOverrideSkins = new[]{ "AstronautTeemo", },
        };
        bool activated;
        int teemoSkinID;
        Fade iD; // UNUSED
        bool hasParticle;
        Particle a;
        int[] effect0 = {50, 100, 150};
        float[] effect1 = {-0.3f, -0.4f, -0.5f};
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            if(owner.Team != attacker.Team)
            {
                if(scriptName == nameof(Buffs.GlobalWallPush))
                {
                    returnValue = false;
                }
                else if(type == BuffType.FEAR)
                {
                    returnValue = false;
                }
                else if(type == BuffType.CHARM)
                {
                    returnValue = false;
                }
                else if(type == BuffType.SILENCE)
                {
                    returnValue = false;
                }
                else if(type == BuffType.SLEEP)
                {
                    returnValue = false;
                }
                else if(type == BuffType.SLOW)
                {
                    returnValue = false;
                }
                else if(type == BuffType.SNARE)
                {
                    returnValue = false;
                }
                else if(type == BuffType.STUN)
                {
                    returnValue = false;
                }
                else if(type == BuffType.TAUNT)
                {
                    returnValue = false;
                }
                else if(type == BuffType.BLIND)
                {
                    returnValue = false;
                }
                else if(type == BuffType.SUPPRESSION)
                {
                    returnValue = false;
                }
                else if(type == BuffType.COMBAT_DEHANCER)
                {
                    returnValue = false;
                }
                else
                {
                    returnValue = true;
                }
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damagePerTick);
            //RequireVar(this.moveSpeedMod);
            //RequireVar(this.attackSpeedMod);
            this.activated = false;
            this.teemoSkinID = GetSkinID(owner);
            if(this.teemoSkinID == 4)
            {
                this.iD = PushCharacterFade(owner, 0.3f, 1.5f);
            }
            else if(this.teemoSkinID == 5)
            {
                this.iD = PushCharacterFade(owner, 0.5f, 1.5f);
            }
            else
            {
                this.iD = PushCharacterFade(owner, 0.3f, 1.5f);
            }
            if(this.teemoSkinID == 5)
            {
                this.hasParticle = false;
                if(RandomChance() < 0.3f)
                {
                    SpellEffectCreate(out this.a, out _, "TeemoEaster2.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                    this.hasParticle = true;
                }
                else if(RandomChance() < 0.3f)
                {
                    SpellEffectCreate(out this.a, out _, "TeemoEaster3.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                    this.hasParticle = true;
                }
            }
            SetGhosted(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            if(this.teemoSkinID == 5)
            {
                if(this.hasParticle == default)
                {
                    SpellEffectRemove(this.a);
                }
            }
            if(owner.IsDead)
            {
            }
            else
            {
                ApplyDamage((ObjAIBase)owner, owner, 4000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
            }
        }
        public override void OnUpdateStats()
        {
            IncPercentBubbleRadiusMod(owner, -0.7f);
        }
        public override void OnUpdateActions()
        {
            int nextBuffVars_DamagePerTick;
            float nextBuffVars_MoveSpeedMod;
            int nextBuffVars_AttackSpeedMod;
            TeamId teamID;
            TeamId mushroomTeamID;
            Vector3 ownerPos;
            Particle particle; // UNUSED
            int level;
            if(lifeTime >= 2)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.Stealth(), 1, 1, 600, BuffAddType.RENEW_EXISTING, BuffType.INVISIBILITY, 0, true, false, true);
                AddBuff((ObjAIBase)owner, owner, new Buffs.BantamArmor(), 1, 1, 600, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                this.activated = true;
            }
            if(this.activated)
            {
                foreach(AttackableUnit unit in GetClosestUnitsInArea(attacker, owner.Position, 160, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, default, true))
                {
                    this.activated = false;
                    teamID = GetTeamID(attacker);
                    mushroomTeamID = GetTeamID(owner);
                    SpellBuffRemove(owner, nameof(Buffs.Stealth), (ObjAIBase)owner, 0);
                    ownerPos = GetUnitPosition(owner);
                    AddPosPerceptionBubble(mushroomTeamID, 700, ownerPos, 4, default, false);
                    SpellEffectCreate(out particle, out _, "ShroomMine.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position, owner, default, default, true, false, false, false, false);
                    level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    nextBuffVars_DamagePerTick = this.effect0[level];
                    nextBuffVars_MoveSpeedMod = this.effect1[level];
                    nextBuffVars_AttackSpeedMod = 0;
                    foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 450, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                    {
                        BreakSpellShields(unit);
                        AddBuff(attacker, unit, new Buffs.BantamTrapTarget(nextBuffVars_DamagePerTick), 1, 1, 4, BuffAddType.STACKS_AND_RENEWS, BuffType.POISON, 0, true, false, false);
                        AddBuff(attacker, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 4, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                    }
                    this.iD = PushCharacterFade(owner, 1, 0.75f);
                    ApplyDamage((ObjAIBase)owner, owner, 500, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
                }
            }
        }
    }
}
namespace Spells
{
    public class BantamTrap : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        public override bool CanCast()
        {
            bool returnValue = true;
            bool canMove; // UNUSED
            bool canCast; // UNUSED
            int count;
            canMove = GetCanMove(owner);
            canCast = GetCanCast(owner);
            count = GetBuffCountFromAll(owner, nameof(Buffs.TeemoMushrooms));
            if(count <= 1)
            {
                returnValue = false;
            }
            else
            {
                returnValue = true;
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            float duration;
            TeamId teamID;
            Vector3 targetPos;
            Minion other3;
            duration = GetBuffRemainingDuration(owner, nameof(Buffs.TeemoMushrooms));
            if(duration > 40)
            {
                SpellBuffRemove(owner, nameof(Buffs.TeemoMushrooms), (ObjAIBase)owner, charVars.MushroomCooldown);
            }
            else
            {
                SpellBuffRemove(owner, nameof(Buffs.TeemoMushrooms), (ObjAIBase)owner, 0);
            }
            teamID = GetTeamID(owner);
            targetPos = GetCastSpellTargetPos();
            other3 = SpawnMinion("Noxious Trap", "TeemoMushroom", "idle.lua", targetPos, teamID, true, true, false, false, true, false, 0, true, false, (Champion)owner);
            AddBuff(attacker, other3, new Buffs.BantamTrap(), 1, 1, 600, BuffAddType.REPLACE_EXISTING, BuffType.INVISIBILITY, 0, true, false, false);
            AddBuff(attacker, other3, new Buffs.SharedWardBuff(), 1, 1, 600, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}