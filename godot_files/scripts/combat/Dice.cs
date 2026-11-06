using System;

namespace Dice {
    public static class Die {
        public const int d2 = 2;
        public const int d4 = 4;
        public const int d6 = 6;
        public const int d8 = 8;
        public const int d10 = 10;
        public const int d12 = 12;
        public const int d20 = 20;

        public static int Roll(int rolls, int die) {
            int rollEvaluation = 0;

            for (int i = 0; i < rolls; i++) {
                rollEvaluation += RollDicePair(die);
            }

            return rollEvaluation;
        }

        private static int RollDicePair(int die) {
            int firstDieOfThePair = Random.Shared.Next(1, die+1); // ceil number is exclusive
            int secondDieOfThePair = Random.Shared.Next(1, die+1);

            int greatestRollOfThePair = Math.Max(firstDieOfThePair, secondDieOfThePair);
            return greatestRollOfThePair;
        }

        public static int RollString(string stringDice) {
            ArgumentNullException.ThrowIfNull(stringDice);

            if (string.IsNullOrWhiteSpace(stringDice))
            {
                throw new ArgumentException(
                        "Dice notation cannot be empty.",
                        nameof(stringDice));
            }

            string[] parts = stringDice.Split('d');

            if (parts.Length != 2 ||
                    !int.TryParse(parts[0], out int dieAmount) ||
                    !int.TryParse(parts[1], out int dieFaces))
            {
                throw new FormatException(
                        $"Invalid dice notation '{stringDice}'. Expected format: '{{dieAmount}}d{{dieFaces}}'.");
            }

            return Roll(dieAmount, dieFaces);
        }

    }
}
