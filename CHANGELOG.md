# Changelog

All notable changes to UrbGEN. Versions follow the Rhino Package Manager (Yak) releases.

## [0.1.15-beta] — 2026-10-30

Rhino 8 only (CPython 3.9). Package: `urbgen-0.1.15-beta-rh8_0-any.yak`.

### ⚠️ Behaviour changes — read before re-running old studies

- **No fallback geometry.** A site that cannot satisfy its constraints now **fails**
  (`Success = False`, no geometry, reason in `Errors`) instead of returning substitute
  geometry. Removed substitutions:
  - the unvalidated single I-shaped "fallback tower" when no tower passed validation;
  - using the full site boundary when the setback offset failed (silently ignored the setback);
  - switching to the site centroid when all supplied `centroids` lay outside the buildable area.
- **BCR is hit much more tightly**, so results differ from earlier versions at the same
  seed: the old shrink/expand loop settled anywhere between ~0.94 × BCR and `upperBCR`.
- `upperBCR` is now a **hard ceiling** — exceeding it fails the site.
- Towers are **never shrunk below `minFootprintPerTower`** (the old calibration allowed 50 %).
- Tower areas are now exact; when several towers share the same area, the floor
  distribution may break ties differently from earlier versions.
- Datasets generated with earlier versions may contain fallback results; re-run them for
  comparisons with 0.1.15.

### Added

- `TowerEdgeAngleError` output: deviation (deg) between each tower spine and its nearest
  boundary edge (0 = parallel).
- BCR cascade log line in `Report`, including an explicit message when the target is
  unreachable and the maximum achievable BCR.

### Changed — edge alignment

- Alignment uses the **real straight edges** of the buildable site (polyline segments,
  near-collinear pieces merged) instead of the curve tangent at the closest point, which
  picked an arbitrary edge at vertices.
- **Nearest-edge priority:** each building aligns to the edge it is closest to. Priority:
  nearest edge parallel → nearest edge perpendicular → next edge parallel → next edge
  perpendicular. Ties (corner bisector) go to the edge the building fronts, then the longer
  edge. Previously the longer edge won anywhere within half a tower width of the nearest one.
- Blocked towers are **slid inward, slid along the edge, or shortened** (≥ 60 %, never below
  `minFootprintPerTower`) before a lower-priority orientation is tried.
- L / C typologies: the flip whose spine backs onto the boundary is preferred.
- Alignment now runs **before** move-to-boundary / move-to-setback; move-to-setback slides
  along the edge normal so towers stay parallel and land flush.
- Test set (226 towers): towers aligned to their nearest edge **92 % → 98 %**.

### Changed — BCR cascade

- Rewritten: (a) uniform tower rescale by bisection when towers alone exceed the target,
  removing a building only if minimum-size towers still exceed it; (b) podium offset solved by
  regula falsi to **0.4 %** of the target BCR area (was 1.5 %); (c) tower rescale under the
  minimum podium when `podiumMinOffset` already overshoots; (d) tower growth + re-solve when
  the podium saturates at `podiumMaxOffset`.
- Courtyard coverage is re-bisected against the full BCR target.

### Fixed

- Adaptive arm length rebuilt towers from their **initial** lengths, silently discarding the
  growth step.
- `podiumMaxOffset` was silently capped at 0.35 × √(site area).
- Setback offset is now orientation-safe and uses the largest offset piece.

### Performance

About **1.8–1.9× faster** than before the optimisation (measured in Rhino 8):

- lazy typology footprints — area from the cached prototype, bounding boxes in pure Python,
  RhinoCommon curves built only when a check needs them;
- no `CreateBooleanIntersection` when two towers are already ≥ `minTowerDistance` apart;
- podiums memoised per site (random state restored, results unchanged); typology-I podiums
  built analytically instead of `Curve.Offset`.

Nearest-edge alignment adds ~25 % back; net ≈ 1.5× faster than the pre-optimisation code.
Courtyard (mode 7) speed is unchanged.

### Known issues

- Two in-component tooltips still describe old behaviour: `podiumMaxOffset` (mentions the
  removed 0.35 × √area cap) and `alignTowersToEdge` (does not mention nearest-edge
  priority). The [component reference](docs/components/UrbGENgenerator.md) is correct.
- `TowerLengthUsed` and `TowerSizeRatio` outputs are not populated.
- Mode 6 (random typology) and mode 4 (C) can fail on small sites: when the chosen typology
  does not fit the footprint limits, no other typology is tried at that seed point.

## Earlier releases

Published on the Rhino Package Manager: 0.1.13-beta, 0.1.12-beta, 0.1.11-beta,
0.1.10-beta, 0.1.9-beta, 0.1.8-beta, 0.1.7-beta, 0.1.6-beta, 0.1.5-beta, 0.1.4-beta,
0.1.3-beta, 0.1.2-beta.
