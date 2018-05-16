using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TitansoftProgrammingChallenges
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            GetHtmlTagAndAttributesByRegex();
        }

        private static void GetHtmlTagAndAttributesByRegex()
        {
            var htmls = new List<string>
            {
                @"<li style=""-moz-float-edge: content-box"">... that <a href=""/wiki/Orval_Overall"" title=""Orval Overall"">Orval Overall</a> <i>(pictured)</i> is the only <b><a href=""/wiki/List_of_Major_League_Baseball_pitchers_who_have_struck_out_four_batters_in_one_inning"" title=""List of Major League Baseball pitchers who have struck out four batters in one inning"">Major League Baseball player to strike out four batters in one inning</a></b> in the <a href=""/wiki/World_Series"" title=""World Series"">World Series</a>?</li>",
                @"<li style=""-moz-float-edge: content-box"">... that the three cities of the <b><a href=""/wiki/West_Triangle_Economic_Zone"" title=""West Triangle Economic Zone"">West Triangle Economic Zone</a></b> contribute 40% of Western China's GDP?</li>",
                @"<li style=""-moz-float-edge: content-box"">... that <i><a href=""/wiki/Kismet_(1943_film)"" title=""Kismet (1943 film)"">Kismet</a></i>, directed by <b><a href=""/wiki/Gyan_Mukherjee"" title=""Gyan Mukherjee"">Gyan Mukherjee</a></b>, ran at the <a href=""/wiki/Roxy_Cinema_(Kolkata)"" title=""Roxy Cinema (Kolkata)"">Roxy, Kolkata</a>, for 3 years and 8 months?</li>",
                @"<li style=""-moz-float-edge: content-box"">... that <a href=""/wiki/Vauix_Carter"" title=""Vauix Carter"">Vauix Carter</a> both coached and played for the <b><a href=""/wiki/1882_Navy_Midshipmen_football_team"" title=""1882 Navy Midshipmen football team"">1882 Navy Midshipmen football team</a></b>?</li>",
                @"<li style=""-moz-float-edge: content-box"">... that <a href=""/wiki/Zhu_Chenhao"" title=""Zhu Chenhao"">Zhu Chenhao</a> was sentenced to <a href=""/wiki/Slow_slicing"" title=""Slow slicing"">slow slicing</a> for leading the <b><a href=""/wiki/Prince_of_Ning_rebellion"" title=""Prince of Ning rebellion"">Prince of Ning rebellion</a></b> against the <a href=""/wiki/Ming_Dynasty"" title=""Ming Dynasty"">Ming Dynasty</a> <a href=""/wiki/Zhengde_Emperor"" title=""Zhengde Emperor"">emperor Zhengde</a>?</li>",
                @"<li style=""-moz-float-edge: content-box"">... that <b><a href=""/wiki/Mirza_Adeeb"" title=""Mirza Adeeb"">Mirza Adeeb</a></b> was a prominent modern Pakistani <a href=""/wiki/Urdu"" title=""Urdu"">Urdu</a> playwright whose later work focuses on social problems and daily life?</li>",
                @"<li style=""-moz-float-edge: content-box"">... that in <i><b><a href=""/wiki/La%C3%9Ft_uns_sorgen,_la%C3%9Ft_uns_wachen,_BWV_213"" title=""Lat uns sorgen, lat uns wachen, BWV 213"">Die Wahl des Herkules</a></b></i>, Hercules must choose between the good cop and the bad cop?<br style=""clear:both;"" />",
                @"<div style=""text-align: right;"" class=""noprint""><b><a href=""/wiki/Wikipedia:Recent_additions"" title=""Wikipedia:Recent additions"">Archive</a></b>  <b><a href=""/wiki/Wikipedia:Your_first_article"" title=""Wikipedia:Your first article"">Start a new article</a></b>  <b><a href=""/wiki/Template_talk:Did_you_know"" title=""Template talk:Did you know"">Nominate an article</a></b></div>",
                @"</li>"
            };

            var openingTagPattern = new Regex(@"<(\w+)([^>]*)>");
            var attributePattern = new Regex(@"(\w+)(?==[""'])");

            var tagAttributeDic = new Dictionary<string, List<string>>();

            foreach (var html in htmls)
            {
                MatchCollection matches = openingTagPattern.Matches(html);
                foreach (Match tags in matches)
                {
                    var tag = tags.Groups[1].Value;
                    if (!tagAttributeDic.TryGetValue(tag, out var value))
                    {
                        tagAttributeDic[tag] = new List<string>();
                    }

                    var attributes = attributePattern.Matches(tags.Value);
                    foreach (Match attribute in attributes)
                    {
                        if (!tagAttributeDic[tag].Contains(attribute.Value))
                        {
                            tagAttributeDic[tag].Add(attribute.Value);
                        }
                    }
                }
            }

            var orderedTagAttributeDic = tagAttributeDic.OrderBy(x => x.Key);
            foreach (var pair in orderedTagAttributeDic)
            {
                Console.WriteLine($"{String.Join(":", pair.Key)}:{String.Join(",", pair.Value.OrderBy(x => x))}");
            }
        }
    }
}
