import requests
import toml
headers = {
    'Content-Type': 'audio/flac',
}

data = toml.load('../conf.toml')

url = f"{data['stt']['base_url']}/v1/recognize" # didnt work bc im stupid and didn't add this shit lmao
api_key = data['stt']['api_key']
with open('../audio.flac', 'rb') as f:
    data = f.read()

response = requests.post(url, headers=headers, data=data, auth=('apikey', f'{api_key}'))
print(response.text)
