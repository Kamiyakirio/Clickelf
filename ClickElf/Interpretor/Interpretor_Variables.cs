using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ClickElf.Interpretor
{
    internal partial class Interpretor
    {
        private Dictionary<string, string> variables;
        private string ReplaceVariables(string input)
        {
            if (input.StartsWith("$"))
            {
                string varName = input.Substring(1);
                if (variables.TryGetValue(varName, out string value))
                {
                    return value;
                }
                throw new ArgumentException($"变量 '{varName}' 未定义");
            }
            return input;
        }

        private int EvaluateMathExpression(string expression)
        {
            DataTable dt = new DataTable();
            var result = dt.Compute(expression,"");
            try
            {
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"表达式计算失败：{ex.Message}");
            }
        }
    }
}
