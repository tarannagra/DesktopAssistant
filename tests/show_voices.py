"""
    show_voices.py - Made by Taran Nagra
    ---
    This just parses the voice information
    from the file created from voices.py!

    Once again a fairly simple file in Python.
"""

import json

with open('voices.json', 'r') as f:
    data = json.load(f)

for x in data['voices']:
    print(f"Gender: {x['gender']}")
    print(f"Name: {x['name']}")
    print(f"Description: {x['description']}")
    print(f"Language: {x['language']}")
    print(f"URL: {x['url']}")
    print("\n-----------\n")