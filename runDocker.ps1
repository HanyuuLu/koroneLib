docker stop KoroneLibraryServer
docker rm KoroneLibraryServer
docker run --name KoroneLibraryServer -p 5001:5001 -p 5000:5000 -v /c/Storage/repo/docker/v:/Lib hanyuufurude/korone-library:v3.0