import json

# 读取原始JSON文件
def read_json(file_path):
    with open(file_path, 'r', encoding='utf-8') as file:
        data = json.load(file)
    return data

# 反转键和值
def reverse_key_value(data):
    reversed_data = {v: k for k, v in data.items()}
    return reversed_data

# 保存新的JSON文件
def save_json(data, file_path):
    with open(file_path, 'w', encoding='utf-8') as file:
        json.dump(data, file, indent=4)

# 主程序
if __name__ == "__main__":
    input_file = "input.json"  # 原始JSON文件路径
    output_file = "output.json"  # 输出JSON文件路径

    # 读取原始数据
    original_data = read_json(input_file)
    print("原始数据：")
    print(original_data)

    # 反转键和值
    reversed_data = reverse_key_value(original_data)
    print("反转后的数据：")
    print(reversed_data)

    # 保存反转后的数据
    save_json(reversed_data, output_file)
    print(f"反转后的数据已保存到 {output_file}")