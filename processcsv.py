import json
import base64

filename = "Crafting Skills csv.csv"
with open(filename, "r") as f:
    lines = f.readlines()
skillsdict = {}
for line in lines:
    tokens = line.split(",")
    no = int(tokens[0], 16)
    chsname = tokens[1]
    engname = tokens[2].replace("\n", "")
    skillsdict[no] = [chsname, engname]
with open("Skills.json", "w", encoding="utf8") as f:
    json.dump(skillsdict, f, indent=4, ensure_ascii=False)

base64.b64encode()
