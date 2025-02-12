using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ClickElf.Crafting
{
    internal class CraftingData
    {
        private readonly static string b64edSkills = "ewogICIyNTciOiBbIuWItuS9nCIsICJCYXNpYyBTeW50aGVzaXMiXSwKICAiMjU4IjogWyLmqKHojIPliLbkvZwiLCAiQ2FyZWZ1bCBTeW50aGVzaXMiXSwKICAiMjU5IjogWyLpq5jpgJ/liLbkvZwiLCAiUmFwaWQgU3ludGhlc2lzIl0sCiAgIjI2MCI6IFsi6IOa5paZ5Yi25L2cIiwgIkdyb3VuZHdvcmsiXSwKICAiMjYxIjogWyLpm4bkuK3liLbkvZwiLCAiSW50ZW5kc2l2ZSBTeW50aGVzaXMiXSwKICAiMjYyIjogWyLkv63nuqbliLbkvZwiLCAiUHJ1ZGVudCBTeW50aGVzaXMiXSwKICAiMjYzIjogWyLnsr7lr4bliLbkvZwiLCAiRGVsaWNhdGUgU3ludGhlc2lzIl0sCiAgIjUxMyI6IFsi5Yqg5belIiwgIkJhc2ljIFRvdWNoIl0sCiAgIjUxNCI6IFsi5Lit57qn5Yqg5belIiwgIlN0YW5kYXJkIFRvdWNoIl0sCiAgIjUxNSI6IFsi5LiK57qn5Yqg5belIiwgIkFkdmFuY2VkIFRvdWNoIl0sCiAgIjUxNiI6IFsi5LuT5L+DIiwgIkhhc3R5IFRvdWNoIl0sCiAgIjUxNyI6IFsi5YaS6L+bIiwgIkRhcmluZyBUb3VjaCJdLAogICI1MTgiOiBbIuavlOWwlOagvOeahOelneemjyIsICJCeXJlZ290J3MgQmxlc3NpbmciXSwKICAiNTE5IjogWyLpm4bkuK3liqDlt6UiLCAiUHJlY2lzZSBUb3VjaCJdLAogICI1MjAiOiBbIuS/ree6puWKoOW3pSIsICJQcnVkZW50IFRvdWNoIl0sCiAgIjUyMSI6IFsi6IOa5paZ5Yqg5belIiwgIlByZXBhdG9yeSBUb3VjaCJdLAogICI1MjIiOiBbIueyvueCvOWKoOW3pSIsICJSZWZpbmVkIFRvdWNoIl0sCiAgIjUyMyI6IFsi5bel5Yyg55qE56We5oqAIiwgIlRyYWluZWQgRmluZXNzZSJdLAogICI3NjkiOiBbIueyvuS/riIsICJNYXN0ZXIncyBNZW5kIl0sCiAgIjc3MCI6IFsi5L+t57qmIiwgIldhc3RlIE5vdCJdLAogICI3NzEiOiBbIumVv+acn+S/ree6piIsICJXYXN0ZSBOb3QgSUkiXSwKICAiNzcyIjogWyLmjozmj6EiLCAiTWFuaXB1bGF0aW9uIl0sCiAgIjc3MyI6IFsi5ben5aS65aSp5belIiwgIkltbWFjdWxhdGUgTWVuZCJdLAogICIxMDI1IjogWyLltIfmlawiLCAiVmVuZXJhdGlvbiJdLAogICIxMDI2IjogWyLpmJTmraUiLCAiR3JlYXQgU3RyaWRlcyJdLAogICIxMDI3IjogWyLmlLnpnakiLCAiSW5ub3ZhdGlvbiJdLAogICIxMDI4IjogWyLlv6vpgJ/mlLnpnakiLCAiUXVpY2sgSW5ub3ZhdGlvbiJdLAogICIxMDI5IjogWyLlt6XljKDnmoTnu53mioAiLCAiVHJhaW5lZCBQZXJmZWN0aW9uIl0sCiAgIjEyODEiOiBbIuWdmuS/oSIsICJNdXNjbGUgTWVtb3J5Il0sCiAgIjEyODIiOiBbIuWotOmdmSIsICJSZWZsZWN0Il0sCiAgIjEyODMiOiBbIuinguWvnyIsICJPYnNlcnZlIl0sCiAgIjEyODQiOiBbIuenmOivgCIsICJUcmlja3Mgb2YgdGhlIFRyYWRlIl0sCiAgIjEyODUiOiBbIuacgOe7iOehruiupCIsICJGaXJzdCBBcHByYWlzYWwiXSwKICAiMTI4NiI6IFsi5LiT5b+D6Ie05b+XIiwgIkhlYXJ0IGFuZCB0aGUgU291bCJdLAogICIxMjg3IjogWyLlt6XljKDnmoTnpZ7pgJ/mioDlt6ciLCAiVHJhaW5lZCBFeWUiXQp9Cg==";

        public static Dictionary<string, List<string>> getSkills()
        {
            // Dictionary<string,List<string>> ans = new Dictionary<string,List<string>>();
            var b64decode = Convert.FromBase64String(b64edSkills);
            string fromb64decode = Encoding.UTF8.GetString(b64decode);
            return JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(fromb64decode);
        }

        public static string FormatPointString(string input, int style = 0)
        {
            // 定义正则表达式，匹配允许的格式
            string pattern = @"^(\()?\s*(\d+)\s*[ ,]\s*(\d+)\s*(\))?$";
            var match = Regex.Match(input, pattern);

            if (!match.Success)
            {
                throw new ArgumentException("Input string is not in a valid format.");
            }
            // 提取数字部分
            string width = match.Groups[2].Value;
            string height = match.Groups[3].Value;

            if (style == 0) return $"({width}, {height})";
            else if (style == 1) return $"{width} {height}";
            else throw new ArgumentException();
        }
    }
}
