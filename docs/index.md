# UrbGEN Documentation

<p align="left">
  <img src="images/urbgen-logo.jpg" width="240" alt="UrbGEN logo">
</p>

<p align="left">
  <strong>Generative urban massing under planning constraints</strong><br>
  A GhPython-based tool for Grasshopper / Rhino
</p>

---
UrbGEN — Generative Urban Massing Tool


UrbGEN is a GhPython-based generative urban massing tool for Grasshopper/Rhino that automatically generates 3D building configurations from site boundaries and planning constraints. The tool enables rapid exploration of seven building typologies while targeting Building Coverage Ratio (BCR), Floor Area Ratio (FAR), building height, orientation,... requirements, supporting early-stage urban design and design-space exploration.


Developed by: [Trong-Tin Tran](https://sites.google.com/view/trantrongtin) and [Ying-Chieh Chan](https://yingchiehchan.com/)

Lab: Ying-Chieh Chan's Lab, Department of Civil Engineering, National Taiwan University

Email: trongtintr@outlook.com | D14521024@ntu.edu.tw

## What is UrbGEN?

UrbGEN is a GhPython-based generative urban massing tool for Grasshopper / Rhino
that automatically generates 3D building configurations from site boundaries and
planning constraints. It enables rapid exploration of seven building typologies
while targeting FAR and BCR requirements, supporting multi-stage urban design
and design-space exploration.

Instead of modelling massing options by hand, the designer defines the
regulatory envelope — coverage, plot ratio, height limit, setback — and UrbGEN
resolves a family of valid configurations that satisfy it.

![UrbGEN generated massing across multiple sites](images/urbgen-generative.gif)

*Massing generated across eleven sites, coloured by building height.*

---

## What's new in 0.1.15

- **No fallback geometry** — a site either returns a fully constrained layout or fails
  with a reason in `Errors`.
- **Nearest-edge alignment** — each building aligns to the boundary edge it is closest to.
  New output `TowerEdgeAngleError`.
- **Tighter BCR** — the podium offset is solved to 0.4 % of the target area;
  `upperBCR` is a hard ceiling.
- **~1.5–1.9× faster** generation. Requires Rhino 8.

Full release notes: [CHANGELOG](https://github.com/trongtintr/UrbGEN/blob/main/CHANGELOG.md).

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

---

## Key Features

<table>
<tr>

<td width="33%" valign="top">

<b>Constraint-driven</b><br><br>

Converges on target <b>BCR</b> and <b>FAR</b> simultaneously, reporting the achieved
values and the residual error for every site.

</td>

<td width="33%" valign="top">

<b>Seven typologies</b><br><br>

<b>I, L, T, H, C/U, Plus</b> and <b>Courtyard</b> footprints, selectable per site or
assigned automatically.

</td>

<td width="33%" valign="top">

<b>Multi-site batch</b><br><br>

Processes an entire block or district in one solve, with independent planning
targets per parcel.

</td>

</tr>

<tr>

<td width="33%" valign="top">

<b>Tower + podium</b><br><br>

Generates podium masses with configurable offset, floor count and thickness,
plus tower-to-podium ratio metrics.

</td>

<td width="33%" valign="top">

<b>Height regulation</b><br><br>

Enforces maximum and minimum building heights in strict or soft modes, and
reports violations and adjusted counts.

</td>

<td width="33%" valign="top">

<b>Design-space ready</b><br><br>

A seed input makes every run reproducible, so the component plugs directly into
Wallacei, Colibri or Sobol sampling.

</td>

</tr>
</table>

---

## How it works

The component takes a site curve and a set of planning targets, grows building
footprints, aligns them to the nearest boundary edges, calibrates coverage with
towers and podium until the BCR target is met, then derives floors from the FAR
target and extrudes the masses within the setback envelope.

[![UrbGEN Grasshopper definition](images/urbgen-workflow.png)](images/urbgen-workflow.png)

*Click to view the full definition.*

**Pipeline**

1. **Input** — site boundary curve(s), setback distance, target BCR and FAR
2. **Populate** — candidate centroids distributed within the buildable area
3. **Typology** — footprint generated for each centroid from the selected grammar
4. **Grow** — footprints scaled iteratively toward the BCR target
5. **Extrude** — floor counts derived from the FAR target and floor height
6. **Position** — snap to setback, align to edges, move tower to podium edge
7. **Report** — actual BCR / FAR / GFA / heights returned as outputs

---

## Building Typologies

Seven footprint grammars (I,L,T,H,C/U,plus,courtyard,mixed), each parametrised by arm length ratio, width and
length-to-width limits.

![Building Typology](images/urbgen-typology.gif)
![Typology variations under identical BCR and FAR targets](images/urbgen-typology2.png)

*Nine configurations at BCR 0.40 / FAR 3.0 — the constraint is held constant
while the typology and rotation mode vary.*

![C and U typology variations](images/urbgen-underBCRFARconstraint.png)

*C/U typology under the same planning envelope, with and without global rotation.*

---

## Planning Parameters

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

See [Components](https://trongtintr.github.io/UrbGEN/components/UrbGENgenerator/) for the full input and output reference.

![Height-coloured output across four seeds](images/urbgen-height-seeds.png)

*Four generated alternatives on the same site, coloured 0–70 m.*

---

## Example

A typical definition: site curves on the left, planning targets grouped by
category in the middle, the UrbGEN component and its outputs on the right.

![UrbGEN in the Rhino viewport and Grasshopper canvas](images/urbgen-viewport.png)

*Each site is labelled with its target BCR and the value actually achieved.*

Because the solver is seeded, the same definition can be swept across a
parameter space to produce large sets of valid alternatives:

![Design space exploration results](images/urbgen-spaceexplore.png)

*64 of 1000 designs — Site 3, 18,481 m², at FAR 3.0 and BCR 40%.*

![Nine generated alternatives at district scale](images/urbgen-alternatives.png)

Sample files are provided in the `UrbGEN_example_v0.1.11beta` folder of the repository.

---

## Documentation

<table>
<tr>
<td width="33%" valign="top">

<b>Installation</b><br><br>

<a href="https://rhinopackages.github.io/?owner=17087&search=urbgen&p=UrbGEN">
Install UrbGEN
</a>

<br><br>

Install through the Rhino Package Manager, Yak CLI, or by dropping the
<code>.gha</code> file into the Grasshopper components folder.

</td>

<td width="33%" valign="top">

<b>Components</b><br><br>

<a href="https://trongtintr.github.io/UrbGEN/components/UrbGENgenerator/">
View Documentation
</a>

<br><br>

Reference for every input and output of the UrbGEN components.

</td>

<td width="33%" valign="top">

<b>Examples</b><br><br>

<a href="https://github.com/trongtintr/UrbGEN/tree/main/UrbGEN_example_v0.1.11beta">
View Examples
</a>

<br><br>

Grasshopper definitions covering single-site, multi-site, and design-space workflows.

</td>
</tr>
</table>

### Quick install

Open Rhino 8, run the `PackageManager` command, search for **urbgen**, tick
**Include pre-releases** and install. Restart Rhino — the components appear under the
**UrbGEN** tab in Grasshopper.

Alternatively, install the `.yak` package from the command line:

```
"C:\Program Files\Rhino 8\System\Yak.exe" install urbgen
```

### Components

| | Component | Purpose |
|---|---|---|
| <img src="images/UrbGENPopulateRegion_icon.png" width="24"> | [**UrbGEN Populate Region**](components/UrbGENPopulateRegion.md) | *1 · Site* — populate a closed region with seed points |
| <img src="images/UrbGENgenerator_icon.png" width="24"> | [**UrbGEN Massing Generator**](components/UrbGENgenerator.md) | *2 · Massing* — podium + tower massing under FAR / BCR / setback / height limits |
| <img src="images/UrbGENHeightColors_icon.png" width="24"> | [**UrbGEN Height Colors**](components/UrbGENHeightColors.md) | *3 · Visualise* — colour buildings by height, with a legend |
| <img src="images/UrbGENPalette_icon.png" width="24"> | [**UrbGEN Palette**](components/UrbGENPalette.md) | *3 · Visualise* — 70 sequential / diverging palettes |
| <img src="images/UrbGENSchemeTagViewer_icon.png" width="24"> | [**UrbGEN Scheme Tag Viewer**](components/UrbGENSchemeTagViewer.md) | *3 · Visualise* — target vs actual FAR / BCR label per site |

---

## Research

### Methodology

UrbGEN was developed as part of ongoing research on generative urban morphology
and environmental performance. The tool couples a rule-based footprint grammar
with an iterative growth solver.

Generated datasets feed downstream analysis in Ladybug/Honeybee, ENVI-met and
multi-objective optimisation workflows, supporting studies of daylight, view
access, energy use intensity and outdoor thermal comfort across morphological
alternatives.




---

## About

**[Trong-Tin Tran](https://sites.google.com/view/trantrongtin)** and **[Ying-Chieh Chan](https://yingchiehchan.com/)**

Ying-Chieh Chan's Lab, Department of Civil Engineering, 
National Taiwan University

Contact: [trongtintr@outlook.com](mailto:trongtintr@outlook.com) | [D14521024@ntu.edu.tw](mailto:d14521024@ntu.edu.tw)

Released under the MIT License.







## Components by Subcategory

### 1 | Site
| Name | Description |
|---|---|
| ![UrbGEN Populate Region](./images/UrbGENPopulateRegion_icon.png) [UrbGEN Populate Region](components/UrbGENPopulateRegion.md) | Populate a closed region with seed points, by count or by minimum distance |

### 2 | Massing
| Name | Description |
|---|---|
| ![UrbGEN Massing Generator](./images/UrbGENgenerator_icon.png) [UrbGEN Massing Generator](components/UrbGENgenerator.md) | Podium + tower massing under FAR / BCR / setback / height limits |

### 3 | Visualise
| Name | Description |
|---|---|
| ![UrbGEN Height Colors](./images/UrbGENHeightColors_icon.png) [UrbGEN Height Colors](components/UrbGENHeightColors.md) | Colour buildings by height, with a height legend |
| ![UrbGEN Palette](./images/UrbGENPalette_icon.png) [UrbGEN Palette](components/UrbGENPalette.md) | 70 sequential / diverging colour palettes |
| ![UrbGEN Scheme Tag Viewer](./images/UrbGENSchemeTagViewer_icon.png) [UrbGEN Scheme Tag Viewer](components/UrbGENSchemeTagViewer.md) | Target vs actual FAR / BCR label per site in the viewport |
