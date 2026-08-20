<p align="center">
  <img src="images/urbgen-logo.jpg" width="260" alt="UrbGEN logo">
</p>

<h1 align="center">UrbGEN</h1>

<p align="center">
  <strong>Generative urban massing under planning constraints</strong><br>
  A GhPython-based tool for Grasshopper / Rhino
</p>

<p align="center">
  <a href="https://rhinopackages.github.io/?owner=17087&search=urbgen&p=UrbGEN">
    <img src="https://img.shields.io/badge/Rhino_Package_Manager-install-1a73e8" alt="Install via Package Manager"></a>
  <a href="https://trongtintr.github.io/UrbGEN/">
    <img src="https://img.shields.io/badge/docs-UrbGEN-0a7d3f" alt="Documentation"></a>
  <img src="https://img.shields.io/badge/Rhino-7%20%7C%208-orange" alt="Rhino 7 | 8">
  <img src="https://img.shields.io/badge/Grasshopper-GhPython-4c9a2a" alt="Grasshopper GhPython">
  <a href="LICENSE"><img src="https://img.shields.io/badge/license-MIT-blue" alt="MIT License"></a>
</p>

---

UrbGEN is a GhPython-based generative urban massing tool for Grasshopper / Rhino
that automatically generates 3D building configurations from site boundaries and
planning constraints. It enables rapid exploration of seven building typologies
while targeting Building Coverage Ratio (BCR), Floor Area Ratio (FAR), building
height and orientation requirements, supporting early-stage urban design and
design-space exploration.

Instead of modelling massing options by hand, the designer defines the
regulatory envelope — coverage, plot ratio, height limit, setback — and UrbGEN
resolves a family of valid configurations that satisfy it.

![UrbGEN generated massing across multiple sites](images/urbgen-generative.gif)

<sub>Massing generated across eleven sites, coloured by building height.</sub>

---

## Contents

