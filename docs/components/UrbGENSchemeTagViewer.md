# UrbGEN Scheme Tag Viewer

![UrbGEN Scheme Tag Viewer icon](../images/UrbGENSchemeTagViewer_icon.png)

- **Nickname:** UrbGEN Scheme Tag Viewer
- **Location:** UrbGEN > 3 | Visualise
- **Version:** 0.1.15-beta (see [CHANGELOG](https://github.com/trongtintr/UrbGEN/blob/main/CHANGELOG.md))

---

## Description

Draws each scheme's `SchemeTag` (from the [Massing Generator](UrbGENgenerator.md)) as a screen-space label pinned next to its site in the Rhino viewports — fixed pixel size, readable at any zoom and always on top of the geometry.

- FAR / BCR lines are colour-coded: **green** = |error| within tolerance, **red** = outside, **grey** = site skipped or failed.
- Labels follow the component preview: hide, disable or delete the component and they disappear.
- **Bake** writes each tag into the document as a text object on layer `SchemeTags` (user string `SchemeTag` attached).

## Inputs

| Name | Type | Access | Description |
|---|---|---|---|
| **Tags** | `Text` | list | `SchemeTag` output of the Massing Generator. |
| **Sites** | `Curve` | list | `SiteCurves` (or `BuildableSite`) of the Massing Generator, one per tag. |
| **Show** | `Boolean` | item | Show the labels (default True). |
| **Corner** | `Integer` | item | Label position: 0 = above top-left (default) · 1 = right of top-right · 2 = below bottom-left · 3 = centre of site. |
| **Offset** | `Number` | item | Gap between site and label in model units (default 3). |
| **TextSize** | `Integer` | item | Label text size in pixels (default 13). |
| **FARTol** | `Number` | item | /FAR error/ tolerance for the green / red colouring (default 0.05). |
| **BCRTol** | `Number` | item | /BCR error/ tolerance for the green / red colouring (default 0.015). |
| **Bake** | `Boolean` | item | Connect a Button: bakes the labels once per press. |
| **BakeHeight** | `Number` | item | Baked text height in model units (default 2). |

## Outputs

| Name | Type | Access | Description |
|---|---|---|---|
| **Anchors** | `Point` | list | Point where each label is pinned. |
| **Status** | `Text` | list | "OK" / "FAR off" / "BCR off" / "FAR+BCR off" / "skipped" / "failed" per tag. |
| **Info** | `Text` | item | Short log. |
