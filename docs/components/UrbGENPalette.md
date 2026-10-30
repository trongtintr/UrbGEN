# UrbGEN Palette

![UrbGEN Palette icon](../images/UrbGENPalette_icon.png)

- **Nickname:** UrbGEN Palette
- **Location:** UrbGEN > 3 | Visualise
- **Version:** 0.1.15-beta (see [CHANGELOG](https://github.com/trongtintr/UrbGEN/blob/main/CHANGELOG.md))

---

## Description

Modern colour palettes — **70 sequential and diverging** — for UrbGEN height colouring and heatmaps. Outputs a smoothly resampled colour list (low → high) for the `palette` input of [UrbGEN Height Colors](UrbGENHeightColors.md); it also works with any colour-list input, e.g. Ladybug Legend Parameters `_colors_`.

Leave `paletteName` unconnected and the component creates a drop-down list with all 70 palettes.

## Inputs

| Name | Type | Access | Description |
|---|---|---|---|
| **paletteName** | `Text / Integer` | item | Palette name (default "viridis"; suffix "_r" = reversed) or index 0–69 from a Number Slider. Unconnected → a drop-down list with all palettes is created. |
| **colorCount** | `Integer` | item | Number of output colours (default 11; resampled smoothly). |
| **previewPoint** | `Point` | item | Base point of the preview swatches (default origin). |
| **reverse** | `Boolean` | item | Reverse the palette (default False). |

## Outputs

| Name | Type | Access | Description |
|---|---|---|---|
| **palette** | `Colour` | list | List of colours, low → high. |
| **paletteNames** | `Text` | list | All available palette names. |
| **palettePreview** | `Mesh` | item | Mesh with one coloured strip per palette (same order as paletteNames). |
