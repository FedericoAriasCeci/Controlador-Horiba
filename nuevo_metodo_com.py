import win32com.client

prog_ids = [
    "JYMono.Monochromator",
    "JYMono.Monochromator.1",
    "JYMono.MonochromatorAut",
    "JYMonoLib.Monochromator"
]

mono = None
for pid in prog_ids:
    try:
        mono = win32com.client.Dispatch(pid)
        print(f"Conexión exitosa con ProgID: '{pid}'")
        break
    except Exception:
        print(f"No se pudo instanciar: '{pid}'")