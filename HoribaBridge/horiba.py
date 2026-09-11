"""Python client for HoribaBridge.exe.  It is intentionally pure stdlib."""
import json
import subprocess
import time
from pathlib import Path


class Horiba:
    def __init__(self, bridge=None):
        bridge = Path(bridge or Path(__file__).with_name("bin") / "HoribaBridge.exe")
        self._process = subprocess.Popen(
            [str(bridge)], stdin=subprocess.PIPE, stdout=subprocess.PIPE,
            stderr=subprocess.PIPE,
            # The legacy COM SDK can write Windows-1252 text (including inside
            # exception messages) to the bridge's console.  ``mbcs`` selects
            # the active Windows ANSI code page instead of assuming UTF-8.
            text=True, encoding="mbcs", bufsize=1,
        )

    def _call(self, command, **arguments):
        self._process.stdin.write(json.dumps({"command": command, **arguments}) + "\n")
        self._process.stdin.flush()
        line = self._process.stdout.readline()
        if not line:
            details = self._process.stderr.read().strip()
            raise RuntimeError(
                "HoribaBridge.exe ended unexpectedly (exit code {0}). {1}".format(
                    self._process.poll(), details or "No diagnostic was written to stderr."
                )
            )
        response = json.loads(line)
        if not response["ok"]:
            raise RuntimeError(response.get("code", "HORIBA error") + ": " + response["error"])
        return response

    def connect(self, device_id="Mono1", timeout_s=60):
        deadline = time.monotonic() + timeout_s
        response = self._call("connect", device_id=device_id)
        while response["state"] == "initializing" and time.monotonic() < deadline:
            time.sleep(0.1)
            response = self._call("status")
        if response["state"] != "ready":
            raise RuntimeError("HORIBA initialization did not complete: " + str(response))
        return response

    def wavelength(self): return self._call("get_wavelength")["wavelength_nm"]
    def move_wavelength(self, nm): self._call("move_wavelength", wavelength_nm=float(nm))
    def turret(self): return self._call("get_turret")["turret"]
    def move_turret(self, turret): self._call("move_turret", turret=int(turret))
    def slit(self, location): return self._call("get_slit", location=location)["width"]
    def move_slit(self, location, width): self._call("move_slit", location=location, width=float(width))
    def status(self): return self._call("status")

    def close(self):
        if self._process.poll() is None:
            try: self._call("quit")
            except (OSError, RuntimeError): pass
            finally: self._process.wait(timeout=5)

    def __enter__(self): return self
    def __exit__(self, *unused): self.close()
