import sys

print("1. Probando import pythoncom...", flush=True)
import pythoncom
print("   └─ Éxito en pythoncom", flush=True)

print("2. Probando import win32com.client...", flush=True)
import win32com.client
print("   └─ Éxito en win32com.client", flush=True)

print("3. Probando import comtypes...", flush=True)
import comtypes
print("   └─ Éxito en comtypes", flush=True)

print("4. Intentando cargar ProgID con Dispatch...", flush=True)
obj = win32com.client.Dispatch("JYMono.Monochromator")
print("   └─ Éxito en Dispatch", flush=True)