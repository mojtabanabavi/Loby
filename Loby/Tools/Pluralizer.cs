using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Loby
{
    /// <summary>
    /// Provides functionality to pluralize and singularize any english word.
    /// </summary>
    public class Pluralizer
    {
        #region Rules

        /// <summary>
        /// Returns a set of words that are uncountable.
        /// </summary>
        /// <returns>
        /// Returns a set of words that cannot be pluralized and singularized.
        /// </returns>
        protected static List<string> GetUncountables()
        {
            var list = new List<string>
            {
                "adulthood",
                "advice",
                "agenda",
                "aid",
                "aircraft",
                "alcohol",
                "ammo",
                "anime",
                "athletics",
                "audio",
                "bison",
                "blood",
                "bream",
                "buffalo",
                "butter",
                "carp",
                "cash",
                "chassis",
                "chess",
                "clothing",
                "cod",
                "commerce",
                "cooperation",
                "corps",
                "debris",
                "diabetes",
                "digestion",
                "elk",
                "energy",
                "equipment",
                "excretion",
                "expertise",
                "firmware",
                "flounder",
                "fun",
                "gallows",
                "garbage",
                "graffiti",
                "headquarters",
                "health",
                "herpes",
                "highjinks",
                "homework",
                "housework",
                "information",
                "jeans",
                "justice",
                "kudos",
                "labour",
                "literature",
                "machinery",
                "mackerel",
                "mail",
                "media",
                "mews",
                "moose",
                "music",
                "mud",
                "manga",
                "news",
                "only",
                "personnel",
                "pike",
                "plankton",
                "pliers",
                "police",
                "pollution",
                "premises",
                "rain",
                "research",
                "rice",
                "salmon",
                "scissors",
                "series",
                "sewage",
                "shambles",
                "shrimp",
                "software",
                "species",
                "staff",
                "swine",
                "tennis",
                "traffic",
                "transportation",
                "trout",
                "tuna",
                "wealth",
                "welfare",
                "whiting",
                "wildebeest",
                "wildlife",
                "you"
            };

            return list;
        }

        /// <summary>
        /// Returns a set of rules needed to make words plurals.
        /// </summary>
        /// <returns>
        /// A dictionary that contains rules for converting singular words into 
        /// plurals.
        /// </returns>
        protected static Dictionary<string, string> GetPluralsRules()
        {
            var dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "s?$" , "s" },
                { "[^\u0000-\u007F]$","$0" },
                { "([^aeiou]ese)$","$1" },
                { "(ax|test)is$","$1es" },
                { "(alias|[^aou]us|t[lm]as|gas|ris)$","$1es" },
                { "(e[mn]u)s?$","$1s" },
                { "([^l]ias|[aeiou]las|[ejzr]as|[iu]am)$","$1" },
                { "(alumn|syllab|vir|radi|nucle|fung|cact|stimul|termin|bacill|foc|uter|loc|strat)(?:us|i)$","$1i" },
                { "(alumn|alg|vertebr)(?:a|ae)$","$1ae" },
                { "(seraph|cherub)(?:im)?$","$1im" },
                { "(her|at|gr)o$","$1oes" },
                { "(agend|addend|millenni|dat|extrem|bacteri|desiderat|strat|candelabr|errat|ov|symposi|curricul|automat|quor)(?:a|um)$","$1a" },
                { "(apheli|hyperbat|periheli|asyndet|noumen|phenomen|criteri|organ|prolegomen|hedr|automat)(?:a|on)$","$1a" },
                { "sis$","ses" },
                { "(?:(kni|wi|li)fe|(ar|l|ea|eo|oa|hoo)f)$","$1$2ves" },
                { "([^aeiouy]|qu)y$", "$1ies"},
                { "([^ch][ieo][ln])ey$","$1ies" },
                { "(x|ch|ss|sh|zz)$","$1es" },
                { "(matr|cod|mur|sil|vert|ind|append)(?:ix|ex)$","$1ices" },
                { "\\b((?:tit)?m|l)(?:ice|ouse)$","$1ice"  },
                { "(pe)(?:rson|ople)$","$1ople" },
                { "(child)(?:ren)?$","$1ren" },
                { "eaux$", "$0"},
                { "m[ae]n$","men" },
                { "^thou$","you" },
                { "pox$","$0" },
                { "o[iu]s$","$0" },
                { "deer$","$0" },
                { "fish$","$0" },
                { "sheep$","$0" },
                { "measles$/","$0" },
                { "[^aeiou]ese$","$0" },
            };

            return dictionary;
        }

        /// <summary>
        /// Returns a set of rules needed to make words singular.
        /// </summary>
        /// <returns>
        /// A dictionary that contains Rules for converting plural words to 
        /// singular.
        /// </returns>
        protected static Dictionary<string, string> GetSingularsRules()
        {
            var dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "s$","" },
                { "(ss)$","$1" },
                { "(wi|kni|(?:after|half|high|low|mid|non|night|[^\\w]|^)li)ves$","$1fe" },
                { "(ar|(?:wo|[ae])l|[eo][ao])ves$","$1f" },
                { "ies$","y" },
                { "\\b([pl]|zomb|(?:neck|cross|hog|aun)?t|coll|faer|food|gen|goon|group|lass|talk|goal|cut|hipp|junk|vegg|(?:pork)?p|charl|calor)ies$","$1ie" },                { "\\b(mon|smil)ies$","$1ey" },
                { "\\b((?:tit)?m|l)ice$","$1ouse" },
                { "(seraph|cherub)im$","$1" },
                { "(x|ch|ss|sh|zz|tto|go|cho|alias|[^aou]us|t[lm]as|gas|(?:her|at|gr)o|[aeiou]ris)(?:es)?$","$1" },
                { "(analy|diagno|parenthe|progno|synop|the|empha|cri|ne)(?:sis|ses)$","$1sis" },
                { "(movie|twelve|abuse|e[mn]u)s$","$1" },
                { "(test)(?:is|es)$","$1is" },
                { "(alumn|syllab|octop|vir|radi|nucle|fung|cact|stimul|termin|bacill|foc|uter|loc|strat)(?:us|i)$","$1us" },
                { "(agend|addend|millenni|dat|extrem|bacteri|desiderat|strat|candelabr|errat|ov|symposi|curricul|quor)a$", "$1um" },
                { "(apheli|hyperbat|periheli|asyndet|noumen|phenomen|criteri|organ|prolegomen|hedr|automat)a$","$1on" },
                { "(alumn|alg|vertebr)ae$","$1a" },
                { "(cod|mur|sil|vert|ind)ices$","$1ex" },
                { "(matr|append)ices$","$1ix" },
                { "(pe)(rson|ople)$","$1rson" },
                { "(child)ren$","$1" },
                { "(eau)x?$","$1" },
                { "men$","man" },
                { "[^aeiou]ese$","$0" },
                { "deer$", "$0" },
                { "fish$", "$0" },
                { "measles$","$0" },
                { "o[iu]s$", "$0" },
                { "pox$","$0" },
                { "sheep$","$0" },
            };

            return dictionary;
        }

        /// <summary>
        /// A set of words that have an alternative value to get plural.
        /// </summary>
        /// <returns>
        /// Returns a dictionary that singular words are used as keys and 
        /// plural words are given as values.
        /// </returns>
        protected static Dictionary<string, string> GetIrregularSingularReplacements()
        {
            var dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                // Pronouns.
                {"i", "we"},
                {"me", "us"},
                {"he", "they"},
                {"she", "they"},
                {"them", "them"},
                {"myself", "ourselves"},
                {"yourself", "yourselves"},
                {"itself", "themselves"},
                {"herself", "themselves"},
                {"himself", "themselves"},
                {"themself", "themselves"},
                {"is", "are"},
                {"was", "were"},
                {"has", "have"},
                {"this", "these"},
                {"that", "those"},
                {"my", "our"},
                {"its", "their"},
                {"his", "their"},
                {"her", "their"},
                // Words ending in with a consonant and `o`.
                {"echo", "echoes"},
                {"dingo", "dingoes"},
                {"volcano", "volcanoes"},
                {"tornado", "tornadoes"},
                {"torpedo", "torpedoes"},
                // Ends with `us`.
                {"genus", "genera"},
                {"viscus", "viscera"},
                // Ends with `ma`.
                {"stigma", "stigmata"},
                {"stoma", "stomata"},
                {"dogma", "dogmata"},
                {"lemma", "lemmata"},
                {"schema", "schemata"},
                {"anathema", "anathemata"},
                // Other irregular rules.
                {"ox", "oxen"},
                {"axe", "axes"},
                {"die", "dice"},
                {"yes", "yeses"},
                {"foot", "feet"},
                {"eave", "eaves"},
                {"goose", "geese"},
                {"tooth", "teeth"},
                {"quiz", "quizzes"},
                {"human", "humans"},
                {"proof", "proofs"},
                {"carve", "carves"},
                {"valve", "valves"},
                {"looey", "looies"},
                {"thief", "thieves"},
                {"groove", "grooves"},
                {"pickaxe", "pickaxes"},
                {"passerby","passersby" },
                {"cookie","cookies" }
            };

            return dictionary;
        }

        /// <summary>
        /// A set of words that have an alternative value to get singular.
        /// </summary>
        /// <returns>
        /// Returns a dictionary that plural words are used as keys and 
        /// singular words are given as values.
        /// </returns>
        protected static Dictionary<string, string> GetIrregularPluralReplacements()
        {
            var dictionary = new Dictionary<string, string>();

            foreach (var item in GetIrregularSingularReplacements())
            {
                if (!dictionary.ContainsKey(item.Value))
                {
                    dictionary.Add(item.Value, item.Key);
                }
            }

            return dictionary;
        }

        #endregion;

        #region Protected

        /// <summary>
        /// Search patterns in the given text and replace them with 
        /// the values ​​intended for it.
        /// </summary>
        /// <param name="value">
        /// A string that the rules will apply on it.
        /// </param>
        /// <param name="rules">
        /// A set of rules for replacing words as dictionary.
        /// </param>
        /// <returns>
        /// A string in which rules have been applied on it.
        /// </returns>
        protected static string ApplyRules(string value, Dictionary<string, string> rules)
        {
            foreach (var rule in rules.Reverse())
            {
                if (Regex.IsMatch(value, rule.Key))
                {
                    return Regex.Replace(value, rule.Key, rule.Value);
                }
            }

            return value;
        }

        #endregion;

        #region Methods

        /// <summary>
        /// Converts a singular word into plural.
        /// </summary>
        /// <param name="value">
        /// The word to be pluralized.
        /// </param>
        /// <returns>
        /// A string that is the plural form of <paramref name="value"/>.
        /// </returns>
        public static string Pluralize(string value)
        {
            if (GetUncountables().Contains(value))
            {
                return value;
            }

            if (GetIrregularPluralReplacements().ContainsKey(value))
            {
                return value;
            }

            if (GetIrregularSingularReplacements().TryGetValue(value, out string valueOfKey))
            {
                return valueOfKey;
            }

            return ApplyRules(value, GetPluralsRules());
        }

        /// <summary>
        /// Converts a plural word into singular.
        /// </summary>
        /// <param name="value">
        /// The word to be singularized.
        /// </param>
        /// <returns>
        /// A string that is the singular form of <paramref name="value"/>.
        /// </returns>
        public static string Singularize(string value)
        {
            if (GetUncountables().Contains(value))
            {
                return value;
            }

            if (GetIrregularSingularReplacements().ContainsKey(value))
            {
                return value;
            }

            if (GetIrregularPluralReplacements().TryGetValue(value, out string valueOfKey))
            {
                return valueOfKey;
            }

            return ApplyRules(value, GetSingularsRules());
        }

        #endregion;
    }
}
