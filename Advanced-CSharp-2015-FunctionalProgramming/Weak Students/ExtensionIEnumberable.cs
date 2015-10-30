namespace FunctionalProgramming
{
    using System.Collections.Generic;
    using System.Linq;

    public static class Extensions
    {
        public static bool HasExcatlyTwoBadMarks(this IEnumerable<int> marks, int badMarkValue)
        {
            var badMarks = marks.Where(mark => mark == badMarkValue);

            return badMarks.Count() == 2;
        }
    }
}
