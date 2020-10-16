
# planarity-winforms-cs

[Planarity](https://en.wikipedia.org/wiki/Planarity) is a graph geometry game, [originally implemented in Flash](http://planarity.net/) by John Tantalo, based on a concept by Mary Radcliffe.

There is an [elegant implementation](https://www.jasondavies.com/planarity/) in JavaScript by Jason Davies, as well as a GTK+ [desktop version](http://web.mit.edu/xiphmont/Public/gPlanarity.html) by Chris Montgomery of Xiph.org.

The goal for this version is to sharpen my C# and WinForms coding skills. A working [implementation in Python](https://github.com/parappayo/planarity-py) is used as a reference.

## Setup

The Makefile is provided if you have `mono-csc` installed and want to build without an IDE. Build and run with `make run`.

Getting unit tests to work was a pain so I installed [MonoDevelop](https://www.monodevelop.com/). To be able to use NUnit. I would have used xUnit but NUnit was included.

[Visual Studio Community](https://visualstudio.microsoft.com/vs/community/) will also work.

## References

* [Mono](https://www.mono-project.com/)
* [MonoDevelop](https://www.monodevelop.com/)
* [NUnit](https://nunit.org/)
* [xUnit](https://xunit.net/)
* [System.Drawing.BufferedGraphics](https://docs.microsoft.com/en-us/dotnet/api/system.drawing.bufferedgraphics?view=netframework-4.7.2)
