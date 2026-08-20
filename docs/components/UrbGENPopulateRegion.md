# UrbGEN_PopulateRegion
**Nickname:** UrbGEN PopulateRegion  
**Location:** UrbGEN > UrbGEN  

<img src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAPuSURBVEhLpZbLbxtVFMa/O08/Wiuqm6SRjKIqYUEVgiAkMnRhqcKxERGwiJF4iBT8HM+M48SPOHQxDXYiikCIp9RtC0JiBYIFQhWtgH2hbNgh+AeaRuyQfNG5mUnHE6dFcKTjeWju+d1z7nfuNQBoAGQAOgAVgFIqQd3bxNm/tpD5r763hbOpFEKYnEQoPYuoG5yuGueQ+g5+6Dv42b3e37uu++7fz0JHahTHJAk/jUTxsJuN4gH+7uFp7kC5nzv2y8nGavlG2yx+yi9D5dcRovECsJxAGACPRZB1S6XmctDuXMTKbzXM0jPnYO16YflCsziFgHEOuW6XvjIMg5umwTdr+TT/AtohQETHIgGSCYR1GW+n04gmEggnkwjXq+WrNNgyy7ffWX/xZADAmna5a9sGt63KrrOWn74nIDWJED3Hw3gWgGTb0FfN8g2aoW1VuNPJT/sBHqRTOz9PwcWzH7A0gYgPoHiAmC5KJtG7Rq0wR2VoV197gYIFAUEbAOTO4JgLyFC951ygHyAGccj/JjjZAIC0KjH8OhLBMxTwKMDBYA5GMP+7oA0AKIAq42NNxieOA2k56apqH0C9IT4SAznYhl0s0Jq0zNcfHwjKxWT274OAmTGMM4bdkIL2Y9MgKfLjmsiIsjmYLS0wLTQtuGWVb3qZOE7+xHq92GxZ+ZzIMAign1gYC4zhDgUnj+s4B2DMC05GErXN8i5Jdr1W+ZJmTb5qlq5UqwYnqXaM8/NDAVTvB0YxpcroRXS8N3Ma49RwfgAZNZtoJnfBKYu1WumKkLFt8DesVxeGAehjEWz6FEZVGR8xhtuqjEup1OAiDzMqUXO9stWyi4WjSkSLyWIxnJAYfmEMtxjDnxLDj7kzh7MYZn5lBQH0I2apMmwzhm/sLPQnZjH2/Dzi2Sx0t9b3lKbfggBSiuLkoFFZElHMUFmosxnDH6UlRBpW+YNh0jzKhgFkd6vgqUcwElJwwVNTx3xpybIMIU2CDJTCp32/BQHUWJK7XfNXZhGNh/FcNAyTeqNXzEyQNAnStCsfetJsmflCu15s0gJ7gUlhDaOQv9rLTPgBtBdRSWgvEgCqu65iR1PwmeNACUqzvVYokiQpq8ZqoSX2KQeKVS38LnrEKt08ONHcDLST+3sRH4/jXEhDR2LYu7SIpbuJ3zXqWAJQczUsAZD4dSh0HhC0Xivt+gEkUe30iDi9RN2pNNdW8G2/h43+NtKHfAeL1zYW3v1648nL/beQ8d5/v/nom583n/ruVnfK9peIALSrKsdDWKHZP3gKD/Uvwuh30etvYedI7w55t+1ee6h4ADpgSBnC5+YEUKa/Lv/XqTL/AEJkMHMMJWV9AAAAAElFTkSuQmCC" alt="UrbGEN_PopulateRegion Icon" width="24" height="24">

---

## Description
UrbGEN_PopulateRegion component

## Inputs
| Name | Type | Access | Description |
|---|---|---|---|
| **Crv** | `Curve` | item | Closed planar boundary (site outline). Open or non-planar curves return empty. (Optional) |
| **Count** | `Integer` | item | Target number of points. In grid modes this is approximate — spacing is solved automatically to within ±2% (Optional) |
| **Mode** | `Number` | item | 0 = Random · 1 = Regular grid · 2 = Jittered grid · 3 = Staggered grid (triangular). Default 0 (Optional) |
| **Jitter** | `Number` | item | Converts to collection of floating point numbers (Optional) |
| **Angle** | `Generic Data` | item | Grid rotation about the curve plane's Z axis, in radians. Modes 1–3 only. Use a Degrees→Radians component if working in degrees. (Optional) |
| **Seed** | `Integer` | item | Random seed. Same seed → same result. Affects Mode 0 and 2 only. Default 0. (Optional) |
| **MinDist** | `Number` | item | (Optional) Minimum spacing between points (model units). Mode 0 only. Set around 0.55·√(A/Count); above 0.7·√(A/Count) the count will fall short. Leave empty to disable. (Optional) |
| **Holes** | `Curve` | list | Closed inner curves to exclude (courtyards, existing blocks, easements). Subtracted from area when solving grid spacing. (Optional) |

## Outputs
| Name | Type | Access | Description |
|---|---|---|---|
| **Pts** | `Generic Data` | item | Points inside the boundary. Ordered by grid row (Modes 1–3) or by generation order (Mode 0). |
