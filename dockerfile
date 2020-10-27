
FROM ubuntu:latest
ADD ./KoroneLibrary/bin/Release/netcoreapp3.1/linux-x64/publish/*  /
CMD ["/KoroneLibrary"]
VOLUME ["/Lib"]
EXPOSE 5001 5000