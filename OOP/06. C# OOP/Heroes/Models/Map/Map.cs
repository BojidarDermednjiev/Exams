namespace Heroes.Models.Map
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Heroes;
    using Utilities.Messages;
    public class Map : IMap
    {
        public string Fight(ICollection<IHero> players)
        {
            var knights = new List<IHero>();
            var barbarians = new List<IHero>();
            foreach (var hero in players)
            {
                if (hero.GetType() == typeof(Knight))
                    knights.Add(hero);
                else if (hero.GetType() == typeof(Barbarian))
                    barbarians.Add(hero);
            }
            var isKnigthsTurn = true;
            while (knights.Any(x => x.IsAlive) && barbarians.Any(x => x.IsAlive))
            {
                if (isKnigthsTurn)
                    PlayOutRound(knights, barbarians);
                else
                    PlayOutRound(barbarians, knights);
                isKnigthsTurn = !isKnigthsTurn;
            }
            if (knights.Any(x => x.IsAlive))
                return string.Format(OutputMessages.MapFightKnightsWin, knights.Where(x => !x.IsAlive).Count());
            else
                return string.Format(OutputMessages.MapFigthBarbariansWin, barbarians.Where(x => !x.IsAlive).Count());
        }
        private void PlayOutRound(List<IHero> attackers, List<IHero> defenders)
        {
           foreach (IHero attacker in attackers.Where(x => x.IsAlive))
                foreach (IHero defender in defenders.Where(x => x.IsAlive))                
                    defender.TakeDamage(attacker.Weapon.DoDamage());
        }
    }
}
