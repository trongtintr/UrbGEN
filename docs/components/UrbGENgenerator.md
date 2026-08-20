# UrbGEN generator
**Nickname:** UrbGEN generator  
**Location:** UrbGEN > UrbGEN  

<img src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAMlSURBVEhLpZZNSBRhGMf/73zvri7S+hUYIuohsQ3aDD0tCNpCUR3cugTSoc2Z2RUj3aTQ2RUTCkGkD+gmRZftFuGhi6wFXYKojh4ib522OkiBzBvP7EzpuppjLzyzzC7z/J7n//7fZxYAFAAiABWADEBKpSBv5DD8cxoTB42NOQzH49DQ2gptIIqQm5w+Fc4h2HmUbAsb9hRKvoOezaO0mICKeANqBAFv6kI45nYjOYAcSps53OQWJN+xAo1ADmCoBQEAPBxEwpVKTiah/JrBwtdJ5zvqbM81Pz9fb1mWxjkX6J4XoOwABFUMEqC3BQFVxL2BAYRayr/R3uy5xsfHE4ZhlHRdv5/NZnv5SnxnBx4g3gqN7iMBnANAFUmVCSvX2NjYoK7rnCKTyay+exwL/gGcPYzgFoDkAcKqI49vgGEY2wHJLtS4gNOkd8wF/gtAehcKBcWyLGlPAHlVYPhUF8QZSrhfACUmOUzTLBqG8dQ0zeoASiCLeKiIeGRZEIZ6XVeVAeSgqptMAErmVb5rBwTobkQTY/imScie6EA7AWoVpyPqhqy7Y/kC0CUcwCnG8J2SOy5S0Q+gsTKx53XfANL7SAPaZRGzQRUL3W1oogO3LXvZ833pdPoSHSw/AOaOCXQ0o0EW8YAxlGQRd+PxcgFU+cjIyAQloQ1Np9OfdV2n2BeANpOFwzgkMHxgDB8Zw7rA8DrZRcOPC6Ojo5OeU7yovN8NQBenSpnhDmN4mUlA7Yui8UIPIokEVAIYhjFbmaxakHWXFxPhrQByimQloZAsLSF0kyx0shnDF7KtHwB1dT0zUrSn2DaA6I4KHj+OOk3Cbc9NqRiCfgAUGfMat3N/AXSwBHdc88tRhCIBnA8FYNLZSMUgHwiwpQOaRSQJzSIHQLqrMuYUCc8seoH4BIxW6UCpL88i3hRBv6ZgUhTw48lFXPEs6gdQ2QFZVGmrQ9TTnaR5exXr9gxe2DncsPPS+Ptbnc+XsyfX9hOvsj1rlQCaqlKthmGqvrMZR7mFJTuPoj1FwYr2NFvd9BF8li15AHrB0EBzIhZzgCL9dfnfIGV+A7UEw3lc2rnTAAAAAElFTkSuQmCC" alt="UrbGEN generator Icon" width="24" height="24">

---

## Description
UrbGEN generator component

