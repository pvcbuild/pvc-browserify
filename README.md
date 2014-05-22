pvc-browserify
==============

Run browserify on your applications JavaScript. This plugin uses the NodeJs Runtime packaged for PVC.

```
/// Execute browserify against the app.js entry point
pvc.Source("app.js")
   .Pipe(new PvcBrowserify())
   .Save("deploy/");

/// Execute browserify against a series of entry points (produces multiple bundles)
pvc.Source("app.js", "lib.js")
   .Pipe(new PvcBrowserify());
```
