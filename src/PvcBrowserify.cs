using PvcCore;
using PvcPlugins;
using PvcRuntime.NodeJs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PvcPlugins
{
    public class PvcBrowserify : PvcPlugin
    {
        public PvcBrowserify()
        {
            PvcRuntimeNodeJs<PvcBrowserify>.InstallPackages();
        }

        public override IEnumerable<PvcStream> Execute(IEnumerable<PvcCore.PvcStream> inputStreams)
        {
            var outputStreams = new List<PvcStream>();
            foreach (var inputStream in inputStreams.AsParallel())
            {
                var browserifyBundle = PvcRuntimeNodeJs<PvcBrowserify>.Execute(
                    new
                    {
                        entryPoint = Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileName(inputStream.StreamName))
                    },
                    @"
                        var browserify = require('browserify');

                        browserify(pvc.data.entryPoint)
                            .bundle(function (err, output) {
                                pvc.doneCallback(err, output);
                            })
                            .on('error', function (err) {
                                console.error(err);
                            });
                    "
                );

                outputStreams.Add(PvcUtil.StringToStream(browserifyBundle, inputStream.StreamName));
            }

            return outputStreams;
        }
    }
}
