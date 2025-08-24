import random
import argparse
from dataclasses import dataclass

try:
    from reportlab.lib.pagesizes import A4
    from reportlab.lib import colors
    from reportlab.platypus import SimpleDocTemplate, Paragraph, Spacer, Table, TableStyle
    from reportlab.lib.styles import getSampleStyleSheet
    REPORTLAB_OK = True
except Exception:
    REPORTLAB_OK = False

SEED_PFIZER = 42
SEED_AZ = 99

class Vaccine:
    PFIZER = "PFIZER"
    ASTRAZENECA = "ASTRAZENECA"

@dataclass(frozen=True, eq=True)
class Citizen:
    id: int
    name: str

def sample(objs, k, seed):
    rng = random.Random(seed)
    pool = list(objs)
    rng.shuffle(pool)
    return set(pool[:k])

def imprimir_titulo_y_listado(titulo, conjunto):
    print(f"\n=== {titulo} (total: {len(conjunto)}) ===")
    for c in sorted(conjunto, key=lambda x: x.id):
        print(c.name)

def generar_pdf(path, no_vacunados, ambas_dosis, solo_pfizer, solo_astra):
    if not REPORTLAB_OK:
        print("No se encontró reportlab; instálalo con: pip install reportlab")
        return

    doc = SimpleDocTemplate(path, pagesize=A4)
    styles = getSampleStyleSheet()
    elems = []

    elems.append(Paragraph("Reporte de Vacunación COVID-19 (Datos Ficticios)", styles['Title']))
    elems.append(Spacer(1, 12))

    # Resumen
    resumen_data = [
        ["Listado", "Total"],
        ["No vacunados", len(no_vacunados)],
        ["Ambas dosis (mezcla)", len(ambas_dosis)],
        ["Solo Pfizer", len(solo_pfizer)],
        ["Solo AstraZeneca", len(solo_astra)],
    ]
    resumen_tbl = Table(resumen_data, hAlign='LEFT')
    resumen_tbl.setStyle(TableStyle([
        ('BACKGROUND', (0,0), (-1,0), colors.lightgrey),
        ('GRID', (0,0), (-1,-1), 0.25, colors.grey),
        ('FONTNAME', (0,0), (-1,0), 'Helvetica-Bold'),
    ]))
    elems.append(Paragraph("Resumen de resultados", styles['Heading2']))
    elems.append(resumen_tbl)
    elems.append(Spacer(1, 12))

    def tabla_seccion(titulo, datos):
        elems.append(Paragraph(f"{titulo} (total: {len(datos)})", styles['Heading3']))
        rows = [["#","Ciudadano"]]
        rows += [[c.id, c.name] for c in sorted(datos, key=lambda x: x.id)]
        tbl = Table(rows, hAlign='LEFT')
        tbl.setStyle(TableStyle([
            ('BACKGROUND', (0,0), (-1,0), colors.lightgrey),
            ('GRID', (0,0), (-1,-1), 0.25, colors.grey),
            ('FONTNAME', (0,0), (-1,0), 'Helvetica-Bold'),
        ]))
        elems.append(tbl)
        elems.append(Spacer(1, 12))

    tabla_seccion("No vacunados", no_vacunados)
    tabla_seccion("Ambas dosis (mezcla Pfizer/AstraZeneca)", ambas_dosis)
    tabla_seccion("Solo Pfizer", solo_pfizer)
    tabla_seccion("Solo AstraZeneca", solo_astra)

    doc.build(elems)
    print(f"\nPDF generado: {path}")

def main():
    parser = argparse.ArgumentParser(description="Procesamiento de vacunación con teoría de conjuntos (datos ficticios)")
    parser.add_argument("--pdf", type=str, default=None, help="Ruta de salida para el PDF (opcional)")
    args = parser.parse_args()

    # Universo U
    universe_list = [Citizen(i, f"Ciudadano {i}") for i in range(1, 501)]
    U = set(universe_list)

    # Muestras (75 y 75)
    pfizer = sample(universe_list, 75, SEED_PFIZER)
    astra = sample(universe_list, 75, SEED_AZ)

    # Operaciones de conjuntos
    union = pfizer | astra
    ambas = pfizer & astra
    solo_pfizer = pfizer - astra
    solo_astra = astra - pfizer
    no_vacunados = U - union

    # Consola
    imprimir_titulo_y_listado("Ciudadanos que NO se han vacunado", no_vacunados)
    imprimir_titulo_y_listado("Ciudadanos que han recibido AMBAS dosis", ambas)
    imprimir_titulo_y_listado("Ciudadanos que SOLO han recibido Pfizer", solo_pfizer)
    imprimir_titulo_y_listado("Ciudadanos que SOLO han recibido AstraZeneca", solo_astra)

    # Verificación
    print("\n--- Verificación ---")
    print("Total ciudadanos:", len(U))
    print("Pfizer (objetivo 75):", len(pfizer))
    print("AstraZeneca (objetivo 75):", len(astra))
    print("Vacunados (P ∪ A):", len(union))
    print("No vacunados (U − (P ∪ A)):", len(no_vacunados))
    print("Ambas dosis (P ∩ A):", len(ambas))
    print("Solo Pfizer (P − A):", len(solo_pfizer))
    print("Solo AstraZeneca (A − P):", len(solo_astra))

    # PDF (opcional)
    if args.pdf:
        generar_pdf(args.pdf, no_vacunados, ambas, solo_pfizer, solo_astra)

if __name__ == "__main__":
    main()