- [Installation](#installation)
- [Why BCR and FAR?](#why-bcr-and-far)
- [Key features](#key-features)
- [How it works](#how-it-works)
- [Building typologies](#building-typologies)
- [Planning parameters](#planning-parameters)
- [Components](#components)
- [Examples](#examples)
- [Research](#research)
- [Citation](#citation)
- [License](#license)
- [Authors and contact](#authors-and-contact)

---

## Installation

**Rhino Package Manager (recommended)**

Open Rhino, run the `PackageManager` command, search for **urbgen** and install.
Restart Rhino — the components appear under the **UrbGEN** tab in Grasshopper.

**Yak CLI**

```
"C:\Program Files\Rhino 8\System\Yak.exe" install urbgen
```

**Manual**

Download the `.gha` file from [Releases](https://github.com/trongtintr/UrbGEN/releases)
and drop it into the Grasshopper components folder
(`%AppData%\Grasshopper\Libraries`). Right-click the file → **Properties** →
**Unblock**, then restart Rhino.

---

## Why BCR and FAR?

Building Coverage Ratio and Floor Area Ratio are how almost every zoning code
regulates urban form. But the pair **underdetermines form**: a parcel at
BCR 0.40 and FAR 3.0 can be slender towers on open ground, a perimeter block
around a courtyard, or a dense low mat. All equally compliant — and very
different for daylight, ventilation, thermal comfort and energy use.

That gap between compliance and performance is the research question. UrbGEN
treats the pair as a **constraint to converge on rather than an outcome to
measure afterwards**, generating hundreds of compliant alternatives at fixed
density — turning the regulatory envelope into a controlled experimental frame.

---

## Key features

<table>
<tr>
<td width="33%" valign="top">

**Constraint-driven**

Converges on target **BCR** and **FAR** simultaneously, reporting the achieved
values and the residual error for every site.

</td>
<td width="33%" valign="top">

**Seven typologies**

**I, L, T, H, C/U, Plus** and **Courtyard** footprints, selectable per site or
assigned automatically.

</td>
<td width="33%" valign="top">

**Multi-site batch**

Processes an entire block or district in one solve, with independent planning
targets per parcel.

</td>
</tr>
<tr>
<td width="33%" valign="top">

**Tower + podium**

Generates podium masses with configurable offset, floor count and thickness,
plus tower-to-podium ratio metrics.

</td>
<td width="33%" valign="top">

**Height regulation**

Enforces maximum and minimum building heights in strict or soft modes, and
reports violations and adjusted counts.

</td>
<td width="33%" valign="top">

**Design-space ready**

A seed input makes every run reproducible, so the component plugs directly into
Wallacei, Colibri or Sobol sampling.

</td>
</tr>
</table>

---

## How it works

The component takes a site curve and a set of planning targets, grows building
footprints iteratively until the coverage and plot-ratio targets are met, then
extrudes, places and aligns the masses within the setback envelope.

[![UrbGEN Grasshopper definition](images/urbgen-workflow.png)](images/urbgen-workflow.png)

<sub>Click to view the full definition.</sub>

**Pipeline**

1. **Input** — site boundary curve(s), setback distance, target BCR and FAR
2. **Populate** — candidate centroids distributed within the buildable area
3. **Typology** — footprint generated for each centroid from the selected grammar
4. **Grow** — footprints scaled iteratively toward the BCR target
5. **Extrude** — floor counts derived from the FAR target and floor height
6. **Position** — snap to setback, align to edges, move tower to podium edge
7. **Report** — actual BCR / FAR / GFA / heights returned as outputs

---

## Building typologies

Seven footprint grammars — **I, L, T, H, C/U, Plus, Courtyard** (plus a *mixed*
mode) — each parametrised by arm length ratio, width, and length-to-width limits.

![Typology variations under identical BCR and FAR targets](images/urbgen-typology2.png)

<sub>Nine configurations at BCR 0.40 / FAR 3.0 — the constraint is held constant
while the typology and rotation mode vary.</sub>

![C and U typology variations](images/urbgen-underBCRFARconstraint.png)

<sub>C/U typology under the same planning envelope, with and without global rotation.</sub>

---

## Planning parameters

| Parameter | Input | Description |
|---|---|---|
| **FAR** | `upperFAR` | Target floor area ratio; drives floor count and total GFA |
| **BCR** | `upperBCR` | Target building coverage ratio; drives footprint growth |
| **Height** | `maxBuildingHeight`, `minBuildingHeight`, `heightVariation` | Regulated envelope with strict or soft enforcement |
| **Setback** | `setback` | Offset from the site boundary defining the buildable area |
| **Orientation** | `globalRotationMode`, `uniformRotationDeg`, `alignTowersToEdge` | Uniform, per-building or edge-aligned rotation |

Additional groups cover **sizing and density** (footprint bounds, minimum tower
distance, growth step and iterations), **podium** (floors, offset range, floor
height), **courtyard** (count, break width, split angle, layout mode) and
**positioning** (snap to boundary, snap to setback, edge alignment).

See the [component reference](https://trongtintr.github.io/UrbGEN/components/UrbGENgenerator/)
for the full list of inputs and outputs.

![Height-coloured output across four seeds](images/urbgen-height-seeds.png)

<sub>Four generated alternatives on the same site, coloured 0–70 m.</sub>

---

## Components

| | Component | Purpose |
|---|---|---|
| <img src="images/UrbGENgenerator_icon.png" width="24"> | [**UrbGEN Generator**](https://trongtintr.github.io/UrbGEN/components/UrbGENgenerator/) | Generates masses from a site curve under BCR / FAR / height constraints |
| <img src="images/UrbGENPopulateRegion_icon.png" width="24"> | [**UrbGEN Populate Region**](https://trongtintr.github.io/UrbGEN/components/UrbGENPopulateRegion/) | Distributes candidate building centroids inside a bounded region |

---

## Examples

A typical definition: site curves on the left, planning targets grouped by
category in the middle, the UrbGEN component and its outputs on the right.

![UrbGEN in the Rhino viewport and Grasshopper canvas](images/urbgen-viewport.png)

<sub>Each site is labelled with its target BCR and the value actually achieved.</sub>

Because the solver is seeded, the same definition can be swept across a
parameter space to produce large sets of valid alternatives:

![Design space exploration results](images/urbgen-spaceexplore.png)

<sub>64 of 1000 designs — Site 3, 18,481 m², at FAR 3.0 and BCR 40%.</sub>

![Nine generated alternatives at district scale](images/urbgen-alternatives.png)

Sample Grasshopper definitions covering single-site, multi-site and design-space
workflows are in
[`UrbGEN_example/`](https://github.com/trongtintr/UrbGEN/tree/main/UrbGEN_example/UrbGEN_example).

---

## Research

UrbGEN was developed as part of ongoing research on generative urban morphology
and environmental performance. The tool couples a rule-based footprint grammar
with an iterative growth solver.

Generated datasets feed downstream analysis in Ladybug / Honeybee, ENVI-met and
multi-objective optimisation workflows, supporting studies of daylight, view
access, energy use intensity and outdoor thermal comfort across morphological
alternatives.

---

## Citation

If you use UrbGEN in academic work, please cite it as:

```bibtex
@software{tran_urbgen,
  author  = {Tran, Trong-Tin and Chan, Ying-Chieh},
  title   = {UrbGEN: Generative urban massing under planning constraints},
  url     = {https://github.com/trongtintr/UrbGEN},
  note    = {Ying-Chieh Chan's Lab, Department of Civil Engineering,
             National Taiwan University}
}
```

---

## License

Released under the [MIT License](LICENSE).

---

## Authors and contact

**[Trong-Tin Tran](https://sites.google.com/view/trantrongtin)** and
**[Ying-Chieh Chan](https://yingchiehchan.com/)**

Ying-Chieh Chan's Lab, Department of Civil Engineering,
National Taiwan University

[trongtintr@outlook.com](mailto:trongtintr@outlook.com) ·
[D14521024@ntu.edu.tw](mailto:d14521024@ntu.edu.tw)

Bug reports and feature requests are welcome via
[Issues](https://github.com/trongtintr/UrbGEN/issues).