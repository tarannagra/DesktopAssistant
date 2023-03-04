"""
    voices.py - Made by Taran Nagra
    ---
    This is to list all the available voices in IBM watson.

    It should be very easy to follow, it's in Python.

"""


import requests
import toml
import json

a = toml.load("../conf.toml")

r = requests.get(
    "https://api.eu-gb.text-to-speech.watson.cloud.ibm.com/instances/cd85e365-9c22-40f7-ab9f-ca89f47a6491/v1/voices",
    auth=("apikey", a['ibm']['api_key'])
)

with open("voices.json", 'w') as f:
    json.dump(r.json(), f, indent=4)