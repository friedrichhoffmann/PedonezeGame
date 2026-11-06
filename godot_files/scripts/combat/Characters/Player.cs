using MainCharacterFile;

namespace PlayerFile
{
    public class Player : MainCharacter
    {
        public static Player player = new(
            levelXp: 1,
            name: "Jogador",
            health: 10,
            resistanceBonus: 0,
            damageBonus: 0,
            armor: 10,
            initiativeBonus: 0,
            attackBonus: 0,
            xp: 10
        );

        public Player(
            int levelXp,
            string name,
            int health,
            int resistanceBonus,
            int damageBonus,
            int armor,
            int initiativeBonus,
            int attackBonus,
            int xp
        )
            : base(
                levelXp,
                name,
                health,
                resistanceBonus,
                damageBonus,
                armor,
                initiativeBonus,
                attackBonus,
                xp
            )
        {
        }
    }
}