## Inputs
| Name | Type | Access | Description |
|---|---|---|---|
| **IIIIISITEandPLANNINGTARGET** | `Generic Data` | item | Site & Planning Targets group (Optional) |
| **________________________________** | `Generic Data` | item | rhinoscriptsyntax geometry (Optional) |
| **siteCrv** | `Curve` | list | Closed planar site boundary curve. All geometry is generated inside this outline. Must be closed, planar and lie on the WorldXY plane. (Optional) |
| **centroids** | `Point` | tree | Candidate seed points for tower placement. Points outside the setback line are discarded. Ignored entirely when towerTypologyMode = 7 (Courtyard), which derives its layout from the site outline instead. (Optional) |
| **setback** | `Number` | list | Setback distance (m) offset inward from the site boundary. Defines the buildable area; every footprint must sit fully inside it. If the offset fails, the full site boundary is used instead. (Optional) |
| **FAR** | `Number` | list | Target Floor Area Ratio. Total GFA (podium + towers) divided by site area. Drives the number of floors assigned to each tower
4=400%
5=500% (Optional) |
| **BCR** | `Number` | list | Target Building Coverage Ratio (0–1). Total ground footprint area divided by site area. Reached in two stages: tower growth first, then podium expansion.
0.3 = 30%
0.6 = 60% (Optional) |
| **upperBCR** | `Number` | list | Converts to collection of floating point numbers (Optional) |
| **________________________________** | `Generic Data` | item | rhinoscriptsyntax geometry (Optional) |
| **IIIIIIIIIIIIIIIIIISIZINGandDENSITY** | `Generic Data` | item | Tower Sizing & Density group (Optional) |
| **________________________________** | `Generic Data` | item | rhinoscriptsyntax geometry (Optional) |
| **minWidth** | `Number` | list | Base width (m) of every tower slab, i.e. the short side of the spine rectangle. Also the fixed band width for Courtyard typology. Values below 2.0 m are clamped. (Optional) |
| **towerSizeMode** | `Integer` | list | Footprint size strategy. 0 = Compact (small footprints, skewed low), 1 = Medium (mid range), 2 = Maximized (large footprints, skewed high), 3 = Random uniform. For Courtyard typology this instead controls break-point density and building length on the ring. (Optional) |
| **minFootprintPerTower** | `Number` | list | Minimum allowed footprint area (m²) per tower. Candidates below this are rejected during placement. Minimum 20 m². (Optional) |
| **maxFootprintPerTower** | `Number` | list | Maximum allowed footprint area (m²) per tower, including all attached arm modules. Caps spine length growth. Forced to be at least minFootprintPerTower + 30. (Optional) |
| **maxLengthWidthRatio** | `Number` | list | Maximum slenderness of a tower: spine length ÷ width. Prevents excessively elongated slabs. Minimum 2.0. (Optional) |
| **minTowerDistance** | `Number` | list | Minimum clear distance (m) between any two building footprints. Enforced during placement, growth, rotation and all move operations. (Optional) |
| **towerBCRPriority** | `Number` | list | Share of the BCR target (0–1) to be carried by tower footprints before the podium is generated. Higher = taller/thicker towers and a thinner podium; lower = a larger podium skirt. (Optional) |
| **towerGrowStep** | `Number` | list | Length increment (m) added to a tower's spine per growth attempt while converging on the tower BCR target. Smaller = finer convergence but slower. (Optional) |
| **towerGrowIterations** | `Integer` | list | towerGrowIterations (Optional) |
| **seed** | `Integer` | list | Random seed controlling point ordering, rotation angles, size sampling, podium noise and height distribution. Same seed = identical result. Taken modulo 10000. (Optional) |
| **________________________________** | `Generic Data` | item | rhinoscriptsyntax geometry (Optional) |
| **IIIIIIIIIIIIIIIIIIIIIIIIIIIIIIITYPOLOGY** | `Generic Data` | item | Building Typology group (Optional) |
| **________________________________** | `Generic Data` | item | rhinoscriptsyntax geometry (Optional) |
| **towerTypologyMode** | `Integer` | list | Building footprint shape. 0 = I (plain rectangle), 1 = L, 2 = T, 3 = H, 4 = C/U, 5 = Plus (+), 6 = Random per tower (picks 0–5), 7 = Courtyard (perimeter ring blocks; uses the site outline instead of centroids). (Optional) |
| **armLengthRatio** | `Number` | list | Length of each attached arm module as a multiple of minWidth. Only affects typologies 1–5. Auto-tuned by ±30% if the tower BCR target is missed. Minimum 0.3. (Optional) |
| **________________________________** | `Generic Data` | item | rhinoscriptsyntax geometry (Optional) |
| **IIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIPODIUM** | `Generic Data` | item | Podium group (Optional) |
| **________________________________** | `Generic Data` | item | rhinoscriptsyntax geometry (Optional) |
| **podiumFloors** | `Integer` | list | Number of podium floors. Podium height = podiumFloors × floorHeight. Towers are extruded starting from the top of the podium. (Optional) |
| **podiumMinOffset** | `Number` | list | Lower bound (m) for the podium offset search. The podium is the tower footprints offset outward by this distance, unioned, then clipped to the setback line. (Optional) |
| **podiumMaxOffset** | `Number` | list | Upper bound (m) for the podium offset search. Additionally capped at 35% of √(site area). Forced to be at least podiumMinOffset + 0.5. (Optional) |
| **floorHeight** | `Number` | list | Floor-to-floor height (m), used for both podium and tower floors. Converts floor counts into metric heights. (Optional) |
| **________________________________** | `Generic Data` | item | rhinoscriptsyntax geometry (Optional) |
| **IIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIROTATION** | `Generic Data` | item | Rotation group (Optional) |
| **________________________________** | `Generic Data` | item | rhinoscriptsyntax geometry (Optional) |
| **globalRotationMode** | `Integer` | list | Rotation strategy. 0 = per-tower random angle from a candidate set, 1 = uniform fixed angle for all towers (uses uniformRotationDeg), 2 or 3 = one randomly chosen angle from 0/45/90/135/180 applied to all towers. (Optional) |
| **uniformRotationDeg** | `Number` | list | Rotation angle in degrees applied to every tower when globalRotationMode = 1. Ignored in all other modes. Clamped 0–180. (Optional) |
| **________________________________** | `Generic Data` | item | rhinoscriptsyntax geometry (Optional) |
| **IIIIIIIIIIIIIIIIIIIIIIIIIIIICOURTYARD** | `Generic Data` | item | Courtyard setting group (Optional) |
| **________________________________** | `Generic Data` | item | rhinoscriptsyntax geometry (Optional) |
| **courtyardCount** | `Integer` | list | Number of separate courtyard zones the site is split into. Each zone produces its own ring of perimeter buildings around an open central void. Minimum 1. (Optional) |
| **courtyardBreakCount** | `Integer` | list | Base number of gaps cut into each courtyard ring. Adjusted by towerSizeMode: Compact adds gaps (more, shorter blocks), Maximized removes them (fewer, longer blocks). (Optional) |
| **courtyardBreakWidth** | `Number` | list | Converts to collection of floating point numbers (Optional) |
| **courtyardZoneGap** | `Number` | list | Clear distance (m) left between adjacent courtyard zones so separate rings never touch. Only applies when courtyardCount > 1. (Optional) |
| **courtyardSplitAngle** | `Number` | list | Rotation (degrees) of the zone-splitting axis relative to the site's longer bounding-box axis. 0 = split strictly along that axis. Clamped −45 to +45. (Optional) |
| **courtyardBreakShift** | `Number` | list | Distance (m) to shift break/cluster positions clockwise along the ring perimeter; wraps around. Used in Cluster layout and in the smooth-boundary fallback of Corner layout. (Optional) |
| **courtyardLayoutMode** | `Integer` | list | Ring layout strategy. 0 = Corner: one building anchored at each polygon corner growing outward along both edges, gaps land mid-edge (courtyardBreakShift ignored). 1 = Cluster: all buildings grouped into one contiguous run separated by courtyardBreakWidth, shifted around the ring by courtyardBreakShift. (Optional) |
| **________________________________** | `Generic Data` | item | rhinoscriptsyntax geometry (Optional) |
| **IIIIIIIIHEIGHTandREGULATION** | `Generic Data` | item | rhinoscriptsyntax geometry (Optional) |
| **________________________________** | `Generic Data` | item | rhinoscriptsyntax geometry (Optional) |
| **heightVariation** | `Number` | list | Converts to collection of floating point numbers (Optional) |
| **enforceHeightRegulation** | `Boolean` | list | Converts to collection of boolean values (Optional) |
| **heightRegulationMode** | `Integer` | list | Converts to collection of integer numbers (Optional) |
| **maxBuildingHeight** | `Number` | list | Converts to collection of floating point numbers (Optional) |
| **minBuildingHeight** | `Number` | list | Converts to collection of floating point numbers (Optional) |
| **________________________________** | `Generic Data` | item | rhinoscriptsyntax geometry (Optional) |
| **IIIIIIIIIIIIIIIIIIIIIIIIIIPOSITIONING** | `Generic Data` | item | Post-Placement Positioning Group (Optional) |
| **________________________________** | `Generic Data` | item | rhinoscriptsyntax geometry (Optional) |
| **moveToBoundary** | `Boolean` | list | Push each tower outward along the direction from the site centroid through the tower, until it reaches the setback line or is blocked by another building. Radial spread; creates an open centre. Skipped for Courtyard typology. (Optional) |
| **moveAllToSetback** | `Boolean` | list | Push every building along the shortest (perpendicular) direction to the nearest point on the setback line, rather than radially from the centre. Produces buildings hugging the site edges. Skipped for Courtyard typology. (Optional) |
| **alignTowersToEdge** | `Boolean` | list | Rotate each building in place, centre unchanged, so it lines up with the nearest boundary edge. Applied both at initial placement and again after the move steps, so alignment reflects each tower's final position. Skipped for Courtyard typology, which already follows the boundary. (Optional) |
| **edgeAlignBothOrientations** | `Boolean` | list | Fallback behaviour for edge alignment. True = if the long-edge-parallel rotation is blocked (leaves the site, overlaps, or breaks minTowerDistance), try the perpendicular orientation instead. False = only attempt the parallel orientation, otherwise keep the existing angle. Long-edge alignment is always attempted for every building before any short-edge fallback. (Optional) |
| **moveTowerToPodiumEdge** | `Boolean` | list | After the podium is built, slide each tower inside its own podium footprint toward the podium edge nearest the setback line, without changing the podium shape. Result: the tower sits flush on the street side while the podium stays wider behind it. Requires podium offset > 0. (Optional) |
| **________________________________** | `Generic Data` | item | rhinoscriptsyntax geometry (Optional) |
| **Run** | `Boolean` | item | Converts to collection of boolean values (Optional) |

