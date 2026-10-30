# UrbGEN Height Colors

![UrbGEN Height Colors icon](../images/UrbGENHeightColors_icon.png)

- **Nickname:** UrbGEN Height Colors
- **Location:** UrbGEN > 3 | Visualise
- **Version:** 0.1.15-beta (see [CHANGELOG](https://github.com/trongtintr/UrbGEN/blob/main/CHANGELOG.md))

---

## Description

Colours UrbGEN buildings by height and builds a matching height legend. Feed it the `TowerMasses` / `PodiumMasses` of the [Massing Generator](UrbGENgenerator.md) and a colour list from [UrbGEN Palette](UrbGENPalette.md).

**Typical wiring:** `buildingGeo` + `buildingColors` → Custom Preview (*Geometry* / *Material*), or preview `buildingMesh` directly; `labelPlanes` + `labelText` → Text Tag 3D (*Location* / *Text*). Flatten the `buildings` input if it arrives as a tree.

## Inputs

| Name | Type | Access | Description |
|---|---|---|---|
| **buildings** | `Geometry` | list | Building massing from UrbGEN (Brep / Extrusion / Mesh). Flatten trees. |
| **palette** | `Colour` | list | Colours from UrbGEN Palette `palette` (low → high). |
| **minHeight** | `Number` | item | Lower limit of the height domain (default: lowest building). |
| **maxHeight** | `Number` | item | Upper limit of the height domain (default: tallest building). |
| **colorMode** | `Integer` | item | 0 = one colour per building (default) · 1 = vertical gradient along Z. |
| **classCount** | `Integer` | item | 0 = smooth (default) · > 0 = number of discrete height classes. |
| **tickCount** | `Integer` | item | Number of legend labels when smooth (default 6). |
| **legendPlane** | `Plane` | item | Legend base plane, X = width, Y = up (default: vertical plane to the right of the site, bottom at ground). |
| **legendHeight** | `Number` | item | Legend height (default: maxHeight − minHeight, i.e. true scale). |
| **legendTitle** | `Text` | item | Legend title (default "Height (m)"). |

## Outputs

| Name | Type | Access | Description |
|---|---|---|---|
| **buildingGeo** | `Geometry` | list | Input buildings (flattened, same order as buildingColors). |
| **buildingColors** | `Colour` | list | One colour per building → Custom Preview *Material*. |
| **buildingHeights** | `Number` | list | Building heights. |
| **buildingMesh** | `Mesh` | list | Vertex-coloured building meshes (preview directly). |
| **legendMesh** | `Mesh` | item | Vertex-coloured legend bar. |
| **legendCurves** | `Curve` | list | Legend frame and tick lines. |
| **legendText** | `Curve` | list | Legend labels and title as curves. |
| **labelPlanes** | `Plane` | list | Label planes → Text Tag 3D *Location*. |
| **labelText** | `Text` | list | Label strings → Text Tag 3D *Text*. |
