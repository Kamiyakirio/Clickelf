import os


def count_lines(filepath):
    """计算文件中除了忽略的行的行数量"""
    count = 0
    with open(filepath, "r", encoding="utf-8") as file:
        for line in file:
            line = line.strip()
            if (
                line
                and not line.startswith("using")
                and not line.startswith("//")
                and not line.startswith("/*")
                and (
                    not (line.startswith("{") and len(line) < 5)
                    and not (line.startswith("}") and len(line) < 5)
                )
            ):
                count += 1
    return count


def find_all_cs_files(directory):
    """递归查找目录及子目录中的所有.cs文件（包括Designer文件）"""
    cs_files = []
    for root, dirs, files in os.walk(directory):
        for file in files:
            if file.endswith(".cs"):
                cs_files.append(os.path.join(root, file))
    return cs_files


def find_cs_files_without_designer(directory):
    """递归查找目录及子目录中的所有.cs文件，忽略文件名包含'Designer'的文件"""
    cs_files = []
    for root, dirs, files in os.walk(directory):
        for file in files:
            if file.endswith(".cs") and "Designer" not in file:
                cs_files.append(os.path.join(root, file))
    return cs_files


def calculate_code_lines(directory):
    """计算目录及子目录中所有.cs文件的总行数"""
    # 计算所有.cs文件（包括Designer文件）的代码行数
    all_cs_files = find_all_cs_files(directory)
    total_lines_all = 0
    for filepath in all_cs_files:
        lines = count_lines(filepath)
        total_lines_all += lines
        print(f"File (All): {filepath} - Lines: {lines}")

    # 计算忽略Designer文件的代码行数
    cs_files_without_designer = find_cs_files_without_designer(directory)
    total_lines_without_designer = 0
    for filepath in cs_files_without_designer:
        lines = count_lines(filepath)
        total_lines_without_designer += lines
        print(f"File (Without Designer): {filepath} - Lines: {lines}")

    # 输出结果
    print("\nSummary:")
    print(f"Total lines of code (All .cs files): {total_lines_all}")
    print(
        f"Total lines of code (Without Designer files): {total_lines_without_designer}"
    )


if __name__ == "__main__":
    directory = input("Enter the directory to scan: ")
    calculate_code_lines(directory)
