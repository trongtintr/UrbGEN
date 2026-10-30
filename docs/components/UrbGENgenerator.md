# UrbGEN Massing Generator

![UrbGEN Massing Generator icon](../images/UrbGENgenerator_icon.png)

- **Nickname:** UrbGEN Massing Generator
- **Location:** UrbGEN > 2 | Massing
- **Version:** 0.1.15-beta (see [CHANGELOG](https://github.com/trongtintr/UrbGEN/blob/main/CHANGELOG.md))

---

## Description

Generates podium + tower massing for one or many sites that meets target FAR, BCR, setback
and height limits. Every site either returns a **fully constrained layout** or **fails**
(`Success = False`, empty geometry, reason in `Errors`) — no fallback geometry is ever returned.

**Pipeline (0.1.15):** buildable site (setback) → seed points → typology footprints →
growth toward the tower share of BCR → adaptive arm length → **edge alignment (nearest edge
first)** → move-to-boundary / move-to-setback → **BCR cascade** (tower rescale → podium offset
solved to 0.4 % of the target area → tower growth if the podium saturates) → floors from FAR
with height regulation → extrusion and metrics.

Most numeric and boolean inputs accept a **per-site list**: one value per site, the last
value repeats for the remaining sites.

## Inputs

| Name | Type | Access | Description |
|---|---|---|---|
| ***Site & planning targets*** | | | |
| **siteCrv** | `Curve` | list | Site boundary curve(s), closed and planar (List Access). One curve = one site. |
| **centroids** | `Point` | tree | Tower seed points (Point3d). DataTree: one branch per site, or a flat list shared by all sites. Points outside the buildable area are dropped; if points are supplied but none lies inside, the site FAILS. With no points at all, the site centroid is used as the seed. Ignored when towerTypologyMode = 7 (Courtyard). |
| **setback** | `Number` | list | Setback from the site boundary (m). Defines the buildable area. Default 5.0. If the setback cannot be applied (too large for the site) the site FAILS - there is no fallback to the full boundary. |
| **FAR** | `Number` | list | Target Floor Area Ratio (total GFA / site area). Default 3.0. |
| **BCR** | `Number` | list | Target Building Coverage Ratio (footprint / site area). Default 0.5. |
| **upperBCR** | `Number` | list | Hard upper BCR limit: the BCR cascade never exceeds it (site fails instead). Default BCR x 1.1 (min BCR x 1.05). |
| ***Sizing & density*** | | | |
| **minWidth** | `Number` | list | Tower plate width / module depth (m). Also the courtyard band width. Default 12. |
| **towerSizeMode** | `Integer` | list | Footprint size bias: 0 small, 1 medium, 2 large, 3 random. Default 1. |
| **minFootprintPerTower** | `Number` | list | Minimum footprint per tower (sqm, >= 20). Default 80. |
| **maxFootprintPerTower** | `Number` | list | Maximum footprint per tower (sqm, >= min + 30). Default 350. |
| **maxLengthWidthRatio** | `Number` | list | Maximum spine length / width ratio of a tower (>= 2). Default 4. |
| **minTowerDistance** | `Number` | list | Minimum clear distance between towers (m). Default 12. |
| **towerBCRPriority** | `Number` | list | Share of the BCR target reached by tower footprints before the podium fills the rest (0-1). Default 0.65. |
| **towerGrowStep** | `Number` | list | Length increment per growth step when growing towers to the BCR target (m). Default 1. |
| **towerGrowIterations** | `Integer` | list | Maximum growth iterations (also caps courtyard coverage search). Default 80. |
| **seed** | `Integer` | list | Random seed (0-9999). Same seed = same result. |
| ***Typology*** | | | |
| **towerTypologyMode** | `Integer` | list | Tower shape: 0 I, 1 L, 2 T, 3 H, 4 C, 5 Plus, 6 random mix, 7 Courtyard (perimeter blocks, centroids ignored). Default 0. |
| **armLengthRatio** | `Number` | list | Arm length of L/T/H/C/Plus shapes as a multiple of minWidth (>= 0.3). Auto-adapted +/-30 % to hit BCR. Default 1.3. |
| ***Podium*** | | | |
| **podiumFloors** | `Integer` | list | Number of podium floors. Default 2. |
| **podiumMinOffset** | `Number` | list | Minimum podium offset around each tower (m). Default 2. |
| **podiumMaxOffset** | `Number` | list | Maximum podium offset around each tower (m). Default 15. Forced to be at least podiumMinOffset + 0.5. |
| **floorHeight** | `Number` | list | Floor-to-floor height (m), used for podium and towers. Default 3.5. |
| ***Rotation*** | | | |
| **globalRotationMode** | `Integer` | list | Tower rotation: 0 free (best fit), 1 uniform angle (uniformRotationDeg), 2/3 one random angle from {0,45,90,135,180} for all towers. Default 0. |
| **uniformRotationDeg** | `Number` | list | Rotation (deg, 0-180) used when globalRotationMode = 1. |
| ***Courtyard*** | | | |
| **courtyardCount** | `Integer` | list | [Courtyard] Number of zones the site is split into. Default 1. |
| **courtyardBreakCount** | `Integer` | list | [Courtyard] Number of breaks (gaps) in each perimeter ring. Default 4. |
| **courtyardBreakWidth** | `Number` | list | [Courtyard] Width of each break (m). Default max(1.5 x minWidth, 4). |
| **courtyardZoneGap** | `Number` | list | [Courtyard] Gap between zones (m). Default max(minTowerDistance, minWidth). |
| **courtyardSplitAngle** | `Number` | list | [Courtyard] Rotation of the zone split lines (deg, -45 to 45). Default 0. |
| **courtyardBreakShift** | `Number` | list | [Courtyard] Shift of the break positions along the ring (m). Default 0. |
| **courtyardLayoutMode** | `Integer` | list | [Courtyard] 0 = blocks anchored on corners, 1 = consecutive bands from the ring start. Default 0. |
| ***Height & regulation*** | | | |
| **heightVariation** | `Number` | list | Height variation between towers as a fraction of the base floor count (0 = all equal, 0.5 = range of 50 %). Default 0. |
| **enforceHeightRegulation** | `Boolean` | list | True = apply heightRegulationMode. Default False. |
| **heightRegulationMode** | `Integer` | list | 0 off, 1 strict (hard cap at maxBuildingHeight, FAR may fall short), 2 flexible (excess softened by 70 %). Default 0. |
| **maxBuildingHeight** | `Number` | list | Maximum total building height incl. podium (m). Default 100. |
| **minBuildingHeight** | `Number` | list | Minimum total building height incl. podium (m). Default 3. |
| ***Positioning*** | | | |
| **moveToBoundary** | `Boolean` | list | True = push towers outward from the site centroid toward the setback line. |
| **moveAllToSetback** | `Boolean` | list | True = slide each tower along the normal of its nearest boundary edge until it is flush with the setback line (keeps the edge alignment). Skipped for Courtyard. |
| **alignTowersToEdge** | `Boolean` | list | True = align every tower to the boundary edge it is CLOSEST to (nearest edge first, next-closest edge second). A tower that is blocked is slid inward, slid along the edge, or its spine is shortened (to >= 60 %, never below minFootprintPerTower) before a lower-priority orientation is tried. Runs before the move steps. Skipped for Courtyard. |
| **edgeAlignBothOrientations** | `Boolean` | list | With alignTowersToEdge: priority order per tower = nearest edge parallel -> nearest edge perpendicular -> next edge parallel -> next edge perpendicular. False = parallel orientations only. Default True. |
| **moveTowerToPodiumEdge** | `Boolean` | list | True = slide each tower to the outer edge of its own podium. |
| **Run** | `Boolean` | item | Gate. True = solve. Per-site list allowed (False skips that site). |

## Outputs

| Name | Type | Access | Description |
|---|---|---|---|
| **TowerFootprints** | `Curve` | list / tree | Tower footprint curves. Tree: {site}. |
| **TowerMasses** | `Brep` | list / tree | Tower 3D masses (Brep), sitting on top of the podium. Tree: {site}. |
| **PodiumFootprints** | `Curve` | list / tree | Podium outline curves (clipped to the buildable area). Tree: {site}. |
| **PodiumMasses** | `Brep` | list / tree | Podium 3D masses (Brep). Tree: {site}. |
| **UnionFootprint** | `Curve` | list / tree | Alias of PodiumFootprints (total built footprint). Tree: {site}. |
| **BuildableSite** | `Curve` | list / tree | Buildable area after setback, per site. |
| **siteOriginal** | `Curve` | list / tree | Original site curves as received. |
| **TowerCentroids** | `Point` | list / tree | Tower placement points after moves. Tree: {site}. |
| **TowerAngles** | `Number` | list / tree | Tower rotation angles (deg). Tree: {site}. |
| **TowerTypologies** | `Integer` | list / tree | Tower typology codes (0 I, 1 L, 2 T, 3 H, 4 C, 5 Plus, 7 Courtyard). Tree: {site}. |
| **TowerTypologyNames** | `Text` | list / tree | Tower typology names. Tree: {site}. |
| **TowerEdgeAngleError** | `Number` | list / tree | NEW 0.1.15. Deviation (deg) of each tower spine from its nearest boundary edge (0 = parallel; with edgeAlignBothOrientations also 0 = perpendicular; -1 = no edge). Tree: {site}. |
| **TowerLengthUsed** | `Number` | list / tree | Not populated in 0.1.15 (always empty). |
| **TowerWidthUsed** | `Number` | list / tree | Tower width used (m). |
| **TowerSizeRatio** | `Number` | list / tree | Not populated in 0.1.15 (always empty). |
| **NumTowers** | `Integer` | list / tree | Number of towers / buildings generated. |
| **ActualFAR** | `Number` | list / tree | Achieved FAR. |
| **FAR_Error** | `Number` | list / tree | /ActualFAR - target FAR/. |
| **ActualBCR** | `Number` | list / tree | Achieved BCR (podium footprint / site area). |
| **BCR_Error** | `Number` | list / tree | /ActualBCR - target BCR/. |
| **TotalGFA** | `Number` | list / tree | PodiumGFA + TowerGFA (sqm). |
| **TowerGFA** | `Number` | list / tree | Tower GFA = sum(tower area x floors) (sqm). |
| **PodiumGFA** | `Number` | list / tree | Podium GFA = podium area x podiumFloors (sqm). |
| **TowerTotalArea** | `Number` | list / tree | Sum of tower footprint areas (sqm). |
| **UnionArea** | `Number` | list / tree | Area of the union footprint (sqm). |
| **UnionBCR** | `Number` | list / tree | UnionArea / site area. |
| **TowerCoverageRatio** | `Number` | list / tree | Tower footprint / site area. |
| **PodiumCoverageRatio** | `Number` | list / tree | Podium footprint / site area. |
| **TowerAreaPercentOfSite** | `Number` | list / tree | Tower footprint as % of site area. |
| **PodiumAreaPercentOfSite** | `Number` | list / tree | Podium footprint as % of site area. |
| **PodiumOffset** | `Number` | list / tree | Podium offset used (m). |
| **ActualPodiumOffset** | `Number` | list / tree | Alias of PodiumOffset. |
| **PodiumThicknessAvg** | `Number` | list / tree | Average podium ring thickness = additional area / tower perimeter (m). |
| **PodiumAreaExpansionRatio** | `Number` | list / tree | Podium area / tower footprint area. |
| **TowerToPodiumRatio** | `Number` | list / tree | Tower footprint area / podium area. |
| **AdditionalPodiumArea** | `Number` | list / tree | Podium area beyond the tower footprints (sqm). |
| **AdditionalPodiumRatio** | `Number` | list / tree | Additional podium area / tower footprint area. |
| **AdditionalPodiumPercentOfSite** | `Number` | list / tree | Additional podium area as % of site area. |
| **PodiumEfficiency** | `Number` | list / tree | Tower footprint / podium area (%). |
| **Floors** | `Integer` | list / tree | Tower floors above the podium. Tree: {site}. |
| **TowerFloors** | `Integer` | list / tree | Alias of Floors. |
| **TotalFloors** | `Integer` | list / tree | Sum of tower floors. |
| **Heights** | `Number` | list / tree | Tower heights above the podium (m). Tree: {site}. |
| **TowerHeights** | `Number` | list / tree | Alias of Heights. |
| **TotalBuildingHeights** | `Number` | list / tree | Podium + tower height per building (m). Tree: {site}. |
| **ActualHeightVariation** | `Number` | list / tree | Achieved height variation (floor range / base floors). |
| **HeightVariationError** | `Number` | list / tree | /ActualHeightVariation - heightVariation/. |
| **HeightBins** | `Generic Data` | list / tree | Height histogram bin centres (m). Tree: {site}. |
| **HeightCounts** | `Generic Data` | list / tree | Height histogram counts. Tree: {site}. |
| **HeightVariationOutputs** | `Generic Data` | list / tree | Readable summary of the height variation check. |
| **HeightRegulationEnforced** | `Boolean` | list / tree | Whether the height regulation was enforced. |
| **HeightRegulationMode** | `Integer` | list / tree | Height regulation mode used (0 off, 1 strict, 2 flexible). |
| **MaxAllowedHeight** | `Number` | list / tree | maxBuildingHeight input per site (m). |
| **MinAllowedHeight** | `Number` | list / tree | minBuildingHeight input per site (m). |
| **HeightViolationsCount** | `Integer` | list / tree | Towers that violated the height limits before adjustment. |
| **HeightAdjustedCount** | `Integer` | list / tree | Towers adjusted by the height regulation. |
| **HeightViolations** | `Generic Data` | list / tree | Violation details per tower. Tree: {site}. |
| **SeedUsed** | `Integer` | list / tree | Seed used. |
| **SchemeTag** | `Text` | list / tree | Scheme label per site: target vs actual FAR / BCR and signed error (actual - target), 3 decimals. Ready for a Text Tag. |
| **Report** | `Text` | list / tree | Text log per site. |
| **Success** | `Boolean` | list / tree | True if the site produced a fully constrained layout. False = the site failed: no geometry is returned and the reason is in Errors (0.1.15 never returns fallback geometry). |
| **Errors** | `Text` | list / tree | Error message per site (empty if OK). |
| **Timings** | `Text` | list / tree | Per-stage solve time per site (ms). |
