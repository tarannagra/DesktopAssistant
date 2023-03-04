"""
    push.p - Made by Taran Nagra
    ---
    Automation for pushing to my GitHub repo.
    I am that lazy to run these myself. I know.
"""

import os

def do_it(filename:str, commit_message:str):
    os.system(f"git add {filename}")
    os.system(f'git commit -m "{commit_message}"')
    os.system(f"git push origin main")
    # do not need done message, it will show in the push command if failed or not.

filename = input("Enter filename: ")
commit_message = input("Enter commit message: ")
input("Press enter to push the repo")

do_it(
    filename=filename,
    commit_message=commit_message
)