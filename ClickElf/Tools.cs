using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickElf
{
    public static class Tools
    {
        public static Dictionary<TValue, TKey> ReverseDictionary<TKey, TValue>(Dictionary<TKey, TValue> original)
        {
            // 创建一个新的字典来存储逆转后的键值对
            Dictionary<TValue, TKey> reversed = new Dictionary<TValue, TKey>();

            // 遍历原始字典并逆转键和值
            foreach (var pair in original)
            {
                // 检查新字典中是否已经存在该键，以避免重复键
                if (!reversed.ContainsKey(pair.Value))
                {
                    reversed.Add(pair.Value, pair.Key);
                }
                else
                {
                    // 如果新字典中已经存在该键，可以选择如何处理这种情况
                    // 例如，可以跳过该键值对，或者合并值等
                    // Console.WriteLine($"Key {pair.Value} already exists in the reversed dictionary.");
                    continue;
                    // throw new Exception();
                }
            }

            return reversed;
        }
    }
}
