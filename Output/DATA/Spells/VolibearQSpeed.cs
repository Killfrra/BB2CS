#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VolibearQSpeed : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "VolibearQSpeed",
            BuffTextureName = "VolibearQ.dds",
        };
        float speedMod;
        public VolibearQSpeed(float speedMod = default)
        {
            this.speedMod = speedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.speedMod);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellBuffRemove(owner, nameof(Buffs.VolibearQSpeedPart), (ObjAIBase)owner, 0);
        }
        public override void OnUpdateStats()
        {
            bool visible;
            bool hunt;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 2000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectHeroes, default, true))
            {
                if(IsInFront(owner, unit))
                {
                    visible = CanSeeTarget(owner, unit);
                    if(visible)
                    {
                        hunt = true;
                        AddBuff((ObjAIBase)owner, unit, new Buffs.VolibearQHunted(), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                    if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.VolibearQHunted)) > 0)
                    {
                        hunt = true;
                    }
                }
            }
            if(hunt)
            {
                IncPercentMovementSpeedMod(owner, this.speedMod);
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.VolibearQSpeedPart)) == 0)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.VolibearQSpeedPart(), 1, 1, 20, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
            else
            {
                SpellBuffRemove(owner, nameof(Buffs.VolibearQSpeedPart), (ObjAIBase)owner, 0);
                IncPercentMovementSpeedMod(owner, 0.15f);
            }
        }
    }
}