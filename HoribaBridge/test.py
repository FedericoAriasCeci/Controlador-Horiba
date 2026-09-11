from horiba import Horiba

with Horiba() as h:
    h.connect("Mono1")

    print(h.wavelength())
    print(h.turret())
    print(h.slit("Front_Entrance"))

    # Movimientos reales: habilitalos sólo cuando sea seguro.
    # h.move_wavelength(500.0)
    # h.move_turret(1)
    h.move_slit("Front_Entrance", 0)
    print(h.slit("Front_Entrance"))