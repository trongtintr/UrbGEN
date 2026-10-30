# UrbGEN Populate Region

![UrbGEN Populate Region icon](../images/UrbGENPopulateRegion_icon.png)

- **Nickname:** UrbGEN Populate Region
- **Location:** UrbGEN > 1 | Site
- **Version:** 0.1.15-beta (see [CHANGELOG](https://github.com/trongtintr/UrbGEN/blob/main/CHANGELOG.md))

---

## Description

Populates a closed planar region with seed points (holes excluded) — typically the `centroids` input of the [Massing Generator](UrbGENgenerator.md). The run type is set automatically by the inputs:

- **By count** (`Count` > 0): places the requested number of points and keeps the selected `Mode`. Grid modes solve the spacing to match `Count` within ±2 %. `MinDist` is an optional minimum-spacing constraint.
- **By distance** (`Count` empty or 0): the number of points follows from region area / `MinDist²`, so points are added or removed automatically when the boundary changes.

Grid modes (1–3) are anchored to world XY, so existing points stay fixed while the boundary is edited.

## Inputs

| Name | Type | Access | Description |
|---|---|---|---|
| **Crv** | `Curve` | item | Closed planar boundary curve (site outline). Points are placed strictly inside it. Open or non-planar curves return no points. |
| **Count** | `Integer` | item | Target number of points. > 0 = **by count** (grid modes solve the spacing to match Count within 2 %). 0 / empty = **by distance** (count follows area / MinDist²). |
| **Mode** | `Number` | item | 0 = Random (by count) / Poisson disk (by distance) (default) · 1 = Grid (square lattice) · 2 = Jittered grid · 3 = Staggered grid (hexagonal). Modes 1–3 are anchored to world XY. |
| **Jitter** | `Number` | item | Random offset strength, Mode 2 only (0–0.9, default 0). 0 = regular grid, 0.9 = very irregular. With MinDist set, the step is enlarged to MinDist / (1 − Jitter) so MinDist still holds. |
| **Angle** | `Generic` | item | Rotation of the pattern in **radians** around the curve plane's Z axis (default 0). Use it to align grid rows with a street or site edge. |
| **Seed** | `Integer` | item | Random seed (default 0). Same seed + same inputs = same result. Affects Mode 0 layout and Mode 2 offsets. |
| **MinDist** | `Number` | item | Minimum distance between points (model units). By distance: required, sets the density. By count: optional constraint — Mode 0 rejects closer points, Modes 1–3 use it as the smallest grid step (Count may fall short if it is too large). |
| **Holes** | `Curve` | list | Optional closed curves excluded from the region (courtyards, existing blocks, easements). Subtracted from the area when solving the grid spacing. |

## Outputs

| Name | Type | Access | Description |
|---|---|---|---|
| **Pts** | `Point` | list | Points inside the region. Ordered by grid row (Modes 1–3) or generation order (Mode 0). |
