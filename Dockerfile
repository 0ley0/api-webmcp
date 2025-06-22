FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# ✅ คัดลอกทุกไฟล์เข้า /src
COPY . .

# ✅ เข้าโฟลเดอร์ที่มี .csproj
WORKDIR /src

RUN dotnet restore "./mcpserver.csproj"
RUN dotnet build "./mcpserver.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./mcpserver.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# ✅ เปลี่ยนชื่อให้ตรงกับชื่อ .csproj ที่คุณใช้
ENTRYPOINT ["dotnet", "mcpserver.dll"]