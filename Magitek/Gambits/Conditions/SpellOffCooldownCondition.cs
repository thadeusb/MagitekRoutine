﻿using System;
using System.Linq;
using ff14bot.Managers;
using ff14bot.Objects;

namespace Magitek.Gambits.Conditions
{
    public class SpellOffCooldownCondition : GambitCondition
    {
        public SpellOffCooldownCondition() : base(GambitConditionTypes.SpellOffCooldown) { }

        public string SpellName { get; set; }
        public bool IsPvpSpell { get; set; }

        public override bool Check(GameObject gameObject = null)
        {
            var spell = ActionManager.CurrentActions.Values.FirstOrDefault(SpellDataCheck);

            if (spell == null)
                return false;

            return spell.Cooldown <= TimeSpan.Zero;
        }

        private bool SpellDataCheck(SpellData spell)
        {
            if (IsPvpSpell && !spell.IsPvP)
                return false;

            return string.Equals(spell.Name, SpellName, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
