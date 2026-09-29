# UrbGEN_PopulateRegion
**Nickname:** UrbGEN PopulateRegion  
**Location:** UrbGEN > UrbGEN  

<img src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAPuSURBVEhLpZbLbxtVFMa/O08/Wiuqm6SRjKIqYUEVgiAkMnRhqcKxERGwiJF4iBT8HM+M48SPOHQxDXYiikCIp9RtC0JiBYIFQhWtgH2hbNgh+AeaRuyQfNG5mUnHE6dFcKTjeWju+d1z7nfuNQBoAGQAOgAVgFIqQd3bxNm/tpD5r763hbOpFEKYnEQoPYuoG5yuGueQ+g5+6Dv42b3e37uu++7fz0JHahTHJAk/jUTxsJuN4gH+7uFp7kC5nzv2y8nGavlG2yx+yi9D5dcRovECsJxAGACPRZB1S6XmctDuXMTKbzXM0jPnYO16YflCsziFgHEOuW6XvjIMg5umwTdr+TT/AtohQETHIgGSCYR1GW+n04gmEggnkwjXq+WrNNgyy7ffWX/xZADAmna5a9sGt63KrrOWn74nIDWJED3Hw3gWgGTb0FfN8g2aoW1VuNPJT/sBHqRTOz9PwcWzH7A0gYgPoHiAmC5KJtG7Rq0wR2VoV197gYIFAUEbAOTO4JgLyFC951ygHyAGccj/JjjZAIC0KjH8OhLBMxTwKMDBYA5GMP+7oA0AKIAq42NNxieOA2k56apqH0C9IT4SAznYhl0s0Jq0zNcfHwjKxWT274OAmTGMM4bdkIL2Y9MgKfLjmsiIsjmYLS0wLTQtuGWVb3qZOE7+xHq92GxZ+ZzIMAign1gYC4zhDgUnj+s4B2DMC05GErXN8i5Jdr1W+ZJmTb5qlq5UqwYnqXaM8/NDAVTvB0YxpcroRXS8N3Ma49RwfgAZNZtoJnfBKYu1WumKkLFt8DesVxeGAehjEWz6FEZVGR8xhtuqjEup1OAiDzMqUXO9stWyi4WjSkSLyWIxnJAYfmEMtxjDnxLDj7kzh7MYZn5lBQH0I2apMmwzhm/sLPQnZjH2/Dzi2Sx0t9b3lKbfggBSiuLkoFFZElHMUFmosxnDH6UlRBpW+YNh0jzKhgFkd6vgqUcwElJwwVNTx3xpybIMIU2CDJTCp32/BQHUWJK7XfNXZhGNh/FcNAyTeqNXzEyQNAnStCsfetJsmflCu15s0gJ7gUlhDaOQv9rLTPgBtBdRSWgvEgCqu65iR1PwmeNACUqzvVYokiQpq8ZqoSX2KQeKVS38LnrEKt08ONHcDLST+3sRH4/jXEhDR2LYu7SIpbuJ3zXqWAJQczUsAZD4dSh0HhC0Xivt+gEkUe30iDi9RN2pNNdW8G2/h43+NtKHfAeL1zYW3v1648nL/beQ8d5/v/nom583n/ruVnfK9peIALSrKsdDWKHZP3gKD/Uvwuh30etvYedI7w55t+1ee6h4ADpgSBnC5+YEUKa/Lv/XqTL/AEJkMHMMJWV9AAAAAElFTkSuQmCC" alt="UrbGEN_PopulateRegion Icon" width="24" height="24">

---

## Description
UrbGEN_PopulateRegion component

Populates a closed planar region with points, with holes excluded. The run type is set automatically by the inputs:

- **By count** (`Count` > 0): places the requested number of points and keeps the selected `Mode`. Grid modes solve the spacing to match `Count` within ±2%. `MinDist` is an optional minimum-spacing constraint.
- **By distance** (`Count` empty or 0): the number of points follows from region area / `MinDist²`, so points are added or removed automatically when the boundary changes.

Grid modes (1–3) are anchored to world XY, so existing points stay fixed while the boundary is edited. Point-in-region tests run on a pre-computed 2D polygon for speed.

## Inputs
| Name | Type | Access | Description |
|---|---|---|---|
| **Crv** | `Curve` | item | Closed planar boundary (site outline). Points are placed strictly inside it. Open or non-planar curves return empty. |
| **Count** | `Integer` | item | Target number of points. > 0 runs **by count**: `Mode` is kept, and grid modes solve the spacing to match `Count` (±2%), then trim to exact. Empty or 0 runs **by distance** (requires `MinDist`). (Optional) |
| **Mode** | `Integer` | item | 0 = Random (by count) / Poisson disk (by distance) · 1 = Regular grid · 2 = Jittered grid · 3 = Staggered grid (hexagonal). Modes 1–3 are anchored to world XY. Default 0. (Optional) |
| **Jitter** | `Number` | item | Random offset strength, Mode 2 only. Range 0–0.9: 0 = regular grid, 0.9 = very irregular. When `MinDist` is set, the step is enlarged to `MinDist / (1 − Jitter)` so `MinDist` still holds. Default 0. (Optional) |
| **Angle** | `Number` | item | Pattern rotation about the curve plane's Z axis, in radians. Aligns grid rows with a street or site edge. Use a Degrees→Radians component if working in degrees. Default 0. (Optional) |
| **Seed** | `Integer` | item | Random seed. Same seed and inputs give the same result. Affects Mode 0 layout and Mode 2 offsets. Default 0. (Optional) |
| **MinDist** | `Number` | item | Minimum distance between points (model units). **By distance:** required, sets the spacing and density. **By count:** optional constraint. Mode 0 rejects closer points, and Modes 1–3 use it as the smallest grid step. `Count` may fall short if it is set too large. |
| **Holes** | `Curve` | list | Closed inner curves to exclude (courtyards, existing blocks, easements). Also subtracted from the area when solving grid spacing. (Optional) |

## Outputs
| Name | Type | Access | Description |
|---|---|---|---|
| **Pts** | `Point` | list | Points inside the region. Ordered by grid row (Modes 1–3) or by generation order (Mode 0). |
| **Info** | `Text` | item | Summary in the form run type \| mode \| points placed / target \| spacing, or an error message. |