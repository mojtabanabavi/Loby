using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Loby
{
    public class Pluralizer
    {
        #region Rules

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
