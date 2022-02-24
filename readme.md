# How to build **OpenCV**:
1. Go to a some directory.
2. Clone the full **opencv** repository.
```
git clone https://github.com/opencv/opencv
git -C opencv checkout dad2633
git clone https://github.com/opencv/opencv_contrib
git -C opencv_contrib checkout 49e8f12
```
3. Make a build directory.
```
mkdir opencv_build
```
4. Go to the build directory.
```
cd opencv_build
```
5. Configure and build.
```
cmake -DOPENCV_EXTRA_MODULES_PATH=../opencv_contrib/modules ../opencv
cmake --build .
```
6. Install.
```
sudo cmake --build . --target install
```

# How to build **opencvsharp** runtime.
1. Go to a some directory.
2. Clone the full **opencvsharp** repository.
```
git clone https://github.com/shimat/opencvsharp.git
```
3. Configure and build.
```
cd opencvsharp/src
cmake .
cmake --build .
```
4. Create an output directory and move the built library `libOpenCvSharpExtern.so` to that.
```
source /etc/os-release
outpath=./runtimes/$(echo $ID.$VERSION_ID.$(uname -m))
mkdir -p $outpath
mv ./OpenCvSharpExtern/libOpenCvSharpExtern.so $outpath
```

Copy runtime to an output directory.
```
cp -r ./runtime/. <project_output_dir>/runtimes
```


Install library `libgdiplus` (on Fedora 35 `libgdiplus-devel` package).