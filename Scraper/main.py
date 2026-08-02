import csv
import time
import requests
'''
MAX_POKEMON = 1025
DELAY = 0.1  # seconds between requests

BASE_URL = "https://pokeapi.co/api/v2"

STAT_ORDER = [
    "hp",
    "attack",
    "defense",
    "special-attack",
    "special-defense",
    "speed",
]


def get_json(url):
    response = requests.get(url, timeout=15)
    response.raise_for_status()
    return response.json()


def get_evolution_stage(species):
    """
    Returns 1 for base form, 2 for first evolution, etc.
    """

    evo_chain = get_json(species["evolution_chain"]["url"])

    target_species = species["name"]

    def search(chain, stage):
        if chain["species"]["name"] == target_species:
            return stage

        for evo in chain["evolves_to"]:
            result = search(evo, stage + 1)
            if result is not None:
                return result

        return None

    return search(evo_chain["chain"], 1)


def get_gen1_entry(species):
    """
    Returns the first English dex entry from Red/Blue/Yellow if possible.
    """

    preferred_versions = {"red", "blue", "yellow"}

    for entry in species["flavor_text_entries"]:
        if (
            entry["language"]["name"] == "en"
            and entry["version"]["name"] in preferred_versions
        ):
            return (
                entry["flavor_text"]
                .replace("\n", " ")
                .replace("\f", " ")
                .strip()
            )

    # fallback
    for entry in species["flavor_text_entries"]:
        if entry["language"]["name"] == "en":
            return (
                entry["flavor_text"]
                .replace("\n", " ")
                .replace("\f", " ")
                .strip()
            )

    return ""


rows = []

for dex in range(1, MAX_POKEMON + 1):
    try:
        print(f"Downloading #{dex}")

        pokemon = get_json(f"{BASE_URL}/pokemon/{dex}")
        time.sleep(DELAY)

        species = get_json(pokemon["species"]["url"])
        time.sleep(DELAY)

        stats = {}

        highest_name = ""
        highest_value = -1

        for stat in pokemon["stats"]:
            stat_name = stat["stat"]["name"]
            value = stat["base_stat"]

            stats[stat_name] = value

            if value > highest_value:
                highest_value = value
                highest_name = stat_name

        types = sorted(
            pokemon["types"],
            key=lambda t: t["slot"]
        )

        primary = types[0]["type"]["name"]
        secondary = (
            types[1]["type"]["name"]
            if len(types) > 1
            else "None"
        )

        row = {
            "PokedexNumber": dex,
            "Name": pokemon["name"].title(),

            "PrimaryType": primary.title(),
            "SecondaryType": secondary.title(),

            "HP": stats.get("hp"),
            "Attack": stats.get("attack"),
            "Defense": stats.get("defense"),
            "SpecialAttack": stats.get("special-attack"),
            "SpecialDefense": stats.get("special-defense"),
            "Speed": stats.get("speed"),

            "HighestStat": highest_name.replace("-", " ").title(),
            "HighestStatValue": highest_value,

            "EvolutionStage": get_evolution_stage(species),

            "Color": species["color"]["name"].title(),

            "Height": pokemon["height"],
            "Weight": pokemon["weight"],

            "DexEntry": get_gen1_entry(species),
        }

        rows.append(row)

        time.sleep(DELAY)

    except Exception as e:
        print(f"Failed #{dex}: {e}")


fieldnames = [
    "PokedexNumber",
    "Name",
    "PrimaryType",
    "SecondaryType",

    "HP",
    "Attack",
    "Defense",
    "SpecialAttack",
    "SpecialDefense",
    "Speed",

    "HighestStat",
    "HighestStatValue",

    "EvolutionStage",

    "Color",

    "Height",
    "Weight",

    "DexEntry",
]

with open("pokemon.csv", "w", newline="", encoding="utf-8") as f:
    writer = csv.DictWriter(f, fieldnames=fieldnames)
    writer.writeheader()
    writer.writerows(rows)

print(f"Saved {len(rows)} Pokémon to pokemon.csv")
'''

def generation(dex):
    if dex <= 151:
        return 1
    elif dex <= 251:
        return 2
    elif dex <= 386:
        return 3
    elif dex <= 493:
        return 4
    elif dex <= 649:
        return 5
    elif dex <= 721:
        return 6
    elif dex <= 809:
        return 7
    elif dex <= 905:
        return 8
    return 9

rows = []

with open("pokemon.csv", newline="", encoding="utf-8") as f:
    reader = csv.DictReader(f)

    fieldnames = reader.fieldnames + ["Generation"]

    for row in reader:
        row["Generation"] = generation(int(row["PokedexNumber"]))
        rows.append(row)

with open("pokemon.csv", "w", newline="", encoding="utf-8") as f:
    writer = csv.DictWriter(f, fieldnames=fieldnames)
    writer.writeheader()
    writer.writerows(rows)

print("Done!")
