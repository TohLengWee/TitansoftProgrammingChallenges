using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TitansoftProgrammingChallenges
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            GetHtmlLinksAndDescription();
            GetUrlDomain();
            GetHtmlTagAndAttributesByRegex();
        }

        private static void GetHtmlLinksAndDescription()
        {
            var html = @"<ul>
<li style=""-moz-float-edge: content-box"">Former Italian Prime Minister <a href=""/wiki/Silvio_Berlusconi"" title=""Silvio Berlusconi"">Silvio Berlusconi</a> <i>(pictured)</i> is <b><a href=""/wiki/Silvio_Berlusconi_underage_prostitution_charges"" title=""Silvio Berlusconi underage prostitution charges"">found guilty</a></b> of paying for sex with an underage prostitute.</li>
<li style=""-moz-float-edge: content-box"">In sports car racing, the <b><a href=""/wiki/2013_24_Hours_of_Le_Mans"" title=""2013 24 Hours of Le Mans"">24 Hours of Le Mans</a></b>, won by <a href=""/wiki/Tom_Kristensen"" title=""Tom Kristensen"">Tom Kristensen</a>, <a href=""/wiki/Allan_McNish"" title=""Allan McNish"">Allan McNish</a> and <a href=""/wiki/Lo%C3%AFc_Duval"" title=""Loc Duval"">Loc Duval</a>, is marred by the death of <b><a href=""/wiki/Allan_Simonsen_(racing_driver)"" title=""Allan Simonsen (racing driver)"">Allan Simonsen</a></b>.</li>
<li style=""-moz-float-edge: content-box""><b><a href=""/wiki/2013_Alberta_floods"" title=""2013 Alberta floods"">Flooding</a></b> in <a href=""/wiki/Alberta"" title=""Alberta"">Alberta</a>, Canada, results in at least three deaths and the evacuation of thousands.</li>
<li style=""-moz-float-edge: content-box""><b><a href=""/wiki/2013_North_India_floods"" title=""2013 North India floods"">Flash floods and landslides</a></b> in <a href=""/wiki/Uttarakhand"" title=""Uttarakhand"">Uttarakhand</a> and <a href=""/wiki/Himachal_Pradesh"" title=""Himachal Pradesh"">Himachal Pradesh</a> in India kill more than <span class=""nowrap"">1,000 people</span> and trap more than 20,000.</li>
<li style=""-moz-float-edge: content-box"">In <a href=""/wiki/Basketball"" title=""Basketball"">basketball</a>, the <a href=""/wiki/Miami_Heat"" title=""Miami Heat"">Miami Heat</a> defeat the <a href=""/wiki/San_Antonio_Spurs"" title=""San Antonio Spurs"">San Antonio Spurs</a> to win the <b><a href=""/wiki/2013_NBA_Finals"" title=""2013 NBA Finals"">NBA Finals</a></b>.</li>
</ul>";

            var aTagPattern = new Regex(@"<a.*?href=""(.*?)"".*?>(.*?)<\/a>");
            var matches = aTagPattern.Matches(html);

            foreach (Match match in matches)
            {
                Console.WriteLine($"{match.Groups[1].Value},{Regex.Replace(match.Groups[2].Value, "<.*?>", " ").Trim()}");
            }
        }

        private static void GetUrlDomain()
        {
            string a =
                "<div style=\"margin-left: 0px; margin-top: -20px; text-align: left;\"><a href=\"/wiki/File:Female_and_male_Pardalotus_punctatus.jpg\" title=\"About this image\"><img alt=\"About this image\" src=\"//bits.wikimedia.org/static-1.22wmf7/extensions/ImageMap/desc-20.png\" style=\"border: none;\" /></a></div>";
            string b =
                "<li class=\"interwiki-he\"><a href=\"//he.wikipedia.org/wiki/\" title=\"\" lang=\"he\" hreflang=\"he\"></a></li>";
            string c =
                @"<li class=""interwiki-da""><a href=""//da.wikipedia.org/wiki/"" title="""" lang=""da"" hreflang=""da""><b>Dansk</b></a></li>";
            var href = new Regex(@"https?://(www2?\.)?(([A-Za-z0-9-]+\.)+[A-Za-z0-9]+)");
            var matches = href.Matches(c);
            foreach (Match match in matches)
            {
                Console.WriteLine($"group 1:{match.Groups[1].Value}, group 2:{match.Groups[2].Value}");
            }
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
