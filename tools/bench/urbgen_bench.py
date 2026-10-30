# UrbGEN benchmark harness - run inside Rhino 8 (CPython)
import io, time, math, cProfile, pstats, random, sys
import Rhino.Geometry as rg

def _poly(pts):
    pl = rg.Polyline([rg.Point3d(x, y, 0) for x, y in pts] + [rg.Point3d(pts[0][0], pts[0][1], 0)])
    return pl.ToPolylineCurve()

def make_sites():
    sites = [
        _poly([(0, 0), (90, 0), (90, 70), (0, 70)]),
        _poly([(200, 0), (300, 0), (300, 40), (245, 40), (245, 95), (200, 95)]),
        _poly([(400, 0), (480, 12), (505, 70), (450, 105), (395, 80)]),
    ]
    c = _poly([(0, 0), (85, 0), (85, 60), (0, 60)])
    c.Rotate(math.radians(27), rg.Vector3d.ZAxis, rg.Point3d(0, 0, 0))
    c.Translate(rg.Vector3d(600, 0, 0))
    sites.append(c)
    cents = []
    for s in sites:
        bb = s.GetBoundingBox(True)
        pts = []
        random.seed(7)
        for _ in range(60):
            p = rg.Point3d(random.uniform(bb.Min.X, bb.Max.X), random.uniform(bb.Min.Y, bb.Max.Y), 0)
            if s.Contains(p, rg.Plane.WorldXY, 0.01) == rg.PointContainment.Inside:
                pts.append(p)
            if len(pts) >= 14:
                break
        cents.append(pts)
    return sites, cents

def run(path, typ_modes=(0, 6, 7), profile=False, top=25, extra=None):
    src = open(path, encoding="utf-8").read()
    code = compile(src, path, "exec")
    sites, cents = make_sites()
    out = []
    pr = cProfile.Profile() if profile else None
    sigs = []
    total = 0.0
    for tm in typ_modes:
        g = {"__name__": "urbgen_bench"}
        g.update(dict(siteCrv=sites, centroids=cents, FAR=3.5, BCR=0.5, setback=4.0,
                      podiumFloors=2, floorHeight=3.5, minWidth=12.0, heightVariation=0.3,
                      towerSizeMode=1, seed=11, podiumMinOffset=2.0, podiumMaxOffset=15.0,
                      globalRotationMode=0, uniformRotationDeg=0.0,
                      minFootprintPerTower=150.0, maxFootprintPerTower=600.0,
                      minTowerDistance=12.0, towerGrowStep=1.0, towerGrowIterations=80,
                      maxLengthWidthRatio=4.0, towerBCRPriority=0.65, upperBCR=0.55,
                      towerTypologyMode=tm, armLengthRatio=1.3,
                      courtyardCount=1, courtyardBreakCount=2, courtyardBreakWidth=6.0,
                      courtyardZoneGap=0.0, courtyardSplitAngle=0.0, courtyardBreakShift=0.0,
                      courtyardLayoutMode=0, maxBuildingHeight=80.0, minBuildingHeight=10.0,
                      heightRegulationMode=1, enforceHeightRegulation=True,
                      moveToBoundary=False, moveAllToSetback=True, moveTowerToPodiumEdge=False,
                      alignTowersToEdge=True, edgeAlignBothOrientations=True, Run=True))
        if extra:
            g.update(extra)
        _stdout = sys.stdout
        sys.stdout = io.StringIO()
        t0 = time.perf_counter()
        try:
            if pr:
                pr.enable()
            exec(code, g)
        finally:
            if pr:
                pr.disable()
            sys.stdout = _stdout
        dt = time.perf_counter() - t0
        total += dt
        bcr = [round(v or 0, 4) for v in g.get("ActualBCR")]
        far = [round(v or 0, 3) for v in g.get("ActualFAR")]
        nt = g.get("NumTowers")
        ok = g.get("Success")
        errs = g.get("Errors")
        tw = g.get("TowerTotalArea")
        sigs.append((tm, bcr, far, nt, [round(v or 0, 1) for v in tw]))
        out.append("mode {}: {:.2f}s | BCR {} | FAR {} | n {} | ok {} | err {}".format(
            tm, dt, bcr, far, nt, ok, [e[:60] for e in errs if e]))
        out.append("   timings: " + " || ".join(t for t in g.get("Timings") if t))
    out.append("TOTAL {:.2f}s".format(total))
    if pr:
        s = io.StringIO()
        pstats.Stats(pr, stream=s).sort_stats("tottime").print_stats(top)
        txt = s.getvalue()
        out.append(txt[txt.find("ncalls") - 200:] if "ncalls" in txt else txt)
    return "\n".join(out), sigs
