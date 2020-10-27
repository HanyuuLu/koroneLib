
FROM ubuntu:latest
ADD ./KoroneLibrary/bin/Release/netcoreapp3.1/linux-x64/publish/  /
ADD ./sources.list /etc/apt/sources.list
RUN chmod 744 /KoroneLibrary && apt update && apt upgrade -y && apt install icu-devtools -y && apt install libssl1.1 -y && apt auto-remove -y && apt clean -y
CMD ["/KoroneLibrary"]
VOLUME ["/Lib"]
EXPOSE 5000