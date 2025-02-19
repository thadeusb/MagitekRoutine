using System.Linq;
using System.Threading.Tasks;
using ff14bot;
using ff14bot.Managers;
using Magitek.Extensions;
using Magitek.Logic;
using Magitek.Logic.BlackMage;
using Magitek.Models.BlackMage;
using Magitek.Utilities;

namespace Magitek.Rotations.BlackMage
{
    internal static class Combat 
    {
        public static async Task<bool> Execute()
        {
            if (BotManager.Current.IsAutonomous)
            {
                if (Core.Me.HasTarget)
                {
                    Movement.NavigateToUnitLos(Core.Me.CurrentTarget, 23);
                }
            }

            if (!Core.Me.HasTarget || !Core.Me.CurrentTarget.ThoroughCanAttack())
                return false;

            if (await CustomOpenerLogic.Opener()) return true;

            if (await Buff.Enochian()) return true;
            if (await Buff.Triplecast()) return true;
            if (await Buff.Sharpcast()) return true;
            if (await Buff.LeyLines()) return true;
            if (await Buff.UmbralSoul()) return true;

            if (await SingleTarget.Xenoglossy()) return true;
            if (await SingleTarget.Despair()) return true;

            if (await SingleTarget.Fire4()) return true;
            if (await SingleTarget.Fire3()) return true;
            if (await SingleTarget.Fire()) return true;

            if (await SingleTarget.Blizzard4()) return true;
            if (await SingleTarget.Blizzard3()) return true;

            if (await SingleTarget.Thunder3()) return true;

            return false;
        }
    }
}