## Outputs
| Name | Type | Access | Description |
|---|---|---|---|
| **________________________________** | `Generic Data` | item | rhinoscriptsyntax geometry |
| **TowerFootprints** | `Curve` | item | Ground footprint curve of each tower/block |
| **TowerMasses** | `Brep` | item | 3D tower solids, extruded from the podium top level |
| **PodiumFootprints** | `Curve` | item | Podium outlines after boolean union and clipping to the setback line |
| **PodiumMasses** | `Brep` | item | 3D podium solids |
| **UnionFootprint** | `Curve` | item | Alias of PodiumFootprints (total covered area) |
| **BuildableSite** | `Curve` | item | Site boundary after applying setback |
| **siteOriginal** | `Curve` | item | The original input site boundary |
| **________________________________** | `Generic Data` | item | rhinoscriptsyntax geometry |
| **TowerCentroids** | `Point` | item | Final centres after all move operations |
| **TowerAngles** | `Number` | item | Final rotation angle (degrees) after the align step |
| **TowerTypologies** | `Integer` | item | Typology code 0–7 |
| **TowerTypologyNames** | `Text` | item | Typology name: I / L / T / H / C / Plus / Courtyard |
| **TowerLengthUsed** | `Number` | item | Actual spine length used per tower |
| **TowerWidthUsed** | `Number` | item | Slab width (= clamped minWidth) |
| **TowerSizeRatio** | `Number` | item | Length / width ratio per tower |
| **NumTowers** | `Integer` | item | Final tower/block count |
| **________________________________** | `Generic Data` | item | rhinoscriptsyntax geometry |
| **ActualFAR** | `Generic Data` | item | rhinoscriptsyntax geometry |
| **FAR_Error** | `Generic Data` | item | rhinoscriptsyntax geometry |
| **ActualBCR** | `Generic Data` | item | rhinoscriptsyntax geometry |
| **BCR_Error** | `Generic Data` | item | rhinoscriptsyntax geometry |
| **TotalGFA** | `Generic Data` | item | rhinoscriptsyntax geometry |
| **TowerGFA** | `Generic Data` | item | rhinoscriptsyntax geometry |
| **PodiumGFA** | `Generic Data` | item | rhinoscriptsyntax geometry |
| **TowerTotalArea** | `Generic Data` | item | Total ground area covered by towers |
| **UnionArea** | `Generic Data` | item | Total covered area and its coverage ratio |
| **UnionBCR** | `Generic Data` | item | Total covered area and its coverage ratio |
| **TowerCoverageRatio** | `Generic Data` | item | Share of the target BCR area achieved |
| **PodiumCoverageRatio** | `Generic Data` | item | Share of the target BCR area achieved |
| **TowerAreaPercentOfSite** | `Generic Data` | item | Share of the site area covered |
| **PodiumAreaPercentOfSite** | `Generic Data` | item | Share of the site area covered |
| **________________________________** | `Generic Data` | item | rhinoscriptsyntax geometry |
| **PodiumOffset** | `Number` | item | Converts to collection of floating point numbers |
| **ActualPodiumOffset** | `Number` | item | Converts to collection of floating point numbers |
| **PodiumThicknessAvg** | `Number` | item | Converts to collection of floating point numbers |
| **PodiumAreaExpansionRatio** | `Number` | item | Converts to collection of floating point numbers |
| **TowerToPodiumRatio** | `Number` | item | Converts to collection of floating point numbers |
| **AdditionalPodiumArea** | `Number` | item | Converts to collection of floating point numbers |
| **AdditionalPodiumRatio** | `Number` | item | Converts to collection of floating point numbers |
| **AdditionalPodiumPercentOfSite** | `Number` | item | Converts to collection of floating point numbers |
| **PodiumEfficiency** | `Number` | item | Converts to collection of floating point numbers |
| **________________________________** | `Generic Data` | item | rhinoscriptsyntax geometry |
| **Floors** | `Integer` | item | Converts to collection of integer numbers |
| **TowerFloors** | `Integer` | item | Converts to collection of integer numbers |
| **TotalFloors** | `Integer` | item | Converts to collection of integer numbers |
| **Heights** | `Number` | item | Converts to collection of floating point numbers |
| **TowerHeights** | `Number` | item | Converts to collection of floating point numbers |
| **TotalBuildingHeights** | `Number` | item | Total height including the podium |
| **ActualHeightVariation** | `Number` | item | Converts to collection of floating point numbers |
| **HeightVariationError** | `Number` | item | Converts to collection of floating point numbers |
| **HeightBins** | `Generic Data` | item | rhinoscriptsyntax geometry |
| **HeightCounts** | `Generic Data` | item | rhinoscriptsyntax geometry |
| **HeightVariationOutputs** | `Generic Data` | item | Full bundle: min/max/avg, StdDev, CV, Q1/Median/Q3/IQR, per-tower HeightDetails |
| **________________________________** | `Generic Data` | item | rhinoscriptsyntax geometry |
| **HeightRegulationEnforced** | `Generic Data` | item | rhinoscriptsyntax geometry |
| **HeightRegulationMode** | `Generic Data` | item | rhinoscriptsyntax geometry |
| **MaxAllowedHeight** | `Generic Data` | item | rhinoscriptsyntax geometry |
| **MinAllowedHeight** | `Generic Data` | item | rhinoscriptsyntax geometry |
| **HeightViolationsCount** | `Generic Data` | item | rhinoscriptsyntax geometry |
| **HeightAdjustedCount** | `Generic Data` | item | rhinoscriptsyntax geometry |
| **HeightViolations** | `Generic Data` | item | rhinoscriptsyntax geometry |
| **________________________________** | `Generic Data` | item | rhinoscriptsyntax geometry |
| **SeedUsed** | `Generic Data` | item | rhinoscriptsyntax geometry |
