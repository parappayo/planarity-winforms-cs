
PROJECT?=Planarity

LIBS?=-r:System.Windows.Forms.dll -r:System.Drawing.dll

build: *.cs
	mono-csc *.cs ${LIBS} -out:${PROJECT}.exe

run: build
	mono ${PROJECT}.exe
